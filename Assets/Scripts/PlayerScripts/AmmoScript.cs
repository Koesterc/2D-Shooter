using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AmmoScript : MonoBehaviour
{
    public int maxAmmo;
    public int ammoCount;
    public float ammoRecoveryTime;
    [HideInInspector]
    public float lastRecovered;
    public float coolDown;

    private void Start()
    {
        ammoCount = maxAmmo;
    }

    // Use this for initialization
    public void UpdateAmmo(int value)
    {
        ammoCount += value;
        if (ammoCount > maxAmmo)
            ammoCount = maxAmmo;
        else if (ammoCount < 0)
            ammoCount = 0;
        DisplayAmmo();
    }

    // Update is called once per frame
    public void DisplayAmmo()
    {
        GameManager.instance.canvasController.uiTexts.ammoText.text = (Mathf.Round(((float)ammoCount / maxAmmo) * 100) / 1).ToString() + "%";
        GameManager.instance.canvasController.uiTexts.ammoText.outlineColor = Color.Lerp(Color.green, Color.red, ammoCount / maxAmmo);
    }

    public string AmmoText()
    {
        return GameManager.instance.canvasController.uiTexts.ammoText.text;
    }

    public void OverHeat()
    {
        GameManager.instance.canvasController.uiTexts.ammoText.text = "ovht";
    }

    private void Update()
    {
        if ((ammoCount < maxAmmo) & Time.time > lastRecovered)
        {
            lastRecovered = Time.time + ammoRecoveryTime;
            ammoCount++;
            if (GameManager.instance.canvasController.uiTexts.ammoText.text == "ovht")
                return;
            DisplayAmmo();
        }
    }
}
