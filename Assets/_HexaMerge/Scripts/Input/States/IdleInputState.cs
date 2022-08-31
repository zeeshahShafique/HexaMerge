using _Hexa_Merge.Scripts.Input;
using _Hexa_Merge.Scripts.Input.Interfaces;
using UnityEngine;

namespace _HexaMerge.Scripts.Input.States
{
    public class IdleInputState : InputState
    {
        private Camera _camera = Camera.main;
        public override void Begin(Touch touch)
        {
            var touchPosition = _camera.ScreenToWorldPoint(touch.position);
            var x = touchPosition.x;
            var y = touchPosition.y;
            if (x is > 1 and < 3 && y is < -6.5f and > -9.5f)
                _Listener.ChangeState(new CalibrationInputState(_Listener, _Tap, _Drag, touch.position));
        }
        
        public IdleInputState(IInputState listener, ITap tap, IDrag drag) : base(listener, tap, drag){}
        
    }
}