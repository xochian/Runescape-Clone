using UnityEngine;
using System.Collections;

public class script_killOnAnimEnd : MonoBehaviour
{
    public void Die()
    {
        Destroy(gameObject.transform.parent.parent.gameObject);
    }
}
