namespace _Hexa_Merge.Scripts.Input
{
    public class CalibrationState : InputState
    {
        public override void Move()
        {
            Listener.ChangeState(new MovingState());
            base.Move();
        }

        public override void End()
        {
            base.End();
            //TODO: call tap functionality here
        }
    }
}