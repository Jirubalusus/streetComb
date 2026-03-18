using StreetComb.AI;
using StreetComb.Fighters;
using StreetComb.Flow;
using StreetComb.InputSystem;
using StreetComb.DebugTools;
using StreetComb.UI;
using UnityEngine;

namespace StreetComb.Core
{
    public class StreetCombBootstrap : MonoBehaviour
    {
        private void Awake()
        {
            SetupCamera();
            CreateBackground();

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

            var hudObject = new GameObject("PrototypeHUD");
            var hud = hudObject.AddComponent<PrototypeHUD>();
            hud.Setup(player, enemy);

            var enemyAIObject = new GameObject("EnemyAI");
            var enemyAI = enemyAIObject.AddComponent<SimpleEnemyAI>();
            enemyAI.Setup(enemy, player);

            var roundFlowObject = new GameObject("RoundFlow");
            var roundFlow = roundFlowObject.AddComponent<RoundFlowController>();
            roundFlow.Setup(player, enemy);

            CreateArena();
        }

        private FighterController CreateFighter(string fighterName, Vector3 position, Color color)
        {
            var fighterObject = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            fighterObject.name = fighterName;
            fighterObject.transform.position = position;
            fighterObject.transform.localScale = new Vector3(1f, 2f, 1f);
            fighterObject.GetComponent<Renderer>().material.color = color;
            fighterObject.AddComponent<StreetComb.Combat.DamageFlash>();

            var shadow = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            shadow.name = fighterName + "_Shadow";
            shadow.transform.SetParent(fighterObject.transform);
            shadow.transform.localPosition = new Vector3(0f, -1f, 0f);
            shadow.transform.localScale = new Vector3(0.8f, 0.03f, 0.8f);
            shadow.GetComponent<Renderer>().material.color = new Color(0f, 0f, 0f, 0.4f);

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

            var leftNeon = GameObject.CreatePrimitive(PrimitiveType.Cube);
            leftNeon.name = "LeftNeon";
            leftNeon.transform.position = new Vector3(-4.5f, 1.2f, 1.6f);
            leftNeon.transform.localScale = new Vector3(1.5f, 2.4f, 0.2f);
            leftNeon.GetComponent<Renderer>().material.color = new Color(1f, 0.25f, 0.5f);

            var rightNeon = GameObject.CreatePrimitive(PrimitiveType.Cube);
            rightNeon.name = "RightNeon";
            rightNeon.transform.position = new Vector3(4.5f, 1.5f, 1.4f);
            rightNeon.transform.localScale = new Vector3(1.8f, 3.1f, 0.2f);
            rightNeon.GetComponent<Renderer>().material.color = new Color(0.2f, 0.9f, 1f);
        }

        private void CreateBackground()
        {
            var backdrop = GameObject.CreatePrimitive(PrimitiveType.Plane);
            backdrop.name = "Backdrop";
            backdrop.transform.position = new Vector3(0f, -0.2f, 8f);
            backdrop.transform.localScale = new Vector3(1.2f, 1f, 0.8f);
            backdrop.transform.rotation = Quaternion.Euler(90f, 180f, 0f);
            backdrop.GetComponent<Renderer>().material.color = new Color(0.08f, 0.08f, 0.14f);
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
