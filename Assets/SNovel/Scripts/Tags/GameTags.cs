using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace SNovel
{ 
    /// <summary>
    /// [startgame]
    /// </summary>
    public class StartgameTag : AbstractTag
    {
        public StartgameTag()
        {
            _defaultParamSet = new Dictionary<string, string>() {               
            };
            _vitalParams = new List<string>() { 
            };
        }

        public override void Excute()
        {
            Debug.Log("[Start Game]");
          //  GameManager.Instance.ChangeState(GameState.Playing);

        }
        public override void After()
        {
            base.After();
        }

    }
}