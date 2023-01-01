using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashBangMainControl : MonoBehaviour
{
    public Animator flashAnim;

    public void Awake()
    {
        flashAnim.Play("Fade");
    }

    public void FlareStart()
    {
        flashAnim.Play("Flare");
    }

    public void FlareEnd()
    {
        flashAnim.Play("Fade");
    }
}
