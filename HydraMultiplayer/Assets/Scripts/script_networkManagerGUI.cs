using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine.UI;

public class script_networkManagerGUI : MonoBehaviour
{
    NetworkManager manager;
    public GameObject ipText;
    public GameObject nameText;
    public Text chatLogText;

    const short MyBeginMsg = 1002;
    const short MySendAllMsg = 1003;

    public static string playerName = "";

    public void UpdateName()
    {
        playerName = nameText.GetComponent<InputField>().text;
    }

    void Awake()
    {
        manager = GetComponent<NetworkManager>();
    }

    public static void UpdateChatLog(NetworkMessage netMsg)
    {
        StringMessage sMessage = netMsg.ReadMessage<StringMessage>();
        FindObjectOfType<Text>().text +=  sMessage.value + "\n";
    }

    public static void SendMsgToAll(NetworkMessage netMsg)
    {
        StringMessage sMessage = netMsg.ReadMessage<StringMessage>();
        NetworkServer.SendToAll(MyBeginMsg, sMessage);
    }

    public void Host()
    {
        manager.StartHost();
        SetupHandlers();
    }

    public void HostServerOnly()
    {
        manager.StartServer();
        SetupHandlers();
    }

    void SetupHandlers()
    {
        NetworkServer.RegisterHandler(MyBeginMsg, UpdateChatLog);
        NetworkServer.RegisterHandler(MySendAllMsg, SendMsgToAll);
    }

    public void Connect(string ip)
    {
        manager.networkAddress = ipText.GetComponent<InputField>().text;
        manager.StartClient();
    }
}