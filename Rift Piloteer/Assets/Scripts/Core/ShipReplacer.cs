using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipReplacer : MonoBehaviour
{
    public GameObject[] shipPrefs;
    public Sprite[] abilitySprites;
    public Image abilityIcon;
    public Transform spawnPoint;
    public static GameObject curShip;
    public CoreEventManager coreEventManager;
    public SaveLoad SaveLoadManager;
    public int curSelectedShip;
    public static Player_Controller player_Controller;
    public Animator selectSubMenu;
    public int[] shipBuyCost;
    public Text cost;
    public Text curRecord;
    public Text curVowTxt;
    public GameObject BuyEffect;
    public float timerToBuyEffect;
    public float timerToBuyEffectMax;

    public void Awake()
    {
        SaveLoadManager = GameObject.Find("SaveLoadManager").GetComponent<SaveLoad>();
        curSelectedShip = SaveLoadManager.sf.curShipSelected;
        SpawnShip();
        coreEventManager.backToMenu.AddListener(SpawnShip);
        coreEventManager.backToMenu.AddListener(UpdateInfo);
        coreEventManager.shipDestroyed.AddListener(UpdateInfo);
        coreEventManager.gameStart.AddListener(ConfirmSelection);
        UpdateInfo();
    }


    public void ChangeToNext()
    {
        coreEventManager.ClickButton.Invoke();
        if (curSelectedShip < SaveLoadManager.totalShipNumber - 1)
        {
            curSelectedShip++;
        }
        else
        {
            curSelectedShip = 0;
        }

        if (SaveLoadManager.sf.inHangar[curSelectedShip] == true)
        {
            selectSubMenu.Play("CanSelect");
        }
        else
        {
            selectSubMenu.Play("CanNotSelect");
            cost.text = shipBuyCost[curSelectedShip] + "";
        }

        SpawnShip();
        UpdateInfo();

    }

    public void ChangeToPrevoius()
    {
        coreEventManager.ClickButton.Invoke();
        if (curSelectedShip > 0)
        {
            curSelectedShip--;
        }
        else
        {
            curSelectedShip = SaveLoadManager.totalShipNumber - 1;
        }

        if (SaveLoadManager.sf.inHangar[curSelectedShip] == true)
        {
            selectSubMenu.Play("CanSelect");
        }
        else
        {
            selectSubMenu.Play("CanNotSelect");
            cost.text = shipBuyCost[curSelectedShip] + "";
        }

        SpawnShip();
        UpdateInfo();

    }

    public void SpawnShip()
    {
        if (curShip != null)
        {
            Destroy(curShip);
        }
        curShip = Instantiate(shipPrefs[curSelectedShip],new Vector3(spawnPoint.position.x,spawnPoint.position.y,spawnPoint.position.z),Quaternion.identity);
        player_Controller = curShip.GetComponent<Player_Controller>();
    }

    public void ConfirmSelection()
    {
        SaveLoadManager.sf.curShipSelected = curSelectedShip;
    }

    public void BuyShip()
    {
        if (shipBuyCost[curSelectedShip] <= SaveLoadManager.sf.curVow)
        {
            SaveLoadManager.sf.inHangar[curSelectedShip] = true;
            SaveLoadManager.sf.curVow -= shipBuyCost[curSelectedShip];
            if (timerToBuyEffect == 0)
            {
                StartCoroutine(BuyEffectShow());
            }
            else
            {
                timerToBuyEffect = 0;
            }
            coreEventManager.BuyShipSound.Invoke();
            UpdateInfo();
        }
        else
        {
            Debug.Log("NotEnoughVow");
        }
        if (SaveLoadManager.sf.inHangar[curSelectedShip] == true)
        {
            selectSubMenu.Play("CanSelect");
        }
        else
        {
            selectSubMenu.Play("CanNotSelect");
            cost.text = shipBuyCost[curSelectedShip] + "";
        }
        SaveLoadManager.ExtraSave();
    }

    IEnumerator BuyEffectShow()
    {
        BuyEffect.SetActive(true);
        while (timerToBuyEffect < timerToBuyEffectMax)
        {
            yield return new WaitForEndOfFrame();
            timerToBuyEffect += Time.deltaTime;
        }
        BuyEffect.SetActive(false);
        timerToBuyEffect = 0;


    }

    public void UpdateInfo()
    {
        curVowTxt.text = SaveLoadManager.sf.curVow + "";
        curRecord.text = SaveLoadManager.sf.records[curSelectedShip] + "";
        abilityIcon.sprite = abilitySprites[curSelectedShip];
    }


}
