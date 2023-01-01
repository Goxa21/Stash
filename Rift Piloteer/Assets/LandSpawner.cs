using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandSpawner : MonoBehaviour
{
    public World_Manager WorldManager;
    public float X = 0f;
    public GameObject[] Planes;
    public Material PlaneMaterial;
    public CoreEventManager coreEventManager;

    private void Awake()
    {
        coreEventManager.InPortal.AddListener(RefreshMaterials);
        RefreshMaterials();
    }
    private void Update()
    {
        
        if (ShipReplacer.curShip.transform.position.x - X > 200)
            MoveToRight();
        else if (ShipReplacer.curShip.transform.position.x - X < -200)
            MoveToLeft();
    }

    public void RefreshMaterials()
    {
        foreach (GameObject planes in Planes)
        {
            planes.GetComponent<MeshRenderer>().material = WorldManager.CurrentWorldMaterial;
        }
    }

    void MoveToRight()
    {
        X = X + 200;
        foreach (GameObject tr in Planes)
        {
            tr.transform.position = tr.transform.position + new Vector3(200, 0, 0);
        }
    }
    void MoveToLeft()
    {
        X = X - 200;
        foreach(GameObject tr in Planes)
        {
            tr.transform.position = tr.transform.position + new Vector3(-200, 0, 0);
        }
    }
    public void DeactivatePlains()
    {
        foreach (GameObject planes in Planes)
        {
            planes.SetActive(false);
        }
    }
    public void ActivatePlains()
    {
        foreach (GameObject planes in Planes)
        {
            planes.SetActive(true);
        }
    }
}
