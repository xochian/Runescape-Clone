using UnityEngine;
using System.Collections;

using UnityEngine.Networking;

public class script_networkRotation : NetworkBehaviour
{
    [SyncVar]
    public Quaternion rotation;

    void Start()
    {
        transform.rotation = rotation;
    }
}
