using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSkills : MonoBehaviour {
    public enum Skill { Agility, Attack, Defensive }
    public Skill skill;
    public string skillName;
    public int levelRequirement;
    public int energyCost;
    public float duration;
}
