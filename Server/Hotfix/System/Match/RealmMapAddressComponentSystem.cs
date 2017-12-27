using Model;

namespace Hotfix
{
    [ObjectEvent]
    public class RealmMapAddressComponentEvent : ObjectEvent<RealmMapAddressComponent>, IStart
    {
        public void Start()
        {
            this.Get().Start();
        }
    }

    public static class RealmMapAddressComponentSystem
    {
        public static void Start(this RealmMapAddressComponent self)
        {
            StartConfig[] startConfigs = self.GetComponent<StartConfigComponent>().GetAll();
            foreach (StartConfig config in startConfigs)
            {
                if (!config.AppType.Is(AppType.Map))
                {
                    continue;
                }

                self.MapAddress.Add(config);
            }
        }
    }
}
