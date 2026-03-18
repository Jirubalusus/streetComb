using StreetComb.Combat;
using StreetComb.InputSystem;
using UnityEngine;

namespace StreetComb.Fighters
{
    [RequireComponent(typeof(HealthComponent))]
    [RequireComponent(typeof(EnergyComponent))]
    public class FighterController : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] private float moveStep = 1.5f;
        [SerializeField] private float jumpForce = 3f;

        [Header("Combat")]
        [SerializeField] private float lightDamage = 8f;
        [SerializeField] private float comboDamage = 14f;
        [SerializeField] private float launcherDamage = 18f;
        [SerializeField] private float specialDamage = 26f;
        [SerializeField] private float specialCost = 25f;

        [Header("References")]
        [SerializeField] private FighterController opponent;

        private readonly CombatStateMachine _stateMachine = new();
        private HealthComponent _health;
        private EnergyComponent _energy;
        private DamageFlash _damageFlash;
        private Vector3 _spawnPosition;

        public string FighterName = "Fighter";
        public CombatState CurrentState => _stateMachine.CurrentState;
        public HealthComponent Health => _health;
        public EnergyComponent Energy => _energy;

        private void Awake()
        {
            _health = GetComponent<HealthComponent>();
            _energy = GetComponent<EnergyComponent>();
            _damageFlash = GetComponent<DamageFlash>();
            _spawnPosition = transform.position;

            _health.OnKO += OnKO;
        }

        public void SetOpponent(FighterController newOpponent)
        {
            opponent = newOpponent;
        }

        public void ResetFighter()
        {
            transform.position = _spawnPosition;
            _stateMachine.SetState(CombatState.Idle);
            _health.ResetHealth();
            _damageFlash?.RefreshBaseColor();
        }

        public void ExecuteGesture(GestureType gestureType)
        {
            if (!_stateMachine.CanAct())
            {
                return;
            }

            switch (gestureType)
            {
                case GestureType.Tap:
                    PerformAction(CombatActionType.LightAttack);
                    break;
                case GestureType.DoubleTap:
                    PerformAction(CombatActionType.PressureCombo);
                    break;
                case GestureType.Hold:
                    PerformAction(CombatActionType.Block);
                    break;
                case GestureType.SwipeLeft:
                    PerformAction(CombatActionType.DashLeft);
                    break;
                case GestureType.SwipeRight:
                    PerformAction(CombatActionType.DashRight);
                    break;
                case GestureType.SwipeUp:
                    PerformAction(CombatActionType.Jump);
                    break;
                case GestureType.SwipeDown:
                    PerformAction(CombatActionType.LowGuard);
                    break;
                case GestureType.UGesture:
                    PerformAction(CombatActionType.Launcher);
                    break;
                case GestureType.SemiCircle:
                    PerformAction(CombatActionType.SignatureSpecial);
                    break;
            }
        }

        public void PerformAction(CombatActionType actionType)
        {
            switch (actionType)
            {
                case CombatActionType.LightAttack:
                    Attack(lightDamage, CombatState.ActiveAttack);
                    _energy.AddEnergy(8f);
                    break;
                case CombatActionType.PressureCombo:
                    Attack(comboDamage, CombatState.ActiveAttack);
                    _energy.AddEnergy(12f);
                    break;
                case CombatActionType.Block:
                    _stateMachine.SetState(CombatState.Block);
                    break;
                case CombatActionType.DashLeft:
                    Dash(Vector3.left);
                    break;
                case CombatActionType.DashRight:
                    Dash(Vector3.right);
                    break;
                case CombatActionType.Jump:
                    Jump();
                    break;
                case CombatActionType.LowGuard:
                    _stateMachine.SetState(CombatState.Block);
                    break;
                case CombatActionType.Launcher:
                    Attack(launcherDamage, CombatState.Special);
                    _energy.AddEnergy(15f);
                    break;
                case CombatActionType.SignatureSpecial:
                    if (_energy.Consume(specialCost))
                    {
                        Attack(specialDamage, CombatState.Special);
                    }
                    break;
            }

            if (_stateMachine.CurrentState is not CombatState.KO)
            {
                _stateMachine.SetState(CombatState.Idle);
            }
        }

        private void Dash(Vector3 direction)
        {
            _stateMachine.SetState(CombatState.Dash);
            transform.position += direction * moveStep;
        }

        private void Jump()
        {
            _stateMachine.SetState(CombatState.Airborne);
            transform.position += Vector3.up * jumpForce;
        }

        private void Attack(float damage, CombatState actionState)
        {
            _stateMachine.SetState(actionState);
            opponent?.ReceiveAttack(damage);
        }

        public void ReceiveAttack(float damage)
        {
            if (_stateMachine.CurrentState == CombatState.Block)
            {
                damage *= 0.35f;
            }

            _stateMachine.SetState(CombatState.HitStun);
            _damageFlash?.Flash();
            transform.position += new Vector3(damage * 0.02f * Mathf.Sign(transform.position.x - (opponent != null ? opponent.transform.position.x : 0f)), 0f, 0f);
            _health.ApplyDamage(damage);
            if (_stateMachine.CurrentState is not CombatState.KO)
            {
                _stateMachine.SetState(CombatState.Idle);
            }
        }

        private void OnKO()
        {
            _stateMachine.SetState(CombatState.KO);
        }
    }
}
