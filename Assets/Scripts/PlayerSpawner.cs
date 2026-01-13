using Fusion;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class PlayerSpawner : SimulationBehaviour, IPlayerJoined
{
    private Transform localHeadCamera;

    private void Awake()
    {
        // Find the XR Origin in the scene
        GameObject xrOrigin = GameObject.FindObjectOfType<UnityEngine.XR.Interaction.Toolkit.XRRig>()?.gameObject;

        if (xrOrigin != null)
        {
            // Camera is usually under Camera Offset
            Transform cameraOffset = xrOrigin.transform.Find("Camera Offset");
            if (cameraOffset != null)
            {
                Transform cam = cameraOffset.Find("Main Camera"); // the camera inside Camera Offset
                if (cam != null)
                {
                    localHeadCamera = cam;
                }
                else
                {
                    Debug.LogError("Main Camera not found under Camera Offset");
                }
            }
            else
            {
                Debug.LogError("Camera Offset not found under XR Origin");
            }
        }
        else
        {
            Debug.LogError("XR Origin not found in scene!");
        }
    }


    public GameObject PlayerPrefab;
    public GameObject RightHandPrefab;
    public GameObject LeftHandPrefab;
    private IKTargetFollowVRRig vrTargets;

    public void PlayerJoined(PlayerRef player)
    {
        if (player == Runner.LocalPlayer)
        {
            
            Debug.Log("Fusion Mode: " + Runner.GameMode);
            NetworkObject racer = Runner.Spawn(PlayerPrefab, new Vector3(0, 0, 0), Quaternion.identity, player);
            NetworkObject rightHand = Runner.Spawn(RightHandPrefab, new Vector3(0, 0, 0), Quaternion.identity, player);
            NetworkObject leftHand = Runner.Spawn(LeftHandPrefab, new Vector3(0, 0, 0), Quaternion.identity, player);

            vrTargets = racer.GetComponent<IKTargetFollowVRRig>();

            Transform rightHandTransform = rightHand.transform;
            Transform leftHandTransform = leftHand.transform;
            Transform rightWrist = rightHandTransform.GetChild(0);
            Transform leftWrist = leftHandTransform.GetChild(0);

            Debug.Log(vrTargets);
            //Debug.Log(Camera.main.transform);
            //vrTargets.head.vrTarget = Camera.main.transform;
            vrTargets.rightHand.vrTarget = rightWrist;
            vrTargets.leftHand.vrTarget = leftWrist;

        }

        
    }
}