using System;
using Model;
using System.Collections.Generic;

namespace Hotfix
{
    [MessageHandler(AppType.Realm)]
    public class Register_RTHandler : AMRpcHandler<Register_RT, Register_RE>
    {
        protected override async void Run(Session session, Register_RT message, Action<Register_RE> reply)
        {
            Register_RE response = new Register_RE();
            try
            {
                //数据库操作对象
                DBProxyComponent dbProxy = Game.Scene.GetComponent<DBProxyComponent>();

                //查询账号是否存在
                List<AccountInfo> result = await dbProxy.QueryJson<AccountInfo>($"{"{'Account':'"}{message.Account}{"'}"}");
                if (result.Count > 0)
                {
                    response.Error = ErrorCode.ERR_AccountAlreadyRegister;
                    reply(response);
                    return;
                }

                //新建账号和用户信息
                AccountInfo newAccount = EntityFactory.CreateWithId<AccountInfo>(IdGenerater.GenerateId());
                newAccount.Account = message.Account;
                newAccount.Password = message.Password;

                UserInfo newUser = EntityFactory.CreateWithId<UserInfo>(newAccount.Id);
                newUser.NickName = $"用户{IdGenerater.GenerateId()}";
                newUser.Money = 10000;

                await dbProxy.Save(newAccount);
                await dbProxy.Save(newUser);

                reply(response);
            }
            catch (Exception e)
            {
                ReplyError(response, e, reply);
            }
        }
    }
}
