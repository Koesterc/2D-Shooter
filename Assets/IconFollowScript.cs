using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconFollowScript : MonoBehaviour {

    [HideInInspector]
    public Transform target;
    public float offSet;
	// Update is called once per frame
	void LateUpdate () {
        if (target == null)
            return;
        transform.position = target.localPosition;
        //transform.position = new Vector3(Mathf.Clamp(transform.position.x, -Screen.width + offSet, Screen.width - offSet), Mathf.Clamp(transform.position.y, -Screen.height + offSet, Screen.height - offSet),0);

	}
}
