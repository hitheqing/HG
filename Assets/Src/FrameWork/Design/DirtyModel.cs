using System;

namespace HG
{
    /// <summary>
    /// 脏数据模式。
    /// 避免重复的没必要的调用。通常用于update等频繁函数。
    /// 用一个变量标记是否变脏。如果变脏就执行回调一次。直到下次再被标记为脏数据的时候才执行回调。
    /// 灵感来自于ugui的 VertsDirty MaterialDirty等。抽象出脏数据模式。
    /// </summary>
    public class DirtyModel
    {
        private bool _dirty;

        private readonly Action _onDirtyCallBack;

        public DirtyModel(Action onDirtyCallBack, bool initDirty)
        {
            _onDirtyCallBack = onDirtyCallBack;
            _dirty           = initDirty;
        }

        public bool IsDirty => _dirty;

        /// <summary>
        /// 标记阶段仅仅只标记
        /// </summary>
        public void SetDirty()
        {
            _dirty = true;
        }

        /// <summary>
        /// 轮询
        /// </summary>
        public void Execute()
        {
            if (_dirty)
            {
                _onDirtyCallBack?.Invoke();
                _dirty = false;
            }
        }
    }
    
    /// <summary>
    /// 基于比较相等方式的脏数据模式
    /// </summary>
    /// <typeparam name="T">可被比较元素</typeparam>
    public class DirtyModelByEqual<T> where T: IEquatable<T>
    {
        private T _dirty;

        private readonly Action _onDirtyCallBack;
        private Func<T, T, bool> _equalFunc;

        public DirtyModelByEqual(Action onDirtyCallBack, T initDirty,Func<T,T,bool> equalFunc = null)
        {
            _onDirtyCallBack = onDirtyCallBack;
            _dirty           = initDirty;
            _equalFunc = equalFunc;
        }

        public T IsDirty => _dirty;

        /// <summary>
        /// 标记阶段仅仅只标记
        /// </summary>
        public void SetDirty(T value)
        {
            _dirty = value;
        }

        /// <summary>
        /// 轮询
        /// </summary>
        public void Execute(T newValue)
        {
            if (_equalFunc != null)
            {
                if (!_equalFunc(newValue,_dirty))
                {
                    _onDirtyCallBack?.Invoke();
                    _dirty = newValue;
                }
            }
            else
            {
                if (!_dirty.Equals(newValue))
                {
                    _onDirtyCallBack?.Invoke();
                    _dirty = newValue;
                }
            }
            
        }
    }
}