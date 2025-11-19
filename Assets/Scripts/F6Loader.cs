using UnityEngine;
using UnityEngine.SceneManagement;

public static class F6Loader
{
    private const string BaseScene = "10_Base";

    private static readonly string[] OtherScenes =
    {
        "11_Lobby",
        "12_Corridor",
        "13_607",
        "14_Stairs",
        "15_CafeMart"
    };

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void LoadAll()
    {
        // 1) ���̽��� ���� ������ �ε�
        if (!SceneManager.GetSceneByName(BaseScene).isLoaded)
        {
            var op = SceneManager.LoadSceneAsync(BaseScene, LoadSceneMode.Additive);
            op.completed += _ =>
            {
                // 2) ���̽��� Active Scene���� ����
                var baseScn = SceneManager.GetSceneByName(BaseScene);
                if (baseScn.IsValid())
                    SceneManager.SetActiveScene(baseScn);

                // 3) ������ ���� �ε�
                LoadOthers();
                // 4) (����) �ߺ� XR Origin ������ ���̽� �͸� �����
                KeepOnlyBaseXROrigin();
            };
        }
        else
        {
            // �̹� ���̽��� ���� ������ �״�� ����
            LoadOthers();
            KeepOnlyBaseXROrigin();
        }
    }

    private static void LoadOthers()
    {
        foreach (var s in OtherScenes)
        {
            if (!SceneManager.GetSceneByName(s).isLoaded)
                SceneManager.LoadSceneAsync(s, LoadSceneMode.Additive);
        }
    }

    // ������ġ: Ȥ�� �ٸ� ���� XR Origin�� �� �־ ���̽� �͸� Ȱ��
    private static void KeepOnlyBaseXROrigin()
    {
        var allRigs = Object.FindObjectsByType<Unity.XR.CoreUtils.XROrigin>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        Unity.XR.CoreUtils.XROrigin baseRig = null;

        foreach (var rig in allRigs)
        {
            if (rig.gameObject.scene.name == BaseScene)
            {
                baseRig = rig;
                break;
            }
        }
        foreach (var rig in allRigs)
        {
            if (rig != baseRig)
                rig.gameObject.SetActive(false);
        }
    }
}
