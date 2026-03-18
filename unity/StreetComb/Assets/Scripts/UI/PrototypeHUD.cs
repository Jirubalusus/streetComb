using StreetComb.Fighters;
using UnityEngine;

namespace StreetComb.UI
{
    public class PrototypeHUD : MonoBehaviour
    {
        [SerializeField] private FighterController player;
        [SerializeField] private FighterController enemy;

        public void Setup(FighterController playerFighter, FighterController enemyFighter)
        {
            player = playerFighter;
            enemy = enemyFighter;
        }

        private void OnGUI()
        {
            if (player == null || enemy == null)
            {
                return;
            }

            DrawHealthBar(new Rect(20, 20, 280, 24), player.Health.CurrentHealth / player.Health.MaxHealth, Color.green, "PLAYER");
            DrawHealthBar(new Rect(Screen.width - 300, 20, 280, 24), enemy.Health.CurrentHealth / enemy.Health.MaxHealth, Color.red, "ENEMY");

            DrawEnergyBar(new Rect(20, 52, 220, 18), player.Energy.CurrentEnergy / 100f, new Color(0.2f, 0.8f, 1f), "ENERGY");
            DrawEnergyBar(new Rect(Screen.width - 240, 52, 220, 18), enemy.Energy.CurrentEnergy / 100f, new Color(1f, 0.7f, 0.15f), "ENERGY");

            if (player.CurrentState == StreetComb.Combat.CombatState.KO)
            {
                DrawCenterMessage("DEFEAT");
            }
            else if (enemy.CurrentState == StreetComb.Combat.CombatState.KO)
            {
                DrawCenterMessage("VICTORY");
            }
        }

        private void DrawHealthBar(Rect rect, float normalized, Color fillColor, string label)
        {
            GUI.Box(rect, string.Empty);
            var fill = new Rect(rect.x + 2, rect.y + 2, (rect.width - 4) * Mathf.Clamp01(normalized), rect.height - 4);
            var old = GUI.color;
            GUI.color = fillColor;
            GUI.Box(fill, string.Empty);
            GUI.color = old;
            GUI.Label(new Rect(rect.x, rect.y - 18, rect.width, 20), label);
        }

        private void DrawEnergyBar(Rect rect, float normalized, Color fillColor, string label)
        {
            GUI.Box(rect, string.Empty);
            var fill = new Rect(rect.x + 2, rect.y + 2, (rect.width - 4) * Mathf.Clamp01(normalized), rect.height - 4);
            var old = GUI.color;
            GUI.color = fillColor;
            GUI.Box(fill, string.Empty);
            GUI.color = old;
            GUI.Label(new Rect(rect.x, rect.y - 16, rect.width, 18), label);
        }

        private void DrawCenterMessage(string text)
        {
            var rect = new Rect((Screen.width * 0.5f) - 120, 90, 240, 44);
            GUI.Box(rect, text);
        }
    }
}
