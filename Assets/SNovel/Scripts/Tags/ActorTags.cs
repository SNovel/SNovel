using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
/// <summary>
/// The AVGPart namespace.
/// </summary>

namespace SNovel
{

    public class ActorTagsUtility
    {
        static public float GetActorPositionX(string positionX)
        {
            switch (positionX)
            {
                case "center":
                    return Settings.Instance.Center_X;
                case "left":
                    return Settings.Instance.Left_X;
                case "right":
                    return Settings.Instance.Right_X;
                case "mid_left":
                    return Settings.Instance.Mid_Left_X;
                case "mid_right":
                    return Settings.Instance.Mid_Right_X;
                default: //center by default
                    return Settings.Instance.Center_X;
            }
        }

        static public float GetActorPositionY()
        {
            return Settings.Instance.Actor_Y;
        }

        static public float GetActorPositionZ(string positionZ)
        {
            float z = Settings.Instance.Normal_Z;
            switch (positionZ)
            {
                case "far":
                    z = Settings.Instance.Far_Z;
                    break;
                case "near":
                    z = Settings.Instance.Near_Z;
                    break;
                case "normal":
                    z = Settings.Instance.Normal_Z;
                    break;
                default:
                    z = Settings.Instance.Normal_Z;
                    break;
            }
            return z;
        }

        static public Vector3 GetActorPosition(string pos, string posZ)
        {
            float z = Settings.Instance.Normal_Z;
            float y = Settings.Instance.Actor_Y;

            switch (posZ)
            {
                case "Far":
                    z = Settings.Instance.Far_Z;
                    break;
                case "near":
                    z = Settings.Instance.Near_Z;
                    break;
                case "normal":
                    z = Settings.Instance.Normal_Z;
                    break;
                default:
                    z = Settings.Instance.Normal_Z;
                    break;
            }

            switch (pos)
            {
                case "center":
                    return new Vector3(Settings.Instance.Center_X, y, z);
                case "left":
                    return new Vector3(Settings.Instance.Left_X, y, z);
                case "right":
                    return new Vector3(Settings.Instance.Right_X, y, z);
                case "mid_left":
                    return new Vector3(Settings.Instance.Mid_Left_X, y, z);
                case "mid_right":
                    return new Vector3(Settings.Instance.Mid_Right_X, y, z);
                default: //center by default
                    return new Vector3(Settings.Instance.Center_X, y, z);
            }
        }
    }

    ///
    /// tag = create_actor
    /// 
    /// <desc>
    /// 预创建新的立绘, 默认为不激活状态，加载所有表情
    /// </desc>
    /// <params>
    /// @name:       立绘的文件名，是一个prefab
    /// @default:    默认立绘表情
    /// </params>
    /// 
    /// <sample>
    /// [create_actor name=Sachi position=center]
    /// </sample>
    ///

    public class Create_actorTag : AbstractTag
    {
        public Create_actorTag()
        {
            DefaultParams = new Dictionary<string, string>()
            {
                {"name", ""},
                {"default", "default"},
            };

            VitalParams = new List<string>()
            {
                "name",
            };
        }

        public override void Excute()
        {
            Debug.LogFormat("Create Actor: {0}", Params["name"]);

            GameObject prefab = Resources.Load<GameObject>(Settings.Instance.ACTOR_PATH + Params["name"]);
            var go = GameObject.Instantiate(prefab);
            var actor = go.GetComponent<ActorObject>();

            go.name = Params["name"];
            actor.Trans.SetParent(Settings.Instance.ActorRoot);
            actor.Trans.localScale = new Vector3(1, 1, 1);
            actor.Trans.anchoredPosition3D = new Vector3(0, 0, 0);
            actor.Trans.anchorMin = Vector2.zero;
            actor.Trans.anchorMax = Vector2.zero;
            actor.gameObject.SetActive(false);
            SceneManager.Instance.AddObject(Params["name"], actor);

            var expId = actor.GetExpression(Params["default"]);
            if (expId >= 0)
            {
                actor.CurImage.sprite = actor.ExpressionImages[expId];
            }
            else
            {
                Debug.LogErrorFormat("[Create Actor] Default Expression Wrong! Actor:{0}, Exp:{1}", Params["Name"],
                                     Params["default"]);
            }

        }
    }

    /// 
    /// tag = expression
    /// 
    /// <desc>
    /// 改变立绘表情
    /// </desc>
    /// 
    /// <params>
    /// @name:       立绘名
    /// @type:       表情名称
    /// </params>
    /// 
    /// <sample>
    /// [expression name=李维特 type=慌张]
    /// </sample>
    /// 
    public class ExpressionTag: AbstractTag
    {
        public ExpressionTag()
        {

            DefaultParams = new Dictionary<string, string>()
            {
                {"name", ""},
                {"type", ""},
                {"fade", "0.1" }

            };

            VitalParams = new List<string>()
            {
                "name",
                "type"
            };
        }

        public override void Excute()
        {   
            Debug.LogFormat("[expression name={0} type={1}]", Params["name"], Params["type"]);
            var actor = SceneManager.Instance.GetObject<ActorObject>(Params["name"]);
            float time = float.Parse(Params["fade"]);
            if (time == 0)
            {
                actor.SetExpressionWithoutAnimation(Params["type"]);
            }
            else
            {
                Engine.Status.EnableNextCommand = false;

                actor.SetExpressionWithAnimation(Params["type"], time, OnFinishAnimation);
            }
        }

        public override void OnFinishAnimation()
        {
            Engine.Status.EnableNextCommand = true;
        }
    }

    /// 
    /// tag = show_actor
    /// 
    /// <desc>
    /// 显示立绘
    /// </desc>
    /// 
    /// <params>
    /// @name:       立绘名
    /// @pos:        立绘所在方位（left mid-left center mid_right right）
    /// @fade:       淡入时间
    /// @expression: 立绘表情
    /// </params>
    /// 
    /// <sample>
    /// [show_actor name=李维特 pos=left fade=0.5]
    /// </sample>
    /// 
    public class Show_actorTag : AbstractTag
    {
        public Show_actorTag()
        {

            DefaultParams = new Dictionary<string, string>()
            {
                {"name", ""},
                {"pos", "center"},
                {"fade", "0.5" },
                {"expression", "default" }
            };

            VitalParams = new List<string>()
            {
                "name"
            };
        }

        public override void Excute()
        {
            Debug.LogFormat("[show_actor] name={0}", Params["name"]);
            var actor = SceneManager.Instance.GetObject<ActorObject>(Params["name"]);
           
            actor.gameObject.SetActive(true);
            actor.Trans.anchoredPosition = ActorTagsUtility.GetActorPosition(Params["pos"], "normal");

            Engine.Status.EnableNextCommand = false;
            
            actor.SetExpressionWithoutAnimation(Params["expression"]);

            actor.FadeIn(float.Parse(Params["fade"]), OnFinishAnimation);
        }

        public override void OnFinishAnimation()
        {
            Engine.Status.EnableNextCommand = true;
        }
    }
}
