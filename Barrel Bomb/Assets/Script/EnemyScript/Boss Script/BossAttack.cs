using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public GameObject attackEffectPrefab; // 範囲攻撃エフェクトのプレハブ
    public float attackInterval; // 範囲攻撃を行う間隔
    public float attackRadius; // 攻撃範囲の半径
    public int damage; // 攻撃のダメージ量
    public Animator animator;
    public AudioSource attackAudioSource;
    public AudioClip attackSound;
    public float effectDurationAfterAttack = 3f;
    private float timer;
    private GameObject attackEffectInstance;
    private bool isAttacking = false; // 範囲攻撃が実行中かどうか

    void Start()
    {
        // エフェクトプレハブを非表示のままインスタンス化
        attackEffectInstance = Instantiate(attackEffectPrefab, transform.position, Quaternion.identity);
        attackEffectInstance.SetActive(false); // 初期状態で非表示
    }

    void Update()
    {
        if (!isAttacking) // 範囲攻撃が実行中でない場合のみタイマーをカウント
        {
            timer += Time.deltaTime;

            // 攻撃間隔に達したら範囲攻撃を発動
            if (timer >= attackInterval)
            {
                StartCoroutine(PerformDelayedAreaAttack());
                timer = 0f; // タイマーリセット
            }
        }
    }

    IEnumerator PerformDelayedAreaAttack()
    {
        isAttacking = true;
        
        // 攻撃エフェクト生成
        attackEffectInstance.transform.position = transform.position;
        attackEffectInstance.SetActive(true);

       
        yield return new WaitForSeconds(2f);

        if (animator != null)
        {
            animator.SetTrigger("Attack");
        }

        if (attackAudioSource != null && attackSound != null)
        {
            attackAudioSource.PlayOneShot(attackSound);
        }

        yield return new WaitForSeconds(2f);

        // 攻撃範囲内のオブジェクトを検出してダメージを与える
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Player"))
            {
                PlayerHealth playerHealth = hitCollider.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(damage);
                }
            }
        }

        yield return new WaitForSeconds(effectDurationAfterAttack);

        attackEffectInstance.SetActive(false);
        isAttacking = false; 
    }

    // 攻撃範囲を可視化（デバッグ用）
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
