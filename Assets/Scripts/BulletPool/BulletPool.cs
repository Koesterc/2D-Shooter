using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BulletPool : NetworkBehaviour {
    public List<GameObject> bulletList = new List<GameObject>();
    [SerializeField]
    GameObject bullet;
    [SerializeField]
    int numberOfBullets;
    [HideInInspector]
    public int curBullet = 0;
    // Use this for initialization

    private void Start()
    {
        Spawn();
    }
    public override void OnStartAuthority()
    {
    }


    public void Spawn()
    {
        for (int i = 0; i < numberOfBullets; i++)
        {
            GameObject clone = Instantiate(bullet, transform.position, transform.rotation) as GameObject;
            clone.name = "Bullet";
            clone.transform.SetParent(gameObject.transform, false);
            bulletList.Add(clone);
            clone.SetActive(false);
         //   NetworkServer.Spawn(clone);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
