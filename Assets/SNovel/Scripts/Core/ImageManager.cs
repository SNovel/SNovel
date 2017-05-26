using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Utilities;

/*
 * ChangeLog
 * 2015.9.15 
 * Change ImageInfo.Position from Vector3 to Vector2
 * Add ImageInfo.AnchorPos
 */
namespace SNovel
{
    //默认锚点在左下角
    class ImageInfo: ObjectInfo
    {
        //Params
      //public  string  ObjName   = "";
      //public  string  Name      = "";
        public  string  Path      = "";       
  //    public  float   x         = 0.0f;
  //    public  float   y         = 0.0f;    
  //    public  float   z         = 0.0f;
        public  bool    Show      = false;
        public  bool    Fade      = false;
        public  float   Fadetime  = 0.0f;
        public  string  Root      = "";
        public  float   Scale     = 1;
        public  Vector2 Position  = new Vector2(0, 0);
        
        public  Vector2 AnchorPos = new Vector2(0, 0);
        public ImageInfo(Dictionary<string, string> param)
        {
            if(param.ContainsKey("objname"))
            {
                ObjName = param["objname"];
            }
            if(param.ContainsKey("name"))
            {
                Name = param["name"];
            }
            if(param.ContainsKey("path"))
            {
                Path = param["path"];
            }
            if(param.ContainsKey("show"))
            {
                Show = bool.Parse(param["show"]);
            }
            if(param.ContainsKey("fadeTime"))
            {
                Fadetime = float.Parse(param["fadeTime"]);
            }
            if(param.ContainsKey("root"))
            {
                Root = param["root"];
            }
            if(param.ContainsKey("scale"))
            {
                Scale = float.Parse(param["scale"]);
            }
        }
    }
    class ImageManager: Singleton<ImageManager>
    {
        /*
        public void ChangeImageWithFade(ImageObject io, string imageFileName, float fadeTime)
        {
            io.ChangeImage(imageFileName, fadeTime);
        }*/

        /*
        public void ChangeImageWithoutFade(string objectName, string imageFileName)
        {
            ImageObject oldImage = ImageObject.CreateWithSceneObject(objectName);
            oldImage.ChangeImage(imageFileName, 0f);
        }*/
        /*
        public void ChangeImageWithoutFade(ImageObject io, string imageFileName)
        {
            io.ChangeImage(imageFileName, 0f);
        }*/
        ImageManager()
        {
            _objectInScene = new Dictionary<string, AbstractObject>();
        }

        Dictionary<string, AbstractObject> _objectInScene;

        public TObject CreateObject<TObject, TInfo>(TInfo info)
            where TObject: AbstractObject, new()
            where TInfo:   ObjectInfo
        {
            TObject obj = new TObject();
            obj.Init(info);

            _objectInScene.Add(info.Name, obj);
            return obj;

        }

        public TObject GetCreatedObject<TObject>(string name)
            where TObject: AbstractObject
        {
            if (!_objectInScene.ContainsKey(name))
                return default(TObject);
            else
            {
                AbstractObject ao = _objectInScene[name];
                return (TObject)ao;
            }
        }

        public TObject GetObjectInScene<TObject>(string objName)
                where TObject : AbstractObject, new()
        {
            if (_objectInScene.ContainsKey(objName))
            {
                Debug.LogFormat("Object:{0} has already add to the manager!", objName);
                return (TObject)_objectInScene[objName];
            }
            else
            {
                TObject ao = new TObject();
                ao.Init(objName);
                _objectInScene.Add(objName, ao);
                return ao;
            }
        }
    }

}
