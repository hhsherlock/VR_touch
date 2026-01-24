using Unity.VisualScripting;
using UnityEngine;
using Fusion;




[System.Serializable]
public class VRMap
{
    public Transform vrTarget;
    public Transform ikTarget;
    public Vector3 trackingPositionOffset;
    public Vector3 trackingRotationOffset;

    public void Map()
    {
        ikTarget.position = vrTarget.TransformPoint(trackingPositionOffset);
        ikTarget.rotation = vrTarget.rotation * Quaternion.Euler(trackingRotationOffset);
    }

    //public void Map_Head()
    //{
    //    ikTarget.position = vrTarget.TransformPoint(trackingPositionOffset);
    //    Quaternion targetRot = Quaternion.Euler(trackingRotationOffset);
    //    Vector3 tempRot = targetRot.eulerAngles;
    //    tempRot.y = 0f;
    //    ikTarget.rotation = Quaternion.Euler(tempRot);

    //}
}

public class IKTargetFollowVRRig : NetworkBehaviour
{
    [Range(0,1)]
    public float turnSmoothness = 0.1f;
    public VRMap head;
    public VRMap leftHand;
    public VRMap rightHand;

    public Vector3 headBodyPositionOffset;
    public float headBodyYawOffset;
    private bool head_assigned = false;
    private bool right_hand_assigned = false;
    private bool left_hand_assigned = false;

    private Transform localHeadCamera;

    private void Awake()
    {
        ////assign head
        //InputDevice headset = InputDevices.GetDeviceAtXRNode(XRNode.Head);

        //if (!head_assigned)
        //{
        //    if (headset.TryGetFeatureValue(CommonUsages.isTracked, out bool isTracked) && isTracked)
        //    {
        //        Debug.Log("Headset connected and tracking.");

        //        //assign main camera to head
        //        Camera cam = Camera.main;
        //        head.vrTarget = cam.transform;

        //        head_assigned = true;

        //    }
        //}

        //------------------assign head----------------------------
        //if (!head_assigned)
        //{
            //if (Object.HasInputAuthority)
            //{
        GameObject xrOrigin = GameObject.FindObjectOfType<Unity.XR.CoreUtils.XROrigin>()?.gameObject;
        if (xrOrigin != null)
        {
            Transform cameraOffset = xrOrigin.transform.Find("Camera Offset");
            if (cameraOffset != null)
            {
                Transform cam = cameraOffset.Find("Main Camera"); // the camera inside Camera Offset
                if (cam != null)
                {
                    localHeadCamera = cam;
                    Debug.Log("found camera");
                    head.vrTarget = localHeadCamera.transform;
                    //head_assigned = true;
                }


            }
        }
            //}
            //else
            //{
            //    Debug.Log("has no input authority");
            //}
        
        //}

        
        ////--------------------assign right hand-------------------------
        //if (!right_hand_assigned)
        //{

        //}


    }


    // Update is called once per frame
    public override void FixedUpdateNetwork()
    {
        //if (head.vrTarget != null)
        //{
            
        //}
        if (Object.HasInputAuthority)
        {
            Debug.Log("fixed update");
            transform.position = head.ikTarget.position + new Vector3(0f, -0.6f, -0.1f);
            //transform.position = head.ikTarget.position;

            float yaw = head.vrTarget.eulerAngles.y;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.eulerAngles.x, yaw, transform.eulerAngles.z), turnSmoothness);

            head.Map();
            leftHand.Map();
            rightHand.Map();
        }
    }

    private void ApplyIk()
    {
        transform.position = head.ikTarget.position + new Vector3(0f, -0.6f, -0.1f);
        //transform.position = head.ikTarget.position;

        float yaw = head.vrTarget.eulerAngles.y;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.eulerAngles.x, yaw, transform.eulerAngles.z), turnSmoothness);

        head.Map();
        leftHand.Map();
        rightHand.Map();
    }
}
