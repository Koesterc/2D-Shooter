using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BulletScript : NetworkBehaviour {
    float damage;
    bool splashDamage;
    float splashRadious;
    public int ownerID;
    public GameObject explosion;
    [HideInInspector]
    public ExplosionInstantiator explosionInstantiator;



    public void Commence(int _id, float _damage, bool _splashDamage, float _splashRadious)
    {
        damage = _damage;
        splashDamage = _splashDamage;
        splashRadious = _splashRadious;
        ownerID = _id;
    }

    [ServerCallback]
    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "Player":
                break;
            case "Enemy":
                other.GetComponent<Health>().TakeDamage(damage);
                explosionInstantiator.CmdExplosion(transform.position, transform.rotation, explosion, 3f);
                StartCoroutine(DestroyBullet());
                break;
            case "Object":
                explosionInstantiator.CmdExplosion(transform.position, transform.rotation, explosion, 3f);
                StartCoroutine(DestroyBullet());
                break;
        }
    }
    public IEnumerator DestroyBullet()
    {
        yield return null;
        NetworkServer.UnSpawn(gameObject);
        gameObject.SetActive(false);
    }
}
