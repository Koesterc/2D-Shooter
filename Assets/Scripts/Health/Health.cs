using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;

public class Health : NetworkBehaviour
{
    [HideInInspector]
    public float health { get; private set; }
    public float maxHealth;
    public float maxShield;
    float shield;
    public int defense;
    [SerializeField]
    GameObject deathParticle;
    [SerializeField]
    GameObject damageText;
    IEnumerator poisonEffect;
    bool isPlayer;
    [HideInInspector]
    public Image multiPlayerHealth;

    // Use this for initialization
    void Start()
    {
        if (gameObject.tag == "Player")
        {
            isPlayer = true;
        }
        if (isPlayer)
        {
            UpdateStats();
        }
        else
        {
            health = maxHealth;
            shield = maxShield;
        }
    }

    public void UpdateStats()
    {
        StartCoroutine(Stats());
    }
    IEnumerator Stats()
    {
        yield return null;
        health = GameManager.instance.playerStats.vitality.maxHealth;
        shield = GameManager.instance.playerStats.vitality.maxShield;
        defense = GameManager.instance.playerStats.vitality.armor;
        UpdateHealthBar();
    }

    public void TakeDamage(float damage)
    {
        float dmg = damage;
        damage -= shield;
        shield -= dmg;
        if (shield <= 0)
            shield = 0;
        if (defense <= (damage * .1f))
            damage -= defense;
        if (damage <= 0)
            damage = 0;
        health -= damage;
        if (isPlayer)
        {
            if (health <= 0)
                health = 0;
            UpdateHealthBar();
            return;
        }
        if (health <= 0 && !isPlayer)
        {
            health = 0;
            Death();
            return;
        }

    }
    public void TakePoisonDamage(float damage)
    {
        if (poisonEffect != null)
            StopCoroutine(poisonEffect);
        poisonEffect = PoisonEffect(damage);
        StartCoroutine(poisonEffect);
    }

    IEnumerator PoisonEffect(float damage)
    {
        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
        sr.color = Color.blue;
        float temp = damage * .1f;
        while (damage > 0 && health > 0)
        {
            yield return new WaitForSeconds(1f);
            damage -= temp;
            if (damage <= 0)
                damage = 0;
            health -= temp;
            if (health <= 0)
            {
                health = 0;
                Death();
            }
            GameObject floatingText = Instantiate(damageText, transform.position, transform.rotation);
            floatingText.GetComponentInChildren<TextMeshProUGUI>().text = "-" + temp.ToString("n0");
            Destroy(floatingText, 1f);
        }
        if (isPlayer)
            UpdateHealthBar();
        sr.color = Color.white;
    }

    void UpdateHealthBar()
    {
        GameManager.instance.canvasController.uiBars.healthBar.fillAmount = (health / maxHealth);
        if (multiPlayerHealth)
            multiPlayerHealth.transform.localScale = new Vector3(health / maxHealth,1,1);
        GameManager.instance.canvasController.uiTexts.healthCount.text = ((int)health).ToString("n0");
        GameManager.instance.coroutineHandler.healthHandler.DrainHP();
        GameManager.instance.coroutineHandler.healthHandler.DrainShield(shield,maxShield);
        if (health <= 0)
            Death();

    }
    void Death()
    {
        Destroy(Instantiate(deathParticle, transform.position, transform.rotation), 1.16f);
        if (!isPlayer)
            Destroy(gameObject);
        else
        {
            GameManager.instance.canvasController.uiBars.healthBar.fillAmount = 0;
            GameObject pc = GetComponentInParent<PlayerController>().gameObject;
            NetworkServer.UnSpawn(pc);
            pc.SetActive(false);
        }
    }
}
