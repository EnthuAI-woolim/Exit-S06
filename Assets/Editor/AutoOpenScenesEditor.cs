using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

[InitializeOnLoad]
public static class AutoOpenScenesEditor
{
    // ��Ʈ�� �̸�
    private const string BootScene = "00_Boot";

    // ���� �� ���
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
        // �����Ϳ��� ���� ���� ������ ȣ��
        EditorSceneManager.sceneOpened += (scene, mode) =>
        {
            // ���� ���� ���� 00_Boot��� �ڵ����� �ٸ� ���� Additive �ε�
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
                            Debug.Log($"[AutoOpen] {s} ���� Additive�� �������ϴ�.");
                        }
                        else
                        {
                            Debug.LogWarning($"[AutoOpen] {path} ���� ã�� �� �����ϴ�!");
                        }
                    }
                }

                // Active Scene�� 00_Boot���� ����
                EditorSceneManager.SetActiveScene(scene);
            }
        };
    }
}
