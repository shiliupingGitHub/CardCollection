using UnityEngine;
using UnityEngine.UI;

namespace Model
{
	[ObjectEvent]
	public class UILoadingComponentEvent : ObjectEvent<UILoadingComponent>, IAwake, IStart
	{
		public void Awake()
		{
			UILoadingComponent self = this.Get();
			self.text = self.GetEntity<UI>().GameObject.Get<GameObject>("Text").GetComponent<Text>();
            self.slider = self.GetEntity<UI>().GameObject.Get<GameObject>("Slider").GetComponent<Slider>();
        }

		public async void Start()
		{
			UILoadingComponent self = this.Get();

			TimerComponent timerComponent = Game.Scene.GetComponent<TimerComponent>();
			
			while (true)
			{
				await timerComponent.WaitAsync(1000);
				
				if (self.Id == 0)
				{
					return;
				}

				BundleDownloaderComponent bundleDownloaderComponent = Game.Scene.GetComponent<BundleDownloaderComponent>();
				if (bundleDownloaderComponent == null)
				{
					continue;
				}
				self.text.text = $"{bundleDownloaderComponent.Progress}%";
                self.slider.value = bundleDownloaderComponent.Progress;

            }
		}
	}
	
	public class UILoadingComponent : Component
	{
		public Text text;
        public Slider slider;
	}
}
