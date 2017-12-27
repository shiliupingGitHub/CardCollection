using System;
using Model;
using UnityEngine;

namespace Hotfix
{
    [UIFactory((int)UIType.Room)]
    public class UIRoomFactory : IUIFactory
    {
        public UI Create(Scene scene, UIType type, GameObject parent)
        {
            try
            {
                GameObject prefab = Resources.Load<GameObject>("UI").Get<GameObject>("UIRoom");
                GameObject room = UnityEngine.Object.Instantiate(prefab);
                room.layer = LayerMask.NameToLayer("UI");

                UI ui = new UI(scene, type, null, room);
                ui.AddComponent<GamerComponent>();
                ui.AddComponent<UIRoomComponent>();

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
