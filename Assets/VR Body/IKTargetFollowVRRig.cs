using Unity.VisualScripting;
using UnityEngine;
using Fusion;
using static UnityEngine.UIElements.UxmlAttributeDescription;




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
    //public Transform headVR;
    //public Transform headIK;

    //public Transform rightHandVR;
    //public Transform rightHandIK;

    //public Transform leftHandVR;
    //public Transform leftHandIK;

    public VRMap head;
    public VRMap leftHand;
    public VRMap rightHand;


    public Vector3 headBodyPositionOffset;
    public float headBodyYawOffset;
    private bool right_hand_assigned = false;
    private bool left_hand_assigned = false;

    private Transform localHeadCamera;
    private Transform rightHandTrack;
    private Transform leftHandTrack;

    public Vector3 trackingPositionOffset;
    public Vector3 trackingRotationOffset;


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


    }




    // Update is called once per frame
    public override void FixedUpdateNetwork()
    {
        ////-----------------assign right hand-------------------------
        //NetworkObject rightHandTrack = GameObject.Find("RightHandTracking").GetComponent<NetworkObject>();
        if (!right_hand_assigned)
        {
            NetworkObject[] networkObjects = FindObjectsOfType<NetworkObject>();

            if (networkObjects != null)
            {

                foreach (var netObj in networkObjects)
                {
                    if (netObj.name == "Right Hand Tracking(Clone)")
                    {
                        rightHandTrack = netObj.transform.GetChild(0);
                        //rightHandVR = rightHandTrack;
                        rightHand.vrTarget = rightHandTrack;
                        right_hand_assigned = true;
                    }
                }
            }
        }

        if (!left_hand_assigned)
        {
            NetworkObject[] networkObjects = FindObjectsOfType<NetworkObject>();

            if (networkObjects != null)
            {

                foreach (var netObj in networkObjects)
                {
                    if (netObj.name == "Left Hand Tracking(Clone)")
                    {
                        leftHandTrack = netObj.transform.GetChild(0);
                        leftHand.vrTarget = leftHandTrack;
                        left_hand_assigned = true;
                    }
                }
            }
        }



        if (Object.HasInputAuthority)
        {
            //transform.position = headVR.position + new Vector3(0f, -0.6f, -0.1f);

            //float yaw = headVR.eulerAngles.y;
            //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.eulerAngles.x, yaw, transform.eulerAngles.z), turnSmoothness);

            //// head
            //headIK.position = headVR.TransformPoint(trackingPositionOffset);
            //headIK.rotation = headVR.rotation * Quaternion.Euler(trackingRotationOffset);

            //rightHandIK.position = rightHandVR.TransformPoint(trackingPositionOffset);
            //rightHandIK.rotation = rightHandVR.rotation * Quaternion.Euler(trackingRotationOffset);

            //leftHandIK.position = leftHandVR.TransformPoint(trackingPositionOffset);
            //leftHandIK.rotation = leftHandVR.rotation * Quaternion.Euler(trackingRotationOffset);


            transform.position = head.vrTarget.position + new Vector3(0f, -0.6f, -0.1f);

            float yaw = head.vrTarget.eulerAngles.y;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.eulerAngles.x, yaw, transform.eulerAngles.z), turnSmoothness);

            // head
            head.ikTarget.position = head.vrTarget.TransformPoint(trackingPositionOffset);
            head.ikTarget.rotation = head.vrTarget.rotation * Quaternion.Euler(trackingRotationOffset);

            rightHand.ikTarget.position = rightHand.vrTarget.TransformPoint(trackingPositionOffset);
            rightHand.ikTarget.rotation = rightHand.vrTarget.rotation * Quaternion.Euler(trackingRotationOffset) * Quaternion.Euler(90f, 0f, 0f);

            leftHand.ikTarget.position = leftHand.vrTarget.TransformPoint(trackingPositionOffset);
            leftHand.ikTarget.rotation = leftHand.vrTarget.rotation * Quaternion.Euler(trackingRotationOffset) * Quaternion.Euler(90f, 0f, 0f);

            //head.Map();
            //leftHand.Map();
            //rightHand.Map();
        }
    }
}