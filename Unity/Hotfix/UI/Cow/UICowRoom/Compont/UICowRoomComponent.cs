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
    public class UICowRoomComponentEvent : ObjectEvent<UICowRoomComponent>, IAwake
    {
        public void Awake()
        {
            this.Get().Awake();
        }
    }

    public class UICowRoomComponent : Component
    {

        Text RoomId;
        public void Awake()
        {
            ReferenceCollector rc = this.GetEntity<UI>().GameObject.GetComponent<ReferenceCollector>();
            RoomId = rc.Get<GameObject>("RoomId").GetComponent<Text>();

        }
        public  void DoCmd(G2C_RoomCommand cmd)
        {
            RoomId.text = "房间号:" + cmd.roomId;
        }
    }
}

