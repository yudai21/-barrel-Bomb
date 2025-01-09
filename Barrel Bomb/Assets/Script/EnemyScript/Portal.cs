using UnityEngine;
using UnityEngine.UI;

public class Portal : MonoBehaviour
{
    public int maxHealth = 100; // 最大HP
    private int currentHealth;

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
        // HPバーをポータルの上に追従させる
        if (hpBarTransform != null)
        {
            Vector3 offset = new Vector3(0, 2.0f, 0); // ポータルの上に表示するためのオフセット
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

        Debug.Log("Portal Destroyed");
        Destroy(gameObject); // ポータルを破壊
    }

    private void OnCollisionEnter(Collision collision)
    {
        // ボムに衝突したらダメージを与える
        if (collision.gameObject.CompareTag("Bomb"))
        {
            TakeDamage(50); // ボムのダメージ量
            Destroy(collision.gameObject); // ボムを破壊
        }
    }
}
