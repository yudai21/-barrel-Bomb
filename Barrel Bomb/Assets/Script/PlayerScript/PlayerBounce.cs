using UnityEngine;

public class PlayerBounce : MonoBehaviour
{
    public float bounceForce = 10f; // 反発力の強さ
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall")) // 壁に触れたかどうか確認
        {
            // 衝突点の法線を取得して反発方向を計算
            Vector3 collisionNormal = collision.contacts[0].normal;
            Vector3 bounceDirection = collisionNormal.normalized;

            // 反発力を加える
            rb.AddForce(bounceDirection * bounceForce, ForceMode.Impulse);

            Debug.Log("Wall hit! Bouncing back.");
        }
    }
}
