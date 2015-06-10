﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Sov.AVGPart
{
    /*
     * tag = image_change
     * 
     * <desc>
     * 更换GameObject的图片
     * 
     * <param>
     * @name:       图片名称
     * @objname:    GameObject的name, 全部小写
     * @path:    文件在"Resources/"下的相对路径
     * @fade:       是否渐变显示
     * @fadetime:   渐变时间
     * 
     * <example>
     * [image_change name=background path=room_tall fade=true]
     */

    class Image_changeTag: AbstractTag
    {
        public Image_changeTag()
        {
            _defaultParamSet = new Dictionary<string, string>() {
                { "objname",    ""},
                { "name",       "" },
                { "path",    "" },
                { "fade",       "false" },
                { "fadetime",   "0.5" } 
            };
            _vitalParams = new List<string>() {
                "name",
                "objname",
                "path"
            };
        }

        public override void Excute()
        {
            string objName = Params["objname"];
            string path = Params["path"] + Params["name"];
            Engine.Status.EnableNextCommand = false;

            ImageObject io = Instances.Instance.ImageManager.GetImageObjectInScene(objName);
            io.OnAnimationFinish = OnFinishAnimation;
            if (Params["fade"] == "true")
            {
                //等待动画结束函数回调继续执行
                Instances.Instance.ImageManager.ChangeImageWithFade(io,
                                                                   path,
                                                           float.Parse(Params["fadetime"]));
            }
            else
            {
                Engine.Status.EnableNextCommand = true;
                Instances.Instance.ImageManager.ChangeImageWithoutFade(io, path);
            }
            

            base.Excute();

        }
        public override void After()
        {
            //base.After();
        }
        public override void OnFinishAnimation()
        {
            if (Params["fade"] == "true")
            {
                Debug.Log("Finish Animation!");
                Engine.Status.EnableNextCommand = true;
                Engine.NextCommand();
            }
        }
    }

    /*
     * tag = image_new
     * 
     * <desc>
     * 创建并显示新的图片
     * 
     */ 
}
