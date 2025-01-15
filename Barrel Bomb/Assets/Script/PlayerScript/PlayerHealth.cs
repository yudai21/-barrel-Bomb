using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth;
    private int currentHealth;
    public Image[] healthIcons; // hpのImage配列
    public string gameoverSceneName = "GameOver";

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // HPが0未満にならないように
        UpdateHealthUI();
        Debug.Log("Player HP: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void UpdateHealthUI()
    {
        // 各アイコンの有効/無効を設定
        for (int i = 0; i < healthIcons.Length; i++)
        {
            healthIcons[i].enabled = i < currentHealth; // 現在のHP以下のアイコンを非表示
        }
    }

    void Die()
    {
        SceneManager.LoadScene(gameoverSceneName);
        Destroy(gameObject);
    }
}
