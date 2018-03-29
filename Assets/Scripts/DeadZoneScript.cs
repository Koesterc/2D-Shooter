using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZoneScript : MonoBehaviour {

    [SerializeField]
    GameObject respawnParticle;
    [SerializeField]
    GameObject afterEffectParticle;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {

             StartCoroutine(Wait(other.GetComponentInParent<PlayerController>().gameObject));
        }
    }

    IEnumerator Wait(GameObject other)
    {
        EnableDisableComponents(other, false);
        Health hp = other.GetComponentInChildren<Health>();
        Vector2 pos = new Vector2(hp.gameObject.transform.position.x, hp.gameObject.transform.position.y+.15f);
        GameObject clone = Instantiate(respawnParticle, pos, hp.gameObject.transform.rotation);
        Destroy(clone, 1.5f);
        clone.transform.SetParent(hp.gameObject.transform);
        yield return new WaitForSeconds(1.5f);
        EnableDisableComponents(other, true);

        hp.TakeDamage(30f);
        if (hp.health > 0)
            other.GetComponentInChildren<Health>().gameObject.transform.position = other.GetComponent<PlayerController>().checkPoint.position;
        pos = new Vector2(hp.gameObject.transform.position.x, hp.gameObject.transform.position.y - .27f);
        clone = Instantiate(afterEffectParticle, pos, hp.gameObject.transform.rotation);
        clone.transform.SetParent(hp.gameObject.transform);
        Destroy(clone, .75f);
    }

    void EnableDisableComponents(GameObject other, bool enable)
    {
        other.GetComponent<PlayerController>().enabled = enable;
        other.GetComponent<ShootingScript>().canFire = enable;
        other.GetComponent<ShootingScript>().enabled = enable;
        other.GetComponentInChildren<Rigidbody2D>().simulated = enable;
        other.GetComponentInChildren<Animator>().enabled = enable;
    }


}
