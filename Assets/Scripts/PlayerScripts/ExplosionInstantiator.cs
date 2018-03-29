using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class ExplosionInstantiator : NetworkBehaviour {
    [HideInInspector]
    public GameObject explosiveObject;
    public float explosionTime;

    [Command]
    public void CmdExplosion(Vector3 pos, Quaternion rotation, GameObject explosion, float explosionTime)
    {
        explosiveObject = explosion;
        GameObject clone = Instantiate(explosion, pos, rotation);
        Destroy(clone, 3f);
        NetworkServer.Spawn(clone);
    }
}
