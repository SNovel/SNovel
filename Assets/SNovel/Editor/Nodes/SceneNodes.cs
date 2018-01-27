using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NodeEditorFramework;
using SNovel.NodeEditor;
using UnityEngine;

namespace SNovel.Editor
{
    [Node(false, "SNovel/Show Node",  typeof(SNovelNodeCanvas) )]
    public class ShowNode:BaseTagNode
    {
        private const string Id = "ShowNode";

        public override string GetID { get { return Id; } }

        protected override void OnCreate()
        {
            Tag = new ShowTag();
            Tag.Init();
        }
        public override string Title { get { return "Show"; } }

        public override Type GetObjectType { get { return typeof(ShowNode); } }
    }

    [Node(false, "SNovel/Hide Node", typeof(SNovelNodeCanvas))]
    public class HideNode : BaseTagNode
    {
        private const string Id = "HideNode";

        public override string GetID { get { return Id; } }

        protected override void OnCreate()
        {
            Tag = new HideTag();
            Tag.Init();
        }
        public override string Title { get { return "Hide"; } }

        public override Type GetObjectType { get { return typeof(HideNode); } }
    }
}
