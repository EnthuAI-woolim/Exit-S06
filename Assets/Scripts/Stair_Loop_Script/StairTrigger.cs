using UnityEngine;

public class StairTrigger : MonoBehaviour
{
    [Header("이 계단이 위로 가는 계단인지? (true=위층, false=아래층)")]
    public bool isUpStair = true;

    [Header("계단을 지난 뒤 플레이어가 도착할 위치")]
    public Transform destinationPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        var gs = GameState.Instance;
        var am = AnomalyManager.Instance;

        if (gs == null || am == null) return;

        // 1) 선택 결과 반영 (카운트/초기화)
        gs.ApplyStairChoice(isUpStair, am.HasAnomaly);

        // 2) 6층 / 7층 등 다음 위치로 텔레포트
        if (destinationPoint != null)
        {
            other.transform.position = destinationPoint.position;
        }
    }
}
