using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketWith2DCalibration : MonoBehaviour
{
    //public static Camera HMDcam;
    [SerializeField] private Transform seatCenter;
    [SerializeField] private Transform facilitatorCam;
    [SerializeField] private Transform zoomOut;



    private Camera sceneCamera;
    //private CalibrationDemo calibrationDemo;
    private LineRenderer heading;
    private Vector3 standardViewportPoint = new Vector3(0.5f, 0.5f, 10);
    private Vector2 gazePointLeft, gazePointRight, gazePointCenter;
    private int oldCulMask;

    private float offsetAngleX, offsetAngleY; // log? public static
    //private static float offsetAngleZ;
    private bool HMDgazeDots = false;
    private bool moveFacilitatorCam = false;
    private bool camMove = true;


    void Start()
    {
        PupilData.calculateMovingAverage = false;
        //calibrationDemo = gameObject.GetComponent<CalibrationDemo>();
        //sceneCamera = Camera.main;
        sceneCamera = gameObject.GetComponent<Camera>();
        heading = gameObject.GetComponent<LineRenderer>();
        oldCulMask = sceneCamera.cullingMask;
        RecenterSeat();
    }

    void OnEnable()
    {
        if (PupilTools.IsConnected)
        {
            PupilTools.IsGazing = true;
            PupilTools.SubscribeTo("gaze");
        }
    }

    void Update()
    {
        // recenter seat and simulate a blink, 
        // works best with press and hold one second, could be implemented as an eye blink
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     resetSeat();
        //     blackCube.SetActive(true);
        // }
        // else if (Input.GetKeyUp(KeyCode.Space))
        //     blackCube.SetActive(false);
        // change camera rotation
        // else if (Input.GetKeyUp(KeyCode.X))
        //     changePitch();

        // toggle line renderer 
        if (Input.GetKeyUp(KeyCode.L))
            heading.enabled = !heading.enabled;
        // toggle gaze dots         
        else if (Input.GetKeyUp(KeyCode.H))
            HMDgazeDots = !HMDgazeDots;
        // press and hold for changing facilitator's view: follow HMD (default) or positioned behind HMD (used in Baking tray to get an overview of controller movements)
        else if (Input.GetKeyDown(KeyCode.B))
            facilitatorCam.position = zoomOut.position;
        else if (Input.GetKeyUp(KeyCode.B))
            facilitatorCam.position = sceneCamera.transform.position;

        Vector3 viewportPoint = standardViewportPoint;
        // for line renderering 
        if (heading.enabled)
        {
            heading.SetPosition(0, sceneCamera.transform.position - sceneCamera.transform.up);

            Ray ray = sceneCamera.ViewportPointToRay(viewportPoint);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                heading.SetPosition(1, hit.point);
            }
            else
            {
                heading.SetPosition(1, ray.origin + ray.direction * 50f);
            }
        }

        // enable and disable the gazeDots layer in camera 
        if (HMDgazeDots)
            sceneCamera.cullingMask = oldCulMask | (1 << 11); // make layer 11 visible (gazeDots)
        else
            sceneCamera.cullingMask = oldCulMask;

    }


    // resets seat to zero in position and rotation (used when the recenterSeat doesn't work) 
    public void ResetSeat()
    {
        seatCenter.position = Vector3.zero;
        seatCenter.rotation = Quaternion.identity;
    }

    // resets the scene position and rotation to the camera, logged variables in PupilManager 
    public void RecenterSeat()
    {
        seatCenter.transform.position = facilitatorCam.position;
        offsetAngleY = 0f;
        offsetAngleY = facilitatorCam.transform.rotation.eulerAngles.y - seatCenter.rotation.eulerAngles.y;
        seatCenter.Rotate(0f, offsetAngleY, 0f);
        seatCenter.position = facilitatorCam.transform.position;

        offsetAngleX = 0f;
        offsetAngleX = facilitatorCam.transform.rotation.eulerAngles.x - seatCenter.rotation.eulerAngles.x;
        seatCenter.Rotate(offsetAngleX, 0f, 0f);
        // not good to rotate on z-axis:
        //offsetAngleZ = 0f;
        //offsetAngleZ = facilitatorCam.transform.rotation.eulerAngles.z - seatCenter.rotation.eulerAngles.z;
        //seatCenter.Rotate(0f, 0f, offsetAngleZ);
    }
}
