using System;
using Model;
using System.Collections.Generic;

namespace Hotfix
{
    [MessageHandler(AppType.Realm)]
    public class Login_RTHandler : AMRpcHandler<Login_RT, Login_RE>
    {
        protected override async void Run(Session session, Login_RT message, Action<Login_RE> reply)
        {
            Login_RE response = new Login_RE();
            try
            {
                Log.Info($"Session请求登录 账号:{message.Account} 密码:{message.Password}");
                //数据库操作对象
                DBProxyComponent dbProxy = Game.Scene.GetComponent<DBProxyComponent>();

                //验证账号密码是否正确
                List<AccountInfo> result = await dbProxy.QueryJson<AccountInfo>($"{"{'Account':'"}{message.Account}{"','Password':'"}{message.Password}{"'}"}");
                if (result.Count == 0)
                {
                    response.Error = ErrorCode.ERR_AccountOrPasswordError;
                    reply(response);
                    return;
                }

                AccountInfo account = result[0];
                //验证账号是否在线
                if (Game.Scene.GetComponent<PlayerLoginManagerComponent>().Get(account.Id) != 0)
                {
                    response.Error = ErrorCode.ERR_AccountOnline;
                    reply(response);
                    return;
                }

                //随机分配网关服务器
                StartConfig gateConfig = Game.Scene.GetComponent<RealmGateAddressComponent>().GetAddress();
                Session gateSession = Game.Scene.GetComponent<NetInnerComponent>().Get(gateConfig.GetComponent<InnerConfig>().Address);

                //请求登录Gate服务器密匙
                GetLoginKey_RE getLoginKeyRE = await gateSession.Call<GetLoginKey_RE>(new GetLoginKey_RT() { UserID = account.Id });

                //添加账号在线标记
                Game.Scene.GetComponent<PlayerLoginManagerComponent>().Add(account.Id, gateConfig.AppId);

                response.Key = getLoginKeyRE.Key;
                response.Address = gateConfig.GetComponent<OuterConfig>().Address;
                reply(response);
            }
            catch (Exception e)
            {
                ReplyError(response, e, reply);
            }
        }
    }
}
