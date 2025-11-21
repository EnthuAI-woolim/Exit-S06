using UnityEngine;
using System;

public class StairTrigger : MonoBehaviour
{
    [Header("is it up stairs? (check=up, no check=down)")]
    public bool isUpStair = true;

    [Header("stair next player position")]
    public Transform destinationPoint;

    private void OnTriggerEnter(Collider other)
    {
        //if (!other.CompareTag("Player")) return;

        var gs = GameState.Instance;
        var am = AnomalyManager.Instance;

        //if (gs == null || am == null) return;

        Debug.Log($"isUpStair = {isUpStair}");
        Debug.Log($"am.HasAnomaly = {am.HasAnomaly}");

        // 1) 선택 결과 반영 (카운트/초기화)
        gs.ApplyStairChoice(isUpStair, am.HasAnomaly);

        // 2) 6층 / 7층 등 다음 위치로 텔레포트
        if (destinationPoint != null)
        {
            other.transform.position = destinationPoint.position;
        }
    }
}
