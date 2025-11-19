// Assets/_Project/Editor/HierarchyActiveSceneHighlighter.cs
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

[InitializeOnLoad]
public static class HierarchyActiveSceneHighlighter
{
    static readonly Color lineColor = new Color(0.25f, 0.55f, 1f, 0.4f);  // 파란 라인
    static readonly Color softBG = new Color(0.25f, 0.55f, 1f, 0.06f);  // 아주 옅은 배경

    static HierarchyActiveSceneHighlighter()
    {
        EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyItemGUI;
    }

    static void OnHierarchyItemGUI(int instanceID, Rect selectionRect)
    {
        var obj = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
        if (obj == null) return;

        var active = SceneManager.GetActiveScene();
        var scene = obj.scene;
        if (!scene.IsValid()) return;

        // Active Scene 소속이면
        if (scene == active)
        {
            // 아주 옅은 배경
            var bg = selectionRect;
            bg.x = 0;
            bg.width = selectionRect.x + selectionRect.width;
            EditorGUI.DrawRect(bg, softBG);

            // 왼쪽 라인(씬 구분용)
            var line = new Rect(bg.x, bg.y, 3, bg.height);
            EditorGUI.DrawRect(line, lineColor);
        }

        // 우클릭 시 씬을 Active로 설정
        if (Event.current.type == EventType.ContextClick && selectionRect.Contains(Event.current.mousePosition))
        {
            var menu = new GenericMenu();
            menu.AddItem(new GUIContent($"Set Active Scene: {scene.name}"), false, () =>
            {
                EditorSceneManager.SetActiveScene(scene);
            });
            menu.ShowAsContext();
            Event.current.Use();
        }
    }

    // 툴바 메뉴에서도 빠르게 전환할 수 있게
    [MenuItem("Tools/Scenes/Set Active To Current Selection Scene %#a")] // Ctrl/Cmd+Shift+A
    static void SetActiveToSelectionScene()
    {
        var go = Selection.activeGameObject;
        if (go == null) return;
        var scn = go.scene;
        if (scn.IsValid()) EditorSceneManager.SetActiveScene(scn);
    }

    [MenuItem("Tools/Scenes/Ping Active Scene Root")]
    static void PingActiveSceneRoot()
    {
        var active = SceneManager.GetActiveScene();
        if (!active.IsValid()) return;
        var roots = active.GetRootGameObjects();
        if (roots.Length > 0) EditorGUIUtility.PingObject(roots[0]);
    }
}
