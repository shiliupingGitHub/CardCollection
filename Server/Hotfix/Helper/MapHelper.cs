using Model;

namespace Hotfix
{
    public static class MapHelper
    {
        /// <summary>
        /// 发送消息给匹配服务器
        /// </summary>
        /// <param name="message"></param>
        public static void SendMessage(AMessage message)
        {
            string matchAddress = Game.Scene.GetComponent<StartConfigComponent>().MatchConfig.GetComponent<InnerConfig>().Address;
            Session matchSession = Game.Scene.GetComponent<NetInnerComponent>().Get(matchAddress);
            matchSession.Send(message);
        }
    }
}
