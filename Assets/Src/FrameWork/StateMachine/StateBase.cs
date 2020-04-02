﻿namespace HG
{
    public abstract class StateBase
    {
        //TODO:根据具体项目改成泛型 持有者
        // public object Owner;
        
        public string Name;

        public abstract void OnEnter();
        
        public abstract void OnExecute();
        
        public abstract void OnLeave();
    }
}