using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SNovel.MessageNotificationCenter;
using UniLua;
//using Event = SNovel.EventDispatchCenter;
//TODO: DEBUG类
namespace SNovel
{
    public class OpCommand
    {
        public OpCommand(Opcode OpType)
        {
            Op = OpType;
            AdditionalOp = new List<Opcode>();
            Params = new Dictionary<string, string>();
        }

        public Opcode Op;

        //public string TagName

        //Params: contains the param write in the script
        public Dictionary<string, string> Params
        {
            get;
            set;
        }

        public List<Opcode> AdditionalOp
        {
            get;
            set;
        }
        public int LineID
        {
            get;
            set;
        }
    };

    /*
     * 运行场景
     * 
     * 
     */
    public class ScriptEngine: MonoBehaviour
    {
        static ScriptEngine _sharedScriptedEngine = null;
       
        public SceneStatus Status
        {
            get
            {
                return _currentBlock.Status;
            }
        }
        
        /*
       // List<OpCommand> _opLines;

        List<AbstractTag> _opTags
        {
            get
            {
                return _currentScene.Tags;
            }
        }

        //Directory?
        //用于记录场景所指的行号
        Dictionary<string, int> _scenarioDict
        {
            get
            {
                return _currentScene.ScenarioDict;
            }
        }
        */
        int _currentLine
        {
            get
            {
                return BlockStack.Peek().Status.CurrentLine;
            }
            set
            {
                BlockStack.Peek().Status.CurrentLine = value;
            }
        }

        private Block _currentBlock
        {
            get
            {
                return BlockStack.Peek();
            }
        }
        KAGPhraser _phraser;

        //记录正在执行的场景
        public Scene CurrentScene;
        #region Public Method

        public ILuaState LuaState { get; private set; }
       
        public virtual bool Init()
        {
            _phraser = new KAGPhraser();
            LuaState = LuaAPI.NewState();
            LuaState.L_OpenLibs();
            CurrentScene = null;
            return true;
        }
        
        void Awake()
        {
            //注册监听事件
            MessageListener listener = new MessageListener("SCRIPT_CLICK_CONTINUE",
                                                 OnClickContinue);

            MessageDispatcher.Instance.RegisterMessageListener(listener);
        }

        public static ScriptEngine Instance
        {
            get
            {
                if (_sharedScriptedEngine == null)
                {
                    GameObject go = new GameObject("ScriptEngine");
                    Debug.Log("create ScriptEngine");
                    if(go == null)
                    {
                        Debug.LogError("ScriptEngine Create Wrong!");
                    }
                    _sharedScriptedEngine =  go.AddComponent<ScriptEngine>();
                    _sharedScriptedEngine.Init();
                   // _sharedScriptedEngine.gameObject.
                }
                return _sharedScriptedEngine;
            }
        }
        /*                             
        public void AddCommand(OpCommand op)
        {
            op.LineID = GetLastedLineNo();

            _opLines.Add(op);
        }

        public void AddCommand(AbstractTag tag)
        {
            //tag.LineNo = _opTags.Count;
            tag.Engine = this;
            if(tag.Name == "scenario")
            {
                AddScenario(tag);
            }
            else
                _opTags.Add(tag);
        }

        public void AddScenario(string scenarioName)
        {
            if (_scenarioDict.ContainsKey(scenarioName))
            {
                //Do Nothing
                return;
            }
            else
            {
                _scenarioDict.Add(scenarioName, _currentLine);
                Debug.LogFormat("[Add Scenario]{0}:{1}", _currentLine, scenarioName);
                Status.CurrentScenario = scenarioName;
            }
        }

        public void AddScenario(AbstractTag tag)
        {
            string scenarioName = tag.Params["scenario"];

            if (_scenarioDict.ContainsKey(scenarioName))
            {
                Debug.LogFormat("Scenario: {0}Is Already Exist", scenarioName);               
                return;
            }
            else
            {
                _scenarioDict.Add(scenarioName, GetLastedLineNo());
                Debug.LogFormat("[Add Scenario]{0}:{1}", GetLastedLineNo(), scenarioName);
                Status.CurrentScenario = scenarioName;
            }
        }*/
        /*
        public void JumpToScenario(string scenarioName)
        {
            
            if (_scenarioDict.ContainsKey(scenarioName))
            {
                Debug.LogFormat("JumpTo line:{0}:{1}", _scenarioDict[scenarioName], scenarioName);
                int line = _scenarioDict[scenarioName];
                _currentLine = line;
                Status.EnableClickContinue = true;
                Status.EnableNextCommand = true;
            }
            else
            {
                Debug.LogFormat("Do not have scenario:{0}", scenarioName);
            }
        }

        public void InsertCommandBefore(AbstractTag tag)
        {
            _opTags.Insert(_currentLine, tag);
        }
        */
        /*
         * @param string filePath:
         * 脚本文件在Resource下的路径
         */
        /*
        public void LoadScript(string filePath)
        {
            string path = Application.dataPath + Settings.SCENARIO_SCRIPT_PATH + filePath;
            if(!File.Exists(path))
            {
                Debug.LogFormat("cannot find script file: {0}!", path);
            }else
                Debug.LogFormat("load script file: {0}!", path);
            StreamReader sr = File.OpenText(path);
            string str = sr.ReadToEnd();
            sr.Close();

            //_phraser.SetScript(str);
            _phraser.Phrase();
        }*/
        /*
        public void Phrase(Scene scenario)
        {
            _phraser.Phrase(scenario);
        }
        */
        /*
        public void RunScript()
        {
            if (_currentScene == null)
            {
                //Debug.Log()
                return;
            }
            Debug.Log("Run Script!");
            StartCoroutine(OnRun());
        }*/

