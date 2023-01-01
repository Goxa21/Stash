using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteToBillboard : MonoBehaviour
{
    public Transform cameraPosition;
    public Transform targetedSprite;

    public void Awake()
    {
        cameraPosition = Camera.main.transform;
    }
    void LateUpdate()
    {
        targetedSprite.LookAt(cameraPosition);
    }
}
