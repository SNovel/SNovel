using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace SNovel
{
    
    class UIManager
    {
        //Instance
        static UIManager _sharedUIManager = null;

        public static UIManager Instance
        {
            get
            {
                if (_sharedUIManager == null)
                {
                    _sharedUIManager = new UIManager();
                }
                return _sharedUIManager;
            }
        }


        public Transform SelectLayout;

        List<GameObject> _selectList;
        
        public UIManager()
        {
            SelectLayout = GameObject.Find("SelectLayout").transform;
            if(SelectLayout == null)
            {
                Debug.LogError("SelectLayout do not exist!");
            }
            _selectList = new List<GameObject>();

        }
        public void AddSelect(string imageFileName, string text, Action onClick)
        {
           // SelectObject selectObject = new SelectObject(imageFileName, text, onClick);
           // selectObject.Trans.SetParent(SelectLayout, false);   
          //  _selectList.Add(selectObject.gameObject);
        }

        public void ShowSelects()
        {
            foreach (Transform t in SelectLayout)
            {
                //t.GetComponent<Image>().DOFade()
          //      GameObject.Instantiate(t.gameObject);
                t.gameObject.SetActive(true);
            }
            //SelectLayout.gameObject.SetActive(true);
        }

        public void ClearSelects()
        {
            foreach(GameObject go in _selectList)
            {
                GameObject.Destroy(go);
            }
            _selectList.Clear();
        }
    }
}
