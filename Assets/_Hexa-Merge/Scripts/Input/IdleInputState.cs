using _Hexa_Merge.Scripts.Input.Interfaces;

namespace _Hexa_Merge.Scripts.Input
{
    public class IdleInputState : InputState
    {
        public override void Begin()
        {
            Listener.ChangeState(new CalibrationInputState(Listener));
        }
        
        public IdleInputState(IInputState listener) : base(listener){}
        
        public IdleInputState(){}
    }
}