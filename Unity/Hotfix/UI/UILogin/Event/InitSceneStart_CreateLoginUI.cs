using Model;

namespace Hotfix
{
    [Event((int)EventIdType.InitSceneStart)]
    public class InitSceneStart_CreateLoginUI : IEvent
    {
        public void Run()
        {
            Hotfix.Scene.GetComponent<UIComponent>().Create(UIType.Login);
        }
    }
}
