using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

namespace SNovel
{
    /// <summary>
    /// [play_animation name=手指平移]
    /// </summary>
    public class Play_animationTag : AbstractTag
    {
        public Play_animationTag()
        {
            DefaultParams = new Dictionary<string, string>()
            {
                {"name", "" }
            };

            VitalParams = new List<string>()
            {
                "name"
            };
        }

        public override void Excute()
        {
            Debug.Log("[Play Animation]");
            string name = Params["name"];
            var Anim = GameObject.Find(name);
            if (Anim == null)
            {
				Debug.LogWarningFormat("Can not play animation:{0}", name);
                //Debug.LogErrorFormat("Can not play animation:{0}", name);
                return;
            }
            var anim = Anim.GetComponent<Animator>();
            Engine.Status.EnableNextCommand = false;
            
            anim.SetTrigger("play");         
        }
    }

    /// tag = fadein
    /// 
    /// <desc>
    /// 立绘淡出
    /// </desc>
    /// 
    /// <params>
    /// @name:       the name of the actor
    /// @time:   淡出时间
    /// </params>
    /// 
    /// <sample>
    /// [fadein name=Sachi fadetime=0.5]
    /// </sample>
    /// 
    public class FadeinTag: AbstractTag
    {
        public FadeinTag()
        {
            DefaultParams = new Dictionary<string, string>()
            {
                {"name", ""},
                {"time", "0.2"},
            };

            VitalParams = new List<string>()
            {
                "name",
            };
        }

        public override void Excute()
        {
            //base.Excute();

            //actor name
            string imageName = Params["name"];

            Debug.LogFormat("Fade In: {0}", imageName);

            //get actor
            ImageObject ao = SceneManager.Instance.GetObject<ImageObject>(imageName);
            
            if(ao == null)
            {
				Debug.LogWarningFormat("Can not find actor to fade:{0}", imageName);
                //Debug.LogErrorFormat("Can not find actor to fade:{0}", imageName);
                return;
            }

            ao.gameObject.SetActive(true);
            // ao.OnAnimationFinish = OnFinishAnimation;

            float time = float.Parse(Params["time"]);

            ao.OnAnimationFinish = OnFinishAnimation;
            //if (time - 0f > float.Epsilon)
            //{
            Engine.Status.EnableNextCommand = false;

            ao.FadeIn(time, OnFinishAnimation);
        }
        public override void OnFinishAnimation()
        {

           // Debug.Log("Finish Animation!");
            Engine.Status.EnableNextCommand = true;
        }
    }

    /// 
    /// tag = fadeout
    /// 
    /// <desc>
    /// 立绘淡出
    /// </desc>
    /// 
    /// <params>
    /// @name:       the name of the actor
    /// @time:   淡出时间
    /// </params>
    /// 
    /// <sample>
    /// [fadeout name=Sachi fadetime=0.5]
    /// </sample>
    /// 
    public class FadeoutTag: AbstractTag
    {
        public FadeoutTag()
        {
            DefaultParams = new Dictionary<string, string>()
            {
                {"name", ""},
                {"time", "0.2"},
            };

            VitalParams = new List<string>()
            {
                "name",
            };
        }

        public override void Excute()
        {
            //base.Excute();

            //actor name
            string imageName = Params["name"];

            Debug.LogFormat("Fade out: {0}", imageName);

            //get actor
            ImageObject ao = SceneManager.Instance.GetObject<ImageObject>(imageName);
            if(ao == null)
            {
                Debug.LogErrorFormat("Can not find actor to fade:{0}", imageName);
                return;
            }
            // ao.OnAnimationFinish = OnFinishAnimation;

            float time = float.Parse(Params["time"]);

        //    ao.OnAnimationFinish = OnFinishAnimation;
            // if (time - 0f > float.Epsilon)
            // {
            Engine.Status.EnableNextCommand = false;

            ao.FadeOut(time, OnFinishAnimation);
            // }
            //  else
            //  {
            //      ao.FadeOut(0);
            // }
        }
        public override void OnFinishAnimation()
        {

            Debug.Log("Finish Animation!");
            Engine.Status.EnableNextCommand = true;
        }
    }

}
