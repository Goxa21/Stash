using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleDestructionFuctonTransfer : MonoBehaviour
{
    public Mover mover;

    public void DestructionFunction()
    {
        if (mover != null)
        {
            mover.Destruction();
        }
    }
}
