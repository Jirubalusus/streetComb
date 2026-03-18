namespace StreetComb.Combat
{
    public class CombatStateMachine
    {
        public CombatState CurrentState { get; private set; } = CombatState.Idle;

        public void SetState(CombatState state)
        {
            CurrentState = state;
        }

        public bool CanAct()
        {
            return CurrentState is CombatState.Idle or CombatState.Block;
        }
    }
}
