using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargeUpScript : MonoBehaviour
{
    public static ChargeUpScript instance;
    Image charge;

    // Use this for initialization
    void Start()
    {
        instance = gameObject.GetComponent<ChargeUpScript>();
        charge = gameObject.GetComponent<Image> ();
    }

    public void PowerUp(float percentage)
    {
        charge.fillAmount = percentage;
    }
}