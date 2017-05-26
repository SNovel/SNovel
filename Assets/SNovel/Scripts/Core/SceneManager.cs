using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace SNovel
{
    public class SceneManager
    {
        static SceneManager _sharedSceneManager = null;
        public static SceneManager Instance
        {
            get
            {
                if (_sharedSceneManager == null)
                {
                    _sharedSceneManager = new SceneManager();
                }
                return _sharedSceneManager;
            }
        }

        SceneManager()
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