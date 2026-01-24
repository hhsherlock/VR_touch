using System.Collections.Generic;
using Fusion;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.XR.Interaction.Toolkit;


public class PlayerSpawner : SimulationBehaviour, IPlayerJoined
{

    public GameObject PlayerPrefab;
    public GameObject RightHandPrefab;
    public GameObject LeftHandPrefab;

    public void PlayerJoined(PlayerRef player)
    {
        Debug.Log(player);
        Debug.Log(Runner.LocalPlayer);
        Debug.Log("lala");

        if (player == Runner.LocalPlayer)
        //if (Runner.IsServer)
        {
            Debug.Log("Fusion Mode: " + Runner.GameMode);
            NetworkObject racer = Runner.Spawn(PlayerPrefab, new Vector3(0, 0, 0), Quaternion.identity, player);
            NetworkObject rightHand = Runner.Spawn(RightHandPrefab, new Vector3(0, 0, 0), Quaternion.identity, player);
            NetworkObject leftHand = Runner.Spawn(LeftHandPrefab, new Vector3(0, 0, 0), Quaternion.identity, player);
        }
        else
        {
            Debug.Log("not server");
        }


        //}


    }
}