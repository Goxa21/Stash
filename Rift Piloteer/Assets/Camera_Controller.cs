using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
    public CoreEventManager Manager;
    public float CameraRotationSpeed;
    public float CameraSpeed;
    public Transform MenuPositon;
    public Transform GamePosition;
    public Transform CurrentPosition;
    private void Awake()
    {
        Manager = GameObject.Find("Managers").GetComponent<CoreEventManager>();
        Manager.gameStart.AddListener(GameStart);
        Manager.backToMenu.AddListener(GameMenu);
        Manager.backToMenu.Invoke();
    }
    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, CurrentPosition.position, CameraSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, CurrentPosition.rotation, CameraRotationSpeed * Time.deltaTime); 
    }
    public void GameStart()
    {
        CurrentPosition = GamePosition;
    }
    public void GameMenu()
    {
        CurrentPosition = MenuPositon;
    }
}
