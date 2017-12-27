using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
namespace Hotfix
{
    [ObjectEvent]
    public class UIWater13ComponentEvent : ObjectEvent<UIWater13Component>, IAwake
    {
        public void Awake()
        {
            this.Get().Awake();
        }
    }

    public class UIWater13Component : Component
    {
        public void Awake()
        {

        }
    }
}
