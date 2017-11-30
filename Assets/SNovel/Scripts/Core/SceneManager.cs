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
            _cachedObject = new Dictionary<string, AbstractObject>();
        }

        Dictionary<string, AbstractObject> _cachedObject;

        public TextBoxObject CurrentMainTextBox
        {
            get;
            set;
        }

        public TextBoxObject CurrentNameTextBox
        {
            get;
            set;
        }

        public TObject AddObject<TObject>(string name, TObject obj)
            where TObject: AbstractObject
        {
            _cachedObject.Add(name, obj);
            return obj;
        }
        /*
        public TObject CreateObject<TObject>(string prefabName)
            where TObject: AbstractObject, new()
        {
            TObject obj = new TObject();
            obj.Init(prefabName);

            _cachedObject.Add(prefabName, obj);
            obj.Go.name = prefabName;
            return obj;
        }
        */
        public TObject GetObject<TObject>(string name)
            where TObject: AbstractObject
        {
            if (!_cachedObject.ContainsKey(name))
                return null;
            else
            {
                AbstractObject ao = _cachedObject[name];
                return (TObject) ao;
            }
        }
        /*
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
        */
        public void Clear()
        {
            foreach (var obj in _cachedObject)
            {
                GameObject.Destroy(obj.Value.gameObject);
            }
            _cachedObject.Clear();

        }
        public GameObject Root
        {
            get
            {
                if (_root == null)
                {
                    _root = Settings.Instance.Root;
                }
                return _root;
            }
        } 

        private GameObject _root= null;
    }
}