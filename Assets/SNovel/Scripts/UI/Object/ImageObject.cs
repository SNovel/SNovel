using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace SNovel
{
    public static class ImageUtil
    {
        public static void FadeIn(Image image, float fadetime, Action onAnimationFinish)
        {
            var originColor = image.color;
            if (fadetime == 0)
                image.color = new Color(originColor.r, originColor.g, originColor.b, 0);
            else
            {
                image.color = new Color(1, 1, 1, 0);
                Tween t = image.DOFade(1, fadetime);
                if (onAnimationFinish != null)
                    t.OnComplete(new TweenCallback(onAnimationFinish));
            }
        }

        public static void FadeOut(Image image, float fadetime, Action onAnimationFinish)
        {
            var originColor = image.color;
            if (fadetime == 0)
                image.color = new Color(originColor.r, originColor.g, originColor.b, 1);
            else
            {
                image.color = new Color(255, 255, 255, 255);
                Tween t = image.DOFade(0, fadetime);
                if (onAnimationFinish != null)
                    t.OnComplete(new TweenCallback(onAnimationFinish));
            }
        }
    }

    //默认锚点在左下角
    class ImageInfo : ObjectInfo
    {
        public string Path = "";
        public bool Show = false;
        public bool Fade = false;
        public float Fadetime = 0.0f;
        public string Root = "";
        public float Scale = 1;
        public Vector2 Position = new Vector2(0, 0);

        public Vector2 AnchorPos = new Vector2(0, 0);

        public ImageInfo(Dictionary<string, string> param)
        {
            if (param.ContainsKey("assetName"))
            {
                AssetName = param["assetName"];
            }
            if (param.ContainsKey("path"))
            {
                Path = param["path"];
            }
            if (param.ContainsKey("show"))
            {
                Show = bool.Parse(param["show"]);
            }
            if (param.ContainsKey("fadeTime"))
            {
                Fadetime = float.Parse(param["fadeTime"]);
            }
            if (param.ContainsKey("root"))
            {
                Root = param["root"];
            }
            if (param.ContainsKey("scale"))
            {
                Scale = float.Parse(param["scale"]);
            }
        }
    }

    public class ImageObject : AbstractObject
    {

        [HideInInspector]
        public Image CurImage
        {
            get
            {
                if (_curImage == null)
                {
                    _curImage = GetComponent<Image>();
                }
                return _curImage;
            }
        }


        private Image _curImage = null;

        public virtual void FadeIn(float fadetime, Action onFinished)
        {
            CurImage.color = new Color(1, 1, 1, 0);
            CurImage.DOFade(1, fadetime).OnComplete(new TweenCallback(onFinished));
        }

        public virtual void FadeOut(float fadetime, Action onFinished)
        {
            CurImage.color = new Color(1, 1, 1, 1);
            CurImage.DOFade(0, fadetime).OnComplete(new TweenCallback(onFinished));

        }
    }
}
