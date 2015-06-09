﻿using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;


namespace Sov.AVGPart
{
    /*
     * tag = scenario
     * 
     * <desc>
     *  记录以下内容为一个新片段，供跳转等实现
     *  
     * <example>
     * *Demonstration(English)/START/1_Dialog
     * 
     */
    public class ScenarioTag: AbstractTag
    {
        public ScenarioTag()
        {
            _vitalParams = new List<string>() {
                "scenario"
            };

            _defaultParamSet = new Dictionary<string, string>() {
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
             
            base.Excute();
        }
    }

    /*
     * tag = s
     * 
     * <desc>
     * 脚本运行到此处时停止
     * 
     * <example>
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
            _vitalParams = new List<string>();
            _defaultParamSet = new Dictionary<string, string>();

        }

        public override void Excute()
        {
            base.Excute();

            Engine.Status.EnableClickContinue   = false;
            Engine.Status.EnableNextCommand     = false;        
        }
    }

    /*
     * tag = select_new
     * 
     * <desc>
     * 显示脚本前面[select_new]的选择肢
     * 
     * <example>
     * [select_show]
     * 
     */

    public class Select_showTag: AbstractTag
    {
        public Select_showTag()
        {
            _defaultParamSet = new Dictionary<string, string> {
            };

            _vitalParams = new List<string>() {
            };
        }

        public override void Excute()
        {
            Instances.Instance.UIManager.ShowSelects();
            base.Excute();
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
     * <example>
     * [select_new  target=*select_a1]Nico~[end]
     * 
     */
    public class Select_newTag: AbstractTag
    {
        public Select_newTag()
        {
            _defaultParamSet = new Dictionary<string, string> {
                {"target", ""},
                {"text",   ""}
            };

            _vitalParams = new List<string>() {
                "target"
            };
        }

        public override void Excute()
        {
            string target = Params["target"];
            Instances.Instance.UIManager.AddSelect("button", Params["text"]
                      , () =>
                      {
                          Engine.JumpToScenario(target);
                          Instances.Instance.UIManager.ClearSelects();
                      }
                                                  );
            base.Excute();
        }

    }

    /*
     * tag = jump
     * 
     * <desc>
     * 跳转到Param["target"]的Scenario处
     * 
     * <param>
     * @target: Target scenario to jump
     * 
     * <example>
     * *select_niko
     * [jump target=*select_niko]
     * 
     */

    public class JumpTag : AbstractTag
    {
        public JumpTag()
        {
            _defaultParamSet = new Dictionary<string, string> {
                {"target", ""},
            };

            _vitalParams = new List<string>() {
                "target"
            };
        }
        public override void Excute()
        {
            base.Excute();
        }
        public override void After()
        {
            Engine.JumpToScenario(Params["target"]);
            //Do Not CurrentLine + 1
            //base.After();
        }
    }
}