using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SNovel
{
    class KAGPhraser
    {
        
        public KAGPhraser()
        {
            _tokenizer = new Tokenizer();
            _tags = new List<AbstractTag>();
           // _tagManager = new KAGTagManager();
        }


        int _currentPhraseLineNo = 0;

        List<AbstractTag> _tags;
        
        string _scriptStream;
        
       // List<KAGWords> _phrasedLines;
        
        Tokenizer _tokenizer;
       // Scene _scenario;
        public List<AbstractTag> Phrase(string script)
        {
            //reset
            _currentPhraseLineNo = 0;
            _tags.Clear();

            _scriptStream = script;

         //   _scenario = s;

            Phrase();
            return _tags;
        }


        #region PrivateMethod
        void Phrase()
        {

            string[] list;
            list = _scriptStream.Split(new Char[] { '\n' }, StringSplitOptions.None);

            for (int i = 0; i < list.Length; ++i)
            {
                string line = list[i];
                string str = line.Trim();
                if (str == "")
                {
                    _currentPhraseLineNo++;
                    continue;
                }
                
                KAGWords l = _tokenizer.GetToken(str);
                if (l == null) continue;
                if (l[0].Name == "script" && l[0].Value == "begin")
                {
                    i = PhraseScript(list, i + 1);
                    _currentPhraseLineNo = i;
                    continue;
                    
                }
                else
                {
                    PhraseALine(l);

                }
                _currentPhraseLineNo++;
            }
        }

        void PhraseALine(KAGWords line)
        {
            KAGWord op = line[0];
            if (op.WordType == KAGWord.Type.TEXT)
            {
                PhraseText(line);
                
            }
            else if(op.WordType == KAGWord.Type.NAME)
            {
                PhraseName(line);
            }
            else
            {
                TagInfo tagInfo = new TagInfo(op.Value.ToLower());
                foreach (KAGWord param in line)
                {
                    if (op != param)
                    {
                        tagInfo.Params[param.Name] = param.Value;
                    }
                }
                CreateTag(tagInfo);
            }
        }

        /*
        void PhraseScenario(KAGWords line)
        {

            OpCommand command = new OpCommand(Opcode.SCENARIO);

            foreach (KAGWord word in line)
            {
                string name = word.Name;
                if (name == "scenario")
                {
                    command.Params["scenario"] = word.Value;
                }
            }

            ScriptEngine.Instance.AddCommand(command);
        }

        void PhraseScenario(KAGWords line)
        {
            TagInfo tagInfo = new TagInfo("scenario");
            foreach (KAGWord param in line)
            {
                tagInfo.Params[param.Name] = param.Value;
            }
            ScriptEngine.Instance.AddCommand(TagFactory.Create(tagInfo));
        }*/
        void PhraseText(KAGWords line)
        {
            TagInfo tagInfo = new TagInfo("print");
            
            foreach (KAGWord word in line)
            {
                string name = word.Name;
                if (name == "text")
                {
                    tagInfo.Params["text"] = word.Value;
                    CreateTag(tagInfo);
                }
                else if (name == "op")
                {
                    TagInfo tagInfo1 = new TagInfo(word.Value.ToLower());
                    CreateTag(tagInfo1);
                }
            }
        }
        
        void PhraseName(KAGWords line)
        {
            TagInfo tagInfo = new TagInfo("setname");

            foreach (KAGWord word in line)
            {
                string name = word.Name;
                if (name == "text")
                {
                    tagInfo.Params["text"] = word.Value;
                    CreateTag(tagInfo);
                }
            }
        }

        int PhraseScript(string[] lines, int beginLine)
        {
            TagInfo tagInfo = new TagInfo("luascript");

            StringBuilder sb = new StringBuilder();
            while (beginLine < lines.Length)
            {
                if (lines[beginLine].StartsWith("$end"))
                {
                    break;
                }
                else
                {
                    sb.AppendLine(lines[beginLine]);
                }
                beginLine++;
            }
            Debug.AssertFormat(beginLine <= lines.Length, "SNovel Phraser: can not find $end, $script line:{0}",
                beginLine - 1
            );
            tagInfo.Params["script"] = sb.ToString();
            CreateTag(tagInfo);
            return beginLine;
        }
        void CreateTag(TagInfo tagInfo)
        {
            AbstractTag tag = TagFactory.Create(tagInfo, _currentPhraseLineNo);
            if (tag != null)
            {
                // ScriptEngine.Instance.AddCommand(tag);
                //_uploadTags.Add(tag);
                //  _scenario.AddCommand(tag);
                _tags.Add(tag);
            }
            else
                Debug.LogFormat("Tag:{0} is not implemented!", tagInfo.TagName);
        }
        #endregion

        
     //   KAGTagManager _tagManager;
    }
}
