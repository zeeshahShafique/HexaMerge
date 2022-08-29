using _HexaMerge.Scripts.NodeStateMachine.Interface;

namespace _HexaMerge.Scripts.NodeStateMachine.States
{
    public class NodeState
    {
        protected INodeState _Listener;

        public NodeState(INodeState listener)
        {
            _Listener = listener;
        }

        public virtual void Enter(){}
        
        public virtual void Execute(){}
        
        public virtual void End(){}
        
        
    
    }
}
