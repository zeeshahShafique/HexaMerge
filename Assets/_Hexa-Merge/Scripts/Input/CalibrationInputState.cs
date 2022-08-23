using _Hexa_Merge.Scripts.Input.Interfaces;
using UnityEngine;

namespace _Hexa_Merge.Scripts.Input
{
    public class CalibrationInputState : InputState
    {
        private Vector2 _startPos;
        public override void Move(Touch touch)
        {
            if (Mathf.Abs(touch.position.magnitude -_startPos.magnitude) > 10)
                _Listener.ChangeState(new MovingInputState(_Listener, _Tap, _Drag));
        }

        public override void End(Touch touch)
        {
            _Tap.OnTap();
            _Listener.ChangeState(new IdleInputState(_Listener, _Tap, _Drag));
            //TODO: call tap functionality here
        }

        public CalibrationInputState(IInputState listener, ITap tap, IDrag drag, Vector2 startPos) : base(listener, tap,
            drag)
        {
            _startPos = startPos;
        }

    }
}