using UnityEngine;
using System.Collections;

public class Furnace_Behavior : MonoBehaviour {

    public GameObject uiObject;

	// Use this for initialization
	void Start ()
    {
        uiObject.SetActive(true);	    
	}
	
	// Update is called once per frame
	public void ToggleUI(bool state)
    {
        uiObject.SetActive(state);

	}
}
