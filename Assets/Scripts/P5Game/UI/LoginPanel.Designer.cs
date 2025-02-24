#if UNITY_EDITOR
using Sirenix.OdinInspector;
#endif
using UnityEngine;

namespace P5Game.UI
{
	public partial class LoginPanel
	{

		public TMPro.TMP_InputField ipt_account;

		public TMPro.TMP_InputField ipt_password;

		public UnityEngine.UI.Button Button;
		public GameObject go;

#if UNITY_EDITOR
        [Button("AutoBind")]
		public void AutoBind()
		{
			go = gameObject;
		}
		#endif
	}
}