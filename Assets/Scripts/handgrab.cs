using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// used for playing the hand animations
public class handgrab : MonoBehaviour
{

    [SerializeField] private Animator anim;
    [SerializeField] private ControllerGrabObject CtrlRight;


    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        if (CtrlRight.trigger)
        {
            anim.speed = 20.0f;
            anim.Play("Take 001");
        }
        else
        {
            anim.speed = -20.0f;
            anim.Play("Take 002");
        }
    }
}
