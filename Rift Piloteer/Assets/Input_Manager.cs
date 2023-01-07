using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Input_Manager : MonoBehaviour
{
    public Progress_Manager Progress;
    public CoreEventManager coreEventManager;
    public float Horizontal;
    public float VisualHorizontal;
    bool TurnLeft;
    bool TurnRight;
    public float TransitionSpeed;
    public float accelSensivity;
    public bool AccelerometerIsActive;
    public bool CanMove = true;
    public bool ReadyForCombo;
    private void Start()
    {
        coreEventManager.gameStart.AddListener(StartMovement);
        coreEventManager.shipDestroyed.AddListener(StopMovement);
    }
    private void Update()
    {
        if (CanMove)
        {

            if (Input.GetAxis("Horizontal")!=0)
            {
                Horizontal = Mathf.MoveTowards(Horizontal, Input.GetAxis("Horizontal"), TransitionSpeed * Time.deltaTime);
                VisualHorizontal = Input.GetAxis("Horizontal");
            }
            else
            {
                Horizontal = Mathf.MoveTowards(Horizontal, 0, TransitionSpeed * Time.deltaTime);
                VisualHorizontal = 0;
            }

            /*if (CoreSettings.AccelerometerIsActive)
            {
                //Horizontal = Input.acceleration.x * accelSensivity;
                if (Input.acceleration.x > 0.1f || Input.acceleration.x < -0.1f)
                {
                    Horizontal = Mathf.MoveTowards(Horizontal, Input.acceleration.x, TransitionSpeed * Time.deltaTime);
                    VisualHorizontal = Input.acceleration.x;
                }
                else
                {
                    Horizontal = Mathf.MoveTowards(Horizontal, 0, TransitionSpeed * Time.deltaTime);
                    VisualHorizontal = 0;
                }
            }
            else
            {
                if (TurnLeft && !TurnRight)
                    Horizontal = Mathf.MoveTowards(Horizontal, -1, TransitionSpeed * Time.deltaTime);
                else if (TurnRight && !TurnLeft)
                    Horizontal = Mathf.MoveTowards(Horizontal, 1, TransitionSpeed * Time.deltaTime);
                else if (TurnRight && TurnLeft)
                    Horizontal = Mathf.MoveTowards(Horizontal, 0, TransitionSpeed * Time.deltaTime);
                else
                    Horizontal = Mathf.MoveTowards(Horizontal, 0, TransitionSpeed * Time.deltaTime);
            }*/
        }
    }
    void StartMovement()
    {
        CanMove = true;
        TurnLeft = false;
        TurnRight = false;
        ReadyForCombo = true;
    }

    void StopMovement()
    {
        CanMove = false;
        TurnLeft = false;
        TurnRight = false;
        ReadyForCombo = false;
    }

    public void TurnLeftOn()
    {
        TurnLeft = true;
    }
    public void TurnLeftOff()
    {
        TurnLeft = false;
    }
    public void TurnRightOn()
    {
        TurnRight = true;
    }
    public void TurnRightOff()
    {
        TurnRight = false;
    }
}
