using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayGun : BaseWeapon
{
    float lastShot;
    IEnumerator myWait;
    int value;

    private void OnEnable()
    {
        GameManager.instance.canvasController.weaponInfo.UpdateInfo(damage,damage + upgrades.damage.firePower,
            upgrades.damage.curLvl, Mathf.Round((1/(fireRate)*10))/10, 
            Mathf.Round((1 / (fireRate - upgrades.fireRate.attackSpeed) * 10)) / 10, 
            upgrades.fireRate.curLvl, Mathf.Round(((100 - accuracy)*10))/10,
            Mathf.Round(((100 - (accuracy+upgrades.accuracy.accuracy)) * 10)) / 10, 
            upgrades.accuracy.curLvl,ammoCost,AP, Damage(damage), 100-Accuracy(accuracy), 
            Mathf.Round((1 / (fireRate) * 10)) / 10, weaponName);

    }

    void Start()
    {
        shootingScript = transform.parent.parent.GetComponent<ShootingScript>();

        //for (int i = upgrades.damage.curLvl; i < upgrades.damage.maxLvl; i++)
        //{
        //    damage = upgrades.UpgradeDamage(damage);
        //    upgrades.damage.curLvl++;
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.shootingScript.fire && Time.time > lastShot && ammoCost <= PlayerController.ammoScript.ammoCount)
        {//checking to see if the weapon is overheated
            if (!PlayerController.shootingScript.canFire)
                return;

            PlayerController.shootingScript.canSwitch = false;
            value = PlayerController.shootingScript.mySprite.flipX ? 1 : 0;
            PlayerController.shootingScript.gunSpark[value].SetActive(true);
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
            shootingScript.CmdFire(PlayerController.shootingScript.rightBarrel.transform.position, t, bulletSpeed, myDamage, bulletRange, 0f, false);
        }
        else
        {
            t = Quaternion.Euler(0, 0, t.z - r);
            shootingScript.CmdFire(PlayerController.shootingScript.leftBarrel.transform.position, t, bulletSpeed, myDamage, bulletRange, 0f, false);
        }
        au.Play();

        myWait = MyWait();
        StartCoroutine(myWait);
    }
    IEnumerator MyWait()
    {
        yield return new WaitForSeconds(.03f);
        PlayerController.shootingScript.gunSpark[value].SetActive(false);
        PlayerController.shootingScript.canSwitch = true;
    }
}
