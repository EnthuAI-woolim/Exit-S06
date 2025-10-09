using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

[InitializeOnLoad]
public static class AutoOpenScenesEditor
{
    // 부트씬 이름
    private const string BootScene = "00_Boot";

    // 열릴 씬 목록
    private static readonly string[] ScenesToAutoOpen =
    {
        "10_Base",
        "11_Lobby",
        "12_Corridor",
        "13_607",
        "14_Stairs",
        "15_CafeMart"
    };

    static AutoOpenScenesEditor()
    {
        // 에디터에서 씬이 열릴 때마다 호출
        EditorSceneManager.sceneOpened += (scene, mode) =>
        {
            // 만약 열린 씬이 00_Boot라면 자동으로 다른 씬들 Additive 로드
            if (scene.name == BootScene)
            {
                foreach (var s in ScenesToAutoOpen)
                {
                    bool alreadyOpen = false;
                    for (int i = 0; i < SceneManager.sceneCount; i++)
                    {
                        if (SceneManager.GetSceneAt(i).name == s)
                        {
                            alreadyOpen = true;
                            break;
                        }
                    }

                    if (!alreadyOpen)
                    {
                        var path = $"Assets/Scenes/{s}.unity";
                        if (System.IO.File.Exists(path))
                        {
                            EditorSceneManager.OpenScene(path, OpenSceneMode.Additive);
                            Debug.Log($"[AutoOpen] {s} 씬을 Additive로 열었습니다.");
                        }
                        else
                        {
                            Debug.LogWarning($"[AutoOpen] {path} 씬을 찾을 수 없습니다!");
                        }
                    }
                }

                // Active Scene은 00_Boot으로 유지
                EditorSceneManager.SetActiveScene(scene);
            }
        };
    }
}
