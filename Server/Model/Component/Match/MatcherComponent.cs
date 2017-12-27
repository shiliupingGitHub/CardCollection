using System.Linq;
using System.Collections.Generic;

namespace Model
{
    public class MatcherComponent : Component
    {
        private readonly Dictionary<long, Matcher> matchers = new Dictionary<long, Matcher>();

        public int Count { get { return matchers.Count; } }

        public void Add(Matcher matcher)
        {
            this.matchers.Add(matcher.PlayerID, matcher);
        }

        public Matcher Get(long id)
        {
            Matcher matcher;
            this.matchers.TryGetValue(id, out matcher);
            return matcher;
        }

        public Matcher[] GetAll()
        {
            return this.matchers.Values.ToArray();
        }

        public void Remove(long id)
        {
            Matcher matcher = Get(id);
            this.matchers.Remove(id);
            matcher?.Dispose();
        }

        public override void Dispose()
        {
            if(this.Id == 0)
            {
                return;
            }

            base.Dispose();

            foreach (var matcher in this.matchers.Values)
            {
                matcher.Dispose();
            }
        }
    }
}
