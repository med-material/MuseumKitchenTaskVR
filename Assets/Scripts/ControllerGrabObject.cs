using UnityEngine;

public class ControllerGrabObject : MonoBehaviour
{

    private SteamVR_TrackedObject trackedObj;
    private GameObject collidingObject;
    private Quaternion oldRot;
    private Transform oldparent;
    private Color shadowBun;
    private MeshRenderer rend;
    private Material bunMat;

    //public Collision table;
    public bool trigger;
    // public Collider tableCollider;

    public static GameObject objectInHand;
    public static string ctrlEvent = "null";


    // used for data logging in LoggerBehavior.cs
    public class bunLastActive
    {
        public static GameObject bunActive
        {
            get
            {
                return objectInHand;
            }
        }
        // public static float bunLastActivePosX
        // {
        //     get
        //     {
        //         return objectInHand.transform.position.x;
        //     }
        // }
        public static string ctrlEventHolder
        {
            get
            {
                return ctrlEvent;
            }
        }
    }

    // TODO: if bun is not on table, reset position




    void Start()
    {
        //shadowBun = new Color(0f, 0f, 0f, 0.8f);
        //rend = objectInHand.GetComponent<MeshRenderer>();
        //bunMat = new Material(Shader.Find("DoughTextureFilter"));
    }


    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        trigger = false;
    }

    private void SetCollidingObject(Collider col)
    {
        if (collidingObject || !col.GetComponent<Rigidbody>())
        {
            return;
        }
        collidingObject = col.gameObject;
    }

    public void OnTriggerEnter(Collider other)
    {
        SetCollidingObject(other);
        //bunMat.color = shadowBun;
        //rend.material = bunMat;

        // restricts bun from moving through the table
        if (other.gameObject.name == "tableTop")// && other.gameObject == objectInHand
        {
            ReleaseObject();
            ctrlEvent = "ctrl_table_col";
        }
    }

    public void OnTriggerStay(Collider other)
    {
        SetCollidingObject(other);
    }

    public void OnTriggerExit(Collider other)
    {

        if (!collidingObject)
        {
            return;
        }

        collidingObject = null;
    }

    private void GrabObject()
    {
        objectInHand = collidingObject;
        collidingObject = null;
        oldRot = objectInHand.transform.rotation;
        oldparent = objectInHand.transform.parent;
        objectInHand.transform.parent = trackedObj.transform;
        objectInHand.GetComponent<Rigidbody>().isKinematic = true;
        objectInHand.GetComponent<BoxCollider>().enabled = false;

        //var joint = AddFixedJoint();
        //joint.connectedBody = objectInHand.GetComponent<Rigidbody>();
    }

    //private FixedJoint AddFixedJoint()
    //{
    //    FixedJoint fx = gameObject.AddComponent<FixedJoint>();
    //    fx.breakForce = 20000;
    //    fx.breakTorque = 20000;
    //    return fx;
    //}

    private void ReleaseObject()
    {
        float x;
        float z;
        if (objectInHand != null)
        {
            objectInHand.transform.parent = oldparent;
            objectInHand.transform.rotation = oldRot;
            if (objectInHand.transform.position.y < 0)
            {
                x = objectInHand.transform.position.x;
                z = objectInHand.transform.position.z;
                objectInHand.transform.position = new Vector3(x, 0.0f, z);
            }

            objectInHand.GetComponent<Rigidbody>().isKinematic = false;
            objectInHand.GetComponent<BoxCollider>().enabled = true;
        }

        //if (GetComponent<FixedJoint>())
        //{
        //    // 2
        //    GetComponent<FixedJoint>().connectedBody = null;
        //    Destroy(GetComponent<FixedJoint>());
        //    // 3
        //    objectInHand.GetComponent<Rigidbody>().velocity = Controller.velocity;
        //    objectInHand.GetComponent<Rigidbody>().angularVelocity = Controller.angularVelocity;
        //}
        objectInHand = null;
    }

    void Update()
    {
        if (Controller.GetHairTriggerDown())
        {
            trigger = true;
            if (collidingObject)
            {
                ctrlEvent = "holding_obj";
                GrabObject();
            }
            else
                ctrlEvent = "pressing_btn";
        }

        else if (Controller.GetHairTriggerUp())
        {
            trigger = false;
            if (objectInHand)
            {
                ReleaseObject();
                ctrlEvent = "released_obj";
            }
            else
                ctrlEvent = "released_btn";

        }
    }
}
