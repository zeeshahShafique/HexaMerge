using _Hexa_Merge.Scripts.Input.Interfaces;
using UnityEngine;

namespace _Hexa_Merge.Scripts.Input
{
    public class IdleInputState : InputState
    {
        public override void Begin(Touch touch)
        {
            _Listener.ChangeState(new CalibrationInputState(_Listener, _Tap, _Drag, touch.position));
        }
        
        public IdleInputState(IInputState listener, ITap tap, IDrag drag) : base(listener, tap, drag){}
        
    }
}