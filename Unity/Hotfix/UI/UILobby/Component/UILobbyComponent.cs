using System;
using Model;
using UnityEngine;
using UnityEngine.UI;

namespace Hotfix
{
    [ObjectEvent]
    public class UILobbyComponentEvent : ObjectEvent<UILobbyComponent>, IAwake
    {
        public void Awake()
        {
            this.Get().Awake();
        }
    }

    public class UILobbyComponent : Component
    {
        public void Awake()
        {
            Init();
        }

        public async void OnStartMatch()
        {
            try
            {
                //发送开始匹配消息
                long playerId = ClientComponent.Instance.LocalPlayer.Id;
                StartMatch_RT startMatchRT = new StartMatch_RT() { PlayerID = playerId, Level = RoomLevel.Lv100 };
                StartMatch_RE startMatchRE = await SessionComponent.Instance.Session.Call<StartMatch_RE>(startMatchRT);

                if (startMatchRE.Error == ErrorCode.ERR_StartMatchError)
                {
                    Log.Error($"匹配失败:{MongoHelper.ToJson(startMatchRT)}");
                    return;
                }

                if(startMatchRE.Error == ErrorCode.ERR_UserMoneyLessError)
                {
                    Log.Error("余额不足");
                    return;
                }

                //切换到房间界面
                Hotfix.Scene.GetComponent<UIComponent>().Create(UIType.Room);
                Hotfix.Scene.GetComponent<UIComponent>().Remove(UIType.Lobby);
            }
            catch (Exception e)
            {
                Log.Error(e.ToStr());
            }
        }

        private async void Init()
        {
            ReferenceCollector rc = this.GetEntity<UI>().GameObject.GetComponent<ReferenceCollector>();

            //添加事件
            rc.Get<GameObject>("StartMatch").GetComponent<Button>().onClick.Add(OnStartMatch);

            //获取玩家数据
            long userId = ClientComponent.Instance.LocalPlayer.UserID;
            GetUserInfo_RT getUserInfo_RT = new GetUserInfo_RT() { UserID = userId };
            GetUserInfo_RE getUserInfoRE = await SessionComponent.Instance.Session.Call<GetUserInfo_RE>(getUserInfo_RT);

            if (getUserInfoRE.Error == ErrorCode.ERR_QueryUserInfoError)
            {
                Log.Error("获取玩家信息异常 << " + MongoHelper.ToJson(getUserInfo_RT));
                return;
            }
            else
            {
                rc.Get<GameObject>("NickName").GetComponent<Text>().text = getUserInfoRE.NickName;
                rc.Get<GameObject>("Money").GetComponent<Text>().text = getUserInfoRE.Money.ToString();
            }
        }
    }
}
