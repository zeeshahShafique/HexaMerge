namespace _Hexa_Merge.Scripts.Input
{
    public class IdleInputState : InputState
    {
        public override void Begin()
        {
            Listener.ChangeState(new CalibrationState());
        }
    }
}