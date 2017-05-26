using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace SNovel
{
    /// 
    /// tag = Lockcamera
    /// 
    /// <desc>
    /// 锁住相机平移
    /// </desc>
    /// 
    /// 
    /// <sample>
    /// [lockcamera]
    /// </sample>
    /// 
    public class Lock_cameraTag : AbstractTag
    {
        public Lock_cameraTag()
        {
            _defaultParamSet = new Dictionary<string, string>()
            {
            };

            _vitalParams = new List<string>()
            {
            };
        }

        public override void Excute()
        {
            //GameManager.Instance.LockCamera();
        }

        public override void After()
        {
            base.After();
        }
    }

    /// <summary>
    /// [unlock_camera]
    /// </summary>
    public class Unlock_cameraTag : AbstractTag
    {
        public Unlock_cameraTag()
        {
            _defaultParamSet = new Dictionary<string, string>()
            {
            };

            _vitalParams = new List<string>()
            {
            };
        }

        public override void Excute()
        {
            //GameManager.Instance.UnlockCamera();
        }

        public override void After()
        {
            base.After();
        }
    }
}
