using UnityEngine;

public class GameState : MonoBehaviour
{
    public static GameState Instance { get; private set; }

    [Header("start floor, default is 6")]
    public int startFloor = 6;

    [Header("current floor")]
    public int currentFloor;

    [Header("start position, teleport position")]
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
        ResetFloors();
        // 처음 시작 시 이상현상 랜덤 배치
        if (AnomalyManager.Instance != null)
            AnomalyManager.Instance.RandomizeAnomaly();
    }

    public void ResetFloors()
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
        // goingUp == true -> 위쪽 이동
        // goingUp == false -> 아래쪽 이동
        // hasAnomaly == true -> 이상 O
        // hasAnomaly == false -> 이상 x
        // case 1. 이상 O & 위로 → 초기화 
        // case 2. 이상 X & 위로 → -1 
        // case 3. 이상 O & 아래 → -1 
        // case 4 이상 X & 아래 → 초기화

        if ((hasAnomaly == true && goingUp == true) ||
            (hasAnomaly == false && goingUp == false)) // case 1, 4 -> 초기화
        {
            Debug.Log($"오답 => 층수 초기화");
            ResetFloors();
            UpdateUI();
        }
        else if((hasAnomaly == false && goingUp == true) ||
                (hasAnomaly == true && goingUp == false)) // case 2, 3 -> 현재 층수--
        {
            Debug.Log($"정답 => 현재 층수--");
            currentFloor--;

            if(currentFloor <= 0) // currentFloor가 0 이하면 게임 클리어 로직
                OnGameClear();
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
        // Debug.Log("게임 클리어! S06 건물에서 탈출!");
        // TODO: 엔딩 연출, 씬 전환 등
    }
}