        public void Run(Scene scene)
        {
            CurrentScene = scene;
            Debug.Log("Run Script!");
            BlockStack.Push(CurrentScene);
            LuaState = LuaAPI.NewState();

            StartCoroutine(OnRun());
        }

        public void NextCommand()
        {
            var curBlock = BlockStack.Peek();

            curBlock.Status.CurrentLine++;
            if (curBlock.Status.CurrentLine >= curBlock.Tags.Count && !curBlock.Status.IsFinish)
            {
                curBlock.Status.IsFinish = true;
                CurrentScene.OnFinish();
            }
           // if (_currentLine < _opTags.Count)
           //     ExcuteCommand();
        }
        /*
        void ExcuteCommand()
        {
            Status.SkipThisTag = false;
            if (_currentLine < _opTags.Count)
            {
                _opTags[_currentLine].Before();
                if (!Status.SkipThisTag)
                {
                    Status.EnableNextCommand = true;
                    _opTags[_currentLine].Excute();
                    _opTags[_currentLine].After();
                }
            }
        }*/

        public Stack<Block> BlockStack = new Stack<Block>(); 
        void ExcuteCommand()
        {
            List<AbstractTag> tags = _currentBlock.Tags;

            if (_currentLine < tags.Count)
            {
                _currentBlock.Tags[_currentLine].Before();
                _currentBlock.Status.EnableNextCommand = true;
                _currentBlock.Tags[_currentLine].Excute();
                _currentBlock.Tags[_currentLine].After();

            }
        }
        #endregion

        #region   Private Method

        void ResetEngine()
        {
          //  _currentLine = 0;
           // _scenarioDict.Clear();

            
        }

        IEnumerator OnRun()
        {
            while (_currentBlock.Status.CurrentLine < _currentBlock.Tags.Count)
            {
                if(_currentBlock.Status.EnableNextCommand)
                    ExcuteCommand();
                yield return new WaitForEndOfFrame();
            }
        }

       
        bool DispatchMessage(Message e)
        {
            return MessageDispatcher.Instance.DispatchMessage(e);
        }
        /*
        int GetLastedLineNo()
        {
            return _opTags.Count;
        }
        */
        //TODO: 实现一个消息通知系统
        public void OnClickContinue(Message pMessage)
        {
            /*
            if (_hideTextBox && _hasTextBoxToHide)
            {
                _hideTextBox = true;
                _hasTextBoxToHide = false;
                Message e = new Message("Message_HIDE_TEXTBOX");
                e.UserData = "TextBox_Name";
                DispatchMessage(e);
            }
            _hasTextBoxToHide = false;
            NextLine();*/
            if (_currentBlock.Status.EnableClickContinue)
            {
                //   _currentLine++;
                _currentBlock.Status.EnableNextCommand = true;
                _currentBlock.Status.EnableClickContinue = false;
            }
            //NextCommand();
        }
        #endregion


    }
}
