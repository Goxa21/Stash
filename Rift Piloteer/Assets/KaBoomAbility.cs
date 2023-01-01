using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KaBoomAbility : MonoBehaviour
{
    public float TimeUntilScaling;
    public GameObject kaBoomSphere;
    CoreEventManager coreEventManager;
    public Animator reactorAnim;
    private void Start()
    {
        coreEventManager = GetComponent<Player_Controller>().coreEventManager;
        coreEventManager.abilityActivation.AddListener(ActivateAbility);
    }
    
    void ActivateAbility()
    {
        StartCoroutine(AbilityDelay());
    }
    IEnumerator AbilityDelay()
    {
        reactorAnim.Play("Show");
        yield return new WaitForSeconds(TimeUntilScaling);
        Instantiate(kaBoomSphere, transform.position, Quaternion.identity);
        coreEventManager.abilityDeactivation.Invoke();
    }
}
