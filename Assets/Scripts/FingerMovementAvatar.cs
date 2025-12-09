using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Hands;

public class FingerMovementAvatar : MonoBehaviour
{
    public Transform leftHandBone;
    public Transform rightHandBone;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //XRHandSubsystem handSubsystem = GetComponent<XRHandSubsystem>();
        //XRHand righthand;
        //righthand = handSubsystem.rightHand;
        
        ////InputDevice righthand;
        ////righthand = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

        ////righthand.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 position);
        ////righthand.TryGetFeatureValue(CommonUsages.deviceRotation, out Quaternion rotation);
        //Debug.Log(righthand);
        
    }
}
