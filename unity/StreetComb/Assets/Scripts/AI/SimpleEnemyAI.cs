using StreetComb.Combat;
using StreetComb.Fighters;
using UnityEngine;

namespace StreetComb.AI
{
    public class SimpleEnemyAI : MonoBehaviour
    {
        [SerializeField] private FighterController controlledFighter;
        [SerializeField] private FighterController targetFighter;
        [SerializeField] private float thinkInterval = 1.1f;
        [SerializeField] private float aggression = 0.7f;

        private float _nextThinkTime;

        private void Update()
        {
            if (controlledFighter == null || targetFighter == null)
            {
                return;
            }

            if (controlledFighter.CurrentState == CombatState.KO || targetFighter.CurrentState == CombatState.KO)
            {
                return;
            }

            if (Time.time < _nextThinkTime)
            {
                return;
            }

            _nextThinkTime = Time.time + thinkInterval + Random.Range(-0.2f, 0.25f);
            Think();
        }

        private void Think()
        {
            float distance = Vector3.Distance(controlledFighter.transform.position, targetFighter.transform.position);
            float roll = Random.value;

            if (distance > 3.5f)
            {
                controlledFighter.PerformAction(targetFighter.transform.position.x > controlledFighter.transform.position.x
                    ? CombatActionType.DashRight
                    : CombatActionType.DashLeft);
                return;
            }

            if (roll < 0.18f)
            {
                controlledFighter.PerformAction(CombatActionType.Block);
                return;
            }

            if (roll < 0.18f + aggression * 0.35f)
            {
                controlledFighter.PerformAction(CombatActionType.LightAttack);
                return;
            }

            if (roll < 0.58f + aggression * 0.15f)
            {
                controlledFighter.PerformAction(CombatActionType.PressureCombo);
                return;
            }

            if (controlledFighter.Energy.CurrentEnergy >= 25f && roll > 0.82f)
            {
                controlledFighter.PerformAction(CombatActionType.SignatureSpecial);
                return;
            }

            controlledFighter.PerformAction(CombatActionType.Launcher);
        }

        public void Setup(FighterController self, FighterController target)
        {
            controlledFighter = self;
            targetFighter = target;
        }
    }
}
