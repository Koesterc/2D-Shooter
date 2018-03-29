using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.UI;

public class PlayerHandlerScript : NetworkBehaviour
{
    [SerializeField]
    TextMeshPro myText;
    static int playerNumber = 0;

    [HideInInspector]
    Image playerIcon;
    [HideInInspector]
    Image multyPlayerHUD;
    [HideInInspector]
    IconFollowScript locator;

    public GameObject bullet;

    void OnDisable()
    {
        try
        {
            locator.target = null;
            multyPlayerHUD.gameObject.SetActive(false);
            playerIcon.gameObject.SetActive(false);
        }
        catch
        {

        }
    }

    // Use this for initialization
    void Start()
    {
        playerNumber++;
        if (isLocalPlayer)
        {
            GetComponent<PlayerController>().enabled = true;
            GameManager.instance.playerController = gameObject.GetComponent<PlayerController>();
            GameManager.instance.smoothFollow.target = GameManager.instance.playerController.transform.Find("Character").transform;
            
        }
        else
        {//turning on the portraits for all the players except for this player
            GameManager.instance.canvasController.uiPortraits.portraits[playerNumber - 1].gameObject.SetActive(true);
            multyPlayerHUD = GameManager.instance.canvasController.uiPortraits.portraits[playerNumber - 1];

            playerIcon = GameManager.instance.canvasController.locatorIcons.playerLocatorIcons[playerNumber - 1];
            locator = GameManager.instance.canvasController.locatorIcons.playerLocatorIcons[playerNumber - 1].GetComponent<IconFollowScript>();
            locator.target = transform.Find("Character").transform;

            Health health = transform.Find("Character").GetComponent<Health>();
            health.multiPlayerHealth = GameManager.instance.canvasController.uiPortraits.healthBars[playerNumber - 1];
      //      Destroy(transform.Find("Character").GetComponent<PickUpScript>());
        }
        ChangeName();
    }

    void ChangeName()
    {
        myText.text = "Player " + playerNumber.ToString();
        switch (playerNumber)
        {//changing player color text based on which player he or she is.
            default:
                myText.outlineColor = Color.yellow;
                break;
            case 1:
                myText.outlineColor = Color.blue;
                break;
            case 2:
                myText.outlineColor = Color.red;
                break;
            case 3:
                myText.outlineColor = Color.green;
                break;
        }
        gameObject.name = myText.text;
    }

}
