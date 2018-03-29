using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CanvasController : MonoBehaviour {
    [System.Serializable]
    public class UIBars
    {
        public Image healthBar;
        public Image healthDrain;
        public Image shieldBar;
        public Image chargeBar;
        [HideInInspector]
        public Animator apAnim;
        public Image apBar;
    }
    public UIBars uiBars;

    [System.Serializable]
    public class UITexts
    {
        public TextMeshProUGUI ammoText;
        public TextMeshProUGUI xpText;
        public TextMeshProUGUI healthCount;
    }
    public UITexts uiTexts;

    [System.Serializable]
    public class UIImages
    {
        public Image weaponImage;
    }
    public UIImages uiImages;

    [System.Serializable]
    public class WeaponSelection
    {
        public WeaponIconScript weaponIcon;
        [HideInInspector]
        public Animator anim;
    }
    public WeaponSelection weaponSelection;

    [System.Serializable]
    public class UIPortraits
    {
        public Image[] portraits;
        public Image[] healthBars;
        public TextMeshProUGUI[] playerName;
    }
    public UIPortraits uiPortraits;

    [System.Serializable]
    public class UILocatorIcons
    {
        public Image[] playerLocatorIcons;
        [HideInInspector]
        public Transform[] playerLocations;
    }
    public UILocatorIcons locatorIcons;

    [System.Serializable]
    public class WeaponInfo
    {
        [SerializeField]
        TextMeshProUGUI totalDamage;
        [SerializeField]
        TextMeshProUGUI totalFireRate;
        [SerializeField]
        TextMeshProUGUI totalAccuracy;

        [SerializeField]
        TextMeshProUGUI damage;
        [SerializeField]
        TextMeshProUGUI accuracy;
        [SerializeField]
        TextMeshProUGUI fireRate;

        [SerializeField]
        TextMeshProUGUI nextDamage;
        [SerializeField]
        TextMeshProUGUI nextFireRate;
        [SerializeField]
        TextMeshProUGUI nextAccuracy;

        [SerializeField]
        TextMeshProUGUI damageLvl;
        [SerializeField]
        TextMeshProUGUI fireRateLvl;
        [SerializeField]
        TextMeshProUGUI accuracyLvl;

        [SerializeField]
        TextMeshProUGUI ammoCost;
        [SerializeField]
        TextMeshProUGUI weaponName;
        bool isClosing;

        [SerializeField]
        Animator infoAnim;

        public void UpdateInfo(float _damage, float _nextDamage, int _damageLvl, float _fireRate, float _nextFireRate, int _fireRateLvl, float _accuracy, float _nextAccuracy, int _accuracyLvl, int _ammoCost, float _AP, float _totalDamage, float _totalAccuracy, float _totalFireRate, string _name)
        {
            if (infoAnim.GetCurrentAnimatorStateInfo(0).normalizedTime > .91f)
                infoAnim.Play("OpenInfo",0,0f);
            else
                infoAnim.Play("OpenInfo", 0, .1f);

            damage.text = "Damage: " +_damage.ToString();
            fireRate.text = "Fire Rate: " +_fireRate.ToString();
            accuracy.text = "Accuracy: " +_accuracy.ToString() + "%";
            nextAccuracy.text = "Next Level: "+_nextAccuracy.ToString() + "%";
            nextFireRate.text = "Next Level: " + _nextFireRate.ToString();
            nextDamage.text = "Next Level: " + _nextDamage.ToString();
            damageLvl.text = "Lvl: "+ (1+_damageLvl).ToString();
            fireRateLvl.text = "Lvl: " + (1+_fireRateLvl).ToString();
            accuracyLvl.text = "Lvl: " + (1+_accuracyLvl).ToString();
            totalDamage.text = _totalDamage.ToString();
            totalAccuracy.text = _totalAccuracy.ToString()+"%";
            totalFireRate.text = _totalFireRate.ToString();
            ammoCost.text = "Energy: " + _AP.ToString()+"\n"+ "Ammo: "+_ammoCost.ToString();
            weaponName.text = _name.ToString();
        }
    }

    public WeaponInfo weaponInfo;

    private void Start()
    {
        weaponSelection.anim = weaponSelection.weaponIcon.gameObject.GetComponent<Animator>();
        uiBars.apAnim = uiBars.apBar.gameObject.GetComponent<Animator>();
        locatorIcons.playerLocations = new Transform[3];
    }
}
