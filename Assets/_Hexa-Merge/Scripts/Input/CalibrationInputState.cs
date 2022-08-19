using _Hexa_Merge.Scripts.Input.Interfaces;
using UnityEngine;

namespace _Hexa_Merge.Scripts.Input
{
    public class CalibrationInputState : InputState
    {
        public override void Move()
        {
            Listener.ChangeState(new MovingInputState(Listener));
        }

        public override void End()
        {
            Debug.Log($"Just Tapped");
            Listener.ChangeState(new IdleInputState(Listener));
            //TODO: call tap functionality here
        }

        public CalibrationInputState(IInputState listener) : base(listener){}
        
        public CalibrationInputState(){}
    }
}