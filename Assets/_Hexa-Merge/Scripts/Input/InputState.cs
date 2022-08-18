using _Hexa_Merge.Scripts.Input.Interfaces;

namespace _Hexa_Merge.Scripts.Input
{
    public class InputState
    {
        public IInputState Listener;
        public virtual void Begin(){}
        public virtual void Move(){}
        public virtual void End(){}
    }
}