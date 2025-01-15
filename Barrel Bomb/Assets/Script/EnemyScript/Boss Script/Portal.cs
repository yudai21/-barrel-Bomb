using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Portal : MonoBehaviour
{
    public int maxHealth = 100; // 最大HP
    private int currentHealth;

    public GameObject hpBarPrefab; // HPバーのプレハブ
    private Slider hpBarSlider;
    private Transform hpBarTransform;

    public GameObject enemyPrefab; // エネミーのプレハブ
    public float enemySpawnInterval = 2f; // エネミー生成間隔

    public bool IsSpawningEnemies = false; // エネミー生成中かどうか
    private Coroutine spawnCoroutine; // エネミー生成のコルーチン

    // イベント: ポータルが破壊されたときに通知
    public event System.Action OnDestroyed;

    void Start()
    {
        currentHealth = maxHealth;

        // HPバーの生成
        GameObject hpBarInstance = Instantiate(hpBarPrefab, transform.position, Quaternion.identity);
        hpBarSlider = hpBarInstance.GetComponentInChildren<Slider>();
        hpBarTransform = hpBarInstance.transform;

        UpdateHPBar();

        // エネミーの生成を開始
        StartSpawningEnemies();
    }

    void Update()
    {
        // HPバーの位置更新
        if (hpBarTransform != null)
        {
            Vector3 offset = new Vector3(0, 2.0f, 0);
            hpBarTransform.position = transform.position + offset;
            hpBarTransform.LookAt(Camera.main.transform);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHPBar();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void UpdateHPBar()
    {
        if (hpBarSlider != null)
        {
            hpBarSlider.value = (float)currentHealth / maxHealth;
        }
    }

    private void Die()
    {
        // HPバーを削除
        if (hpBarTransform != null)
        {
            Destroy(hpBarTransform.gameObject);
        }

        // エネミー生成を停止
        StopSpawningEnemies();

        // イベント発火: 破壊を通知
        OnDestroyed?.Invoke();

        Debug.Log("Portal Destroyed");
        Destroy(gameObject);
    }

    private void StartSpawningEnemies()
    {
        if (!IsSpawningEnemies)
        {
            IsSpawningEnemies = true;
            spawnCoroutine = StartCoroutine(SpawnEnemies());
        }
    }

    private void StopSpawningEnemies()
    {
        if (IsSpawningEnemies)
        {
            IsSpawningEnemies = false;
            if (spawnCoroutine != null)
            {
                StopCoroutine(spawnCoroutine);
            }
        }
    }

    private IEnumerator SpawnEnemies()
    {
        while (IsSpawningEnemies)
        {
            // エネミーをポータルの位置に生成
            Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(enemySpawnInterval);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bomb"))
        {
            TakeDamage(50);
            Destroy(collision.gameObject);
        }
    }
}
