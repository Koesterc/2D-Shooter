using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepScript : MonoBehaviour {
    [SerializeField]
    AudioController au;

    public void Play()
    {
        au.Play();
    }
}
