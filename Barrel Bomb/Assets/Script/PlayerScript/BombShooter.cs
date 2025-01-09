using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombShooter : MonoBehaviour
{
    public GameObject bombPrefab; // 爆弾のプレハブ
    public Transform firePoint; // 爆弾発射位置(プレイヤーの前方)
    public Transform cameraTransform; // カメラのTransform
    public float bombForce = 10f; // 爆弾が発射される力
    public float cooldownTime = 1f; // クールタイム(1秒)

    private bool canShoot = true; // 発射できるかどうかのフラグ
    private float cooldownTimer = 0f; // クールタイム計測用タイマー

    void Update()
    {
        // クールタイム中であれば、タイマーを減少させる
        if (!canShoot)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0f)
            {
                canShoot = true; // クールダウン終了後に発射可能にする
            }
        }

        // 左クリックが押された場合、かつ発射可能なら爆弾発射
        if (Input.GetMouseButtonDown(0) && canShoot)
        {
            ShootBomb();
        }
    }

    void ShootBomb()
    {
        // 爆弾発射
        GameObject bomb = Instantiate(bombPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bomb.GetComponent<Rigidbody>();
        
        // カメラの向きに基づいて力を加える
        if (rb != null)
        {
            rb.AddForce(cameraTransform.forward * bombForce, ForceMode.Impulse);
        }

        // 発射後にクールタイム開始
        canShoot = false;
        cooldownTimer = cooldownTime;
    }
}
