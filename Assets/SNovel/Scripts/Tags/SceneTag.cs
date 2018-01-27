using UnityEngine;
using System.Collections.Generic;

namespace SNovel
{
    /// <summary>   
    /// 显示某个元素
    /// </summary>
    /// 
    /// <params>
    /// @name: 元素名称（已经create过的）
    /// @x, y: 设置坐标
    /// @scale: 缩放
    /// </params>

    public class ShowTag : AbstractTag
    {
        public ShowTag()
        {
            DefaultParams = new Dictionary<string, string>()
            {
                {"name", ""},
                {"x", "null"},
                {"y", "null"},
                {"scale", "null"},
                {"layer", ""}
            };

            VitalParams = new List<string>()
            {
                "name"
            };
        }

        public override void Excute()
        {
            Debug.LogFormat("[Show {0}]", Params["name"]);

            var obj = SceneManager.Instance.GetObject<AbstractObject>(Params["name"]);
            if(obj == null)
            {
                Debug.LogErrorFormat("can not find {0}, do you create before?\nline:{1}", Params["name"], ToString());
            }
            //递归显示所有子元素
            foreach (Transform tr in obj.transform)
            {
                tr.gameObject.SetActive(true);
            }
            obj.gameObject.SetActive(true);
            if (Params["x"] != "null")
            {
                obj.SetPositionX(float.Parse(Params["x"]));
            }
            if(Params["y"] != "null")
            {
                obj.SetPositionY(float.Parse(Params["y"]));
            }
            if (Params["scale"] != "null")
            {
                obj.Trans.localScale = new Vector3(float.Parse(Params["scale"]), float.Parse(Params["scale"]), 1);

            }

            switch (Params["layer"])
            {
                case "bg":
                    obj.Trans.SetParent(Settings.Instance.BGRoot);
                    break;
                case "actor":
                    obj.Trans.SetParent(Settings.Instance.ActorRoot);
                    break;
                case "ui":
                    obj.Trans.SetParent(Settings.Instance.UIRoot);
                    break;
            }
        }

    }

    /// <summary>   
    /// 不显示某个元素
    /// </summary>
    /// 
    /// <params>
    /// @name: 元素名称（已经create过的）
    /// </params>

    public class HideTag: AbstractTag
    {
        public HideTag()
        {
            DefaultParams = new Dictionary<string, string>()
            {
                {"name", ""},
            };

            VitalParams = new List<string>()
            {
                "name"
            };
        }

        public override void Excute()
        {
            Debug.LogFormat("[Hide {0}]", Params["name"]);

            var obj = SceneManager.Instance.GetObject<AbstractObject>(Params["name"]);

            obj.gameObject.SetActive(false);
        }

    }

    /// <summary>   
    /// 清屏
    /// </summary>
    /// 
    /// <params>
    /// @name: 元素名称（已经create过的）
    /// </params>

    public class Clear_allTag : AbstractTag
    {
        public Clear_allTag()
        {
            DefaultParams = new Dictionary<string, string>()
            {
            };

            VitalParams = new List<string>()
            {
            };
        }

        public override void Excute()
        {
            Debug.Log("[Clear all]");

            SceneManager.Instance.Clear();
        }

    }

}