using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Audio_Controller : MonoBehaviour
{
    public AudioClip[] Ambients;
    public AudioClip MenuMusic;
    public AudioClip ButtonClip;
    public AudioClip BackButtonClip;
    public AudioClip abilitySound;
    public AudioClip BuyClip;
    public AudioClip PortalSound;
    public AudioClip EngineNoise;
    public AudioSource ButtonSource;
    public AudioSource AmbientSource;
    CoreEventManager coreEventManager;
    private void Start()
    {
        coreEventManager = GetComponent<CoreEventManager>();
        coreEventManager.gameStart.AddListener(PlayAmbient);
        coreEventManager.StopAmbient.AddListener(PauseAmbient);
        coreEventManager.ContinueAmbient.AddListener(ContinueAmbient);
        coreEventManager.ClickBackButton.AddListener(ClickBackButton);
        coreEventManager.ClickButton.AddListener(ClickButton);
        coreEventManager.OpenPortal.AddListener(PortalSFX);
        coreEventManager.backToMenu.AddListener(PlayMainMenuMusic);
        coreEventManager.BuyShipSound.AddListener(BuyShipSFX);
        PlayMainMenuMusic();
    }
    void PlayMainMenuMusic()
    {
        AmbientSource.clip = MenuMusic;
        AmbientSource.Play();
    }
    void PlayAmbient()
    {
        AmbientSource.clip = Ambients[Random.Range(0, Ambients.Length)];
        AmbientSource.Play();
    }
    void PauseAmbient()
    {
        AmbientSource.Pause();
    }
    void ContinueAmbient()
    {
        AmbientSource.Play();
    }
    void ClickButton()
    {
        ButtonSource.clip = ButtonClip;
        ButtonSource.Play();
    }
    void ClickBackButton()
    {
        ButtonSource.clip = BackButtonClip;
        ButtonSource.Play();
    }
    void PortalSFX()
    {
        ButtonSource.clip = PortalSound;
        ButtonSource.Play();
    }
    void BuyShipSFX()
    {
        ButtonSource.clip = BuyClip;
        ButtonSource.Play();
    }
}
