using _HexaMerge.Scripts.NodeStateMachine.Interface;
using _HexaMerge.Scripts.NodeStateMachine.States;

namespace _HexaMerge.Scripts.TileStateMachine.States
{
    public class NullTileState : NodeState
    {
        public NullTileState(INodeState listener) : base(listener) {}
        
    }
}