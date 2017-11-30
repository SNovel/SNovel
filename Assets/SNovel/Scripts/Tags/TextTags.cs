using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

namespace SNovel
{
    ///
    /// tag = print
    /// 
    /// <summary>
    /// 设置主文本框中的语句
    /// </summary>
    /// 
    /// <param>
    /// @text:   待显示的文本
    /// </param>
    /// 
    /// <sample>
    /// Hello,World[p]
    /// </sample>
    public class PrintTag : AbstractTag
    {
        public PrintTag()
        {
            _vitalParams = new List<string>
            {
                "text"
            };

            _defaultParams = new Dictionary<string, string>
            {
                {"text", ""}
            };
        }

        public override void Excute()
        {
            Debug.Log("[Print Text]");

            string text = Params["text"];
            var replacedText = LuaHelper.ReplaceScopedVarInString(Engine.LuaState, text);

            if (SceneManager.Instance.CurrentMainTextBox != null)
            {
                SceneManager.Instance.CurrentMainTextBox.SetText(replacedText, ()=>
                {
                    Engine.Status.EnableNextCommand = true;
                });
            }
            Engine.Status.EnableNextCommand = false;
        }
    }

    ///
    /// tag = format_text
    /// 
    /// <summary>
    /// 格式化文本并显示在主文本框中
    /// </summary>
    /// 
    /// <params>
    /// @text:   格式化文本
    /// @var:    变量名（GlobalData中已经缓存的）
    /// </params>
    /// 
    /// <sample>
    /// [formatText text=您的逃生舱坠落地点为{0} var=PlayerPos]
    /// </sample>
    /// 
    public class Format_textTag : AbstractTag
    {
        public Format_textTag()
        {
            _vitalParams = new List<string>
            {
                "text",
                "var"

            };

            _defaultParams = new Dictionary<string, string>
            {
                {"text", ""},
                {"var", ""}
            };
        }

        public override void Excute()
        {
            string arg = GlobalData.Instance.GetData(Params["var"]);
            string text = Params["text"];
            string formattedText = string.Format(text, arg);
            var mainTextBox = SceneManager.Instance.CurrentMainTextBox;

            if(mainTextBox != null)
            {
                mainTextBox.SetText(formattedText, ()=> Engine.Status.EnableNextCommand = true);
          //      mainTextBox.Reline();
            }
            Engine.Status.EnableNextCommand = false;

        }
    };

    ///
    /// tag = set_text
    ///
    /// <desc>
    /// 设置文本框内的语句
    /// 
    /// <param>
    /// @text:       待显示的文本
    /// @textbox:    文本框名称
    /// 
    /// <sample>
    /// [set_text text=Sachi textbox=TextBox_Name]
    ///
    public class Set_textTag : AbstractTag
    {
        public Set_textTag()
        {
            _vitalParams = new List<string>
            {
                "text"
            };

            _defaultParams = new Dictionary<string, string>()
            {
                {"text", ""},
                {"textbox", ""}
            };

        }

        public override void Excute()
        {
            Debug.Log("[SetText]");
            string boxName = Params["textbox"];

            var textBox = SceneManager.Instance.GetObject<TextBoxObject>(boxName);
            if (textBox != default(TextBoxObject))
            {
                string text = Params["text"];

                textBox.ClearMessage();
                textBox.SetText(text, () => Engine.Status.EnableClickContinue = true);
            }

            Engine.Status.EnableNextCommand = false;
        }
    }

    ///
    /// tag = setname
    ///
    /// <desc>
    /// 显示人名
    ///  
    /// <sample>
    /// #Sachi
    ///
    public class SetnameTag : AbstractTag
    {
        public SetnameTag()
        {
            _vitalParams = new List<string>
            {
                "text"
            };

            _defaultParams = new Dictionary<string, string>()
            {
                {"text", ""},
            };
        }

        public override void Excute()
        {
            string text = Params["text"];

            TextBoxObject textbox = SceneManager.Instance.CurrentNameTextBox;

            var replacedText = LuaHelper.ReplaceScopedVarInString(Engine.LuaState, text);


            Debug.LogFormat("[SetName]:{0}", replacedText);
            if (textbox != null)
            {
                textbox.ClearMessage();
                textbox.SetText(replacedText, () => Engine.Status.EnableClickContinue = true);
            }
        }
    }

