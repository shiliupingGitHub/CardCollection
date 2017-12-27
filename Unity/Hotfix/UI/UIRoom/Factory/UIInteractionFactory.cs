using Model;
using UnityEngine;

namespace Hotfix
{
    public class UIInteractionFactory
    {
        public static UI Create(Scene scene, UIType type, UI parent)
        {
            GameObject prefab = Resources.Load<GameObject>("UI").Get<GameObject>("InteractionPanel");
            GameObject interaction = UnityEngine.Object.Instantiate(prefab);

            interaction.layer = LayerMask.NameToLayer("UI");

            UI ui = new UI(scene, type, parent, interaction);
            ui.AddComponent<InteractionComponent>();
            return ui;
        }
    }
}
