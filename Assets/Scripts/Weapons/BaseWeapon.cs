using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeapon : MonoBehaviour {
    public float fireRate;
    public float damage;
    public float accuracy;
    public float bulletSpeed;
    public float bulletRange;
    public float AP;
    public AudioController au;
    public GameObject gunParticle;
    public int weaponID;
    public int ammoCost;
    public string weaponName;

    [HideInInspector]
    public ShootingScript shootingScript;

    public Upgrades upgrades;

    // Use this for initialization
    void Start ()
    {
        weaponID = transform.parent.GetComponent<PlayerController>().playerID;

    }

    public float Damage(float _damage)
    {
        //the player's damage musn't exceed a certain limit
        float bonusDamage = GameManager.instance.playerStats.combat.extraDamage;
        if (bonusDamage >= .8f)
            bonusDamage = .8f;
        //adding any bonus damage to the weapon's damage
        _damage = _damage+(bonusDamage*_damage);
        return _damage;
    }

    public float Accuracy(float _accuracy)
    {
        //Getting the player's accuracy from the player's stats so we can later combined it to the gun's accuracy
        float playerAC = GameManager.instance.playerStats.combat.extraAccuracy;
        //the playerAC musn't exceed a certain limit
        if (playerAC >= .8f)
            playerAC = .8f;
        _accuracy = _accuracy - (_accuracy * playerAC);
        return _accuracy;
    }

    // Update is called once per frame
    void Update () {
	}

    public virtual void Fire()
    {

    }
}
