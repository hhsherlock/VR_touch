using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

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
    private bool assigned = false;

    private void Update()
    {
        InputDevice headset = InputDevices.GetDeviceAtXRNode(XRNode.Head);

        // Check if a headset device exists
        if (!headset.isValid)
        {
            Debug.Log("No VR headset detected.");
            return;
        }

        // Check if tracking is active
        if (headset.TryGetFeatureValue(CommonUsages.isTracked, out bool isTracked) && isTracked)
        {
            Debug.Log("Headset connected and tracking.");
            if (!assigned)
            {
                Camera cam = Camera.main; 
                head.vrTarget = cam.transform;
                assigned = true;
            }
        }
        else
        {
            Debug.Log("Headset detected but NOT tracking.");
        }
    }


    // Update is called once per frame
    void LateUpdate()
    {
        if (head.vrTarget != null)
        {
            transform.position = head.ikTarget.position + headBodyPositionOffset + new Vector3(0f, -0.6f, -0.1f);
        
            Debug.Log("this line runs");
            float yaw = head.vrTarget.eulerAngles.y;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.eulerAngles.x, yaw, transform.eulerAngles.z), turnSmoothness);

            head.Map();
            leftHand.Map();
            rightHand.Map();
        }
    }
}
