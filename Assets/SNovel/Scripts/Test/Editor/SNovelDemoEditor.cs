using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace SNovel.Test
{
    [CustomEditor(typeof(SNovelDemo))]
    public class SNovelDemoEditor : UnityEditor.Editor
    {
        private SNovelDemo demo
        {
            get { return target as SNovelDemo; }
        }

        public override void OnInspectorGUI()
        {
            demo.RunTestCases = EditorGUILayout.Toggle("Run TestCases", demo.RunTestCases);
            if (demo.RunTestCases)
            {
                demo.SelectedTestCase = (SNovelDemo.TestCase)EditorGUILayout.EnumPopup("TestCase: ", demo.SelectedTestCase);
            }
            else
            {
                demo.FileName = EditorGUILayout.TextField("FileName", demo.FileName);
            }
        }
    }
}