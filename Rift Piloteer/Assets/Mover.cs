using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public Transform Child;
    public GameObject Fragments;
    public GameObject SolidObj;
    public MeshCollider meshCollider;
    bool isDestroyed;
    public float destructionTimer;
    public static bool canSlowTime;

    private void Start()
    {
        
        if(Child!=null)
            Child.rotation = Quaternion.Euler(Random.Range(-30, 30), Random.Range(0, 360), Random.Range(-20, 20));
    }
    private void Update()
    {
        if (!isDestroyed)
        {
            transform.Translate(Vector3.forward * -Spawner.Speed * Spawner.SpeedMod * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.forward * -Spawner.DestroyedObstaclesSpeed * Time.deltaTime);
        }
        if (transform.position.z < -70)
        {
            Destroy(gameObject);
        }
    }

    public void Destruction()
    {
        isDestroyed = true;
        meshCollider.enabled = false;
        SolidObj.SetActive(false);
        if (Fragments != null)
        {
            Fragments.SetActive(true);
        }
        StartCoroutine(DestructionSequence());
    }

    IEnumerator DestructionSequence()
    {
        if (canSlowTime)
        {
            Time.timeScale = 0.1f;
            yield return new WaitForSecondsRealtime(0.7f);
            Time.timeScale = 1f;
        }
        yield return new WaitForSeconds(destructionTimer);
        Destroy(gameObject);
    }
}
