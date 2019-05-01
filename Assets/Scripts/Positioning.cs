using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Positioning : MonoBehaviour
{

    public Transform myPlay;


    // Update is called once per frame
    void Update()
    {
        transform.position = myPlay.position;
    }
}
