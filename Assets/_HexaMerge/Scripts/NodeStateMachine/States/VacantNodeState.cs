using _HexaMerge.Scripts.NodeStateMachine.Interface;
using _HexaMerge.Scripts.NodeStateMachine.States;

namespace _HexaMerge.Scripts.TileStateMachine.States
{
    public class VacantNodeState: NodeState
    {
        public VacantNodeState(INodeState listener) : base(listener) {}
        
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
        
        public void Highlight()
        {
            // Implement Highlighting functionality here.
            _Listener.ChangeNodeState(new HighlightedTileState(_Listener));
        }
    }
}