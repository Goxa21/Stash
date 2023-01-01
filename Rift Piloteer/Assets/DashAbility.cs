using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAbility : MonoBehaviour
{
    public float AblilityDuration;
    public float SpeedModificator;
    public float DensityModificator;
    public int ScoreModificator;
    World_Manager WorldManager;
    CoreEventManager coreEventManager;
    Player_Controller Player;
    public float timer = 0f;
    public Animator camAnim;
    public Animator engineAnim;
    public Camera_Controller camera_Controller;
    public GameObject boostParticles;
    float defaultCameraSpeed;
    public float buffedCameraSpeed;
    float defaultManeuverSpeed;
    public float buffedManeuverSpeed;
    public MeshRenderer shipMesh;
    public MeshRenderer ringMesh;
    public Material[] defaultShipMaterials;
    public Material[] defaultRingMaterials;
    public Material[] dashMaterial;


    private void Start()
    {
        WorldManager = GameObject.Find("Managers").GetComponent<World_Manager>();
        coreEventManager = GetComponent<Player_Controller>().coreEventManager;
        camAnim = GameObject.Find("InGameCameraDriver").GetComponent<Animator>();
        camera_Controller = Camera.main.gameObject.GetComponent<Camera_Controller>();
        defaultCameraSpeed = camera_Controller.CameraSpeed;
        coreEventManager.abilityActivation.AddListener(ActivateAbility);
        Player = GetComponent<Player_Controller>();
        defaultManeuverSpeed = Player.MovementSpeed;
        defaultShipMaterials = shipMesh.materials;
        defaultRingMaterials = ringMesh.materials;
    }
    void ActivateAbility()
    {
        if (timer == 0)
        {
            Spawner.SpeedMod = SpeedModificator;
            Spawner.DensityMod = DensityModificator;
            Progress_Manager.ScoreMod = ScoreModificator;
            Player.CanDestroyObstacles = false;
            Player.CanDie = false;
            coreEventManager.ChangeMaterialSpeed.Invoke();
            
            ringMesh.materials = dashMaterial;
            shipMesh.materials = dashMaterial;
            
            camAnim.SetTrigger("SpeedUp");
            engineAnim.SetTrigger("EngineUp");
            boostParticles.SetActive(true);
            StartCoroutine(TimeToDeactivate());
        }
        else
        {
            timer = 0;
        }
    }
    IEnumerator TimeToDeactivate()
    {
        while (timer <= AblilityDuration && WorldManager.CurrentScore < WorldManager.ScoreToChange)
        {
            camera_Controller.CameraSpeed += (buffedCameraSpeed - camera_Controller.CameraSpeed) * Time.deltaTime;
            Player.MovementSpeed += (buffedManeuverSpeed - Player.MovementSpeed) * Time.deltaTime;

            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
        }
        camera_Controller.CameraSpeed = defaultCameraSpeed;
        Player.MovementSpeed = defaultManeuverSpeed;
        camAnim.SetTrigger("BackToIdle");
        engineAnim.SetTrigger("BackToIdle");
        boostParticles.SetActive(false);
        Spawner.DensityMod = 1;
        Spawner.SpeedMod = 1;
        Progress_Manager.ScoreMod = 1;
        shipMesh.materials = defaultShipMaterials;
        ringMesh.materials = defaultRingMaterials;
        coreEventManager.ChangeMaterialSpeed.Invoke();
        yield return new WaitForSeconds(1f);
        Player.CanDie = true;
        Player.CanDestroyObstacles = true;
        coreEventManager.abilityDeactivation.Invoke();
        timer = 0f;

    }
}
