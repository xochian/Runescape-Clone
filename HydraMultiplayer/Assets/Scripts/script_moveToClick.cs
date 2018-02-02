using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class script_moveToClick : NetworkBehaviour
{
    private NavMeshAgent agent;
    public GameObject ringArrow;

    void Start()
    {
        if (!isLocalPlayer)
            return;

        agent = GetComponent<NavMeshAgent>();
    }
    enum test { One, Two, Three }
    void Update()
    {
        if (!isLocalPlayer)
            return;

        RaycastHit hit;
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag != "Floor")
                    return;

                Vector3 point = hit.point;

                GameObject rArrow = GameObject.Instantiate(ringArrow);
                rArrow.transform.position = point;

                agent.SetDestination(point);
            }
        }
    }
}
