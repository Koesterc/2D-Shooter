using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponCoolDownScript : MonoBehaviour
{
    float lastCoolDOwnTime;
    [SerializeField]
    AudioController overHeat;
    float AP; //attack power

    private void Start()
    {
        AP = 0;
    }

    public void Update()
    {
        if (AP > 0 && PlayerController.shootingScript.canFire == true && Time.time > lastCoolDOwnTime)
        {
            lastCoolDOwnTime = Time.time + .02f;
            AP -= (GameManager.instance.playerStats.combat.maxAP * .02f);
            if (AP <= 0)
                AP = 0;
            GameManager.instance.canvasController.uiBars.apBar.fillAmount = (AP / GameManager.instance.playerStats.combat.maxAP);
        }
    }

    public void OverHeating(float value)
    {
        lastCoolDOwnTime = Time.time + GameManager.instance.playerStats.combat.coolDownDelay;
        AP += value;
        float maxAP = GameManager.instance.playerStats.combat.maxAP;
        if (AP >= maxAP)
        {
            PlayerController.shootingScript.canFire = false;
            AP = maxAP;
            StartCoroutine(OverHeated());
        }
        GameManager.instance.canvasController.uiBars.apBar.fillAmount = (AP / maxAP);
    }

    IEnumerator OverHeated()
    {
        overHeat.Play();
        PlayerController.ammoScript.OverHeat();
        GameManager.instance.canvasController.uiBars.apAnim.enabled = true;
        GameManager.instance.canvasController.uiBars.apAnim.Play("OverHeated", 0, 0);
        yield return new WaitForSeconds(GameManager.instance.playerStats.combat.coolDownDelay);
        GameManager.instance.canvasController.uiBars.apAnim.Play("OverHeated", 0, 0);
        GameManager.instance.canvasController.uiBars.apAnim.enabled = false;
        PlayerController.ammoScript.DisplayAmmo();
        while (AP > 0)
        {
            float maxAP = GameManager.instance.playerStats.combat.maxAP;
            AP -= (maxAP * .02f);
            if (AP <= 0)
                AP = 0;
            GameManager.instance.canvasController.uiBars.apBar.fillAmount = (AP / maxAP);
            yield return new WaitForSeconds(GameManager.instance.playerStats.combat.coolDownRate);
        }
        PlayerController.shootingScript.canFire = true;
    }
}