    ///
    /// tag = current
    /// 
    /// <desc>
    /// 切换当前显示的主文本框
    /// 
    /// <param>
    /// @name:  主文本框名称
    /// @type:   文本框的种类 main/name
    /// <sample>
    /// [current layer=TextBox type=Name]
    /// 
    ///
    public class CurrentTag : AbstractTag
    {
        public CurrentTag()
        {
            _vitalParams = new List<string>
            {
                "name",
                "type"
            };

            _defaultParams = new Dictionary<string, string>()
            {
                {"name", "TextBox"},
                {"type", "main" }
            };
        }

        public override void Excute()
        {
            Debug.Log("[Current]");
            string layer;
            layer = Params["name"];

            var textBox = SceneManager.Instance.GetObject<TextBoxObject>(layer);
            if (textBox != null)
            {
                Debug.LogFormat("change main textbox to: {0}", layer);
                switch (Params["type"].ToLower())
                {
                    case "main":
                        SceneManager.Instance.CurrentMainTextBox = textBox;
                        break;
                    case "name":
                        SceneManager.Instance.CurrentNameTextBox = textBox;
                        break;
                }
                
            }
        }
    }

    ///
    /// tag = create_textbox
    /// 
    /// <desc>
    /// 创建textbox, 默认放在ui目录下面
    /// </desc>
    /// <param>
    /// @name:     
    /// @type:       
    /// </param>
    /// <sample>
    /// [create_textbox name=Dos]
    /// </sample>
    ///
    public class Create_textboxTag : AbstractTag
    {
        public Create_textboxTag()
        {
            _defaultParams = new Dictionary<string, string>()
            {
                {"name", ""},
            };
            _vitalParams = new List<string>()
            {
                "name"
            };
        }

        public override void Excute()
        {
            Debug.Log("[Create TextBox]");
           

            GameObject prefab = Resources.Load<GameObject>(Settings.Instance.TEXBOX_PATH + Params["name"]);
            var go = GameObject.Instantiate(prefab);
            go.name = Params["name"];
            var obj = go.GetComponent<TextBoxObject>();

            if(obj == null)
            {
                Debug.LogErrorFormat("Can not find script textbox on {0}", Settings.Instance.TEXBOX_PATH + Params["name"]);
            }

            obj.Trans.localScale = new Vector3(1,1,1);
            obj.Trans.anchoredPosition3D = new Vector3(0,0,0);
            obj.Trans.anchorMin = Vector2.zero;
            obj.Trans.anchorMax = Vector2.zero;
            obj.gameObject.SetActive(false);

            TextBoxObject t = SceneManager.Instance.AddObject(Params["name"], obj);
            if(t == null)
            {
                Debug.LogFormat("Can not Create TestBox:{0}", Params["name"]);
                return;
            }
         //   t.SetNativeSize();
            obj.Trans.SetParent(Settings.Instance.UIRoot, false);    
        }
    }

    /// 
    /// tag = create_textbox_prefab
    /// 
    /// <desc>
    /// 创建一个装有几个textbox的prefab
    /// 为了方便姓名栏与文本框定位，可以他们化做一个整体
    /// </desc>
    /// 
    /// <param>
    /// @objname:     
    /// @type:       
    /// 
    /// <sample>
    /// [create_textbox name=Dos]
    /// 
    ///
    public class Create_textbox_prefabTag: AbstractTag
    {
        public Create_textbox_prefabTag()
        {
            _defaultParams = new Dictionary<string, string>()
            {
                {"name", ""},
            };
            _vitalParams = new List<string>()
            {
                "name"
            };
        }

        public override void Excute()
        {
            Debug.Log("Create TextBox Prefab");

            GameObject prefab = Resources.Load<GameObject>(Settings.Instance.TEXBOX_PATH + Params["name"]);
            var go = GameObject.Instantiate(prefab);
            go.name = Params["name"];
            var trans = go.GetComponent<RectTransform>();
            trans.SetParent(Settings.Instance.UIRoot);
            trans.localScale = new Vector3(1, 1, 1);
            trans.anchoredPosition3D = new Vector3(0, 0, 0);
            trans.anchorMin = Vector2.zero;
            trans.anchorMax = Vector2.zero;
            trans.gameObject.SetActive(false);

            //在prefab上挂一个空object
            SceneManager.Instance.AddObject(Params["name"], go.GetComponent<AbstractObject>());

            var textboxes = go.GetComponentsInChildren<TextBoxObject>();

            foreach (var textbox in textboxes)
            {
                textbox.gameObject.SetActive(false);
                textbox.gameObject.name = Params["name"] + "_" + textbox.Name;
                SceneManager.Instance.AddObject(Params["name"] + "_" + textbox.Name, textbox);                
            }
            go.SetActive(false);
        }
    }

