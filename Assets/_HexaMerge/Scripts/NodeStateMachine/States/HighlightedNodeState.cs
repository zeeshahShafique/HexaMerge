using _HexaMerge.Scripts.NodeStateMachine.Interface;
using _HexaMerge.Scripts.NodeStateMachine.States;
using UnityEngine;

namespace _HexaMerge.Scripts.TileStateMachine.States
{
    public class HighlightedTileState : NodeState
    {
        private SpriteRenderer _spriteRenderer;
        public HighlightedTileState(INodeState listener) : base(listener) {}

        public override void Enter()
        {
            Highlight();
        }

        private void Highlight()
        {
            // implement highlight functionality here.
        }

        public override void Execute()
        {
            base.Execute();
        }

        public override void End()
        {
            UnHighlight();
        }

        public void UnHighlight()
        {
            // Implement unhighlight functionality here.
            _Listener.ChangeNodeState(new VacantNodeState(_Listener));
        }
    }
}