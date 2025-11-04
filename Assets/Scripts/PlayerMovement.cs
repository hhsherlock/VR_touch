using Fusion;
using UnityEngine;
using UnityEngine.XR;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : NetworkBehaviour
{
    public Camera Camera;
    public float GravityValue = -9.81f;

    private CharacterController _controller;
    private Vector3 _velocity;
    //private Vector3 _initialHeadsetOffset;
    //private bool headInitialised = false;

    public override void Spawned()
    {
        if (HasStateAuthority)
        {
            Camera = Camera.main;

            // Link the camera to the player's transform
            var headCamera = Camera.GetComponent<FirstPersonCamera>();
            if (headCamera != null)
                headCamera.Target = transform;
            
        }
        else
        {
            // Disable camera for remote (non-local) players
            if (Camera != null)
                Camera.enabled = false;
        }
    }

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    //private void Update()
    //{
    //    if (!headInitialised) {
    //        InputDevice headDevice = InputDevices.GetDeviceAtXRNode(XRNode.Head);

    //        Debug.Log(headDevice.TryGetFeatureValue(CommonUsages.centerEyePosition, out Vector3 headPos));
    //        Debug.Log("this line runs");
    //        Debug.Log(headPos);
    //        _initialHeadsetOffset = headPos;
    //        Debug.Log(_initialHeadsetOffset);
    //        headInitialised = true;
            
    //    }
        
    //}

    public override void FixedUpdateNetwork()
    {
        if (!HasStateAuthority)
            return;

        // Get headset rotation (Y-axis only)
        InputDevice headDevice = InputDevices.GetDeviceAtXRNode(XRNode.Head);
        if (!headDevice.isValid)
            return;

        if (headDevice.TryGetFeatureValue(CommonUsages.centerEyeRotation, out Quaternion headRotation) &&
            headDevice.TryGetFeatureValue(CommonUsages.centerEyePosition, out Vector3 headPosition))
        {
            if (_controller.isGrounded && _velocity.y < 0)
                _velocity.y = -1f;
            else
                _velocity.y += GravityValue * Runner.DeltaTime;

            Vector3 worldHeadPosition = Camera.transform.TransformPoint(headPosition);
            Vector3 horizontalMove = new Vector3(
                worldHeadPosition.x - transform.position.x,
                0,
                worldHeadPosition.z - transform.position.z
                );

            _controller.Move(horizontalMove + _velocity*Runner.DeltaTime);
            transform.rotation = Quaternion.Euler(0, headRotation.eulerAngles.y, 0);
        }
    }
}