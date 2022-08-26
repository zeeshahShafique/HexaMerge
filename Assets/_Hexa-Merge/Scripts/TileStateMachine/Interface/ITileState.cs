using _Hexa_Merge.Scripts.TileStateMachine.States;

namespace _Hexa_Merge.Scripts.TileStateMachine.Interface
{
    public interface ITileState
    {
        void ChangeTileState(TileState tileState);
    }
}