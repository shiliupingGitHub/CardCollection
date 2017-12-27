using System;
using Model;

namespace Hotfix
{
    [MessageHandler(AppType.Gate)]
    public class StartMatch_RTHandler : AMRpcHandler<StartMatch_RT, StartMatch_RE>
    {
        protected override async void Run(Session session, StartMatch_RT message, Action<StartMatch_RE> reply)
        {
            StartMatch_RE response = new StartMatch_RE();
            try
            {
                //验证玩家ID是否正常
                Player player = Game.Scene.GetComponent<PlayerComponent>().Get(message.PlayerID);
                if (player == null)
                {
                    response.Error = ErrorCode.ERR_StartMatchError;
                    reply(response);
                    return;
                }

                //验证玩家是否符合进入房间要求
                RoomConfig roomConfig = RoomHelper.GetConfig(message.Level);
                UserInfo userInfo = await Game.Scene.GetComponent<DBProxyComponent>().Query<UserInfo>(player.UserID);
                if(userInfo.Money < roomConfig.MinThreshold)
                {
                    response.Error = ErrorCode.ERR_UserMoneyLessError;
                    reply(response);
                    return;
                }

                //向匹配服务器发送匹配请求
                StartConfigComponent config = Game.Scene.GetComponent<StartConfigComponent>();
                string matchAddress = config.MatchConfig.GetComponent<InnerConfig>().Address;
                Session matchSession = Game.Scene.GetComponent<NetInnerComponent>().Get(matchAddress);
                JoinMatch_RE joinMatchRE = await matchSession.Call<JoinMatch_RE>(new JoinMatch_RT()
                {
                    PlayerID = player.Id,
                    UserID = player.UserID,
                    GateSessionID = session.Id,
                    GateAppID = config.StartConfig.AppId
                });

                //设置玩家的Actor消息直接发送给匹配对象
                player.ActorID = joinMatchRE.ActorID;

                reply(response);
            }
            catch (Exception e)
            {
                ReplyError(response, e, reply);
            }
        }
    }
}
