using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDrain : MonoBehaviour {
    IEnumerator drainHealth;
    IEnumerator drainShield;

    public void DrainHP()
    {
        drainHealth = DrainHealth();
        StartCoroutine(drainHealth);
    }

    public void DrainShield(float shield, float maxShield)
    {
        drainShield = ShieldDrain(shield,maxShield);
        StartCoroutine(drainShield);
    }

    IEnumerator DrainHealth()
    {
        while (GameManager.instance.canvasController.uiBars.healthDrain.fillAmount > GameManager.instance.canvasController.uiBars.healthBar.fillAmount)
        {
            GameManager.instance.canvasController.uiBars.healthDrain.fillAmount -= .01f;
            if (GameManager.instance.canvasController.uiBars.healthDrain.fillAmount <= GameManager.instance.canvasController.uiBars.healthBar.fillAmount)
                GameManager.instance.canvasController.uiBars.healthDrain.fillAmount = GameManager.instance.canvasController.uiBars.healthBar.fillAmount;
            yield return new WaitForSeconds(.02f);
        }
    }

    IEnumerator ShieldDrain(float shield, float maxShield)
    {
        while (GameManager.instance.canvasController.uiBars.shieldBar.fillAmount > shield / maxShield)
        {
            GameManager.instance.canvasController.uiBars.shieldBar.fillAmount -= .01f;
            if (GameManager.instance.canvasController.uiBars.shieldBar.fillAmount <= shield / maxShield)
                GameManager.instance.canvasController.uiBars.shieldBar.fillAmount = shield / maxShield;
            yield return new WaitForSeconds(.02f);
        }
    }
}
