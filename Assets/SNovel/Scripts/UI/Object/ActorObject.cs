using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace SNovel
{
    //TODO
    [RequireComponent(typeof(Image))]
    public class ActorObject: ImageObject
    {
        public List<string> ExpressionNames;
        public List<Sprite> ExpressionImages;

        public int ExpressionCount
        {
            get { return ExpressionNames.Count; }
        }

        public int GetExpression(string name)
        {
            int idx = -1;
            for (int i = 0; i < ExpressionNames.Count; ++i)
            {
                if (ExpressionNames[i] == name)
                {
                    idx = i;
                    break;
                }
            }
            return idx;
        }

        public void SetExpressionWithoutAnimation(string name)
        {
            var idx = GetExpression(name);
            if (idx >= 0)
            {
                CurImage.sprite = ExpressionImages[idx];
            }
            else
            {
                Debug.LogErrorFormat("Can not set expression!Actor:{0} Expression:{1}", gameObject.name, name);
            }
        }

        public void SetExpressionWithAnimation(string name, float duration, Action onFinished)
        {
            var idx = GetExpression(name);
            if (idx >= 0)
            {
                var sprite = ExpressionImages[idx];

                Sequence s = DOTween.Sequence();
                s.Append(CurImage.DOFade(0f, duration/2));            
                s.AppendCallback(() => CurImage.sprite = sprite);
                s.Append(CurImage.DOFade(1f, duration / 2));
                s.AppendCallback(new TweenCallback(onFinished));
                s.Play();
            }
            else
            {
                Debug.LogErrorFormat("Can not set expression!Actor:{0} Expression:{1}", gameObject.name, name);
                onFinished();
            }
        }


        public void SetScale(float dt)
        {
            Trans.localScale = new Vector3(dt, dt, 1);
        }

        public void MoveTo(Vector2 to, float dt)
        {
            Tween t = Trans.DOAnchorPos(to, dt);
            if (OnAnimationFinish != null)
                t.OnComplete(new TweenCallback(OnAnimationFinish));
        }

        public void ScaleTo(float range, float dt)
        {
            Tween t = Trans.DOScale(range, dt);
            if (OnAnimationFinish != null)
                t.OnComplete(new TweenCallback(OnAnimationFinish));
        }
    }
}
