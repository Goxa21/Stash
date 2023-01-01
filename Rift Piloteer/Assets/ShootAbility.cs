using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAbility : MonoBehaviour
{
    public float TimeBetweenShots;
    public int MaxNumberOfShots;
    int NumberOfShots;
    public GameObject BulletPref;
    public Transform BulletPos;
    bool canShoot = true;
    CoreEventManager coreEventManager;
    public Animator shotAnim;
    public Camera_Controller camera_Controller;
    public Animator camAnim;
    float defaultCameraSpeed;
    public float buffedCameraSpeed;
    public GameObject aimSubCanvas;
    public AudioSource BulletSource;


    private void Start()
    {
        coreEventManager = GetComponent<Player_Controller>().coreEventManager;
        coreEventManager.abilityActivation.AddListener(ActivateAbility);
        camera_Controller = Camera.main.gameObject.GetComponent<Camera_Controller>();
        camAnim = GameObject.Find("InGameCameraDriver").GetComponent<Animator>();
        defaultCameraSpeed = camera_Controller.CameraSpeed;
        NumberOfShots = MaxNumberOfShots;
    }
    void ActivateAbility()
    {
        if (canShoot)
        {
            StartCoroutine(ShootingSequence());
            Instantiate(aimSubCanvas);
            canShoot = false;
            camAnim.SetTrigger("Aim");
        }
    }
    IEnumerator Shooting()
    {
        yield return new WaitForSeconds(TimeBetweenShots);
        shotAnim.Play("Show");
        Instantiate(BulletPref, BulletPos.position, Quaternion.identity);
        canShoot = true;
        ActivateAbility();
    }

    IEnumerator ShootingSequence()
    {
        yield return new WaitForEndOfFrame();
        int shotsleft = NumberOfShots;
        while (shotsleft > 0)
        {
            float timer = 0;
            while (timer < TimeBetweenShots)
            {
                camera_Controller.CameraSpeed += (buffedCameraSpeed - camera_Controller.CameraSpeed) * Time.deltaTime;
                timer += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            shotAnim.Play("Show");
            Instantiate(BulletPref, BulletPos.position, Quaternion.identity);
            coreEventManager.abilityShot.Invoke();
            BulletSource.Play();
            shotsleft--;
        }
        yield return new WaitForSeconds(TimeBetweenShots);
        camera_Controller.CameraSpeed = defaultCameraSpeed;
        canShoot = true;
        coreEventManager.abilityDeactivation.Invoke();
        camAnim.SetTrigger("BackToIdle");
    }

}
