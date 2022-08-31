using _HexaMerge.Scripts.MergeSystem.MergeStateMachine.States;

namespace _HexaMerge.Scripts.MergeSystem.MergeStateMachine.Interface
{
    public interface IMergeState
    {
        public void ChangeState(MergeState mergeState);
    }
}