using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrades : MonoBehaviour
{

    [System.Serializable]
    public class Damage
    {
        public float firePower;
        public int curLvl;
        public int maxLvl;
        public int cost;
    }

    public Damage damage;

    [System.Serializable]
    public class FireRate
    {
        public float attackSpeed;
        public int curLvl;
        public int maxLvl;
        public int cost;
    }
    public FireRate fireRate;

    [System.Serializable]
    public class Accuracy
    {
        public float accuracy;
        public int curLvl;
        public int maxLvl;
        public int cost;
    }
    public Accuracy accuracy;

    [System.Serializable]
    public class AP
    {
        public float ap;
        public int curLvl;
        public int maxLvl;
        public int cost;
    }
    public AP ap;


    public float UpgradeDamage(float _damage)
    {
        _damage += damage.firePower;
        return _damage;
    }
}

