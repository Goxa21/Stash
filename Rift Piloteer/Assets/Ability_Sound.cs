using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Ability_Sound : MonoBehaviour
{
    public AudioClip Intro;
    public AudioClip Middle;
    public AudioClip Outro;
    AudioSource Source;
    CoreEventManager coreEventManager;


    private void Start()
    {
        Source = GetComponent<AudioSource>();
        coreEventManager = GetComponent<Player_Controller>().coreEventManager;
        coreEventManager.abilityActivation.AddListener(StartAbility);
        coreEventManager.abilityDeactivation.AddListener(EndAbility);
    }
    void StartAbility()
    {
        Source.clip = Intro;
        Source.Play();
        StartCoroutine(GoToLoop());
    }
    void LoopAbility()
    {
        Source.clip = Middle;
        Source.loop = true;
        Source.Play();
    }
    void EndAbility()
    {
        Source.clip = Outro;
        Source.loop = false;
        Source.Play();
    }
    IEnumerator GoToLoop()
    {
        yield return new WaitForSeconds(Intro.length);
        LoopAbility();
    }
}
