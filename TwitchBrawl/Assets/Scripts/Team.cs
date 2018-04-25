using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team : MonoBehaviour {


    public List<Twitch_Player> AllPlayers;
    public List<GameObject> AllUnits;

    public GameObject playerPrefab;

    public int points;

    public Material teamMat;

    public enum direction { moveleft, moveright, dontMove };

    public direction movingDirection;


    public PlayingGridPosition  GetRandomEligibleSpawnPoint()
    {


        List<PlayingGridPosition> eligiblePositions = new List<PlayingGridPosition>();

        for (int u = 0; u < PlayingGrid.Instance.nRows_Y; u++)
        {
            if (movingDirection == direction.moveright)
            {
                if (PlayingGrid.Instance.GetPlayingGridPosition(0,u).GetOccupyingObject() == null)
                {
                    eligiblePositions.Add(PlayingGrid.Instance.GetPlayingGridPosition(0, u));
                }
            } else
            {
                if (PlayingGrid.Instance.GetPlayingGridPosition(PlayingGrid.Instance.nColumns_X -1, u).GetOccupyingObject() == null)
                {
                    eligiblePositions.Add(PlayingGrid.Instance.GetPlayingGridPosition(PlayingGrid.Instance.nColumns_X - 1, u));
                }
            }
        }

        //get all free spawnPoints

        int nRand = Random.Range(0, eligiblePositions.Count);

        return eligiblePositions[nRand];

        //return random spawnpoints
    }


    public void AddPoints(int nPoints)
    {
        points += nPoints;
    }

    public Twitch_Player HasPlayer(string name)
    {

        Twitch_Player foundPlayer = null;
        foreach (Twitch_Player tp in AllPlayers)
        {
            if (name == tp.name)
            {
                foundPlayer = tp;
            }
        }

        return foundPlayer;

    }

    public Twitch_Player AddPlayer(string name)
    {
        GameObject newPlayer = GameObject.Instantiate(playerPrefab);
        newPlayer.transform.parent = TeamManager.Instance.gameObject.transform;

        newPlayer.GetComponent<Twitch_Player>().name = name;
        newPlayer.GetComponent<Twitch_Player>().team = this;

        AllPlayers.Add(newPlayer.GetComponent<Twitch_Player>());

        return newPlayer.GetComponent<Twitch_Player>();

    }

    // Use this for initialization
    void Start () {

        AllPlayers = new List<Twitch_Player>();
        AllUnits = new List<GameObject>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    
}
