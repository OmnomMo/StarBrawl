using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TwitchChatter;

public class ParseTwitchChat : MonoBehaviour {


    public ChatCommands _chatCommands;
    char commandSeperator;

    void Awake()
    {
        _chatCommands = new ChatCommands();

        string separator = " ";
        commandSeperator = separator[0];
    }

	// Use this for initialization
	void Start () {

        // Add a chat listener.
        TwitchChatClient.singleton.AddChatListener(OnChatMessage);




        // Set your credentials. If you're not planning on sending messages,
        // you can remove these lines.
        //TwitchChatClient.singleton.userName = "MyUserName";
        //TwitchChatClient.singleton.oAuthPassword = "oauth:###################";

        //// Join some channels.
        //TwitchChatClient.singleton.JoinChannel("SomeChannelName");

        // If you set your credentials and you'd like to receive whispers,
        //  call EnableWhispers to allow for sending/receiving whispers.
        TwitchChatClient.singleton.EnableWhispers();

        // Then, add any whisper listeners you'd like.
        TwitchChatClient.singleton.AddWhisperListener(OnWhisper);
    }

    public void ParseChatMessage(string user, string message)
    {

        string[] allCommands = message.Split(commandSeperator);

        _chatCommands.CallCommand(user, allCommands);

        //Twitch_Player parsedUser = null;

        ////TODO move from this class

        //if (TeamManager.Instance.teamL.HasPlayer(user) != null)
        //{
        //    parsedUser = TeamManager.Instance.teamL.HasPlayer(user);
        //}


        //if (TeamManager.Instance.teamR.HasPlayer(user) != null)
        //{
        //    parsedUser = TeamManager.Instance.teamR.HasPlayer(user);
        //}

        //if (parsedUser == null)
        //{
        //    char firstLetter = user[0];

        //    if ((int) firstLetter % 2 == 0)
        //    {
        //        parsedUser = TeamManager.Instance.teamL.AddPlayer(user);
        //    } else
        //    {
        //        parsedUser = TeamManager.Instance.teamR.AddPlayer(user);
        //    }
        //}

        //parsedUser.SpawnNewUnit();
    }

    void Chat()
    {
        // If you set your credentials, send some messages.
        TwitchChatClient.singleton.SendMessage("SomeChannelName", "Kappa Message sent by Twitch Chatter! Kappa");
    }

    // Update is called once per frame
    void Update () {
		
	}


    void Cleanup()
    {
        // When you're done, leave the channels and remove the chat listeners.
        TwitchChatClient.singleton.LeaveChannel("SomeChannelName");
        TwitchChatClient.singleton.RemoveChatListener(OnChatMessage);

        // Also remove any whisper listeners you've added.
        TwitchChatClient.singleton.RemoveWhisperListener(OnWhisper);
    }


    // You'd define your chat message callback like this:
    public void OnChatMessage(ref TwitchChatMessage msg)
    {
        Debug.Log("Received Chatmessage: " + msg.userName + " - " + msg.chatMessagePlainText);
        // Do something with the message here.
        ParseChatMessage(msg.userName, msg.chatMessagePlainText);
    }

    // You'd define your whisper callback like this:
    public void OnWhisper(ref TwitchChatMessage msg)
    {
        // Do something with the whisper here.
    }
}
