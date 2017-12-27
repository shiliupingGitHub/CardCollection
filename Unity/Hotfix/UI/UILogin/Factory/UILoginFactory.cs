using UnityEngine;
using Model;
using System;

namespace Hotfix
{
    [UIFactory((int)UIType.Login)]
    public class UILoginFactory : IUIFactory
    {
        public UI Create(Scene scene, UIType type, GameObject parent)
        {
            try
            {
                GameObject prefab = Resources.Load<GameObject>("UI").Get<GameObject>("UILogin");
                GameObject login = UnityEngine.Object.Instantiate(prefab);
                login.layer = LayerMask.NameToLayer("UI");
                UI ui = new UI(scene, type, null, login);

                ui.AddComponent<UILoginComponent>();
                return ui;
            }
            catch (Exception e)
            {
                Log.Error(e.ToStr());
                return null;
            }
        }
    }
}
