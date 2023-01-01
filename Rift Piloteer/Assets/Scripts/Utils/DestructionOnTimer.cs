using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructionOnTimer : MonoBehaviour
{
    public float timeBeforeDestruction;

    public void Awake()
    {
        StartCoroutine(DestructionSequence());
    }

    IEnumerator DestructionSequence()
    {
        yield return new WaitForSeconds(timeBeforeDestruction);
        Destroy(gameObject);
    }
}
