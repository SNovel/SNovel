using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace SNovel
{
    class ActorObject: AbstractObject
    {
        public bool IsEnterScene
        {
            get;
            set;
        }
        string ActorPrefabPath = Settings.Instance.PREFAB_PATH + "Image";

        Image _image;

        RectTransform _transform;
        public ActorObject(): base()
        { 
           
        }

        public override void Init(ObjectInfo infoo)
        {
            ImageInfo info = (ImageInfo)infoo;
            //load image
            GameObject go = Resources.Load<GameObject>(Settings.Instance.PREFAB_PATH + "Image");
            go = GameObject.Instantiate(go);
            
            //set tag

            go.layer = Settings.Instance.ACTOR_LAYER;

            //set name
            go.name = info.ObjName;

            //add Image
            _image = go.GetComponent<Image>();
            Sprite sp = Resources.Load<Sprite>(info.Path + info.Name);

            if (sp)
            {
                _image.sprite = sp;
                _image.SetNativeSize();
            }
            else
            {
                Debug.LogFormat("Actor: {0} not found", info.Path + info.Name);
            }

            _transform = go.GetComponent<RectTransform>();

            //set local position
            _transform.anchorMin = Vector2.zero;
            _transform.anchorMax = Vector2.zero;

            //set parent
            _transform.SetParent(Settings.ActorRoot, true);

            //set position and scale
            _transform.anchoredPosition = info.Position;

            _transform.localScale = new Vector3(info.Scale, info.Scale, info.Scale);
             
            
            go.SetActive(false);
            this.Go = go;

            IsEnterScene = false;
        }

        public override void FadeIn(float fadetime)
        {
            /*
            if (fadetime == 0)
            {
                Go.SetActive(true);
                _image.color = new Color(255, 255, 255, 255);
            }
            else
            {
                Go.SetActive(true);
                */
            //_image.color = new Color(255, 255, 255, 0);
            Tween t = _image.DOFade(1, fadetime);
            if (OnAnimationFinish != null)
                t.OnComplete(new TweenCallback(OnAnimationFinish));
            //}
        }

        public override void FadeOut(float fadetime)
        {
/*
            if (fadetime == 0)
            {
                Go.SetActive(false);
                _image.color = new Color(255, 255, 255, 0);
            }
            else
            {
                Go.SetActive(false);
                */
           // _image.color = new Color(255, 255, 255, 255);
            Tween t = _image.DOFade(0, fadetime);
            if (OnAnimationFinish != null)
                t.OnComplete(new TweenCallback(OnAnimationFinish));
            //}
        }

        public override void SetPosition2D(Vector2 p)
        {
            Go.GetComponent<RectTransform>().anchoredPosition = p;
        }

        public void SetScale(float dt)
        {
            _transform.localScale = new Vector3(dt, dt, 1);
        }

        public void MoveTo(Vector2 to, float dt)
        {
            Tween t = _transform.DOAnchorPos(to, dt);
            if (OnAnimationFinish != null)
                t.OnComplete(new TweenCallback(OnAnimationFinish));
        }

        public void ScaleTo(float range, float dt)
        {
            Tween t = _transform.DOScale(range, dt);
            if (OnAnimationFinish != null)
                t.OnComplete(new TweenCallback(OnAnimationFinish));
        }
    }
}
