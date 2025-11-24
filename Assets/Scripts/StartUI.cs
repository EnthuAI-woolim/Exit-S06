using UnityEngine;

public class StartUI : MonoBehaviour
{
    [Header("start main panel")]
    public GameObject startPanel;

    [Header("info panel")]
    public GameObject infoPanel;

    [Header("Text_info1")]
    public GameObject Text_Info1;

    [Header("Button info next1")]
    public GameObject Button_Next1;

    [Header("Text_info2")]
    public GameObject Text_Info2;

    [Header("Button info next2")]
    public GameObject Button_Next2;

    [Header("Text_info3")]
    public GameObject Text_Info3;

    [Header("Button info next3")]
    public GameObject Button_Next3;

    [Header("Text_info4")]
    public GameObject Text_Info4;

    [Header("Button_info_close")]
    public GameObject Button_CloseInfo;

    [Header("before start TimeScale")]
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
        {
            startPanel.SetActive(false);
            infoPanel.SetActive(true);
            Text_Info1.SetActive(true);
            Button_Next1.SetActive(true);
        }
    }

    public void OnClickInfo_Next1()
    {
        if (infoPanel != null)
        {
            Text_Info1.SetActive(false);
            Button_Next1.SetActive(false);
            Text_Info2.SetActive(true);
            Button_Next2.SetActive(true);
        }
    }

    public void OnClickInfo_Next2()
    {
        if (infoPanel != null)
        {
            Text_Info2.SetActive(false);
            Button_Next2.SetActive(false);
            Text_Info3.SetActive(true);
            Button_Next3.SetActive(true);
        }
    }

    public void OnClickInfo_Next3()
    {
        if (infoPanel != null)
        {
            Text_Info3.SetActive(false);
            Button_Next3.SetActive(false);
            Text_Info4.SetActive(true);
            Button_CloseInfo.SetActive(true);
        }
    }

    public void OnClickCloseInfo()
    {
        if (infoPanel != null)
        {
            infoPanel.SetActive(false);
            startPanel.SetActive(true);
        }
    }
}
