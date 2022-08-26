using _Hexa_Merge.Scripts.TileStateMachine.Interface;

namespace _Hexa_Merge.Scripts.TileStateMachine.States
{
    public class VacantTileState: TileState
    {
        public VacantTileState(ITileState listener) : base(listener) {}

        public override void Highlight()
        {
            // Implement Highlighting functionality here.
            _Listener.ChangeTileState(new HighlightedTileState(_Listener));
        }
    }
}