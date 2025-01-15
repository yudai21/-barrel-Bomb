using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PortalData
{
    public Vector3 position;
    public Vector3 rotation;
}

public class BossSummonAttack : MonoBehaviour
{
    public GameObject portalPrefab; // ポータルのプレハブ
    public PortalData[] portalDataArray; // ポータル位置と回転の配列
    public float spawnInterval = 5f; // ポータル生成間隔
    public Animator animator;
    private float timer;
    public AudioSource attackAudioSource;
    public AudioClip summonAttack;
    private bool isAttacking = false;
    private List<Portal> activePortals = new List<Portal>(); // 現在アクティブなポータルのリスト

    void Update()
    {
        if (!isAttacking)
        {
            timer += Time.deltaTime;

            if (timer >= spawnInterval)
            {
                StartCoroutine(SummonPortals());
                timer = 0f;
            }
        }
    }

    IEnumerator SummonPortals()
    {
        // 攻撃アニメーションとサウンドを再生
        if (animator != null)
        {
            animator.SetTrigger("BossSummonAttack");
        }
        if (attackAudioSource != null && summonAttack != null)
        {
            attackAudioSource.PlayOneShot(summonAttack);
        }

        // ポータルを生成
        foreach (PortalData portalData in portalDataArray)
        {
            GameObject portalInstance = Instantiate(
                portalPrefab, 
                portalData.position, 
                Quaternion.Euler(portalData.rotation)
            );

            Portal portalScript = portalInstance.GetComponent<Portal>();
            if (portalScript != null)
            {
                activePortals.Add(portalScript); // リストに追加

                // ポータルが破壊されたときにリストから自動で削除
                portalScript.OnDestroyed += () => activePortals.Remove(portalScript);
            }
        }

        // 次の生成まで待機
        yield return new WaitForSeconds(spawnInterval);
        isAttacking = false;
    }
}
