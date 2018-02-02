using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class script_chatInput : MonoBehaviour
{
    public GameObject chatLog;
    Text chatComp;
    InputField inputF;


    void Start()
    {
        chatComp = chatLog.GetComponent<Text>();
        inputF = GetComponent<InputField>();
    }

    void Update()
    {
        if(ClientScene.ready)
            ClientScene.readyConnection.RegisterHandler(1002, script_networkManagerGUI.UpdateChatLog);
    }

    public void SendInput()
    {
        
        TransferMessage("[" + System.DateTime.Now.Hour + ":" +System.DateTime.Now.Minute + "] " + script_networkManagerGUI.playerName + ": " + inputF.text);
        inputF.text = "";
    }

    const short MyBeginMsg = 1002;

    NetworkClient client;

    void TransferMessage(string sendMe)
    {
        StringMessage sMessage = new StringMessage(sendMe);


        if (NetworkServer.active)
        {
            NetworkServer.SendToAll(MyBeginMsg, sMessage);
        }
        else
        {
            ClientScene.readyConnection.Send(1003, sMessage);
        }



    }
}
