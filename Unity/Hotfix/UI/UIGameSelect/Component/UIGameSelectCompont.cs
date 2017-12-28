using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using UnityEngine;
using UnityEngine.UI;
namespace Hotfix
{
    [ObjectEvent]
    public class UIGameSelectComponentEvent : ObjectEvent<UIGameSelectComponent>, IAwake
    {
        public void Awake()
        {
            this.Get().Awake();
        }
    }

    public class UIGameSelectComponent : Component
    {
        

        public void Awake()
        {

            ReferenceCollector rc = this.GetEntity<UI>().GameObject.GetComponent<ReferenceCollector>();
            Button CowCow = rc.Get<GameObject>("CowCow").GetComponent<Button>();
            CowCow.onClick.Add(() =>
            {
                Hotfix.Scene.GetComponent<UIComponent>().Create(UIType.UICowCreateRoom);
            });
            Button btn_close = rc.Get<GameObject>("btn_close").GetComponent<Button>();
            btn_close.onClick.Add(() =>
            {
                Hotfix.Scene.GetComponent<UIComponent>().Remove(UIType.UIGameSelect);
            });
        }
    }
}
