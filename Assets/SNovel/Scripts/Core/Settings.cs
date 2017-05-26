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
                    GameObject go = new GameObject("Setting");
                    _sharedSettings = go.AddComponent<Settings>();
                    if(_sharedSettings == null)
                    {
                        Debug.LogError("Cannot create Settings");
                    }
                }
                return _sharedSettings;
            }
        }
        public string SCENARIO_SCRIPT_PATH   = "/ThirdParty/Resources/ScenarioScripts/";
        public string PREFAB_PATH            = "Prefab/";
        public string UI_IMAGE_PATH          = "UImage/";
        public string BG_IMAGE_PATH          = "BG/";
        public string ACTOR_IMAGE_PATH       = "Actor/Image/";

        public string UI_ROOT_IN_SCENE       = "UICanvas";
        public string ACTOR_ROOT_IN_SCENE    = "ActorCanvas";
        public string BG_ROOT_IN_SCENE       = "BGCanvas";

        public int UI_LAYER = 5;
        public int ACTOR_LAYER = 9;
        public int BG_LAYER = 8;
        
        public int PreferredScreenWidth  = 750;
        public int PreferredScreenHeight = 1334;
      //  [Serializable]
       // static public class ActorSettings
      //  {
            [Range(0, 1080)]
            public float Actor_Y = 0f;
            
            [Range(0, 1920)]
            public float Center_X = 0f;

            [Range(0, 1920)]
            public float Left_X = 0f;

            [Range(0, 1920)]
            public float Right_X = 0f;

            [Range(0, 1920)]
            public float Mid_Left_X = 0f;

            // [Range(-Screen.width / 2, Screen.width)]
            [Range(0, 1920)]
            public float Mid_Right_X = 0f;

            [Range(0, 2)]
            public float Far_Z = 0f;

            [Range(0, 2)]
            public float Near_Z = 0f;

            [Range(0, 2)]
            public float Normal_Z = 0f;
      //  }

        public static Transform UIRoot
        {
            get
            {
                if (_uiRoot == null)
                {
                    GameObject go = GameObject.Find(Settings.Instance.UI_ROOT_IN_SCENE);
                    if (go == null)
                    {
                        Debug.Log("Can not find UICanvas, create it in Scene!");
                        return null;
                    }
                    _uiRoot = go.transform;
                }
                return _uiRoot;
            }
        }
        static Transform _uiRoot = null;

        public static Transform BGRoot
        {
            get
            {
                if (_bgRoot == null)
                {
                    GameObject go = GameObject.Find(Settings.Instance.BG_ROOT_IN_SCENE);
                    if (go == null)
                    {
                        Debug.Log("Can not find BGCanvas, create it in Scene!");
                        return null;
                    }
                    _bgRoot = go.transform;
                }
                return _bgRoot;
            }
        }
        static Transform _bgRoot = null;

        public static Transform ActorRoot
        {
            get
            {
                if (_actorRoot == null)
                {
                    GameObject go = GameObject.Find(Settings.Instance.ACTOR_ROOT_IN_SCENE);
                    if (go == null)
                    {
                        Debug.Log("Can not find ActorCanvas, create it in Scene!");
                        return null;
                    }
                    _actorRoot = go.transform;
                }
                return _actorRoot;
            }
        }
        static Transform _actorRoot = null;
    }
}
