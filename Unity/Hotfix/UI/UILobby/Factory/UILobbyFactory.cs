using System;
using Model;
using UnityEngine;

namespace Hotfix
{
    [UIFactory((int)UIType.Lobby)]
    public class UILobbyFactory : IUIFactory
    {
        public UI Create(Scene scene, UIType type, GameObject parent)
        {
            try
            {
                GameObject prefab = Resources.Load<GameObject>("UI").Get<GameObject>("UILobby");
                GameObject lobby = UnityEngine.Object.Instantiate(prefab);
                lobby.layer = LayerMask.NameToLayer("UI");
                UI ui = new UI(scene, type, null, lobby);

                ui.AddComponent<UILobbyComponent>();
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
