using System;
using Model;

namespace Hotfix
{
    [MessageHandler(AppType.Gate)]
    public class GetUserInfo_RTHandler : AMRpcHandler<GetUserInfo_RT, GetUserInfo_RE>
    {
        protected override async void Run(Session session, GetUserInfo_RT message, Action<GetUserInfo_RE> reply)
        {
            GetUserInfo_RE response = new GetUserInfo_RE();
            try
            {
                DBProxyComponent dbProxy = Game.Scene.GetComponent<DBProxyComponent>();
                
                //查询用户信息
                UserInfo userInfo = await dbProxy.Query<UserInfo>(message.UserID);

                if(userInfo == null)
                {
                    response.Error = ErrorCode.ERR_QueryUserInfoError;
                    reply(response);
                    return;
                }

                response.NickName = userInfo.NickName;
                response.Wins = userInfo.Wins;
                response.Loses = userInfo.Loses;
                response.Money = userInfo.Money;
                reply(response);
            }
            catch (Exception e)
            {
                ReplyError(response, e, reply);
            }
        }
    }
}
