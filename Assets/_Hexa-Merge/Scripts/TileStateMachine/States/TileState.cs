using _Hexa_Merge.Scripts.TileStateMachine.Interface;

namespace _Hexa_Merge.Scripts.TileStateMachine.States
{
    public class TileState
    {
        protected ITileState _Listener;

        public TileState(ITileState listener)
        {
            _Listener = listener;
        }

        public virtual void Begin(){}
        public virtual void CheckVacantTile(){}
        public virtual void Highlight(){}
        public virtual void UnHighlight(){}
        public virtual void SnapOnGrid(){}
        public virtual void Return(){}
    
    }
}
