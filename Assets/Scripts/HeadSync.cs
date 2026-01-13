using UnityEngine;
using Fusion;

public class HeadSync : NetworkBehaviour
{
    public Transform Cam;

    public override void FixedUpdateNetwork()
    {
        if (!Object.HasInputAuthority)
            return;

        transform.SetPositionAndRotation(
            Cam.position,
            Cam.rotation
        );
    }
}
