using System;

namespace HG
{
    /// <summary>
    /// 异步模式
    /// 异步结果成功或失败的包装，适用于执行某个动作时，预先定义好成功和失败的后续行为。
    /// 在异步结果返回后调用Execute。
    /// 灵感来自于上阵下阵等有可能操作失败的请求。抽象出异步模式
    /// </summary>
    public class AsyncModel
    {
        private readonly Action _onSuccess;
        
        private readonly Action _onFail;

        public AsyncModel(Action onSuccess, Action onFail)
        {
            _onSuccess = onSuccess;
            _onFail    = onFail;
        }

        /// <summary>
        /// 异步结果返回后执行
        /// </summary>
        /// <param name="success">是否成功</param>
        public void Execute(bool success)
        {
            if (success) {
                _onSuccess?.Invoke();
            }
            else {
                _onFail?.Invoke();
            }
        }
    }
    
    /// <summary>
    /// AsyncModel的泛型版本。保存一个状态对象。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AsyncModel<T>
    {
        private readonly Action<T> _onSuccess;
        
        private readonly Action<T> _onFail;

        private readonly T _state;

        public AsyncModel(Action<T> onSuccess, Action<T> onFail,T state)
        {
            _onSuccess = onSuccess;
            _onFail    = onFail;
            _state = state;
        }

        public void Execute(bool success)
        {
            if (success) {
                _onSuccess?.Invoke(_state);
            }
            else {
                _onFail?.Invoke(_state);
            }
        }
    }
    
}