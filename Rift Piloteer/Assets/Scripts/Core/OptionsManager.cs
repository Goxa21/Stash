using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptionsManager : MonoBehaviour
{
    public Text ctrlName;
    public AudioMixerGroup Mixer;
    public Slider SFXSlider;
    public Slider MusicSlider;
    CoreEventManager coreEventManager;

    public void Awake()
    {
        coreEventManager = GetComponent<CoreEventManager>();
        UpdateLanguage();

        SFXSlider.value = CoreSettings.audioSFXVolume;
        MusicSlider.value = CoreSettings.audioMusicVolume;
        StartCoroutine(ApplyMixer());
    }
    public void ChangeControlMethod()
    {
        coreEventManager.ClickButton.Invoke();
        CoreSettings.AccelerometerIsActive = !CoreSettings.AccelerometerIsActive;
        UpdateLanguage();
    }
    public void UpdateLanguage()
    {
        if (CoreSettings.AccelerometerIsActive)
        {
            switch (CoreSettings.languageActiveID)
            {
                case 0:
                    ctrlName.text = "Accelerometer";

                    break;
                case 1:
                    ctrlName.text = "Акселерометр";

                    break;
                case 2:
                    ctrlName.text = "Acelerómetro";

                    break;
                case 3:
                    ctrlName.text = "Acelerômetro";

                    break;
                case 4:
                    ctrlName.text = "Beschleunigungsmesser";

                    break;
                case 5:
                    ctrlName.text = "Accéléromètre";

                    break;
                case 6:
                    ctrlName.text = "加速度计";

                    break;
                case 7:
                    ctrlName.text = "加速度計";

                    break;
                case 8:
                    ctrlName.text = "Accelerometro";

                    break;
                case 9:
                    ctrlName.text = "가속도계";

                    break;
                default:
                    Debug.Log("WTF");
                    break;
            }
        }
        else
        {
            switch (CoreSettings.languageActiveID)
            {
                case 0:
                    ctrlName.text = "TouchScreen";

                    break;
                case 1:
                    ctrlName.text = "Сенсорный экран";

                    break;
                case 2:
                    ctrlName.text = "Pantalla táctil";

                    break;
                case 3:
                    ctrlName.text = "Tela sensível ao toque";

                    break;
                case 4:
                    ctrlName.text = "Berührungssensitiver Bildschirm";

                    break;
                case 5:
                    ctrlName.text = "Écran tactile";

                    break;
                case 6:
                    ctrlName.text = "触摸屏";

                    break;
                case 7:
                    ctrlName.text = "タッチスクリーン";

                    break;
                case 8:
                    ctrlName.text = "Touch screen";

                    break;
                case 9:
                    ctrlName.text = "터치 스크린";

                    break;
                default:
                    Debug.Log("WTF");
                    break;
            }
        }
    }
    public void SFXController(float SFXVolume)
    {
        CoreSettings.audioSFXVolume = SFXVolume;
        if (SFXVolume == -30f)
            Mixer.audioMixer.SetFloat("SFX", -80);
        else
            Mixer.audioMixer.SetFloat("SFX", SFXVolume);
    }
    public void MusicController(float MusicVolume)
    {
        CoreSettings.audioMusicVolume = MusicVolume;
        if (MusicVolume == -30f)
            Mixer.audioMixer.SetFloat("Music", -80);
        else
            Mixer.audioMixer.SetFloat("Music", MusicVolume);
    } 
    IEnumerator ApplyMixer()
    {
        yield return new WaitForEndOfFrame();
        Mixer.audioMixer.SetFloat("SFX", CoreSettings.audioSFXVolume);
        Mixer.audioMixer.SetFloat("Music", CoreSettings.audioMusicVolume);
    }
}
