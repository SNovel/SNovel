using System;
using System.IO;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using System.Collections;
using System.Linq;

namespace SNovel
{
    public class SceneStatus
    {
        public int CurrentLine
        {
            get;
            set;
        }
        /*
        public string CurrentScenario
        {
            get;
            private set;
        }

        public bool SkipThisTag
        {
            get;
            internal set;
        }*/

        //当需要点击继续的时候为true，其余为false
        public bool EnableClickContinue
        {
            get;
            internal set;
        }

        //停止，收到消息为true时再继续
        public bool EnableNextCommand
        {
            get;
            internal set;
        }

        public bool IsFinish
        {
            get;
            set;
        }

        public SceneStatus()
        {
            Reset();
        }

        internal void Reset()
        {
            //Status reset
            EnableClickContinue = true;
            EnableNextCommand = true;
           // SkipThisTag = false;
            IsFinish = false;
            CurrentLine = 0;
        }


    }
    public class Block
    {
        //开始的行号 对应脚本中
        public int StartLine;
        public int EndLine;
        public SceneStatus Status = new SceneStatus();

        //树
        public Dictionary<string, Block> BlockDict = new Dictionary<string, Block>();

        //块的名字 对应Lable的name
        public string Name;
        public List<AbstractTag> Tags = new List<AbstractTag>();
    }
    /*
     * 一个场景类
     * 一个游戏中可以有多个场景但是运行中的只有一个
     * 压栈
     */

    public class Scene: Block 
    {
        public string ScriptFilePath
        {
            get;
            set;
        }

        public string ScriptContent;

        public Dictionary<string, int> ScenarioDict
        {
            get;
            private set;
        }

        // public Action PhraseFinishCalback;

        public bool IsPhraseFinish
        {
            get;
            set;
        }
       // ScriptEngine _engine;


        public Scene(string scriptPath) :
            this() //init
        {
            ScriptFilePath = scriptPath;
            Name = scriptPath;
        }

        public void PhraseFinish()
        {
            Debug.LogFormat("Phrase Before IsPhraseFinish={0}", IsPhraseFinish);
            IsPhraseFinish = true;
            Debug.LogFormat("Phrase After IsPhraseFinish={0}", IsPhraseFinish);
        }

        //some init
        Scene()
        {
            Tags = new List<AbstractTag>();
            Status = new SceneStatus();
            ScriptFilePath = "";
            ScriptContent = "";
            Status.CurrentLine = 0;

            ScenarioDict = new Dictionary<string, int>();
           // _engine = ScriptEngine.Instance;
            IsPhraseFinish = false;
            OnFinish = () => { };
        }

        public void Reset()
        {
            Status.CurrentLine = 0;
            Status.Reset();
        }

        public void LoadScript()
        {
            string path = Settings.Instance.SCENARIO_SCRIPT_PATH + ScriptFilePath;
            /*
#if UNITY_STANDALONE_WIN          
            if (!File.Exists(path))
            {
                Debug.LogFormat("cannot find script file: {0}!", path);
            }else
                Debug.LogFormat("load script file: {0}!", path);
            StreamReader sr = File.OpenText(path);
            ScriptContent = sr.ReadToEnd();
            sr.Close();
#endif
*/
            //ScriptFilePath = ScriptFilePath.Substring(0, 3);
            //#if UNITY_ANDROID
            
                var textAsset = Resources.Load<TextAsset>(path);
            ScriptContent = textAsset.text;
            // path = Application.dataPath + path;
            //string url = Application.dataPath  + path;
            /*
            #if UNITY_EDITOR
                        ScriptContent = File.ReadAllText(url);
            #elif UNITY_ANDROID
                        path = "jar:file://" + Application.dataPath + "!/assets" + path;

                        WWW www = new WWW(path);
                        while (!www.isDone) { }

                        ScriptContent = www.text;
                        Debug.Log("Load Script: "+ ScriptContent);
            #endif
            */
            /*
            if (!File.Exists(path))
            {
                Debug.LogFormat("cannot find script file: {0}!", path);
            }
            else
                Debug.LogFormat("load script file: {0}!", path);
            //ScriptContent = t.text;
            StreamReader sr = File.OpenText(path);
            ScriptContent = sr.ReadToEnd();
            sr.Close();

            //#endif
            // ScriptContent= 
            //_phraser.SetScript(str);*/
            //  _engine.Phrase(this);
            Phrase();
        }

        public void LoadScriptAsync()
        {
            string path = Settings.Instance.SCENARIO_SCRIPT_PATH + ScriptFilePath;

#if UNITY_STANDALONE_WIN          
            if (!File.Exists(path))
            {
                Debug.LogFormat("cannot find script file: {0}!", path);
            }else
                Debug.LogFormat("load script file: {0}!", path);
            StreamReader sr = File.OpenText(path);
            ScriptContent = sr.ReadToEnd();
            sr.Close();
#endif

#if UNITY_ANDROID
            TextAsset t = Resources.Load<TextAsset>(path);
            if (t == null)
            {
                Debug.LogFormat("cannot find script file: {0}!", path);
            }
            else
                Debug.LogFormat("load script file: {0}!", path);
            ScriptContent = t.text;
#endif
            //_phraser.SetScript(str);
            Thread thread = new Thread(
                () =>
                {
                    Phrase();
                    PhraseFinish();
                });
            thread.Start();
        }
        private Stack<AbstractTag> _tagStack = new Stack<AbstractTag>();
        
        public void Phrase()
        {
            var phraser = new KAGPhraser();
            var tags = phraser.Phrase(ScriptContent);
            var curBlock = this as Block;
            curBlock.StartLine = 0;
            PhraseBlock(tags, curBlock, 0, tags.Count - 1);
        }

        void PhraseBlock(List<AbstractTag> tags, Block curBlock, int startLine, int endLine)
        {
            for (int i = startLine; i <= endLine; ++i)
            {
                var tag = tags[i];
                //tag.LineNo = _opTags.Count;
                tag.Engine = ScriptEngine.Instance;

                if (tag.Name == "block")
                {
                    //find block_end
                    int endBlockIdx = -1;
                    for (int findEndBlockIdx = i + 1; findEndBlockIdx <= endLine; ++findEndBlockIdx)
                    {
                        if (tags[findEndBlockIdx].Name == "block_end")
                        {
                            endBlockIdx = findEndBlockIdx;
                            break;
                        }
                    }
                    if (endBlockIdx == -1)
                    {
                        Debug.LogErrorFormat("do not find block_end in script:{0}, tag:{1} ", Name, tag.ToString());
                        return;
                    }

                    var block = new Block();
                    block.Name = tag.Params["name"];
                    block.StartLine = i;
                    block.Tags.Add(tag);
                    curBlock.BlockDict.Add(block.Name, block);
                    PhraseBlock(tags, block, i+1, endBlockIdx);
                }
                else
                {
                    curBlock.Tags.Add(tag);
                }
            }
        }
        public Block GetBlock(string name)
        {
            if(BlockDict.ContainsKey(name))
            {
                return BlockDict[name];
            }
            else
            {
                Debug.LogErrorFormat("can not find block:{0} in scene:{1}", name, Name);
            }
            return null;
        }
        public Action OnFinish;
    }
}
