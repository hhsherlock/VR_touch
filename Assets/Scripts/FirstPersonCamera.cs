using UnityEngine;
using UnityEngine.XR;

public class FirstPersonCamera : MonoBehaviour
{
    public Transform Target;
    public XRNode HeadNode = XRNode.Head;
    public bool MatchPosition = true;

    void LateUpdate()
    {
        if (Target == null)
        {
            return;
        }

        InputDevice headDevice = InputDevices.GetDeviceAtXRNode(HeadNode);

        if (headDevice.TryGetFeatureValue(CommonUsages.centerEyeRotation, out Quaternion headRotation))
        {
            transform.rotation = headRotation;
        }

        if (MatchPosition && headDevice.TryGetFeatureValue(CommonUsages.centerEyePosition, out Vector3 headPosition))
        {
            Debug.Log("got center eye");
            transform.position = Target.position + headPosition;
            Debug.Log(Target.position);
            Debug.Log(headPosition);
        }
        else
        {
            transform.position = Target.position;
        }
    }
}