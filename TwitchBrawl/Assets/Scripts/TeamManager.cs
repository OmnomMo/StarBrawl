using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamManager : MonoBehaviour {


    public Team teamL;
    public Team teamR;
    public Team teamNeutral;
    public Team noTeamSelected;


    public static TeamManager _instance;
    public static TeamManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<TeamManager>();

                if (_instance == null)
                {
                    GameObject container = new GameObject("TeamManager");
                    _instance = container.AddComponent<TeamManager>();
                }
            }

            return _instance;
        }
    }

    public Twitch_Player ParseJoinCommand(string userName, string[] parameters)
    {
        


        //Check parameters
        if (parameters.Length < 2)
        {
            Debug.Log("No parameters given, please select a team.");
            return null;
        }

        Twitch_Player foundUser = TeamManager.Instance.GetPlayer(userName);

        if (foundUser != null)
        {

            //TODO:
            //Message: User already joined
            Debug.Log("User " + userName + "already exists!");

            return foundUser;
        }


        Twitch_Player newPlayer;

        if (parameters[1] == "blue")
        {
            //joine team blue


            //Todo: Welcome Message
            Debug.Log("User " + userName + " joined team blue!");
            return teamR.AddPlayer(userName);

           
        }
        if (parameters[1] == "red")
        {
            //joine team red
            Debug.Log("User " + userName + " joined team red!");
            //Todo: Welcome Message
            return teamL.AddPlayer(userName);
        }

        //Return error, null

        return null;



    }

    public Twitch_Player GetPlayer(string playerName)
    {
        if (teamL.HasPlayer(playerName) != null)
        {
            return teamL.HasPlayer(playerName);
        }


        if (teamR.HasPlayer(playerName) != null)
        {
            return teamR.HasPlayer(playerName);
        }

        if (noTeamSelected.HasPlayer(playerName) != null)
        {
            return noTeamSelected.HasPlayer(playerName);
        }

        //player doesnt exist
        return null;
    }

    // Use this for initialization
    void Start () {
        //teamL = new Team(Team.direction.moveright);
        //teamR = new Team(Team.direction.moveleft);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

