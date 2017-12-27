using Model;
using UnityEngine;

namespace Hotfix
{
    public class UIEndFactory
    {
        public static UI Create(Scene scene, UIType type, UI parent, bool isWin)
        {
            GameObject prefab = Resources.Load<GameObject>("UI").Get<GameObject>("EndPanel");
            GameObject endPanel = UnityEngine.Object.Instantiate(prefab);

            endPanel.layer = LayerMask.NameToLayer("UI");

            UI ui = new UI(scene, type, parent, endPanel);
            ui.AddComponent<UIEndComponent, bool>(isWin);
            return ui;
        }
    }
}
