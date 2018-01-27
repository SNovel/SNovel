using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NodeEditorFramework;
using SNovel.NodeEditor;
using UnityEditor;
using UnityEngine;

namespace SNovel.Editor
{
    [Node(false, "SNovel/Print Node", typeof(SNovelNodeCanvas))]
    public class PrintNode : BaseTagNode
    {
        private const string Id = "PrintNode";

        public override string GetID { get { return Id; } }

        protected override void OnCreate()
        {
            Tag = new PrintTag();
            Tag.Init();
        }
        public override string Title { get { return "Dialog"; } }

        public override Type GetObjectType { get { return typeof(PrintNode); } }
        public override void NodeGUI()
        {
            GUILayout.BeginVertical();
            EditorStyles.textField.wordWrap = true;
            Tag.Params["text"] = EditorGUILayout.TextArea(Tag.Params["text"], GUILayout.ExpandHeight(true));
            EditorStyles.textField.wordWrap = false;
            GUILayout.EndVertical();

            Previous.SetPosition();
            Next.SetPosition();
        }
    }

    [Node(false, "SNovel/CreateTextBox Node", typeof(SNovelNodeCanvas))]
    public class CreateTextBoxNode : BaseTagNode
    {
        private const string Id = "CreateTextBoxNode";

        public override string GetID { get { return Id; } }

        protected override void OnCreate()
        {
            Tag = new Create_textboxTag();
            Tag.Init();
        }
        public override string Title { get { return "CreateTextBox"; } }
        public override Type GetObjectType { get { return typeof(CreateTextBoxNode); } }
    }

    [Node(false, "SNovel/Current Node", typeof(SNovelNodeCanvas))]
    public class CurrentNode : BaseTagNode
    {
        private const string Id = "Current";

        public override string GetID { get { return Id; } }

        protected override void OnCreate()
        {
            Tag = new CurrentTag();
            Tag.Init();
        }
        public override string Title { get { return "Current"; } }
        public override Type GetObjectType { get { return typeof(CurrentNode); } }
    }

    [Node(false, "SNovel/CM Node", typeof(SNovelNodeCanvas))]
    public class CMNode : BaseTagNode
    {
        private const string Id = "CM";

        public override string GetID { get { return Id; } }

        protected override void OnCreate()
        {
            Tag = new CmTag();
            Tag.Init();
        }
        public override string Title { get { return "Clear Text"; } }
        public override Type GetObjectType { get { return typeof(CMNode); } }

    }
}
