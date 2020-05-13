using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounds : MonoBehaviour
{
    public PlayerHealth playerHealth;

    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (playerHealth != null)
            {
                playerHealth.Die();
            }
            else
            {
                Destroy(col.gameObject);
            }
        }
    }
}
