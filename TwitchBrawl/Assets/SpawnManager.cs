using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HutongGames.PlayMaker;


public class SpawnManager : MonoBehaviour
{

    public GameObject unitPrefab;

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

    public GameObject BuildUnit()
    {
        GameObject newUnit = GameObject.Instantiate(unitPrefab);

        //Todo: Set Items, stats and shit

        return newUnit;
    }

    

    public bool SpawnUnit(GameObject unit, PlayingGridPosition pos, Team team)
    {

        if (pos.GetOccupyingObject() == null)
        {

            pos.SetOccupyingObject(unit);
            unit.transform.position = pos.GetTransform().position;

            

            unit.GetComponent<PlayMakerFSM>().FsmVariables.GetFsmInt("posX").Value = pos.GetPosX();
            unit.GetComponent<PlayMakerFSM>().FsmVariables.GetFsmInt("posY").Value = pos.GetPosY();


            unit.GetComponent<PlayMakerFSM>().FsmVariables.GetFsmGameObject("Unit_gameObject").Value = unit;
            unit.GetComponent<PlayMakerFSM>().FsmVariables.GetFsmObject("PlayingGrid_script").Value = PlayingGrid.Instance;
            unit.GetComponent<PlayMakerFSM>().FsmVariables.GetFsmObject("Team").Value = team;
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

