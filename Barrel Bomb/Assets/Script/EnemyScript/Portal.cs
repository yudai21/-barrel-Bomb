using UnityEngine;

public class Portal : MonoBehaviour
{
    public int hp = 10;
    private BossSummonAttack bossScript;

    void Start()
    {
        // ボススクリプトの参照を取得
        bossScript = FindObjectOfType<BossSummonAttack>();
        if (bossScript != null)
        {
            bossScript.RegisterPortal(this);
        }
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;

        if (hp <= 0)
        {
            DestroyPortal();
        }
    }

    private void DestroyPortal()
    {
        if (bossScript != null)
        {
            bossScript.UnregisterPortal(this);
        }

        Destroy(gameObject);
    }
}
