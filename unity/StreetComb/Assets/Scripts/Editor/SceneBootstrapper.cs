#if UNITY_EDITOR
using StreetComb.Core;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace StreetComb.EditorTools
{
    public static class SceneBootstrapper
    {
        public static void CreateBootstrapScene()
        {
            var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);

            var root = new GameObject("StreetCombRoot");
            root.AddComponent<StreetCombBootstrap>();

            EditorSceneManager.SaveScene(scene, "Assets/Scenes/PrototypeArena.unity");
            Debug.Log("Created scene: Assets/Scenes/PrototypeArena.unity");
        }
    }
}
#endif
