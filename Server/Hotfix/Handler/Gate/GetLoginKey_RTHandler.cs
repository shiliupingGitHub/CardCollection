using System;
using Model;

namespace Hotfix
{
    [MessageHandler(AppType.Gate)]
    public class GetLoginKey_RTHandler : AMRpcHandler<GetLoginKey_RT, GetLoginKey_RE>
    {
        protected override void Run(Session session, GetLoginKey_RT message, Action<GetLoginKey_RE> reply)
        {
            GetLoginKey_RE response = new GetLoginKey_RE();
            try
            {
                //随机密匙并添加到管理
                long key = RandomHelper.RandInt64();
                Game.Scene.GetComponent<GateSessionKeyComponent>().Add(key, message.UserID);

                response.Key = key;
                reply(response);
            }
            catch (Exception e)
            {
                ReplyError(response, e, reply);
            }
        }
    }
}
