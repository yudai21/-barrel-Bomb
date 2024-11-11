using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    public float mouseSensitivity = 100f; // マウス感度
    public Transform playerBody;          // プレイヤーのBody (カメラの親オブジェクト)

    private float xRotation = 0f;         // 垂直回転の回転角度を保存

    void Start()
    {
        // マウスカーソルを非表示
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        transform.localRotation = Quaternion.Euler(0f, 0f, 0f); // カメラの回転をリセット
    }

    void Update()
    {
        // マウス入力を取得
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // 垂直方向の回転（カメラの上下を制御）
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // 上下の回転を90度に制御

        // カメラの垂直方向の回転
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // プレイヤーの水平方向の回転（左右の回転）
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
