using _Hexa_Merge.Scripts.Input.Interfaces;
using UnityEngine;

namespace _Hexa_Merge.Scripts.Input
{
    public class InputState
    {
        public IInputState Listener;

        public InputState(IInputState listener)
        {
            Listener = listener;
            Debug.LogError($"Current Type: {GetType()}");
        }
        
        public InputState(){}
        public virtual void Begin(){}
        public virtual void Move(){}
        public virtual void End(){}
    }
}