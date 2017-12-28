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
    public class UICowCreateRoomComponentEvent : ObjectEvent<UICowCreateRoomComponent>, IAwake
    {
        public void Awake()
        {
            this.Get().Awake();
        }
    }

    public class UICowCreateRoomComponent : Component
    {


        public void Awake()
        {
            ReferenceCollector rc = this.GetEntity<UI>().GameObject.GetComponent<ReferenceCollector>();
            Button btn_close = rc.Get<GameObject>("btn_close").GetComponent<Button>();
            btn_close.onClick.Add(() =>
            {
                Hotfix.Scene.GetComponent<UIComponent>().Remove(UIType.UICowCreateRoom);
            });

            Button btn_enter = rc.Get<GameObject>("btn_enter").GetComponent<Button>();
            btn_enter.onClick.Add(() =>
            {
                C2G_CreateCow msg = new C2G_CreateCow();
                SessionComponent.Instance.Session.Send(msg);
            });

        }
    }
}
