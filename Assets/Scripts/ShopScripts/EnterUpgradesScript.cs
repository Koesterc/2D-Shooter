using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterUpgradesScript : MonoBehaviour {

    [SerializeField]
    GameObject myButton;
    [SerializeField]
    GameObject myContent;
    GameObject player;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && !myContent.activeSelf)
        {
            myContent.SetActive(true);
            player.GetComponent<ShootingScript>().isBusy = true;
        }
        else if (Input.GetButtonDown("Fire2") && myContent.activeSelf)
        {
            myContent.SetActive(false);
            player.GetComponent<ShootingScript>().isBusy = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && other.transform.parent.GetComponent<PlayerController>().isLocalPlayer)
        {
            player = other.transform.parent.gameObject;
            gameObject.GetComponent<EnterUpgradesScript>().enabled = true;
            myButton.SetActive(true);
        }
        else
            return;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player" && other.transform.parent.GetComponent<PlayerController>().isLocalPlayer)
        {
            gameObject.GetComponent<EnterUpgradesScript>().enabled = false;
            myButton.SetActive(false);
        }
        else
            return;
    }
}
