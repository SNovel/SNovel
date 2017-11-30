using System.Collections.Generic;
using UnityEngine;

namespace SNovel.Test
{
    public class SNovelDemo : MonoBehaviour
    {
        public bool RunTestCases;
        public TestCase SelectedTestCase;

        public enum TestCase
        {
            Text,
            //Image,
            //Flow,
            Block,
            //Actor,
            //Button,
            遮罩,
            Lua
        }
        public static Dictionary<TestCase, string> TestDict = new Dictionary<TestCase, string>
        {
            {TestCase.Block, "block"},
            {TestCase.Text, "text"  },
            {TestCase.遮罩, "遮罩" },
            {TestCase.Lua, "lua" }
        };
        [HideInInspector]
        public const string TestPathPrefix = "TestCases/";
        public string FileName;
        void Awake()
        {

        }

        // Use this for initialization
        void Start()
        {

            string file;
            if (RunTestCases)
            {
                file = TestPathPrefix + TestDict[SelectedTestCase];
            }
            else
            {
                file = FileName;
            }
            var s = new Scene(file);
            s.LoadScript();
            ScriptEngine.Instance.Run(s);
        }
        
        // Update is called once per frame
        void Update()
        {

        }
    }
}