using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClampRotationScript : MonoBehaviour {
    [SerializeField]
    float minValue;
    [SerializeField]
    float maxValue;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // transform.localRotation = Quaternion.Euler(transform.localRotation.x, transform.localRotation.y, Mathf.Clamp(transform.localRotation.z,minValue,maxValue));
        //Quaternion rot = transform.localRotation;
        //float angle = Vector3.Angle(transform.up, Vector3.up);
        //if (angle > minValue)
        //{
        //    transform.localEulerAngles = new  Vector3(0,0,minValue);
        //    print(angle);
        //}
        //else if (angle > maxValue)
        //    {
        //        transform.localEulerAngles = new Vector3(0, 0, maxValue);
        //        print(angle);
        //    }


    }
}
