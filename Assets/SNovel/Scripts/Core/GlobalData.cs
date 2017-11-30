using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SNovel
{
    public class GlobalData
    {
        static GlobalData _instance = null;

        public static GlobalData Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GlobalData();
                }
                return _instance;
            }
        }

        private Dictionary<string, string> _globalData = new Dictionary<string, string>();

        public void AddData(string name, string value)
        {
            if (_globalData.ContainsKey(name))
            {
                Debug.LogWarningFormat("SNovel: name {0} has data, override");
                _globalData[name] = value;
            }
            else
            {
                _globalData.Add(name, value);
            }
        }

        public void Clear()
        {
            _globalData.Clear();
        }

        public string GetData(string name)
        {
            if (_globalData.ContainsKey(name))
            {
                return _globalData[name];
            }

            Debug.LogError(string.Format("SNovel: Has not cached data: {0} in GlobalData", name));
            return "";

        }


    }
}
