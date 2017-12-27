using System.Collections.Generic;

namespace Model
{
    public class RealmMapAddressComponent : Component
    {
        public readonly List<StartConfig> MapAddress = new List<StartConfig>();

        public StartConfig GetAddress()
        {
            int n = RandomHelper.RandomNumber(0, this.MapAddress.Count);
            return this.MapAddress[n];
        }
    }
}
