using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 3f; // 移動速度
    public float attackRange = 2f; // 攻撃範囲
    public int attackDamage = 10; // 攻撃ダメージ
    public float attackInterval = 2f; // 攻撃間隔
    private Transform player; // PlayerのTransform
    private bool isAttacking = false; // 攻撃中かどうか

    private Animator animator; // Animatorコンポーネント
    private EnemyHealth enemyHealth; // EnemyHealthスクリプト参照

    void Start()
    {
        // Playerのオブジェクトを探す
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }

        // EnemyHealthスクリプトを取得
        enemyHealth = GetComponent<EnemyHealth>();

        // Animatorを取得
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (player == null) return;

        // Playerとの距離を計算
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (!isAttacking)
        {
            if (distanceToPlayer > attackRange)
            {
                // Playerが攻撃範囲外なら近づく
                MoveTowardsPlayer();
            }
            else
            {
                // Playerが攻撃範囲内なら攻撃
                StartCoroutine(AttackPlayer());
            }
        }
    }

    void MoveTowardsPlayer()
    {
        // 歩行アニメーションを再生
        animator.SetBool("isWalking", true);

        // Playerの方向を計算
        Vector3 direction = (player.position - transform.position).normalized;

        // Playerに向かって移動
        transform.position += direction * moveSpeed * Time.deltaTime;

        // Playerの方向を向く
        transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
    }

    IEnumerator AttackPlayer()
    {
        isAttacking = true;

        // 攻撃アニメーションを再生
        animator.SetBool("isWalking", false);
        animator.SetBool("isAttacking", true);

        // 攻撃アニメーションの再生時間分待機
        yield return new WaitForSeconds(0.5f); // アニメーションの長さに合わせる

        // Playerにダメージを与える
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(attackDamage);
        }

        Debug.Log("Enemy attacked the Player!");

        // 攻撃間隔分待つ
        yield return new WaitForSeconds(attackInterval - 0.5f);

        // 攻撃アニメーションを終了
        animator.SetBool("isAttacking", false);

        isAttacking = false;
    }

    // ボムが敵に衝突した時の処理
    private void OnCollisionEnter(Collision collision)
    {
        // ボムに衝突したらダメージを与える
        if (collision.gameObject.CompareTag("Bomb"))
        {
            // EnemyHealthのTakeDamageメソッドを呼び出し
            enemyHealth.TakeDamage(50); // ボムのダメージ量
            Destroy(collision.gameObject); // ボムを破壊
        }
    }
}
