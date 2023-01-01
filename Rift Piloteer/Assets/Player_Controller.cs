using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    Input_Manager Manager;
    AbilityManager abilityManager;
    Progress_Manager progress_Manager;
    public CoreEventManager coreEventManager;
    public float MovementSpeed;
    public Transform Ship;
    public float ShipRotationSpeed;
    public float MaxShipRotation;
    public Transform Circle;
    public float CircleRotationSpeed;
    public float IdleCircleRotationSpeed;
    public float abilityFillIndex;
    float H;
    float VisualH;
    Animator Anim;
    public GameObject VowEffect;
    public bool CanDie;
    public bool CanDestroyObstacles = true;
    public float slowmoMultiplier;
    public float slowmoDuration;
    public AudioSource nearCollision;


    private void Start()
    {
        CanDie = true;
        Anim = GetComponentInChildren<Animator>();
        Manager = GameObject.Find("Managers").GetComponent<Input_Manager>();
        progress_Manager = GameObject.Find("Managers").GetComponent<Progress_Manager>();
        abilityManager = GameObject.Find("Managers").GetComponent<AbilityManager>();
        coreEventManager = GameObject.Find("Managers").GetComponent<CoreEventManager>();
        abilityManager.chargeBit = abilityFillIndex;
    }
    private void Update()
    {
        InputToVisual();
        InputToGameplay();
        //CheckCombo();
    }
    void InputToGameplay()
    {
        H = Manager.Horizontal;
        if (H > 0.1f)
            transform.position = Vector3.MoveTowards(transform.position, transform.position + Vector3.right, MovementSpeed * Time.deltaTime * (H - 0.1f));
        else if (H < -0.1f)
            transform.position = Vector3.MoveTowards(transform.position, transform.position + Vector3.right, MovementSpeed * Time.deltaTime * (H + 0.1f));
    }
    void InputToVisual()
    {
        VisualH = Manager.VisualHorizontal;
        float CircleSpeed;
        if (Manager.CanMove)
        {
            if (CoreSettings.AccelerometerIsActive)
            {
                float angle = Mathf.LerpAngle(Ship.eulerAngles.z, VisualH * -90f, ShipRotationSpeed);
                Ship.eulerAngles = new Vector3(0, 0, angle);
            }
            else
            {
                Ship.rotation = Quaternion.Lerp(Ship.rotation, Quaternion.Euler(0, 0, H * -90f), ShipRotationSpeed);
            }
        }
        else
            Ship.rotation = Quaternion.Euler(Vector3.zero);
        if (H > 0.1f)
        {
            IdleCircleRotationSpeed = 1;
            CircleSpeed = Mathf.MoveTowards(CircleRotationSpeed, 1, 1);
            Circle.rotation = Quaternion.Euler(0, 0, Circle.transform.rotation.eulerAngles.z - CircleRotationSpeed * Time.deltaTime * Mathf.Abs(H));
        }
        else if (H < -0.1f)
        {
            IdleCircleRotationSpeed = -1;
            CircleSpeed = Mathf.MoveTowards(CircleRotationSpeed, -1, 1); Circle.rotation = Quaternion.Euler(0, 0, Circle.transform.rotation.eulerAngles.z + CircleRotationSpeed * Time.deltaTime * Mathf.Abs(H));
        }
        else
            CircleSpeed = Mathf.MoveTowards(CircleRotationSpeed, 0, 1);
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            if (CanDie)
            {
                Manager.Horizontal = 0;
                Anim.Play("Destroyed");
                Manager.CanMove = false;
                Manager.Progress.SpawnController.CanSpawn = false;
                Manager.coreEventManager.shipDestroyed.Invoke();
                Manager.coreEventManager.ShowResults.Invoke();
            }
            if(CanDestroyObstacles)
                collision.gameObject.GetComponent<ObstacleDestructionFuctonTransfer>().DestructionFunction();
        }
        if (collision.collider.CompareTag("Vow"))
        {
            Instantiate(VowEffect, Circle.transform.position, Quaternion.identity, Circle.transform);
            Manager.Progress.GetVow();
            Destroy(collision.collider.gameObject);
        }
    }

    

    public void ImproveCombo()
    {
        if (Manager.ReadyForCombo)
        {
            nearCollision.pitch = 1f + progress_Manager.comboState * 0.15f;
            nearCollision.Play();
            coreEventManager.ComboStep.Invoke();
            StartCoroutine(SlowMotion(slowmoMultiplier, slowmoDuration));
        }
    }

    IEnumerator SlowMotion(float slowedTimeScale, float slowedTime)
    {
        Time.timeScale = slowedTimeScale;
        yield return new WaitForSecondsRealtime(slowedTime);
        if (Time.timeScale == slowedTimeScale)
        {
            Time.timeScale = 1f;
        }
    }
}
