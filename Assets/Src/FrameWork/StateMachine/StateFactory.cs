﻿namespace HG
{
    public class StateFactory:Singleton<StateFactory>
    {
        public StateBase CreateState(string name)
        {
            return default(StateBase);
        }
    }
}