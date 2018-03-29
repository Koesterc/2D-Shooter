using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAttack : BaseSkills {
    public enum SkillType { Active, Passive }
    public SkillType skillType;
    public int damage;
}
