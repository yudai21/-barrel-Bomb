using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossHealth : MonoBehaviour
{
    public int maxHealth = 100; // ボスの最大hp
    private int currentHealth; // ボスの現在のhp
    public Slider hpBar;

    // シーン名（クリアシーン）
    public string clearSceneName = "GameClear"; // クリアシーンの名前を設定

    void Start()
    {
        currentHealth = maxHealth;
        hpBar.maxValue = maxHealth;
        hpBar.value = currentHealth;
    }

    void Update()
    {
        if (Camera.main != null)
        {
            hpBar.transform.LookAt(Camera.main.transform);
            hpBar.transform.Rotate(0, 180, 0); // UIが反転しないように調整
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // hp減少
        Debug.Log("Boss HP: " + currentHealth);

        hpBar.value = currentHealth;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Boss Defeated!"); // ボスが倒されたことを確認
        SceneManager.LoadScene(clearSceneName); // クリアシーンに遷移
        Destroy(gameObject); // ボスオブジェクトを破壊
    }
}
