//#define NO_CONTROLLER

using System;
using System.Collections.Generic;
using SNovel.MessageNotificationCenter;
using UnityEngine;
using WentOne;
namespace SNovel
{

    /// <summary>
    /// [startgame]
    /// </summary>
    public class StartgameTag : AbstractTag
    {
        public StartgameTag()
        {
            _defaultParams = new Dictionary<string, string>()
            {
            };
            _vitalParams = new List<string>()
            {
            };
        }

        public override void Excute()
        {
            Debug.Log("[Start Game]");
            //  GameManager.Instance.ChangeState(GameState.Playing);

        }

    }
}