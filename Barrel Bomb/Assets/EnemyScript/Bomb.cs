using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Boss") || collision.gameObject.CompareTag("WeakSpot"))
        {
            Destroy(gameObject);
        }
    }
}
