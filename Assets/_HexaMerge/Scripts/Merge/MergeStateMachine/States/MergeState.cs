
using _HexaMerge.Scripts.MergeSystem.MergeStateMachine.Interface;

namespace _HexaMerge.Scripts.MergeSystem.MergeStateMachine.States
{
    public class MergeState
    {
        private IMergeState _mergeState;
        
        public virtual void Enter(){}
        
        public virtual void Execute(){}
        
        public virtual void Exit(){}
    }
}
