using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;


namespace SNovel
{
    /*
     * tag = scenario
     * 
     * <desc>
     *  记录以下内容为一个新片段，供跳转等实现
     *  
     * <sample>
     * *Demonstration(English)/START/1_Dialog
     * 
     */
    public class ScenarioTag: AbstractTag
    {
        public ScenarioTag()
        {
            VitalParams = new List<string>() {
                "scenario"
            };

            DefaultParams = new Dictionary<string, string>() {
                { "scenario", "" }
            };
        }

        public override void Excute()
        {
            Debug.LogFormat("[Scenario]: {0}", Params["scenario"]);
            //Done By ScriptEngine
            /*
            if(Params.ContainsKey("scenario"))
            {
                string scenarioName = Params["scenario"];
                Engine.AddScenario(scenarioName);
            }*/
        }
    }

    /*
     * tag = s
     * 
     * <desc>
     * 脚本运行到此处时停止
     * 
     * <sample>
     * 
     * [select num = 2]
     * [select_new  target=*select_a1]View from the beginning.[end]
     * [select_new  target=*select_a2]Finish this demo.[end]
     * [s]
     * 
     */
    public class STag: AbstractTag
    {
        public STag()
        {
            VitalParams = new List<string>();
            DefaultParams = new Dictionary<string, string>();

        }

        public override void Excute()
        {
            Engine.Status.EnableClickContinue   = false;
            Engine.Status.EnableNextCommand     = false;        
        }
    }

    /*
     * tag = select_show
     * 
     * <desc>
     * 显示脚本前面[select_new]的选择肢
     * 
     * <sample>
     * [select_show]
     * 
     */

    public class Select_showTag: AbstractTag
    {
        public Select_showTag()
        {
            DefaultParams = new Dictionary<string, string> {
            };

            VitalParams = new List<string>() {
            };
        }

        public override void Excute()
        {
            UIManager.Instance.ShowSelects();
           
        }
    }

    /*
     * tag = select_new
     * 
     * <desc>
     * 创建新的选择肢
     * 
     * <param>
     * @target: 点击后跳转的Scenario标签
     * @text:   标签上显示的文字
     * 
     * <sample>
     * [select_new  target=*select_a1]Nico~[end]
     * 
     */
    public class Select_newTag: AbstractTag
    {
        public Select_newTag()
        {
            DefaultParams = new Dictionary<string, string> {
                {"target", ""},
                {"text",   ""}
            };

            VitalParams = new List<string>() {
                "target"
            };
        }

        public override void Excute()
        {
          
        }

    }

    /*
     * tag = block
     * 
     * <desc>
     * 块@name开始,需和block_end成对使用
     * 
     * <param>
     * @name: 块的名称
     * 
     * <sample>
     * [block name=block1]
     * do something
     * [block_end]
     */

    public class BlockTag : AbstractTag
    {
        public BlockTag()
        {
            DefaultParams = new Dictionary<string, string> {
                {"name", ""},
            };

            VitalParams = new List<string>() {
                "name"
            };
        }
        public override void Excute()
        {
            Debug.Log("In Block: " + Params["name"]);
        }
    }
    public class Block_endTag : AbstractTag
    {
        public Block_endTag()
        {
            DefaultParams = new Dictionary<string, string> {
            };

            VitalParams = new List<string>() {
            };
        }
        public override void Excute()
        {
            Debug.Log("Out Block");
            
        }
        public override void After()
        {
            Engine.BlockStack.Pop();
            base.After();
        }
    }
    /*
     * tag = call
     * 
     * <desc>
     * 执行某个block
     * 
     * <param>
     * 
     * <sample>
     * [call name=nextChapter]
     * 
     W*/
    public class CallTag : AbstractTag
    {
        public CallTag()
        {
            DefaultParams = new Dictionary<string, string>
            {
                { "block", ""}
            };

            VitalParams = new List<string>()
            {
                "block"
            };
        }
        public override void Excute()
        {
            Debug.Log("Call Block: " + Params["block"]);
            var block = Engine.CurrentScene.BlockDict[Params["block"]];
            block.Status.Reset();
            Engine.BlockStack.Push(block);
        }
    }

    /*
     * tag = wait
     * 
     * <desc>
     * 暂停指定时间后继续
     * 
     * <param>
     * @time:   需要等待的时间，单位为秒
     * 
     * <sample>
     * [wait time=1000]
     * 
     */
    public class WaitTag: AbstractTag
    {
        public WaitTag()
        {
            DefaultParams = new Dictionary<string, string>() {
                { "time", "0"}
            };

            VitalParams = new List<string>() {
                "time"
            };
        }

        public override void Excute()
        {
            Debug.LogFormat("Wait: {0}ms", Params["time"]);
            Engine.Status.EnableNextCommand = false;
            Engine.StartCoroutine(Util.DelayToInvoke(this.OnFinishAnimation, int.Parse(Params["time"]) / 1000f));
        }

        public override void OnFinishAnimation()
        {
            Debug.Log("Wait Finish!");
            Engine.Status.EnableNextCommand = true;
        }
    }

    /// <summary>  
    /// tag = end 
    /// </summary>
    /// 
    /// <desc> 
    /// 本节结束，关闭所有对话框即立绘
    /// </desc>
    /// 
    /// <sample>
    /// [end]
    /// </sample>
    

    public class EndTag: AbstractTag
    {
        public EndTag()
        {
            DefaultParams = new Dictionary<string, string>() {
            };

            VitalParams = new List<string>() {
            };
        }

        public override void Excute()
        {
            Debug.Log("End");
            //TextBoxesManager.Instance.CloseAll();
            //foreach (var obj in ImageManager.Instance.ObjectInScene)
            //{
            //    obj.Value.Go.active = false;
            //}
            SceneManager.Instance.Clear();
            SceneManager.Instance.Root.SetActive(false);
            
        }
    }

    /// <summary>  
    /// tag = start 
    /// </summary>
    /// 
    /// <desc> 
    /// 本节开始，显示所有对话框
    /// </desc>
    /// 
    /// <sample>
    /// [end]
    /// </sample>


    public class StartTag: AbstractTag
    {
        public StartTag()
        {
            DefaultParams = new Dictionary<string, string>()
            {
            };

            VitalParams = new List<string>()
            {
            };
        }

        public override void Excute()
        {
            Debug.Log("Start");
            //TextBoxesManager.Instance.CloseAll();
            //foreach (var obj in ImageManager.Instance.ObjectInScene)
            //{
            //    obj.Value.Go.active = false;
            //}
            SceneManager.Instance.Root.SetActive(true);
        }
    }
}
