using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{
    float speed;
    public Rigidbody2D rb;
    [SerializeField]
    PlayerFeet feet;
    [SerializeField]
    Animator anim;
    public static ShootingScript shootingScript;
    public static AmmoScript ammoScript;
    public static BulletPool bulletPool;
    public int playerID;
    [HideInInspector]
    public Transform checkPoint;
    bool isDoubleJumping;

    [HideInInspector]
    Transform character;

    public override void OnStartLocalPlayer()
    {
      //  anim.gameObject.GetComponent<NetworkAnimator>().SetParameterAutoSend(0, true);

    }
    public override void PreStartClient()
    {
     //   anim.gameObject.GetComponent<NetworkAnimator>().SetParameterAutoSend(0, true);
    }


    public void Start()
    {
        shootingScript = gameObject.GetComponent<ShootingScript>();
        ammoScript = gameObject.GetComponent<AmmoScript>();
        bulletPool = GameObject.Find("BulletPool").GetComponent<BulletPool>();
        character = transform.Find("Character");
        GameManager.instance.playerStats = transform.Find("StatsandSkills/PlayerStats").GetComponent<PlayerStats>();
        GameManager.instance.playerSkills = transform.Find("StatsandSkills/PlayerSkills").GetComponent<PlayerSkills>();
        speed = GameManager.instance.playerStats.movement.speed;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Crouch();
    }

    void Move()
    {
        if (shootingScript.isBusy)
            return;
        if (anim.GetBool("isSliding"))
            return;
        float h = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        if (Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Horizontal") < 0)
        {//determining which direction the player is facing after releasing the joystick
            float temp = Input.GetAxis("Horizontal");
            if (temp > 0)
            {
                temp = 1;
            }
            else
            {
                temp = -1;
            }
            anim.SetFloat("Dir", temp);
        }
        Animation();
        character.Translate(h, 0, 0);
    }
    void Animation()
    {
        if (Input.GetButtonDown("Fire2") && feet.onGround && Input.GetAxis("Vertical") >= 0)
        {
            rb.AddForce(transform.up * GameManager.instance.playerStats.movement.agility);
        }
        else if (Input.GetButtonDown("Fire2") && !feet.onGround)
        {
            if (!isDoubleJumping)
            {
                isDoubleJumping = true;
                anim.Play("DoubleJump", 0, 0);
                rb.AddForce(transform.up * (GameManager.instance.playerStats.movement.agility));
            }
        }
        anim.SetBool("isJumping", !feet.onGround);
        if (!anim.GetBool("isJumping") && isDoubleJumping)
            isDoubleJumping = false;

        anim.SetFloat("Horizontal", -Input.GetAxis("Horizontal"));
        if (Input.GetAxis("Horizontal") != 0)
            anim.SetBool("isRunning", true);
        else
            anim.SetBool("isRunning", false);

        anim.SetBool("isShooting", shootingScript.fire);
    }
    void Crouch()
    {
        if (Input.GetAxis("Vertical") < 0 && feet.onGround)
        {
            speed = 0;
            anim.SetBool("isCrouching", true);
            if (Input.GetButtonDown("Fire2") && !anim.GetBool("isSliding") && !shootingScript.fire)
            {
                shootingScript.enabled = false;
                anim.SetBool("isSliding", true);
                rb.AddForce(transform.right * (anim.GetFloat("Dir") * GameManager.instance.playerStats.movement.agility));
                StartCoroutine(Unslide());
            }
        }
        else
        {
            if (!anim.GetBool("isSliding"))
                speed = GameManager.instance.playerStats.movement.speed;
            anim.SetBool("isCrouching", false);
        }
    }
    IEnumerator Unslide()
    {
        yield return new WaitForSeconds(.5f);
        shootingScript.enabled = true;
        anim.SetBool("isSliding", false);
        speed = GameManager.instance.playerStats.movement.speed;
    }
}
