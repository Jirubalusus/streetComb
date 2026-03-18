using StreetComb.Fighters;
using StreetComb.InputSystem;
using StreetComb.DebugTools;
using UnityEngine;

namespace StreetComb.Core
{
    public class StreetCombBootstrap : MonoBehaviour
    {
        private void Awake()
        {
            SetupCamera();
            var player = CreateFighter("Raze", new Vector3(-2f, 0f, 0f), new Color(1f, 0.65f, 0.1f));
            var enemy = CreateFighter("Iron Monk", new Vector3(2f, 0f, 0f), new Color(0.3f, 0.55f, 0.95f));

            player.SetOpponent(enemy);
            enemy.SetOpponent(player);

            var inputObject = new GameObject("TouchCapture");
            var touchCapture = inputObject.AddComponent<TouchCapture>();

            var debugObject = new GameObject("GestureDebugOverlay");
            var overlay = debugObject.AddComponent<GestureDebugOverlay>();

            typeof(GestureDebugOverlay)
                .GetField("touchCapture", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                ?.SetValue(overlay, touchCapture);

            typeof(GestureDebugOverlay)
                .GetField("playerFighter", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                ?.SetValue(overlay, player);

            CreateArena();
        }

        private FighterController CreateFighter(string fighterName, Vector3 position, Color color)
        {
            var fighterObject = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            fighterObject.name = fighterName;
            fighterObject.transform.position = position;
            fighterObject.transform.localScale = new Vector3(1f, 2f, 1f);
            fighterObject.GetComponent<Renderer>().material.color = color;

            var fighter = fighterObject.AddComponent<FighterController>();
            fighter.FighterName = fighterName;
            fighterObject.AddComponent<StreetComb.Combat.HealthComponent>();
            fighterObject.AddComponent<StreetComb.Combat.EnergyComponent>();

            return fighter;
        }

        private void CreateArena()
        {
            var ground = GameObject.CreatePrimitive(PrimitiveType.Cube);
            ground.name = "ArenaGround";
            ground.transform.position = new Vector3(0f, -1.25f, 0f);
            ground.transform.localScale = new Vector3(14f, 0.5f, 4f);
            ground.GetComponent<Renderer>().material.color = new Color(0.12f, 0.12f, 0.16f);
        }

        private void SetupCamera()
        {
            if (Camera.main == null)
            {
                var cameraObject = new GameObject("Main Camera");
                cameraObject.tag = "MainCamera";
                var cam = cameraObject.AddComponent<Camera>();
                cam.clearFlags = CameraClearFlags.SolidColor;
                cam.backgroundColor = new Color(0.08f, 0.08f, 0.12f);
                cameraObject.transform.position = new Vector3(0f, 1.5f, -10f);
            }
            else
            {
                Camera.main.transform.position = new Vector3(0f, 1.5f, -10f);
                Camera.main.backgroundColor = new Color(0.08f, 0.08f, 0.12f);
            }
        }
    }
}
