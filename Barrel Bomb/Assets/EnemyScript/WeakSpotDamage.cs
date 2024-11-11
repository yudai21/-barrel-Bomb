using UnityEngine;

public class WeakSpot : MonoBehaviour
{
    public int damageAmount = 10;    // 弱点が受けるダメージ量
    private BossHealth bossHealth;    // ボスのHP管理スクリプトの参照

    void Start()
    {
        // ボスのHP管理スクリプトを取得
        GameObject boss = GameObject.FindGameObjectWithTag("Boss");
        if (boss != null)
        {
            bossHealth = boss.GetComponent<BossHealth>(); // GameObjectに対してGetComponentを呼び出す
        }
        else
        {
            Debug.LogError("Boss not found! Make sure the Boss object is tagged with 'Boss'.");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bomb")) // 爆弾と衝突した場合
        {
            if (bossHealth != null)
            {
                bossHealth.TakeDamage(damageAmount);  // ボスにダメージを与える
            }
            gameObject.SetActive(false);            // 弱点を非表示にする
        }
    }
}
