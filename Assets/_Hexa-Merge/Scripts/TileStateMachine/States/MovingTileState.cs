using _Hexa_Merge.Scripts.TileStateMachine.Interface;

namespace _Hexa_Merge.Scripts.TileStateMachine.States
{
    public class MovingTileState : TileState
    {
        public MovingTileState(ITileState listener) : base(listener) {}

        public override void Return()
        {
            _Listener.ChangeTileState(new IdleTileState(_Listener));
        }
    }
}