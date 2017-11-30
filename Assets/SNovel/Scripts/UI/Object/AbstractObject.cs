using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SNovel
{
    public abstract class ObjectInfo
    {
        //带读取的文件名称
        public string AssetName = "";
    }

    /*
     * AbstractObject
     * 
     * 引擎通过这个创建UI元素
     * 
     */
    public class AbstractObject:MonoBehaviour
    {
       // public GameObject Go;
        [HideInInspector]
        public RectTransform Trans
        {
            get
            {
                if (_trans == null)
                    _trans = GetComponent<RectTransform>();
                return _trans;
            }
            
        }

        private RectTransform _trans = null;

        public Action OnAnimationFinish
        {
            protected get;
            set;
        }

        public virtual void SetPosition3D(float x, float y, float z)
        {
            Trans.position = new Vector3(x, y, z);
        }

        public virtual void SetPosition3D(Vector3 p)
        {
            Trans.position = p;
        }
        public virtual void SetPosition2D(Vector2 p)
        {
            Trans.anchoredPosition = p;
        }
        public virtual void SetPosition2D(float x, float y)
        {
            Trans.anchoredPosition = new Vector2(x, y);
        }

        public virtual void SetPositionX(float x)
        {
            var p = Trans.anchoredPosition;
           Trans.anchoredPosition = new Vector2(x, p.y);
        }
        public virtual void SetPositionY(float y)
        {
            var p = Trans.anchoredPosition;
            Trans.anchoredPosition = new Vector2(p.x, y);
        }
        public virtual void SetParent(string name)
        {
            GameObject p =  GameObject.Find(name);
            if(p == null)
            {
                Debug.LogFormat("Can not find Parent:{0}", name);
                return;
            }
            Trans.SetParent(p.transform);
        }

    }
}
