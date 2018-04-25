using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Helper class that contains all available chat commands
public class ChatCommands {

    List<ChatCommand> allCommands;

    public ChatCommands()
    {
        allCommands = new List<ChatCommand>();
        allCommands.Add(new SpawnCommand());
        allCommands.Add(new DebugCommand());
        allCommands.Add(new JoinCommand());
    }

    public bool CallCommand(string userName, string[] parameters)
    {
        foreach (ChatCommand cc in allCommands)
        {
            if (cc.GetCommandText() == parameters[0])
            {
                cc.CallCommand(userName, parameters);
            }
        }
        return false;
    }
}


public abstract class ChatCommand
{
    string commandText;
    public abstract string GetCommandText();
    public abstract bool CallCommand(string userName,  string[] parameters);
}


public class SpawnCommand : ChatCommand
{
    string commandText = "!spawn";

    public override string GetCommandText()
    {
        return commandText;
    }

    public override bool CallCommand(string userName,  string[] parameters)
    {
        //Do something, parse paramters


        SpawnManager.Instance.ParseSpawnCommand(userName, parameters);


        

        return true;
    }
}


public class JoinCommand : ChatCommand
{
    string commandText = "!join";

    public override string GetCommandText()
    {
        return commandText;
    }

    public override bool CallCommand(string userName, string[] parameters)
    {
        //Do something, parse paramters

    

        TeamManager.Instance.ParseJoinCommand(userName, parameters);




        return true;
    }
}

public class DebugCommand : ChatCommand
{
    string commandText = "!debug";

    public override bool CallCommand(string userName, string[] parameters)
    {
        string parameterString = "";

        foreach (string p in parameters)
        {
            parameterString += p;
            parameterString += " - ";
        }


        Debug.Log("Got Debug commands by user " + userName + "  with paramters: " + parameterString);

        return true;
    }

    public override string GetCommandText()
    {
        return commandText;
    }
}
