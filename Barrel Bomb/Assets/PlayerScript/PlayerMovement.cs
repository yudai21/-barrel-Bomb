using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // プレイヤーの移動速度
    public Transform cameraTransform; // カメラのTransform(FPS視点カメラ)
    private Rigidbody rb;        // Rigidbodyへの参照

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // WASDキーの入力を取得
        float moveHorizontal = Input.GetAxis("Horizontal"); // A/Dキー or 左/右矢印 
        float moveVertical = Input.GetAxis("Vertical");     // W/Sキー or 上/下矢印

        // 移動ベクトルを計算
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        // 水平方向無視（Y軸の値を0）することで、地面に沿った動きだけにする
        forward.y = 0f;
        right.y = 0f;

        //  ベクトルを正規化して長さを1にする
        forward.Normalize();
        right.Normalize();

        // プレイヤーの移動ベクトル計算
        Vector3 movement = (forward * moveVertical + right * moveHorizontal) * moveSpeed * Time.deltaTime;

        // Rigidbodyを使って移動させる
        rb.MovePosition(rb.position + movement);
    }
}
