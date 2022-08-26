using _Hexa_Merge.Scripts.TileStateMachine.Interface;

namespace _Hexa_Merge.Scripts.TileStateMachine.States
{
    public class HighlightedTileState : TileState
    {
        public HighlightedTileState(ITileState listener) : base(listener) {}

        public override void UnHighlight()
        {
            // Implement unhighlight functionality here.
            _Listener.ChangeTileState(new VacantTileState(_Listener));
        }

        public override void SnapOnGrid()
        {
            // Implement snapping on grid functionality here.
            _Listener.ChangeTileState(new OccupiedTileState(_Listener));
        }
    }
}