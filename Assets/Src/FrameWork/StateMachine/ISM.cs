namespace HG
 {
    public interface ISM
    {
        void Register(string stateName);

        void Enter(string stateName);
        
        void Enter(StateBase stateBase);

        void Update();

        StateBase GetCurrent();

        void Dispose();
    }
}