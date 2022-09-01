using _HexaMerge.Scripts.NodeStateMachine.Interface;
using _HexaMerge.Scripts.NodeStateMachine.States;

namespace _HexaMerge.Scripts.TileStateMachine.States
{
    public class OccupiedNodeState : NodeState
    {
        public OccupiedNodeState(INodeState listener) : base(listener) {}
        
        public override void Enter()
        {
            base.Enter();
        }

        public override void Execute()
        {
            base.Execute();
        }

        public override void End()
        {
            base.End();
        }
        
        // Implement Merge functionality in this state.
    }
}