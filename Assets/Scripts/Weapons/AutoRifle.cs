using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRifle : BaseWeapon {
    float lastShot;
	// Use this for initialization
	void Start () {
        shootingScript = transform.parent.parent.GetComponent<ShootingScript>();

    }

    private void OnEnable()
    {
        GameManager.instance.canvasController.weaponInfo.UpdateInfo(damage, damage + upgrades.damage.firePower,
            upgrades.damage.curLvl, Mathf.Round((1 / (fireRate) * 10)) / 10,
            Mathf.Round((1 / (fireRate - upgrades.fireRate.attackSpeed) * 10)) / 10,
            upgrades.fireRate.curLvl, Mathf.Round(((100 - accuracy) * 10)) / 10,
            Mathf.Round(((100 - (accuracy + upgrades.accuracy.accuracy)) * 10)) / 10,
            upgrades.accuracy.curLvl, ammoCost, AP, Damage(damage), 100 - Accuracy(accuracy),
            Mathf.Round((1 / (fireRate) * 10)) / 10, weaponName);

    }

    // Update is called once per frame
    void Update () {
        if (PlayerController.shootingScript.fire && Time.time > lastShot && ammoCost <= PlayerController.ammoScript.ammoCount)
        {
            if (!PlayerController.shootingScript.canFire)
                return;
            lastShot = Time.time + fireRate;
            PlayerController.ammoScript.lastRecovered = Time.time + PlayerController.ammoScript.ammoRecoveryTime;
            Fire();
            PlayerController.ammoScript.UpdateAmmo(-ammoCost);
            PlayerController.shootingScript.coolDownScript.OverHeating(AP);
        }
    }

    public override void Fire()
    {
        Quaternion t = Quaternion.identity;
        float myDamage = Damage(damage);
        float ac = Accuracy(accuracy);
        float r = Random.Range(-ac, ac);
        if (!PlayerController.shootingScript.mySprite.flipX)
        {
            t = Quaternion.Euler(0, 0, t.z + r);
            shootingScript.CmdFire(PlayerController.shootingScript.rightBarrel.transform.position, t, bulletSpeed, myDamage, bulletRange, 0f, true);
        }
        else
        {
            t = Quaternion.Euler(0, 0, t.z - r);
            shootingScript.CmdFire(PlayerController.shootingScript.leftBarrel.transform.position, t, bulletSpeed, myDamage, bulletRange, 0f, true);
        }
        au.Play();
    }
}
