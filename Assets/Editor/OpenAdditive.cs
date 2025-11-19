using UnityEditor;
using UnityEditor.SceneManagement;

public static class OpenAdditive
{
    [MenuItem("Tools/Open Additive/10_Base")]
    static void OpenBase() =>
        EditorSceneManager.OpenScene("Assets/Scenes/10_Base.unity", OpenSceneMode.Additive);

    [MenuItem("Tools/Open Additive/11_Lobby")]
    static void OpenLobby() =>
        EditorSceneManager.OpenScene("Assets/Scenes/11_Lobby.unity", OpenSceneMode.Additive);

    [MenuItem("Tools/Open Additive/12_Corridor")]
    static void OpenCorridor() =>
        EditorSceneManager.OpenScene("Assets/Scenes/12_Corridor.unity", OpenSceneMode.Additive);

    [MenuItem("Tools/Open Additive/13_607")]
    static void Open607() =>
        EditorSceneManager.OpenScene("Assets/Scenes/13_607.unity", OpenSceneMode.Additive);

    [MenuItem("Tools/Open Additive/14_Stairs")]
    static void OpenStairs() =>
        EditorSceneManager.OpenScene("Assets/Scenes/14_Stairs.unity", OpenSceneMode.Additive);

    [MenuItem("Tools/Open Additive/15_CafeMart")]
    static void OpenCafeMart() =>
        EditorSceneManager.OpenScene("Assets/Scenes/15_CafeMart.unity", OpenSceneMode.Additive);

}
