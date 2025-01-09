using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ポータルデータを格納するクラス
[System.Serializable]
public class PortalData
{
    public Vector3 position; // ポータルの位置
    public Vector3 rotation; // ポータルの回転（角度）
}

public class BossSummonAttack : MonoBehaviour
{
    public GameObject portalPrefab;
    public PortalData[] portalDataArray; // ポータルの位置と回転の配列
    public float spawnInterval = 5f;
    private bool isSummoning = false;
    private List<Portal> activePortals = new List<Portal>();

    void Update()
    {
        if (!isSummoning && activePortals.Count == 0)
        {
            StartCoroutine(SummonPortals());
        }
    }

    public void RegisterPortal(Portal portal)
    {
        if (!activePortals.Contains(portal))
        {
            activePortals.Add(portal);
        }
    }

    public void UnregisterPortal(Portal portal)
    {
        if (activePortals.Contains(portal))
        {
            activePortals.Remove(portal);
        }
    }

    IEnumerator SummonPortals()
    {
        isSummoning = true;

        // ポータルを生成
        foreach (PortalData portalData in portalDataArray)
        {
            GameObject portal = Instantiate(portalPrefab, portalData.position, Quaternion.Euler(portalData.rotation)); // 回転を適用
            yield return new WaitForSeconds(0.5f); // ポータル生成間隔
        }

        // 次の攻撃まで待機
        yield return new WaitForSeconds(spawnInterval);
        isSummoning = false;
    }
}
