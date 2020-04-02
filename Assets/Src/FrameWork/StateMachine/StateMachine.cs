﻿using System.Text;

namespace HG
{
    public class StateMachine:ISM
    {
        protected StateBase _current;

        private int _recordIndex = 0;

        private StringBuilder _stringBuilder = new StringBuilder();
        
        public virtual void Register(string stateName)
        {
            
        }

        public virtual void Enter(string stateName)
        {
            _current?.OnLeave();

            var stateBase = StateFactory.Instance.CreateState(stateName);
            _current = stateBase;
            _current.OnEnter();

            AddRecord(_current.Name);
        }
        
        public virtual void Enter(StateBase stateBase)
        {
            _current?.OnLeave();
            _current = stateBase;
            _current.OnEnter();

            AddRecord(_current.Name);
        }

        public virtual void Update()
        {
            _current?.OnExecute();
        }

        public virtual StateBase GetCurrent()
        {
            return _current;
        }

        public virtual void Dispose()
        {
            
        }

        protected void AddRecord(string name)
        {
            _stringBuilder.AppendLine(_recordIndex + "->" + name);
            _recordIndex++;
        }

        public string GetRecord()
        {
            return _stringBuilder.ToString();
        }
    }
}