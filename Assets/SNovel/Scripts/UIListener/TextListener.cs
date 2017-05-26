using UnityEngine;
using UnityEngine.UI;
using SNovel.MessageNotificationCenter;
using System.Collections;
using DG.Tweening;
using System;
namespace SNovel
{
    /* 
     * TextListener:
     * Add it on the Text GameObject
     * Notificate TextBox to display new text
     */

    class TextListener : MessageListener
    {
        public TextListener(string name, Action<Message> messageCallBack)
            : base(name, messageCallBack)
        {

        }
    }
}