//using UnityEngine;
//using UnityEngine.XR;
//using UnityEngine.XR.Hands;
//using System.Collections.Generic;
//using UnityEngine.InputSystem.XR;

//public class HandTrackingLogger : MonoBehaviour
//{
//    // HandSubsystem reference
//    private XRHandSubsystem handSubsystem;

//    void Start()
//    {
//        // Get the active hand subsystem
//        List<XRHandSubsystem> subsystems = new List<XRHandSubsystem>();
//        SubsystemManager.GetInstances(subsystems);

//        if (subsystems.Count > 0)
//            handSubsystem = subsystems[0];
//        else
//            Debug.LogWarning("No XRHandSubsystem found!");
//    }

//    void Update()
//    {
//        if (handSubsystem == null) return;

//        // Check if left hand is tracked
//        XRHand leftHand = handSubsystem.leftHand;
//        XRHand rightHand = handSubsystem.rightHand;

//        if (leftHand.isTracked)
//            LogHandData("Left", leftHand);

//        if (rightHand.isTracked)
//            LogHandData("Right", rightHand);
//    }

//    private void LogHandData(string handName, XRHand hand)
//    {
//        // Hand root position & rotation
//        Vector3 handPosition = hand.rootPose.position;
//        Quaternion handRotation = hand.rootPose.rotation;

//        // Pinch detection
//        bool pinch = hand.GetFingerIsPinching(XRHandFinger.Index);

//        Debug.Log($"{handName} Hand Position: {handPosition}, Rotation: {handRotation}");
//        Debug.Log($"{handName} Hand Index Pinch: {pinch}");

//        // Optional: log each finger's curl
//        for (int i = 0; i < 5; i++)
//        {
//            XRHandFinger finger = (XRHandFinger)i;
//            float curl = hand.GetFingerCurl(finger);
//            Debug.Log($"{handName} Finger {finger} Curl: {curl:F2}");
//        }
//    }
//}
