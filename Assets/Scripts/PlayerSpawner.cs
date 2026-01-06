using Fusion;
using UnityEngine;

public class PlayerSpawner : SimulationBehaviour, IPlayerJoined
{
    public GameObject PlayerPrefab;
    public GameObject RightHandPrefab;
    public GameObject LeftHandPrefab;
    private IKTargetFollowVRRig vrTargets;

    public void PlayerJoined(PlayerRef player)
    {
        if (player == Runner.LocalPlayer)
        {
            NetworkObject racer = Runner.Spawn(PlayerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            NetworkObject rightHand = Runner.Spawn(RightHandPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            NetworkObject leftHand = Runner.Spawn(LeftHandPrefab, new Vector3(0, 0, 0), Quaternion.identity);

            vrTargets = racer.GetComponent<IKTargetFollowVRRig>();

            Transform rightHandTransform = rightHand.transform;
            Transform leftHandTransform = leftHand.transform;
            Transform rightWrist = rightHandTransform.GetChild(0);
            Transform leftWrist = leftHandTransform.GetChild(0);

            vrTargets.head.vrTarget = Camera.main.transform;
            vrTargets.rightHand.vrTarget = rightWrist;
            vrTargets.leftHand.vrTarget = leftWrist;

        }

        
    }
}