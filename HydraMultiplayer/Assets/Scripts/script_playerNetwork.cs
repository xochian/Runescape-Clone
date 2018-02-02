using UnityEngine;
using System.Collections;

using UnityEngine.Networking;

public class script_playerNetwork : NetworkBehaviour
{
    [SyncVar]
    private Color myColor;

    [SyncVar]
    private string myName;

    public TextMesh nameTag;

    void Start()
    {
        if (isLocalPlayer)
        {
            myColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
            transform.position = GameObject.FindGameObjectWithTag("Respawn").transform.position + new Vector3(0,0.5f,0);
            myName = script_networkManagerGUI.playerName;
            GetComponent<Renderer>().material.color = myColor;

            TransmitColor();
            TramsmitName();

            nameTag.text = myName;
        }
        else
        {

        }
    }

    public void ChangeName(string newName)
    {
        myName = newName;
        TramsmitName();
    }

    void Update()
    {
        if (!isLocalPlayer)
        {
            GetComponent<Renderer>().material.color = myColor;
            nameTag.text = myName;
        }
    }

    [Command]
    void Cmd_ProvideColorToServer(Color c)
    {

        myColor = c;
    }

    [Command]
    void Cmd_ProvideNameToServer(string n)
    {

        myName = n;
    }

    [ClientCallback]
    void TransmitColor()
    {
        if (isLocalPlayer)
        {
            Cmd_ProvideColorToServer(myColor);
        }
    }

    [ClientCallback]
    void TramsmitName()
    {
        if (isLocalPlayer)
        {
            Cmd_ProvideNameToServer(myName);
        }
    }
}
