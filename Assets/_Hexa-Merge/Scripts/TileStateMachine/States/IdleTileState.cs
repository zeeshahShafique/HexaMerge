using _Hexa_Merge.Scripts.TileStateMachine.Interface;

namespace _Hexa_Merge.Scripts.TileStateMachine.States
{
    public class IdleTileState: TileState
    {
        public IdleTileState(ITileState listener) : base(listener){}

        public override void Begin()
        {
            _Listener.ChangeTileState(new MovingTileState(_Listener));
        }
    }
}