using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Hotfix
{
    [MessageHandler((int)Opcode.G2C_RoomCommand)]
    public class G2C_RoomCommandHandler : AMHandler<G2C_RoomCommand>
    {
        protected override  async void Run(Session session, G2C_RoomCommand message)
        {
            switch (message.GameType)
            {
                case GameType.GT_Cow:
                    {
                        Hotfix.Scene.GetComponent<UIComponent>().RemoveAll();
                        UI ui = Hotfix.Scene.GetComponent<UIComponent>().Get(UIType.UICowRoom);
                        if(null == ui)
                        {
                           ui = Hotfix.Scene.GetComponent<UIComponent>().Create(UIType.UICowRoom);
                        }
                        UICowRoomComponent crc = ui.GetComponent<UICowRoomComponent>();
                        crc.DoCmd(message);
                    }
                    break;
            }

           // Hotfix.Scene.GetComponent<UIComponent>().RemoveAll();
           

        }
    }
}


