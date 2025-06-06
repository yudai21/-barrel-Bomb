using UnityEngine;
using UnityEngine.UI; // Sliderの使用

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 50; // 最大HP
    private int currentHealth; // 現在のHP

    public GameObject hpBarPrefab; // HPバーのプレハブ
    private Slider hpBarSlider; // HPバーのスライダー
    private Transform hpBarTransform; // HPバーのTransform

    void Start()
    {
        currentHealth = maxHealth; // 初期HPを設定

        // HPバーを生成
        GameObject hpBarInstance = Instantiate(hpBarPrefab, transform.position, Quaternion.identity);
        hpBarSlider = hpBarInstance.GetComponentInChildren<Slider>();
        hpBarTransform = hpBarInstance.transform;

        // HPバーの初期値を設定
        UpdateHPBar();
    }

    void Update()
    {
        // HPバーを敵の上に追従させる
        if (hpBarTransform != null)
        {
            Vector3 offset = new Vector3(0, 2.0f, 0); // 敵の上に表示するためのオフセット
            hpBarTransform.position = transform.position + offset;
            hpBarTransform.LookAt(Camera.main.transform); // カメラに向けて回転
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // ダメージを減少
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // HPが負の値にならないように制限
        UpdateHPBar();

        if (currentHealth <= 0)
        {
            Die(); // HPが0以下の場合、破壊
        }
    }

    private void UpdateHPBar()
    {
        if (hpBarSlider != null)
        {
            hpBarSlider.value = (float)currentHealth / maxHealth; // HPを正規化して設定
        }
    }

    private void Die()
    {
        // HPバーを削除
        if (hpBarTransform != null)
        {
            Destroy(hpBarTransform.gameObject);
        }

        Debug.Log("Enemy Destroyed");
        Destroy(gameObject); // 敵を破壊
    }
}
