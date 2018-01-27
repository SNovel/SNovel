using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace SNovel
{
    /*
     * tag = create_image
     * 
     * <desc>
     * 预创建新的图片
     * 
     * <param>
     * @name:       File name
     * @blockTouch: 是否屏蔽位于其后的触摸事件
     * @alpha:透明度
     * @nativeSize: 是否设置图片原始大小
     * @width， height ： 手动设置的图像大小
     * <sample>
     * [create_image name=sachi blockTouch=true]
     */
    public class Create_imageTag: AbstractTag
    {
        public Create_imageTag()
        {
            DefaultParams = new Dictionary<string,string>() {
                { "name",    "null"         },
                {"blockTouch","false" },
                {"alpha", "1" },
                {"nativeSize", "true" },
                {"width","100" },
                {"height","100" }
            };

            VitalParams = new List<string>() {
            };
        }

        public override void Excute()
        {

            //把创建prefab实例的操作放到这里来
            var prefab = Resources.Load<GameObject>(Settings.Instance.PREFAB_PATH + "ImageObject");
            var go = GameObject.Instantiate(prefab);
            if (go == null)
            {
                Debug.LogErrorFormat("Can not load prefab ImageObject in {0}", Settings.Instance.PREFAB_PATH + "ImageObject");
                return;
            }
            var obj = go.GetComponent<ImageObject>();

            obj.gameObject.layer = Settings.Instance.BG_LAYER;
            obj.gameObject.name = Params["name"];
            //add Image
            var image = go.GetComponent<Image>();
            if (Params["name"] != "null")
            {
                Sprite sp = Resources.Load<Sprite>(Settings.Instance.IMAGE_PATH + Params["name"]);

                if (sp == null)
                {
                    Debug.LogWarningFormat("Image: {0} not found", Settings.Instance.IMAGE_PATH + Params["name"]);
                    //Debug.LogErrorFormat("Image: {0} not found", Settings.Instance.IMAGE_PATH + Params["name"]);
                    return;
                }
                image.sprite = sp;
            }
            if (bool.Parse(Params["nativeSize"]))
            {
                image.SetNativeSize();
            }
            else
            {
                obj.Trans.sizeDelta = new Vector2(float.Parse(Params["width"]), float.Parse(Params["height"]));
            }
            image.color = new Color(1,1,1,float.Parse(Params["alpha"]));
            image.raycastTarget = bool.Parse(Params["blockTouch"]);
            //set local position
            obj.Trans.anchorMin = Vector2.zero;
            obj.Trans.anchorMax = Vector2.zero;
            //set parent
            obj.Trans.SetParent(Settings.Instance.BGRoot, true);
            obj.Trans.localScale = new Vector3(1,1,1);
            obj.Trans.anchoredPosition3D = new Vector3(0, 0, 0);

            go.SetActive(false);

            SceneManager.Instance.AddObject(Params["name"], obj);
            // Instances.Instance.ImageManager.CreateImage()
        }
    }

}
