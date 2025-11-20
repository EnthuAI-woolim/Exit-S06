using UnityEngine;

public class AnomalyManager : MonoBehaviour
{
    public static AnomalyManager Instance { get; private set; }

    [Tooltip("이상현상 오브젝트들 (교실 안에서 켜고 끌 프리팹들)")]
    public GameObject[] anomalyObjects;

    public bool HasAnomaly { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // 새 라운드 시작할 때마다 호출: 이상현상 랜덤 배치
    public void RandomizeAnomaly()
    {
        // 일단 전부 끔
        foreach (var obj in anomalyObjects)
        {
            if (obj != null) obj.SetActive(false);
        }

        // 50% 확률로 이상현상 존재 여부 결정
        HasAnomaly = Random.value < 0.5f;

        if (HasAnomaly && anomalyObjects.Length > 0)
        {
            int idx = Random.Range(0, anomalyObjects.Length);
            if (anomalyObjects[idx] != null)
                anomalyObjects[idx].SetActive(true);
        }
    }
}
