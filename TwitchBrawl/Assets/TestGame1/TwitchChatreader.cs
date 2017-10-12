using System.Collections.Generic;

using UnityEngine;

using TwitchChatter;

namespace TwitchBrawl
{

    [RequireComponent(typeof(TwitchChatClient))]
    public class TwitchChatreader : MonoBehaviour
    {
        public bool printChatMessages;
        public bool printServerMessages;
        public bool printWhispers;

        private void Start()
        {
            if (TwitchChatClient.singleton != null)
            {
                TwitchChatClient.singleton.AddChatListener(OnChatMessage);
                TwitchChatClient.singleton.AddServerListener(OnServerMessage);
                TwitchChatClient.singleton.AddWhisperListener(OnWhisper);
            }
        }

        private void OnDestroy()
        {
            if (TwitchChatClient.singleton != null)
            {
                TwitchChatClient.singleton.RemoveChatListener(OnChatMessage);
                TwitchChatClient.singleton.RemoveServerListener(OnServerMessage);
                TwitchChatClient.singleton.RemoveWhisperListener(OnWhisper);
            }
        }

        private void OnChatMessage(ref TwitchChatMessage msg)
        {

            char testChar = msg.userName[0];
            int testInt = (int)testChar;





            if (GetComponent<TwitchUsers>().getUser(msg.userName) == null)
            {
                GetComponent<TwitchUsers>().AddUser(msg.userName);
            }


            User actingUser = GetComponent<TwitchUsers>().getUser(msg.userName);


     

            float availablePower = actingUser.PayAllPoints();

            if (availablePower != 1)
            {
                Debug.Log(actingUser.userName + " Returns! - " + availablePower);
            }

         
            GetComponent<SpawnManager>().SpawnUnit(testInt % 2 != 0, msg.userName, availablePower);





            if (printChatMessages)
            {
                Debug.Log("TwitchChatter: " + msg.userName + "(" + msg.channelName + "): " + msg.chatMessagePlainText);
            }
        }

        private void OnServerMessage(ref TwitchServerMessage msg)
        {
            if (printServerMessages)
            {
                Debug.Log("TwitchChatter: " + msg.rawText);
            }
        }

        private void OnWhisper(ref TwitchChatMessage msg)
        {
            if (printWhispers)
            {
                Debug.Log("TwitchChatter: " + msg.userName + "(whisper): " + msg.chatMessagePlainText);
            }
        }
    }
}