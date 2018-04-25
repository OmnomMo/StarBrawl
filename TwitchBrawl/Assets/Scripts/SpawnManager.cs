using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HutongGames.PlayMaker;


public class SpawnManager : MonoBehaviour
{

    public GameObject unitPrefab;
    public GameObject obstaclePrefab;

    public static SpawnManager _instance;
    public static SpawnManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<SpawnManager>();

                if (_instance == null)
                {
                    GameObject container = new GameObject("SpawnManager");
                    _instance = container.AddComponent<SpawnManager>();
                }
            }

            return _instance;
        }
    }

    // Use this for initialization
    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool ParseSpawnCommand(string playerName, string[] commands)
    {
        Twitch_Player parsedUser = null;

        Twitch_Player foundUser = TeamManager.Instance.GetPlayer(playerName);

        if (foundUser != null) {
            parsedUser = foundUser;
        } else {

            Debug.Log("User doesnt exist, cant spawn unit");
            return false;

        }

        if (parsedUser.team == TeamManager.Instance.noTeamSelected)
        {
            Debug.Log("User hasnt joined team yet, cant spawn unit");
            return false;
        }


         GameObject spawnedUnit = SpawnManager.Instance.BuildUnit();
         PlayingGridPosition spawnPoint = parsedUser.team.GetRandomEligibleSpawnPoint();

        if (spawnPoint != null)
        {
            SpawnUnit(spawnedUnit, spawnPoint, parsedUser.team, parsedUser);
        }

        parsedUser.activeUnit = spawnedUnit;

        return true;
    }

    public GameObject BuildUnit()
    {
        GameObject newUnit = GameObject.Instantiate(unitPrefab);

        //Todo: Set Items, stats and shit

        return newUnit;
    }

    public GameObject BuildObstacle()
    {
        return GameObject.Instantiate(obstaclePrefab);
    }

    
    

    public bool SpawnUnit(GameObject unit, PlayingGridPosition pos, Team team, Twitch_Player player)
    {

        if (pos.GetOccupyingObject() == null)
        {

            pos.SetOccupyingObject(unit);
            unit.transform.position = pos.GetTransform().position;

            

            unit.GetComponent<PlayMakerFSM>().FsmVariables.GetFsmInt("posX").Value = pos.GetPosX();
            unit.GetComponent<PlayMakerFSM>().FsmVariables.GetFsmInt("posY").Value = pos.GetPosY();



            if (unit.GetComponentInChildren<UnityEngine.UI.Text>(true) != null)
            {
                unit.GetComponentInChildren<UnityEngine.UI.Text>(true).text = player.name;
            }

            unit.GetComponent<MeshRenderer>().material = team.teamMat;


            if (team.movingDirection == Team.direction.moveright)
            {
                unit.GetComponent<PlayMakerFSM>().FsmVariables.GetFsmInt("posXEnd").Value = PlayingGrid.Instance.nColumns_X - 1;
            } else
            {
                unit.GetComponent<PlayMakerFSM>().FsmVariables.GetFsmInt("posXEnd").Value = 0;
            }

            unit.GetComponent<PlayMakerFSM>().FsmVariables.GetFsmGameObject("Unit_gameObject").Value = unit;
            unit.GetComponent<PlayMakerFSM>().FsmVariables.GetFsmObject("PlayingGrid_script").Value = PlayingGrid.Instance;
            unit.GetComponent<PlayMakerFSM>().FsmVariables.GetFsmObject("Team").Value = team;
            unit.GetComponent<PlayMakerFSM>().FsmVariables.GetFsmObject("player_obj").Value = player;
            unit.GetComponent<PlayMakerFSM>().FsmVariables.GetFsmEnum("movingDirection").Value = team.movingDirection;
            unit.GetComponent<PlayMakerFSM>().FsmVariables.GetFsmGameObject("GridPos").Value = pos.gameObject;
            unit.GetComponent<PlayMakerFSM>().FsmVariables.GetFsmGameObject("GridPos_Target").Value = pos.gameObject; 
            //unit.GetComponent<PlayMakerFSM>().FsmVariables.GetFsmObject("PlayingGrid").Value = PlayingGrid.Instance.gameObject;

            //TODO: Unit Initialization stuff;


            return true;
        } else
        {
            return false;
        }
    }
}

