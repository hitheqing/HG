using System.Collections.Generic;

namespace HG
{
    public class OrderStateMachine:StateMachine
    {
        private readonly List<StateBase> _orderedList = new List<StateBase>();
        
        public override void Register(string stateName)
        {
            var stateBase = StateFactory.Instance.CreateState(stateName);
            _orderedList.Add(stateBase);
        }
        
        public void Register(List<StateBase> list)
        {
            _orderedList.AddRange(list);
        }

        public void Run()
        {
            if (_orderedList.Count == 0) return;

            Enter(_orderedList[0]);
        }

        public void End()
        {
            Loger.Color("Order Machine End");
        }

        public void MoveNext()
        {
            if (_current == null)
            {
                Run();
                return ;
            }
            
            var curIndex = _orderedList.FindIndex(x => x == _current);

            if (++curIndex < _orderedList.Count - 1)
            {
                Enter(_orderedList[curIndex]);
                
            }
            else
            {
                End();
            }
        }
    }
}