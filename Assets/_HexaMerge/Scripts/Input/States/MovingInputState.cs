using _Hexa_Merge.Scripts.Input;
using _Hexa_Merge.Scripts.Input.Interfaces;
using UnityEngine;

namespace _HexaMerge.Scripts.Input.States
{
    public class MovingInputState : InputState
    {
        public override void Move(Touch touch)
        {
            _Drag.OnDrag(touch);
            // TODO: Implement DragMove functionality here.
        }

        public override void End(Touch touch)
        {
            _Drag.OnDragEnd();
            _Listener.ChangeState(new IdleInputState(_Listener, _Tap, _Drag));
            // TODO: Implement Drag end functionality here.
        }

        public MovingInputState(IInputState listener, ITap tap, IDrag drag) : base(listener, tap, drag){}
        
    }
}