using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VowC_Controller : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
