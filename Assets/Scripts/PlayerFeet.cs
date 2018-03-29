using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFeet : MonoBehaviour {
    public bool onGround { get; private set; }


    private void OnTriggerStay2D(Collider2D other)
    {
        onGround = true;
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        onGround = false;
    }
}
