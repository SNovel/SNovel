using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;
using UnityEditor;
using UnityEngine;
namespace SNovel.NodeEditor
{
    [Node(true, "SNovel/BaseTag Node", new Type[] { typeof(SNovelNodeCanvas) })]

    public abstract class BaseTagNode:Node
    {
        public override Vector2 MinSize { get { return new Vector2(100, 60); } }

        public override bool AllowRecursion { get { return false; } }
        public abstract Type GetObjectType { get; }
        public override bool AutoLayout { get { return true; } }  //resizable renamed to autolayout?

        protected AbstractTag Tag;
        public virtual BaseTagNode PassAhead(int inputValue)
        {
            return this;
        }

        ///check if the first connection of the specified port points to something
        protected bool IsAvailable(ConnectionPort port)
        {
            return port != null
                   && port.connections != null && port.connections.Count > 0
                   && port.connections[0].body != null
                   && port.connections[0].body != default(Node);
        }
        ///return the dialog node pointed to by the first connection in the specified port
        protected BaseTagNode GetTargetNode(ConnectionPort port)
        {
            if (IsAvailable(port))
                return port.connections[0].body as BaseTagNode;
            return null;
        }
        //Previous Node Connections
        [ConnectionKnob("Previous", Direction.In, "Previous", NodeSide.Top)]
        public ConnectionKnob Previous;


        //Next Node to go to
        [ConnectionKnob("From Next", Direction.Out, "Next", NodeSide.Bottom)]
        public ConnectionKnob Next;

        public override void NodeGUI()
        {
          
            GUILayout.BeginVertical();

            foreach (var param in Tag.Params.Keys.ToList())
            {
                Tag.Params[param] =  RTEditorGUI.TextField(new GUIContent(param),  Tag.Params[param],null, GUILayout.ExpandWidth(true));
                 //EditorGUILayout.TextField(param, );
            }
            GUILayout.EndVertical();
            Previous.SetPosition();
            Next.SetPosition();
        }
        public virtual bool IsBackAvailable()
        {
            return IsAvailable(Previous);
        }

        public virtual bool IsNextAvailable()
        {
            return IsAvailable(Next);
        }

        public virtual BaseTagNode Input(int inputValue)
        {
            switch (inputValue)
            {
                case (int)EDialogInputValue.Next:
                    if (IsNextAvailable())
                        return GetTargetNode(Next);
                    break;
                case (int)EDialogInputValue.Back:
                    if (IsBackAvailable())
                        return GetTargetNode(Previous);
                    break;
            }
            return null;
        }
    }

    public class PreviousTagType : ConnectionKnobStyle //: IConnectionTypeDeclaration
    {
        public override string Identifier { get { return "Previous"; } }
        public override Color Color { get { return Color.yellow; } }
    }

    public class NextTagType : ConnectionKnobStyle // : IConnectionTypeDeclaration
    {
        public override string Identifier { get { return "Next"; } }
        public override Color Color { get { return Color.cyan; } }
    }


}
