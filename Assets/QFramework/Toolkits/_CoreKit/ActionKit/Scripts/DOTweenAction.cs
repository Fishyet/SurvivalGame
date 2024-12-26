/****************************************************************************
 * Copyright (c) 2016 - 2023 liangxiegame UNDER MIT License
 *
 * https://qframework.cn
 * https://github.com/liangxiegame/QFramework
 * https://gitee.com/liangxiegame/QFramework
 ****************************************************************************/

using System;
using DG.Tweening;

namespace QFramework
{
    public class DOTweenAction : IAction
    {
        private DOTweenAction()
        {
        }
        
        private static SimpleObjectPool<DOTweenAction> mPool =
            new SimpleObjectPool<DOTweenAction>(() => new DOTweenAction(), null, 10);
        
        private Func<Tween> mTweenGetter;
        
        
        public static DOTweenAction Allocate(Func<Tween> tweenGetter)
        {
            var doTweenAction = mPool.Allocate();
            doTweenAction.ActionID = ActionKit.ID_GENERATOR++;
            doTweenAction.Deinited = false;
            doTweenAction.Reset();
            doTweenAction.mTweenGetter = tweenGetter;
            return doTweenAction;
        }
        
        private Tween mExecutingTween;
        
        public void OnStart()
        {
            mExecutingTween = mTweenGetter();
            mExecutingTween.Restart();
            
            var tween = mTweenGetter();
            tween.Restart();
        }
        
        public void OnExecute(float dt)
        {
            if (mExecutingTween != null && !mExecutingTween.IsPlaying())
            {
                this.Finish();
            }
        }
        
        public void OnFinish()
        {
            mExecutingTween = null;
        }
        
        public bool Paused { get; set; }
        
        public void Deinit()
        {
            if (!Deinited)
            {
                Deinited = true;
                if (mExecutingTween != null)
                {
                    if (mExecutingTween.IsPlaying())
                    {
                        mExecutingTween.Kill();
                    }
                }
                
                mExecutingTween = null;
                mPool.Recycle(this);
            }
        }
        
        public void Reset()
        {
            Status = ActionStatus.NotStart;
            Paused = false;
            mExecutingTween = null;
        }
        
        public bool Deinited { get; set; }
        public ulong ActionID { get; set; }
        public ActionStatus Status { get; set; }
    }
    
    public static class ActionKitDOTweenExtension
    {
        public static IAction ToAction(this Tween self)
        {
            self.Pause();
            return DOTweenAction.Allocate(() => self);
        }
        
        public static ISequence DOTween(this ISequence self, Func<Tween> tweenGetter)
        {
            return self.Append(DOTweenAction.Allocate(tweenGetter));
        }
    }
}