    /// ********************* Message Tag ******************* */
    ///
    /// tag = l
    /// 
    /// <desc>
    /// 暂停点击继续
    /// 
    /// <sample>
    /// Hi,World.[l]
    ///
    public class LTag : AbstractTag
    {
        public LTag()
        {
            _vitalParams = new List<string>();
            _defaultParams = new Dictionary<string, string>();
        }

        public override void Excute()
        {
            //Do Nothing
            Debug.Log("[l]");
            Engine.Status.EnableClickContinue = true;
            Engine.Status.EnableNextCommand = false;
        }
    }

    ///
    /// tag = cm
    /// 
    /// <desc>
    /// 清除主文本框中的文字
    /// 
    /// <sample>
    /// [cm]
    /// 
    ///
    public class CmTag : AbstractTag
    {
        public CmTag()
        {
            _vitalParams = new List<string>();
            _defaultParams = new Dictionary<string, string>();
        }

        public override void Excute()
        {
            if(SceneManager.Instance.CurrentMainTextBox!= null)
                SceneManager.Instance.CurrentMainTextBox.ClearMessage();
            if(SceneManager.Instance.CurrentNameTextBox!=null)
                SceneManager.Instance.CurrentNameTextBox.ClearMessage();
        }
    }

    ///
    /// tag = p
    /// 
    /// <desc>
    /// 清除当前文本框内容并显示新的文本
    /// 
    /// <sample>
    /// Hello,World[p]
    /// 
    ///
    public class PTag : AbstractTag
    {
        public PTag()
        {
            _vitalParams = new List<string>();
            _defaultParams = new Dictionary<string, string>();
        }

        public override void Before()
        {
            base.Before();
           
        }

        public override void Excute()
        {
            Debug.Log("[p]");
            SceneManager.Instance.CurrentMainTextBox.ClearMessage();
        }
    }

    ///
    /// tag = r
    /// 
    /// <desc>
    /// 在当前文本框显示基础上换行显示新文本
    /// 默认为[r]
    /// 
    /// <sample>
    /// Hello,World[r]
    /// 
    ///
    public class RTag : AbstractTag
    {
        public RTag()
        {
            _vitalParams = new List<string>();
            _defaultParams = new Dictionary<string, string>();
        }

        public override void Excute()
        {
            Debug.Log("[r]");
            SceneManager.Instance.CurrentMainTextBox.Reline();
        }
    }

    ///
    /// tag = pl
    /// 
    /// <desc>
    /// 清除当前文本框内容并显示新的文本,暂停
    /// 
    /// <sample>
    /// Hello,World[p]
    /// 
    ///
    public class PlTag: AbstractTag
    {
        public PlTag()
        {
            _vitalParams = new List<string>();
            _defaultParams = new Dictionary<string, string>();
        }

        public override void Before()
        {
            base.Before();

        }

        public override void Excute()
        {
            Debug.Log("[pl]");
            SceneManager.Instance.CurrentMainTextBox.ClearMessage();
            Engine.Status.EnableClickContinue = true;
            Engine.Status.EnableNextCommand = false;
        }
    }

    ///
    /// tag = rl
    /// 
    /// <desc>
    /// 在当前文本框显示基础上换行显示新文本,暂停
    /// 默认为[rl]
    /// 
    /// <sample>
    /// Hello,World[rl]
    /// 
    ///
    public class RlTag: AbstractTag
    {
        public RlTag()
        {
            _vitalParams = new List<string>();
            _defaultParams = new Dictionary<string, string>();
        }

        public override void Excute()
        {
            Debug.Log("[rl]");
            SceneManager.Instance.CurrentMainTextBox.Reline();
            Engine.Status.EnableClickContinue = true;
            Engine.Status.EnableNextCommand = false;
        }
    }
    /* ********************* Message Tag ******************* */
}