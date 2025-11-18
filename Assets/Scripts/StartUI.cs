using UnityEngine;

public class StartUI : MonoBehaviour
{
    [Header("메인 시작 패널")]
    public GameObject startPanel;

    [Header("정보 패널 (처음엔 비활성화)")]
    public GameObject infoPanel;

    [Header("시작 전에 TimeScale을 멈출지 여부")]
    public bool pauseBeforeStart = true;

    void Start()
    {
        if (startPanel != null) startPanel.SetActive(true);
        if (infoPanel != null) infoPanel.SetActive(false);

        if (pauseBeforeStart)
            Time.timeScale = 0f;
    }

    public void OnClickStart()
    {
        if (startPanel != null)
            startPanel.SetActive(false);

        if (pauseBeforeStart)
            Time.timeScale = 1f;
    }

    public void OnClickInfo()
    {
        if (infoPanel != null)
            infoPanel.SetActive(true);
    }

    public void OnClickCloseInfo()
    {
        if (infoPanel != null)
            infoPanel.SetActive(false);
    }
}
