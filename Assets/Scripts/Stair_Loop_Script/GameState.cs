using UnityEngine;

public class GameState : MonoBehaviour
{
    public static GameState Instance { get; private set; }

    [Header("시작층 (예: 6이면 6층부터 시작)")]
    public int startFloor = 6;

    [Header("현재 남은 층수(F)")]
    public int currentFloor;

    [Header("초기 위치(틀렸을 때 되돌릴 위치)")]
    public Transform startPoint;     // 6층
    public Transform player;         // Player Transform

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

    void Start()
    {
        ResetDays();
        // 처음 시작 시 이상현상 랜덤 배치
        if (AnomalyManager.Instance != null)
            AnomalyManager.Instance.RandomizeAnomaly();
    }

    public void ResetDays()
    {
        currentFloor = startFloor;
        UpdateUI();
        TeleportToStart();
    }

    public void TeleportToStart()
    {
        if (player != null && startPoint != null)
        {
            player.position = startPoint.position;
        }
    }

    // 계단 선택 결과를 여기로 넘김
    public void ApplyStairChoice(bool goingUp, bool hasAnomaly)
    {
        // 이상 O & 위로 → -1
        // 이상 X & 위로 → 초기화
        // 이상 O & 아래 → 초기화
        // 이상 X & 아래 → -1

        bool correct =
            (hasAnomaly && goingUp) ||
            (!hasAnomaly && !goingUp);

        if (correct)
        {
            currentFloor--;
            UpdateUI();

            Debug.Log($"정답! D-{currentFloor}");

            if (currentFloor <= 0)
            {
                OnGameClear();
            }
        }
        else
        {
            Debug.Log("오답! 층수 초기화 -> 6층");
            ResetDays();
        }

        // 다음 라운드 이상현상 새로 뽑기
        if (AnomalyManager.Instance != null)
            AnomalyManager.Instance.RandomizeAnomaly();
    }

    void UpdateUI()
    {
        // D-표시 텍스트 갱신 (TMP_Text 같은 거 연결해서 쓰면 됨)
        // 예: dayText.text = $"D-{currentDays}";
    }

    void OnGameClear()
    {
        Debug.Log("게임 클리어! S06 건물에서 탈출!");
        // TODO: 엔딩 연출, 씬 전환 등
    }
}
