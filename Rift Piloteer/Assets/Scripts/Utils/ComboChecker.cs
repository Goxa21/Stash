using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboChecker : MonoBehaviour
{
    public Player_Controller player_Controller;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Obstacle")
        {
            Debug.Log("ObstacleNearby");
            player_Controller.ImproveCombo();
        }
    }
}
