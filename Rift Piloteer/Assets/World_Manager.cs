using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World_Manager : MonoBehaviour
{
    public Spawner spawner;
    public LandSpawner LandController;
    public FlashBangMainControl Flashing;
    public CoreEventManager coreEventManager;
    public int ScoreToChange;
    public int CurrentScore;
    public int CurrentWorldID;
    public int WorldID;
    public float defaultFogDensity;
    public Material Water;
    public Material Grass;
    public Material Desert;
    public Material CurrentWorldMaterial;
    public GameObject[] WaterObstacles;
    public GameObject[] GrassObstacles;
    public GameObject[] DesertObstacles;
    public GameObject[] CurrentObstacles;
    public Animator inPortalAnim;
    Input_Manager IManager;


    private void Start()
    {
        IManager = GetComponent<Input_Manager>();
        CurrentWorldID = 0;
        WorldID = Random.Range(1, 3);
        WorldController();
        defaultFogDensity = RenderSettings.fogDensity;
        coreEventManager.backToMenu.AddListener(ResetWorld);
        coreEventManager.ChangeMaterialSpeed.AddListener(WorldSpeedController);
    }
    private void Update()
    {
        WaitForChangeWorld();
    }
    void WaitForChangeWorld()
    {
        if (CurrentScore >= ScoreToChange)
        {
            spawner.StopSpawn();
            CurrentScore = 0;
            ChangeWorld();
            print("World was changed");
        }
    }
    public void WorldSpeedController()
    {
        //CurrentWorldMaterial.SetFloat("Speed", -1 * Spawner.SpeedMod);
        Desert.SetFloat("Speed", -1 * Spawner.SpeedMod);
        Water.SetFloat("Speed", -1 * Spawner.SpeedMod);
        Grass.SetFloat("Speed", -1 * Spawner.SpeedMod);
    }

    void WorldController()
    {
        switch (CurrentWorldID)
        {
            case 2:
                CurrentObstacles = WaterObstacles;
                CurrentWorldMaterial = Water;
                break;
            case 1:
                CurrentObstacles = GrassObstacles;
                CurrentWorldMaterial = Grass;
                break;
            case 0:
                CurrentObstacles = DesertObstacles;
                CurrentWorldMaterial = Desert;
                break;
            default:
                Debug.Log("WTF?");
                break;
        }
    }
    void ChangeWorld()
    {
        StartCoroutine(WorldChangeProcess());
    }

    void WhatIsNextWorld()
    {
        WorldID = Random.Range(0, 3);
        if (WorldID == CurrentWorldID)
        {
            WhatIsNextWorld();
        }
        else
        {
            CurrentWorldID = WorldID;
            WorldController();
        }
    }

    public void ResetWorld()
    {
        CurrentWorldID = 0;
        WorldController();
        LandController.RefreshMaterials();
    }

    IEnumerator WorldChangeProcess()
    {
        yield return new WaitForEndOfFrame();
        IManager.ReadyForCombo = false;
        coreEventManager.OpenPortal.Invoke();
        Mover.canSlowTime = false;

        yield return new WaitForSeconds(3.5f);
        WhatIsNextWorld();
        inPortalAnim.Play("Show");
        RenderSettings.fogDensity = 0f;
        coreEventManager.InPortal.Invoke();
        coreEventManager.StopAmbient.Invoke();
        Flashing.FlareEnd();
        LandController.DeactivatePlains();

        yield return new WaitForSeconds(5f);
        RenderSettings.fogDensity = defaultFogDensity;
        coreEventManager.OutPortal.Invoke();
        coreEventManager.ContinueAmbient.Invoke();
        spawner.StartSpawn();
        LandController.ActivatePlains();
        Flashing.FlareEnd();
        inPortalAnim.Play("Off");
        Mover.canSlowTime = true;
        IManager.ReadyForCombo = true;
    }
}
