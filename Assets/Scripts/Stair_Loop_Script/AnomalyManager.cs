using UnityEngine;

public class AnomalyManager : MonoBehaviour
{
    public static AnomalyManager Instance { get; private set; }

    [Tooltip("Abnomal objects (on/off objects)")]
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
        // 일단 전부 끔 (오류 방지)
        foreach (var obj in anomalyObjects)
        {
            if (obj != null) obj.SetActive(false);
        }

        // 디폴트로 false 처리
        HasAnomaly = false;

        foreach (var obj in anomalyObjects)
        {
            if (Random.value > 0.5f)
            {
                obj.SetActive(true);
                HasAnomaly = true;
            }
        }
    }
}
