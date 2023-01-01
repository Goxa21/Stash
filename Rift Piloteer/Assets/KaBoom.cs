using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KaBoom : MonoBehaviour
{
    public float MaxSphereRadius;
    public float SphereSpeed;
    float curScale;
    private void Start()
    {
        Mover.canSlowTime = false;
        StartCoroutine(Destroyer());
    }
    private void Update()
    {
        SphereScaling();
    }
    void SphereScaling()
    {
        curScale = Mathf.MoveTowards(curScale, MaxSphereRadius, SphereSpeed * Time.deltaTime);
        transform.localScale = new Vector3(curScale, curScale, curScale);
        print("Scale " + curScale);
    }
    IEnumerator Destroyer()
    {
        while (curScale < MaxSphereRadius)
        {
            yield return new WaitForEndOfFrame();
        }
        Mover.canSlowTime = true;
        Destroy(gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Obstacle"))
        {
            collision.gameObject.GetComponent<ObstacleDestructionFuctonTransfer>().DestructionFunction();
        }
    }
}
