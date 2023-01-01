using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield_Ability : MonoBehaviour
{
    public float ShieldTime;
    public int ShieldHealth;
    public int maxShieldHealth;
    public bool canTakeHit;
    Player_Controller Player;
    public CoreEventManager coreEventManager;
    public Animator shieldAnim;
    float timer = 0f;


    private void Awake()
    {
        Player = GetComponent<Player_Controller>();
        coreEventManager = GameObject.Find("Managers").GetComponent<CoreEventManager>();
        //coreEventManager = GetComponent<Player_Controller>().coreEventManager;
        //coreEventManager = Player.GetComponent<CoreEventManager>();
        coreEventManager.abilityActivation.AddListener(ActivateShieldAbility);
        canTakeHit = true;
    }

    void ActivateShieldAbility()
    {
        ShieldHealth = maxShieldHealth;
        Player.CanDie = false;

        shieldAnim.Play("Show");
        timer = 0f;
        StartCoroutine(TimeToDeactivate());
    }
    IEnumerator TimeToDeactivate()
    {
        while (ShieldHealth > 0 && timer <= ShieldTime)
        {
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
        }
        shieldAnim.Play("Hide");
        Player.CanDie = true;
        ShieldHealth = 0;
        coreEventManager.abilityDeactivation.Invoke(); 
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (canTakeHit)
        {
            if (collision.gameObject.tag == "Obstacle")
            {
                ShieldHealth -= 1;
                canTakeHit = false;
                StartCoroutine(canTakeHitCooldown());

            }
        }
        
    }

    IEnumerator canTakeHitCooldown()
    {
        yield return new WaitForSeconds(0.1f);
        canTakeHit = true;
    }
}
