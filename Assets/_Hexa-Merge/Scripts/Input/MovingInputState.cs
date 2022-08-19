using _Hexa_Merge.Scripts.Input.Interfaces;

namespace _Hexa_Merge.Scripts.Input
{
    public class MovingInputState : InputState
    {
        public override void Begin()
        {
            base.Begin();
            // TODO: Implement DragStart functionality here.
        }

        public override void Move()
        {
            base.Move();
            // TODO: Implement DragMove functionality here.
        }

        public override void End()
        {
            base.End();
            Listener.ChangeState(new IdleInputState(Listener));
            // TODO: Implement Drag end functionality here.
        }

        public MovingInputState(IInputState listener) : base(listener){}
        
        public MovingInputState(){}
    }
}