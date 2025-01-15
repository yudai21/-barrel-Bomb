using UnityEngine;

public class CursorManager : MonoBehaviour
{
    void Start()
    {
        // カーソルを表示
        Cursor.visible = true;
        // カーソルのロックを解除
        Cursor.lockState = CursorLockMode.None;
    }
}
