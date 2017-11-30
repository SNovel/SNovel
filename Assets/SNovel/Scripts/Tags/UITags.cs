using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace SNovel
{
    ///
    /// tag = button
    /// 
    /// <summary>
    /// 加载一个包含按钮的prefab，点击后跳转到指定的label执行接下来的命令
    /// </summary>
    /// 
    /// <param>
    /// @prefabName：包含按钮的prefab（prefab可以更好的调整样式）
    /// @name : 按钮控件的名字，
    /// @text： 按钮上的文本,同时Button的UI名字也是text
    /// @image: 按钮图像 在IMAGE_PATH中
    /// @label：跳转的label
    /// @x，y:  button的位置
    /// @layer: 需要放置的层级
    /// @nativeSize: 默认为prefab的大小，若有image则调整为image大小，若该参数为false则调整为指定大小
    /// @width height： 指定大小
    /// </param>
    /// 
    /// <sample>
    /// Hello,World[p]
    /// </sample>
    public class ButtonTag : AbstractTag
    {
        public ButtonTag()
        {
            _vitalParams = new List<string>
            {
                "prefabName"
            };

            _defaultParams = new Dictionary<string, string>
            {
                {"prefabName", ""},
                {"name", "" },
                {"image", "null" },
                {"text", "" },
                {"onClick","null" },
                {"x", "0" },
                {"y", "0" },
                { "layer","ui" },
                { "nativeSize", "true" },
                { "width","100" },
                { "height","100" },
                { "do", "null"}
        };
        }

        public override void Excute()
        {
            Debug.Log("[button]");

            string text = Params["text"];
            GameObject prefab = Resources.Load<GameObject>(Settings.Instance.UI_PATH + Params["prefabName"]);

            var go = GameObject.Instantiate(prefab);
            go.name = Params["name"];

            var obj = go.GetComponent<ButtonObject>();
            obj.Text = text;

            if (Params["image"] != "null")
            {
                Sprite imageRes = Resources.Load<Sprite>(Settings.Instance.IMAGE_PATH + Params["image"]);
                var image = GameObject.Instantiate(imageRes);
                obj.UIImage.sprite = image;
                obj.UIImage.SetNativeSize();
            }
            if (!bool.Parse(Params["nativeSize"]))
            {
                obj.Trans.sizeDelta = new Vector2(float.Parse(Params["width"]), float.Parse(Params["height"]));
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
            obj.Trans.anchoredPosition3D = new Vector3(float.Parse(Params["x"]), float.Parse(Params["y"]), 0);
            obj.Trans.localScale = new Vector3(1, 1, 1);
            ButtonObject t = SceneManager.Instance.AddObject(Params["name"], obj);
            if (t == null)
            {
                Debug.LogFormat("Can not Create Button:{0}", Params["name"]);
                return;
            }
            if (Params["onClick"] != "null")
            {
                t.UIBtn.onClick.AddListener(() =>
                {
                    if (Engine.CurrentScene.BlockDict.ContainsKey(Params["onClick"]))
                    {
                        var block = Engine.CurrentScene.BlockDict[Params["onClick"]];
                        block.Status.Reset();
                        Engine.BlockStack.Push(block);
                    }
                    else
                    {
                        Debug.Log("Cannot find Block! Line: " + ToString());
                    }
                });
            }
            if (Params["do"] != null)
            {
                t.UIBtn.onClick.AddListener(() =>
                {
                    Engine.LuaState.L_DoString(Params["do"]);
                });
            }
        }

    }
}
