using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TablePos : MonoBehaviour
{

    private Vector3 InitialPos;
    private Vector3 hmdPos;
    public GameObject HMD;
    public Transform CameraPos;

    // Use this for initialization
    void Start()
    {
        InitialPos = transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        hmdPos = HMD.transform.position;
        transform.position = InitialPos - hmdPos;
        hmdPos = HMD.transform.localPosition;
        transform.position = CameraPos.position - hmdPos;
    }
}
