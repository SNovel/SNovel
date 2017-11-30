using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/*
 * ChangeLog
 * 2015.9.16
 * Use Singleton
 */
namespace SNovel
{
    //挂在场景中用来设置
    //做成独立窗口形式
    class Settings : MonoBehaviour
    {
        //Instance
        static Settings _sharedSettings = null;
        void Awake()
        {
            _sharedSettings = this;
        }

        public static Settings Instance
        {
            get
            {
                if(_sharedSettings == null)
                {
                     Debug.LogError("SNovel: No Settings ");
                }
                return _sharedSettings;
            }
        }
        public string SCENARIO_SCRIPT_PATH   = "SNovel/ScenarioScripts/";
        public string PREFAB_PATH            = "SNovel/Prefab/";
        public string UI_PATH = "SNovel/UImage/";
        public string IMAGE_PATH = "SNovel/Image/";
        public string BG_PATH          = "SNovel/BG/";
        public string ACTOR_PATH       = "SNovel/Actor/";
        public string TEXBOX_PATH            = "SNovel/TextBox/";

        public int UI_LAYER = 5;
        public int ACTOR_LAYER = 9;
        public int BG_LAYER = 8;

        //  [Serializable]
        // static public class ActorSettings
        //  {
        [Range(0, 1920)]
            public float Actor_Y = 0f;
            
            [Range(0, 1080)]
            public float Center_X = 0f;

            [Range(0, 1080)]
            public float Left_X = 0f;

            [Range(0, 1080)]
            public float Right_X = 0f;

            [Range(0, 1080)]
            public float Mid_Left_X = 0f;

            // [Range(-Screen.width / 2, Screen.width)]
            [Range(0, 1080)]
            public float Mid_Right_X = 0f;

            [Range(0, 2)]
            public float Far_Z = 0f;

            [Range(0, 2)]
            public float Near_Z = 0f;

            [Range(0, 2)]
            public float Normal_Z = 0f;
      //  }

        public Transform UIRoot;

        public Transform BGRoot;

        public Transform ActorRoot;

        public GameObject Root;
    }
}
