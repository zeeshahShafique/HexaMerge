using _Hexa_Merge.Scripts.TileStateMachine.Interface;

namespace _Hexa_Merge.Scripts.TileStateMachine.States
{
    public class OccupiedTileState : TileState
    {
        public OccupiedTileState(ITileState listener) : base(listener) {}
        
        // Implement Merge functionality in this state.
    }
}