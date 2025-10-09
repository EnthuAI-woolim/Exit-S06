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
        // 1) 베이스가 먼저 없으면 로드
        if (!SceneManager.GetSceneByName(BaseScene).isLoaded)
        {
            var op = SceneManager.LoadSceneAsync(BaseScene, LoadSceneMode.Additive);
            op.completed += _ =>
            {
                // 2) 베이스를 Active Scene으로 고정
                var baseScn = SceneManager.GetSceneByName(BaseScene);
                if (baseScn.IsValid())
                    SceneManager.SetActiveScene(baseScn);

                // 3) 나머지 씬들 로드
                LoadOthers();
                // 4) (선택) 중복 XR Origin 있으면 베이스 것만 남기기
                KeepOnlyBaseXROrigin();
            };
        }
        else
        {
            // 이미 베이스가 열려 있으면 그대로 진행
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

    // 안전장치: 혹시 다른 씬에 XR Origin이 들어가 있어도 베이스 것만 활성
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
