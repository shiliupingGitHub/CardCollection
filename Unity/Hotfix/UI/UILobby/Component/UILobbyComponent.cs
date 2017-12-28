using System;
using Model;
using UnityEngine;
using UnityEngine.UI;

namespace Hotfix
{
	[ObjectEvent]
	public class UILobbyComponentEvent : ObjectEvent<UILobbyComponent>, IAwake
	{
		public void Awake()
		{
			this.Get().Awake();
		}
	}
	
	public class UILobbyComponent : Component
	{
		private GameObject enterMap;
		private Text text;

		public void Awake()
		{
			ReferenceCollector rc = this.GetEntity<UI>().GameObject.GetComponent<ReferenceCollector>();
            Button btn_createroom = rc.Get<GameObject>("btn_createroom").GetComponent<Button>();
            btn_createroom.onClick.Add(() =>
            {
   
                     Hotfix.Scene.GetComponent<UIComponent>().Create(UIType.UIGameSelect);
            });


        }


		//private async void EnterMap()
		//{
		//	try
		//	{
		//		G2C_EnterMap g2CEnterMap = await SessionComponent.Instance.Session.Call<G2C_EnterMap>(new C2G_EnterMap());
		//		Hotfix.Scene.GetComponent<UIComponent>().Remove(UIType.UILobby);
  //              Hotfix.Scene.GetComponent<UIComponent>().Create(UIType.UIWater13);
		//	}
		//	catch (Exception e)
		//	{
		//		Log.Error(e.ToStr());
		//	}	
		//}
	}
}
