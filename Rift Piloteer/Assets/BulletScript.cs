using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float Speed;
    public float DestructionTimer;
    public GameObject boomEffect;
    private void Start()
    {
        StartCoroutine(DestructionSequence());
    }
    private void Update()
    {
        transform.Translate(Vector3.forward * Speed * Time.deltaTime);
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            collision.gameObject.GetComponent<ObstacleDestructionFuctonTransfer>().DestructionFunction();
            Instantiate(boomEffect, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            Destroy(gameObject);
        }
    }
    IEnumerator DestructionSequence()
    {
        yield return new WaitForSeconds(DestructionTimer);
        Destroy(gameObject);
    }


}
