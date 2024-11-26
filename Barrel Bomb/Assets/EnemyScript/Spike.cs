using UnityEngine;

public class Spike : MonoBehaviour
{
    public Transform playerTransform;
    public float speed; // スパイクの移動速度
    public float rotationSpeed; // スパイクの回転速度

    // ターゲットを設定
    public void SetPlayerTarget(Transform target)
    {
        playerTransform = target;
    }

    private void Update()
    {
        // プレイヤーが存在する場合
        if (playerTransform != null)
        {
            // 先端部分の向きをプレイヤーに向ける
            Vector3 direction = (playerTransform.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);

            // 回転の補間（スムーズに回転）
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);

            // 先端部分移動
            transform.position += transform.forward * speed * Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // プレイヤーに当たった場合の処理
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(1); // ダメージ量は必要に応じて調整
            }
            Destroy(gameObject); // スパイクを削除
        }

        // 地面や障害物に当たった場合も削除
        if (collision.gameObject.CompareTag("Bomb") || collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
