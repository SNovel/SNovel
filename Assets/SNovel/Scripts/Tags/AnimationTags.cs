using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

namespace SNovel
{
    /// <summary>
    /// [playAnimation name=手指平移]
    /// </summary>
    public class PlayanimationTag : AbstractTag
    {
        public PlayanimationTag()
        {
            _defaultParamSet = new Dictionary<string, string>()
            {
                {"name", "" }
            };

            _vitalParams = new List<string>()
            {
                "name"
            };
        }

        public override void Excute()
        {
            string name = Params["name"];
            var Anim = GameObject.Find(name);
            if (Anim == null)
            {
                Debug.LogErrorFormat("Can not play animation:{0}", name);
            }
            var anim = Anim.GetComponent<Animator>();
            Engine.Status.EnableNextCommand = false;
            
            anim.SetTrigger("play");         
        }

        public override void After()
        {
            //base.After();
        }
    }
}
