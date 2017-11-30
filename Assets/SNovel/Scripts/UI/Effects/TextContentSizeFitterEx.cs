using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SNovel
{
    [RequireComponent(typeof(RectTransform))]
    [RequireComponent(typeof(ContentSizeFitter))]
    [RequireComponent(typeof(Text))]
    public class TextContentSizeFitterEx : MonoBehaviour
    {
        public RectTransform PreferredSizeUI;
        [HideInInspector]
        public ContentSizeFitter Fitter;
        [HideInInspector]
        public Text TextUI;
        public Vector2 PerferredSize
        {
            get { return PreferredSizeUI.sizeDelta; }
        }
        [HideInInspector]
        public RectTransform RectTr;
        // Use this for initialization
        void Start()
        {
            RectTr = GetComponent<RectTransform>();
            Fitter = GetComponent<ContentSizeFitter>();
            TextUI = GetComponent<Text>();
            _beginPos = RectTr.anchoredPosition;
            _beginSize = RectTr.sizeDelta;
        }

        private float _beginHeight;
        private Vector2 _beginSize;
        private Vector3 _oldPos;
        private Vector2 _beginPos;
        // Update is called once per frame
        void Update()
        {
            if (TextUI.preferredHeight > _beginSize.y)
            {
                Fitter.enabled = true;
            }
            else
            {
                Fitter.enabled = false;
                RectTr.sizeDelta = _beginSize;
                RectTr.anchoredPosition = _beginPos;
            }
        }
    }
}
