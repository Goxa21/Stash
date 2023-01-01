using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Input_Manager IManager;
    public Progress_Manager Progress;
    public World_Manager WorldManager;
    public CoreEventManager eventManager;
    public static float Speed;
    public static float SpeedMod;
    public static float DestroyedObstaclesSpeed;
    public float TimeUntilSpawn;
    public float StartDencity;
    public float Density;
    public static float DensityMod;
    public float SpeedPub;
    public float DestroyedObstaclesSpeedPub;
    public float MaxRange;
    public GameObject[] Obstacles;
    public GameObject Vows;
    public Transform Pen;
    public Transform Ruler;
    public float PenSpeed;
    public float PenMaxRange;
    float Mod;
    public bool CanSpawn = true;
    bool IsStart = true;

    private void Start()
    {
        DensityMod = 1;
        SpeedMod = 1;
        eventManager.gameStart.AddListener(StartSpawn);
        eventManager.gameStart.AddListener(FirstStartSpawn);
        eventManager.shipDestroyed.AddListener(StopSpawn);
    }
    private void Update()
    {
        Obstacles = WorldManager.CurrentObstacles;
        Ruler.transform.position = ShipReplacer.curShip.transform.position + new Vector3(0, 0, 500);
        Painter();
        if (Pen.localPosition.x > PenMaxRange)
        {
            Mod = 0;
            Pen.position -= new Vector3(0.5f, 0, 0);
        }
        else if(Pen.localPosition.x < -PenMaxRange)
        {
            Mod = 0;
            Pen.position += new Vector3(0.5f, 0, 0);
        }
    }
    void FirstStartSpawn()
    {
        IsStart = true;
    }
    public void StartSpawn()
    {
        StartCoroutine(CanSpawnDelayer());
    }
    IEnumerator CanSpawnDelayer()
    {
        yield return new WaitForEndOfFrame();
        if (IManager.CanMove)
        {
            if (IsStart)
            {
                yield return new WaitForSeconds(TimeUntilSpawn);
                IsStart = false;
                Speed = SpeedPub;
                DestroyedObstaclesSpeed = DestroyedObstaclesSpeedPub;
                Density = StartDencity;
            }
            else
                yield return new WaitForEndOfFrame();

            CanSpawn = true;
            Progress.StartCount();
            StartCoroutine(SpawnObstacles());
            StartCoroutine(SpawnVow());
            StartCoroutine(Drawing());
        }
    }
    public void StopSpawn()
    {
        CanSpawn = false;
        StopCoroutine(SpawnObstacles());
    }
    IEnumerator SpawnObstacles()
    {
        while (CanSpawn)
        {
            yield return new WaitForSeconds(Density / DensityMod);
            Instantiate(Obstacles[Random.Range(0, Obstacles.Length)], ShipReplacer.curShip.transform.position + new Vector3(Random.Range(-MaxRange, MaxRange), -10, 500), Quaternion.identity);
        }
    }
    IEnumerator SpawnVow()
    {
        while (CanSpawn)
        {
            yield return new WaitForSeconds(Density / DensityMod);
            Instantiate(Vows, new Vector3(Pen.position.x, Pen.position.y, Pen.position.z), Quaternion.identity);
        }
    }
    void Painter()
    {
        if(CanSpawn)
            Pen.transform.Translate(Vector3.right * PenSpeed * Mod * Time.deltaTime);
    }
    IEnumerator Drawing()
    {
        yield return new WaitForEndOfFrame();
        while (CanSpawn)
        {
            yield return new WaitForSeconds(Random.Range(0f, 5f));
            Mod = 1;
            yield return new WaitForSeconds(Random.Range(0f, 5f));
            Mod = 0;
            yield return new WaitForSeconds(Random.Range(0f, 5f));
            Mod = -1;
            yield return new WaitForSeconds(Random.Range(0f, 5f));
            Mod = 0;
        }
    }
}
