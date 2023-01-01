using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Progress_Manager : MonoBehaviour
{
    public Text ScoreText;
    public CoreEventManager coreEventManager;
    public ADSManager aDSManager;
    public Spawner SpawnController;
    public float Modificator;
    public int Score;
    public static int ScoreMod;
    public int Vow;
    public Text FinalScore;
    public Text CollectedVows;
    SaveLoad SaveLoadManager;
    public GameObject vowRewardButton;
    public GameObject vowRewardButtonText;
    public GameObject newRecordInfo;
    public AbilityManager abilityManager;
    public World_Manager WorldManager;
    public Text vowText;
    public float comboFillAmount;
    public int comboState;
    public Image comboGauge;
    public Text comboText;
    public float comboChargeAmount;
    public float comboReduceSpeed;
    public Animator comboAnim;

    private void Start()
    {
        ScoreMod = 1;
        Mover.canSlowTime = true;
        SaveLoadManager = GameObject.Find("SaveLoadManager").GetComponent<SaveLoad>();
        coreEventManager.gameStart.AddListener(ClearScore);
        coreEventManager.ShowResults.AddListener(ShowScore);
        coreEventManager.backToMenu.AddListener(SaveResults);
        coreEventManager.ComboStep.AddListener(ComboStep);
        StartCoroutine(comboReducer());
    }
    private void Update()
    {
        ScoreText.text = Score.ToString() + "m";
        comboGauge.fillAmount = comboFillAmount;
    }
    public void StartCount()
    {
        print("Start");
        StartCoroutine(ScoreCounter());
        StartCoroutine(DencityCounter());
        StartCoroutine(SpeedController());
    }
    void ClearScore()
    {
        Score = 0;
        Vow = 0;
        WorldManager.CurrentScore = 0;
        comboFillAmount = 0;
        comboState = 0;
        vowText.text = Vow + "v";
        UpdateComboData();
    }
    public void GetVow()
    {
        Vow += 1 + comboState;
        abilityManager.IncreaseCharge();
        vowText.text = Vow + "v";
    }
    public void ShowScore()
    {
        vowRewardButtonText.SetActive(true);
        StartCoroutine(DelayedButtonActivation());
        FinalScore.text = Score.ToString() + "m";
        if (Score > SaveLoadManager.sf.records[SaveLoadManager.sf.curShipSelected])
        {
            SaveLoadManager.sf.records[SaveLoadManager.sf.curShipSelected] = Score;
            newRecordInfo.SetActive(true);
        }
        else
        {
            newRecordInfo.SetActive(false);
        }
        CollectedVows.text = Vow.ToString();
    }

    IEnumerator DelayedButtonActivation()
    {
        vowRewardButton.SetActive(false);
        yield return new WaitForSeconds(1f);
        vowRewardButton.SetActive(true);
    }

    void SaveResults()
    {
        SaveLoadManager.sf.curVow += Vow;
        SaveLoadManager.ExtraSave();
    }
    IEnumerator ScoreCounter()
    {
        while (SpawnController.CanSpawn)
        {
            yield return new WaitForSeconds(Modificator);

            Score = Score += 1 * ScoreMod;
            WorldManager.CurrentScore = WorldManager.CurrentScore += 1 * ScoreMod;
        }
    }
    IEnumerator DencityCounter()
    {
        while (SpawnController.CanSpawn && SpawnController.Density > 0.11f)
        {
            yield return new WaitForSeconds(3f);
            //1 секунда-40 сек в игре
            SpawnController.Density = SpawnController.Density - 0.01f;
        }
    }
    IEnumerator SpeedController()
    {
        while (SpawnController.CanSpawn && Spawner.Speed<250)
        {
            yield return new WaitForSeconds(3f);
            Spawner.Speed = Spawner.Speed + 10f;
        }
    }

    public void TryToDoubleReward()
    {
        aDSManager.doubleVowFlag = true;
        aDSManager.LaunchRewardedADS();
    }

    public void DoubleReward()
    {
        Vow = Vow * 2;
        CollectedVows.text = Vow.ToString();
        vowRewardButton.SetActive(false);
        vowRewardButtonText.SetActive(false);
        aDSManager.doubleVowFlag = false;
    }

    public void ComboStep()
    {
        comboFillAmount += comboChargeAmount;
        if (comboFillAmount > 1 && comboState < 5)
        {
            comboState++;
            comboFillAmount = 0.5f;
            comboAnim.Play("Up");
        }
        else
        {
            comboFillAmount = 1;
        }
        UpdateComboData();
    }

    public void UpdateComboData()
    {
        switch (comboState)
        {
            case 0:
                comboText.text = "x1";
                break;
            case 1:
                comboText.text = "x2";
                break;
            case 2:
                comboText.text = "x3";
                break;
            case 3:
                comboText.text = "x4";
                break;
            case 4:
                comboText.text = "x5";
                break;
            case 5:
                comboText.text = "x6";
                break;
            default:
                Debug.Log("WTF");
                break;
        }
    }

    IEnumerator comboReducer()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
            if (comboFillAmount > 0)
            {
                comboFillAmount -= Time.deltaTime * comboReduceSpeed;
            }
            else if (comboState > 0)
            {
                comboFillAmount = 0.95f;
                comboState--;
                UpdateComboData();
            }
        }
    }
}
