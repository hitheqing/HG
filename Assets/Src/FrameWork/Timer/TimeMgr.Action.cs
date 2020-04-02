﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HG
{
    /*
     *    一般不会用到很多个  不同间隔的定时器，所以协程不移除也没关系
     *     针对特定的业务  可再封一层。比如固定1s，1/3的定时器
     * /
     */
    public partial class TimeMgr:SingletonMono<TimeMgr>
    {
        public static WaitForSeconds WaitOnThird = new WaitForSeconds(0.33f);
        public static WaitForSeconds WaitFor1 = new WaitForSeconds(1);
        public static WaitForEndOfFrame WaitForFrame = new WaitForEndOfFrame();

        Dictionary<float,RepeataSet> repeatdic = new Dictionary<float, RepeataSet>();
        
        private IEnumerator _endless(RepeataSet obj)
        {
            while (true)
            {
                yield return obj.Interval;
                obj.Act?.Invoke();
            }
        }

        public void DelayExecute(float delay, Action func)
        {
            StartCoroutine(_delay(delay, func));
        }

        private IEnumerator _delay(float delay, Action func)
        {
            yield return new WaitForSeconds(delay);
            func?.Invoke();
        }

        public void NextFrameExecute(Action func)
        {
            StartCoroutine(_nextFrame(func));
        }

        private IEnumerator _nextFrame(Action func)
        {
            yield return WaitForFrame;
            func?.Invoke();
        }

        public void AddRepeatExecute(float interval, Action func)
        {
            if (!repeatdic.ContainsKey(interval))
            {
                repeatdic[interval] = new RepeataSet() {Interval = new WaitForSeconds(interval)};
                repeatdic[interval].Act += func;

                StartCoroutine(_endless(repeatdic[interval]));
                return;
            }
            
            repeatdic[interval].Act += func;
        }
        
        public void RemoveRepeatExecute(float interval, Action func)
        {
            if (!repeatdic.ContainsKey(interval))
            {
                Loger.Error("not find key");
            }
            
            repeatdic[interval].Act -= func;
        }

        
        

        class RepeataSet
        {
            public WaitForSeconds Interval;
            public Action Act;
        }
    }
}