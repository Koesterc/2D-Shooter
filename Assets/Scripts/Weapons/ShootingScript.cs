using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ShootingScript : NetworkBehaviour
{
    public bool fire { get; private set; }
    public Transform leftBarrel;
    public Transform rightBarrel;
    public GameObject[] gunSpark;
    public List<GameObject> weapons;
    [HideInInspector]
    public int curWeapon;
    [HideInInspector]
    public bool canSwitch = true;
    [HideInInspector]
    public bool canFire = true;
    [SerializeField]
    AudioController weaponSwitch;
    public WeaponCoolDownScript coolDownScript;
    [HideInInspector]
    ExplosionInstantiator explosionInstantiator;
    [HideInInspector]
    public SpriteRenderer mySprite;
    [HideInInspector]
    public bool isBusy;


    // Use this for initialization
    void Start()
    {
        Transform _weapons = transform.Find("Weapons");
        foreach (Transform weapon in _weapons)
        {
            weapons.Add(weapon.gameObject);
        }
        coolDownScript = gameObject.GetComponent<WeaponCoolDownScript>();
        explosionInstantiator = gameObject.GetComponent<ExplosionInstantiator>();
        if (!isLocalPlayer)
            gameObject.GetComponent<ShootingScript>().enabled = false;
        mySprite = transform.Find("Character").GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isBusy)
            return;
        fire = Input.GetButton("Fire1");
        if (Input.GetButtonDown("Fire3") && canSwitch)
        {
            canSwitch = false;
            weapons[curWeapon % weapons.Count].SetActive(false);
            curWeapon++;
            weapons[curWeapon % weapons.Count].SetActive(true);
            GameManager.instance.canvasController.weaponSelection.weaponIcon.CycleWeapon(curWeapon % weapons.Count, -1);
            StartCoroutine(Wait());
            weaponSwitch.Play();
        }
    }

    [Command]
    public void CmdFire(Vector3 position, Quaternion rotation, float speed,  float damage, float range, float gravityScale, bool enableRenderer)
    {
        // var obj = objectPool.GetFromPool(position);                             //Grab bullet from pool
        GameObject bullet = PlayerController.bulletPool.bulletList[PlayerController.bulletPool.curBullet % PlayerController.bulletPool.bulletList.Count];
        //getting the bullet script
        BulletScript bs = bullet.GetComponent<BulletScript>();
        bs.Commence(0, damage, false, 0);
        PlayerController.bulletPool.curBullet++;
        bullet.transform.position = position;
        bullet.transform.rotation = rotation;
        bullet.SetActive(true);     
        int dir = mySprite.flipX ? -1 : 1;
        //**dealing with the sprite renderer
        //setting the bullet to face the proper direction and turning it either on or off.
        SpriteRenderer sr = bullet.GetComponent<SpriteRenderer>();
        sr.flipX = mySprite.flipX;
        sr.enabled = enableRenderer;
      //  print(facingRight);
        //**setting the velocity for the rigid body
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (gravityScale > 0)
        {
            rb.gravityScale = gravityScale;
            rb.AddForce (Vector3.up *200);
        }
        rb.velocity = (bullet.transform.right*dir) * speed;
        bs.explosionInstantiator = explosionInstantiator;
        NetworkServer.Spawn(bullet);
        // spawn bullet on client, custom spawn handler will be called      , objectPool.assetId
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(.5f);
        canSwitch = true;
    }
}
