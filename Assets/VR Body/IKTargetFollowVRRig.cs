using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Hands;
using UnityEngine.XR.Management;


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

    public void Map_Head()
    {
        ikTarget.position = vrTarget.TransformPoint(trackingPositionOffset);
        Quaternion targetRot = Quaternion.Euler(trackingRotationOffset);
        Vector3 tempRot = targetRot.eulerAngles;
        tempRot.y = 0f;
        ikTarget.rotation = Quaternion.Euler(tempRot);

    }
}

public class IKTargetFollowVRRig : MonoBehaviour
{
    [Range(0,1)]
    public float turnSmoothness = 0.1f;
    public VRMap head;
    public VRMap leftHand;
    public VRMap rightHand;

    public Vector3 headBodyPositionOffset;
    public float headBodyYawOffset;
    //private bool head_assigned = false;
    //private bool right_hand_assigned = false;
    //private bool left_hand_assigned = false;

    //private void Update()
    //{
    //    //assign head
    //    InputDevice headset = InputDevices.GetDeviceAtXRNode(XRNode.Head);

    //    if (!head_assigned)
    //    {
    //        if (headset.TryGetFeatureValue(CommonUsages.isTracked, out bool isTracked) && isTracked)
    //        {
    //            Debug.Log("Headset connected and tracking.");

    //            //assign main camera to head
    //            Camera cam = Camera.main;
    //            head.vrTarget = cam.transform;

    //            head_assigned = true;

    //        }
    //    }



    //}


    // Update is called once per frame
    void LateUpdate()
    {
        if (head.vrTarget != null)
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
}
