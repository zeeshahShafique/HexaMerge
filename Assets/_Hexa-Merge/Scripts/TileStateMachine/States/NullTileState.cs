using _Hexa_Merge.Scripts.TileStateMachine.Interface;

namespace _Hexa_Merge.Scripts.TileStateMachine.States
{
    public class NullTileState : TileState
    {
        public NullTileState(ITileState listener) : base(listener) {}
    }
}