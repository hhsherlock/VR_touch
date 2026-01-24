using Fusion;
using UnityEngine;
using UnityEngine.XR;

public class AssignIK : NetworkBehaviour
{
    //public IKTargetFollowVRRig vrTargets;
    private Transform localHeadCamera;

    public override void Spawned()
    {
        // ONLY the local player assigns VR input
        if (Object.HasInputAuthority)
        {
            // get the vr variables from the IKTargetFollowVRRig script
            IKTargetFollowVRRig vrIks = GetComponent<IKTargetFollowVRRig>();


            if (vrIks != null)
            {
                // assign camera
                GameObject xrOrigin = FindObjectOfType<Unity.XR.CoreUtils.XROrigin>()?.gameObject;
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
                            vrIks.head.vrTarget = localHeadCamera.transform;
                        }


                    }
                }

            }
        }
        else
        {
            Debug.Log("does not have input authority");
        }
    }
}
