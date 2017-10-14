using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamManager : MonoBehaviour {


    public Team teamL;
    public Team teamR;


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

    // Use this for initialization
    void Start () {
        //teamL = new Team(Team.direction.moveright);
        //teamR = new Team(Team.direction.moveleft);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

