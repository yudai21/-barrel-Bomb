using System.Collections;
using UnityEngine;

public class BossSpikeAttack : MonoBehaviour
{
    public GameObject spikePrefab; 
    public Transform playerTransform;
    public float spawnInterval; // スパイク生成間隔
    public int spikeCount; // 一度に生成するスパイクの数
    public float spawnRadius; // スパイク生成範囲（半径）
    public Color spawnAreaColor = Color.blue;
    public float spikeYPosition; // スパイクの生成(y軸)
    public Animator bossAnimator;
    public AudioSource attackAudioSource;
    public AudioClip spikeAttackSound;
    private float timer;
    private bool isAttacking = false;

    void Update()
    {
        if (!isAttacking)
        {
            timer += Time.deltaTime;

            if (timer >= spawnInterval)
            {
                StartCoroutine(SpawnSpikes());
                timer = 0f;
            }
        }
    }

    IEnumerator SpawnSpikes()
    {
        isAttacking = true;

        if (attackAudioSource != null && spikeAttackSound != null)
        {
            attackAudioSource.PlayOneShot(spikeAttackSound);
        }

        if (bossAnimator != null)
        {
            bossAnimator.SetTrigger("SpikeAttack");
        }

        yield return new WaitForSeconds(2f);

        // 指定された数のスパイクを生成
        for (int i = 0; i < spikeCount; i++)
        {
            // ボスの周りのランダムな位置にスパイク生成
            Vector3 spawnPosition = transform.position + (Random.insideUnitSphere * spawnRadius);
            spawnPosition.y = spikeYPosition; // y軸は指定した場所

            GameObject spike = Instantiate(spikePrefab, spawnPosition, Quaternion.identity);

            // Spikeスクリプトにプレイヤーの位置を設定
            Spike spikeScript = spike.GetComponent<Spike>();
            if (spikeScript != null)
            {
                spikeScript.SetPlayerTarget(playerTransform);
            }
        }

        yield return new WaitForSeconds(spawnInterval);
        isAttacking = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = spawnAreaColor;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}
