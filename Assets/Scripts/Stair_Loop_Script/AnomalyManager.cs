using UnityEngine;

public class AnomalyManager : MonoBehaviour
{
    public static AnomalyManager Instance { get; private set; }

    [Tooltip("Abnomal objects (on/off objects)")]
    public GameObject[] anomalyObjects;
    public int count;
    public int RandomIndex;
    // public bool FirstRound = true;

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

        count = Random.Range(0, 2);

        if(count == 1)
        {
            // 랜덤 인덱스 생성 (0~7) 따로 설정 해줘야함 (하드코딩)
            RandomIndex = Random.Range(0, 11); 
            var obj = anomalyObjects[RandomIndex];
            obj.SetActive(true);
            HasAnomaly = true;
        }
        
    }
}
