using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SNovel;
using SNovel.MessageNotificationCenter;
using UnityEngine;
using UnityEngine.UI;

namespace SNovel
{
    /// <summary>  
    /// 文本框Object 默认一个Obj有name和main两个文本框，方便定位
    /// </summary>
    /// 
    /// <remarks>   , 2017/7/24. </remarks>
    [RequireComponent(typeof(Image))]
    public class TextBoxObject : AbstractObject
    {
        public string Name;

        public Text UIText;

        public bool UseEffect;
        public TextEffect TextEffect;
     //   public float EffectDuration;

        public string ShownText
        {
            get;
            set;
        }

        public virtual void ClearMessage()
        {
            ShownText = "";
        }

        public virtual void Reline()
        {
            ShownText += '\n';
        }

        // Use this for initialization
        void Start()
        {
            //check textEffect
            if(UseEffect && TextEffect == null)
            {
                Debug.LogFormat("no textEffect component on this TextBox:{0}",
                                Name);
            }
            if (UIText == null)
            {
                UIText = GetComponentInChildren<Text>();
            }
            UIText.text = ShownText = "";

        }
        //待显示的文字
        string _textToShow = "";

        public void SetText(string text, Action onFinish)
        {

            if(ShownText != "")
            {
                _textToShow = ShownText + text;
            }
            else
                _textToShow = text;
            // UpdateText();

            if (UseEffect)
            {
                if (TextEffect.IsWaiting())
                {
                    TextEffect.StartEffect(ShownText, _textToShow, onFinish);
                    ShownText = _textToShow;
                }
                else
                {
                    Debug.Log("Display Text Error");
                }
            }
            else
            {
                UIText.text = _textToShow;
            
                ShownText = _textToShow;
                onFinish();
                // text.text = ShownText;
            }
            // ShownText += text;
            //  UpdateText();
        }

        public void OnClick()
        {
            if(TextEffect.IsWaiting())
            {
                MessageDispatcher.Instance.DispatchMessage(new Message("SCRIPT_CLICK_CONTINUE"));
            }
            else if(TextEffect.IsRendering())
            {
                TextEffect.DisplayTextRemain();
            }

        }

        public void SetNativeSize()
        {
            GetComponent<Image>().SetNativeSize();
        }
    }
}
