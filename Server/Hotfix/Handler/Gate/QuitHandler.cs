using Model;

namespace Hotfix
{
    [MessageHandler(AppType.Gate)]
    public class QuitHandler : AMHandler<Quit>
    {
        protected override void Run(Session session, Quit message)
        {
            Player player = Game.Scene.GetComponent<PlayerComponent>().Get(message.PlayerID);
            if(player != null)
            {
                //向Actor对象发送退出消息
                ActorProxy actorProxy = Game.Scene.GetComponent<ActorProxyComponent>().Get(player.ActorID);
                actorProxy.Send(new PlayerQuit() { PlayerID = player.Id });
                player.ActorID = 0;
            }
        }
    }
}
