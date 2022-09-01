using _HexaMerge.Scripts.NodeStateMachine.States;

namespace _HexaMerge.Scripts.NodeStateMachine.Interface
{
    public interface INodeState
    {
        void ChangeNodeState(NodeState nodeState);
    }
}