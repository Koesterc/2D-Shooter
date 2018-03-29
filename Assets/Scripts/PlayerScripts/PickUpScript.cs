using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;

public class PickUpScript : MonoBehaviour
{
    public AudioController cell;
    [SerializeField]
    GameObject floatingText;
    [SerializeField]
    GameObject pickUpParticle;
    GameObject particle;
    bool isLocalPLayer;

    private void Start()
    {
        isLocalPLayer = transform.parent.GetComponent<NetworkIdentity>().isLocalPlayer;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Cell")
        {
            Vector3 pos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            if (particle != null)
                Destroy(particle);
            particle = Instantiate(pickUpParticle, pos, transform.rotation);
            particle.transform.SetParent(gameObject.transform, true);
            particle.GetComponent<Animator>().Play("PickUpParticle");
            Destroy(particle, 1f);
            Destroy(other.gameObject);
            cell.Play();
            if (!isLocalPLayer)
                return;
            if (PlayerController.ammoScript.AmmoText() != "ovht")
                PlayerController.ammoScript.UpdateAmmo(5);
            GameObject clone = Instantiate(floatingText, new Vector3(transform.position.x + .2f, transform.position.y - .2f, transform.position.z), transform.rotation);
            clone.name = "FloatingText";
            Destroy(clone, 2f);
        }
    }
}
