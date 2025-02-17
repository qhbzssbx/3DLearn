using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;

namespace P5Game
{
    public class GameStart : MonoBehaviour
    {
        private IArchitecture gameArchitecture;
        private void Awake() {
            DontDestroyOnLoad(this.gameObject);
        }
        // Start is called before the first frame update
        void Start()
        {
            // Game.Interface.SendCommand();
            gameArchitecture = GameArchitecture.Interface; // 触发初始化
        }
    }
    
}
