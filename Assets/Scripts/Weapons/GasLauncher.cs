using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasLauncher : BaseWeapon {
    float lastShot;
    // Use this for initialization
    void Start()
    {
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
    void Update()
    {
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
        if (!PlayerController.shootingScript.mySprite.flipX)
        {
            shootingScript.CmdFire(PlayerController.shootingScript.rightBarrel.transform.position, Quaternion.identity, bulletSpeed, damage, bulletRange, 1f, true);
        }
        else
        {
            shootingScript.CmdFire(PlayerController.shootingScript.leftBarrel.transform.position, Quaternion.identity, bulletSpeed, damage, bulletRange, 1f, true);
        }
        au.Play();
    }
}

