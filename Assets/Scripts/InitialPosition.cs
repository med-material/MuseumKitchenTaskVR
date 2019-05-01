using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;
using Valve.VR;

public class InitialPosition : MonoBehaviour
{
    Transform parentObj;
    Vector3 startPos;
    Vector3 originalPos;

    void Start()
    {
        startPos = transform.localPosition;
        originalPos = transform.localPosition;
        parentObj = transform.root;
    }

    void Update()
    {
        ResetVR();
    }

    void ResetVR()
    {
        if (parentObj != null)
        {
            startPos -= UnityEngine.XR.InputTracking.GetLocalPosition(UnityEngine.XR.XRNode.CenterEye);

            Quaternion tempRot = Quaternion.Inverse(parentObj.localRotation);
            Vector3 newAngle = tempRot.eulerAngles;
            transform.localEulerAngles = new Vector3(originalPos.x, originalPos.y, originalPos.z);
        }
    }
}
