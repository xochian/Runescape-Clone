using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class script_waitForPlayerFollow : MonoBehaviour
{
	
	void Update ()
    {

        GameObject[] pL;
        if ( (pL = GameObject.FindGameObjectsWithTag("Player")) != null && target == null)
        {
            foreach (GameObject p in pL)
                if (p.GetComponent<NetworkIdentity>().isLocalPlayer)
                {
                    target = p;
                    transform.position += target.transform.position;
                }
        }    
	}

    public GameObject target;
    public float xOffset = 0;
    public float yOffset = 0;
    public float zOffset = 0;
    public float sensitivity = 0.5f;

    void LateUpdate()
    {
        if(target != null)
        this.transform.position = Vector3.Lerp(this.transform.position, new Vector3(target.transform.position.x + xOffset,
                                              target.transform.position.y + yOffset,
                                              target.transform.position.z + zOffset), Time.deltaTime * sensitivity);
    }
}