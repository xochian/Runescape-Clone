using UnityEngine;
using System.Collections;

public class script_nameFollow : MonoBehaviour {

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        transform.eulerAngles = new Vector3(35.0f, 0, 0);
	}
}
