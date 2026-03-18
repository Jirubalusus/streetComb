using System.Collections;
using StreetComb.Combat;
using StreetComb.Fighters;
using UnityEngine;

namespace StreetComb.Flow
{
    public class RoundFlowController : MonoBehaviour
    {
        [SerializeField] private FighterController player;
        [SerializeField] private FighterController enemy;
        [SerializeField] private float roundResetDelay = 2.2f;

        private bool _roundEnding;

        public void Setup(FighterController playerFighter, FighterController enemyFighter)
        {
            player = playerFighter;
            enemy = enemyFighter;

            player.Health.OnKO += HandleRoundFinished;
            enemy.Health.OnKO += HandleRoundFinished;
        }

        private void HandleRoundFinished()
        {
            if (_roundEnding)
            {
                return;
            }

            _roundEnding = true;
            StartCoroutine(ResetRoundRoutine());
        }

        private IEnumerator ResetRoundRoutine()
        {
            yield return new WaitForSeconds(roundResetDelay);
            player.ResetFighter();
            enemy.ResetFighter();
            _roundEnding = false;
        }
    }
}
