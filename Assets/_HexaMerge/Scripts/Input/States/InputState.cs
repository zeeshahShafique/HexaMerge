using _Hexa_Merge.Scripts.Input.Interfaces;
using UnityEngine;

namespace _HexaMerge.Scripts.Input.States
{
    public class InputState
    {
        protected IInputState _Listener;
        protected ITap _Tap;
        protected IDrag _Drag;

        protected InputState(IInputState listener, ITap tap, IDrag drag)
        {
            _Listener = listener;
            _Tap = tap;
            _Drag = drag;
        }
        
        public virtual void Begin(Touch touch){}
        public virtual void Move(Touch touch){}
        public virtual void End(Touch touch){}
    }
}