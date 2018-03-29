using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

    //current level
    public int currentLevel;
    //level stats
    public int statPoints;
    [System.Serializable]
    public class Attributes
    {
        public int marksmanship;  //dexterity
        public int strength;
        public int firePower; //damage
        public int intelligence; //ap
        public int agility; //speed and agility
        public int constantution; //health
        public int defense; //armor
    }
    public Attributes attributes;

    [System.Serializable]
    public class Combat
    {
        [Range(0, 1)]
        public float extraDamage;
        [Range (0,1)]
        public float extraAccuracy;
        public float maxAmmo;
        public float maxAP;
        public float coolDownDelay;
        public float coolDownRate;
    }
    public Combat combat;

    [System.Serializable]
    public class Vitality
    {
        public float maxHealth;
        public float maxShield;
        public float shieldRechargeRate;
        public int armor;
    }
    public Vitality vitality;

    [System.Serializable]
    public class Movement
    {
        public float speed;
        public float agility;
    }
    public Movement movement;

    [System.Serializable]
    public class Powerups
    {
        public int powerUpDuration;
        public int powerUpAmplification;
        [Tooltip("The amount of health gained when the player picks up health")]
        public float pickUpHealth;
        [Tooltip("The amount of ammo gained when the player picks up ammo")]
        public float pickUpAmmo;
        [Tooltip("The amount of shield gained when the player picks up shield")]
        public float pickUpShield;
    }
    public Powerups powerups;
}
