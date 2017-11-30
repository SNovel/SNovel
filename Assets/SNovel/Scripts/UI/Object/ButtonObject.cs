using UnityEngine;
using UnityEngine.UI;

namespace SNovel
{
    /*
     * ButtonObject
     * 
     * Use this to create Button UI elements in scene
     */
    [RequireComponent(typeof(Button))]
    class ButtonObject: AbstractObject
    {
        public string Text
        {
            set
            {
                UIText.text = value;
            }
            get
            {
                return UIText.text;
            }
        }

        public Text UIText;

        public Button UIBtn;

        public Image UIImage
        {
            get
            {
                return GetComponent<Image>();
            }
        }
    }
}
