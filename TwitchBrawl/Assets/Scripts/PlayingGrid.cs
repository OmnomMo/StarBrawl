using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HutongGames.PlayMaker;


public class PlayingGrid : MonoBehaviour
{

    [SerializeField]
    public int nColumns_X;
    [SerializeField]
    public int nRows_Y;

    public GameObject gridPositionPrefab;

    [SerializeField]
    private PlayingGridPosition[,] gridPositions;


    public static PlayingGrid _instance;
    public static PlayingGrid Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<PlayingGrid>();

                if (_instance == null)
                {
                    GameObject container = new GameObject("PlayingGrid");
                    _instance = container.AddComponent<PlayingGrid>();
                }
            }

            return _instance;
        }
    }


    public Vector2 GetPlayingGridDimensions()
    {
        Vector2 dim = new Vector2(nColumns_X, nRows_Y);

        return dim;
    }

    public PlayingGridPosition GetPlayingGridPosition(int x, int y)
    {
        return gridPositions[x, y];
    }

    public void GetPlayingPositionFSM(FsmGameObject outGridPos, int x, int y)
    {
        outGridPos.Value = gridPositions[x, y].gameObject;
    }


    public bool AttemptToOccupyPosition(GameObject occupant, GameObject position)
    {
        if (position.GetComponent<PlayingGridPosition>().GetOccupyingObject() == null)
        {
           // Debug.Log("Occupying Position " + position.GetComponent<PlayingGridPosition>().GetPosX() + "/" + position.GetComponent<PlayingGridPosition>().GetPosY());

            position.GetComponent<PlayingGridPosition>().SetOccupyingObject(occupant);
            return true;
        }
        else
        {
            //Debug.Log("There is already anobject on targeted Position");
            return false;
        }
    }

    public bool FreePosition(GameObject occupant, GameObject position)
    {
        // Debug.Log("Freeing Position " + position.GetComponent<PlayingGridPosition>().GetPosX() + "/" + position.GetComponent<PlayingGridPosition>().GetPosY());

        if (occupant != null && position.GetComponent<PlayingGridPosition>().GetOccupyingObject().gameObject != null)
        {
            if (position.GetComponent<PlayingGridPosition>().GetOccupyingObject().gameObject == occupant)
            {
                position.GetComponent<PlayingGridPosition>().SetOccupyingObject(null);
                return true;
            }
            else
            {
                Debug.Log("object was not occupying vacated position");
                return false;
            }
        } else
        {
            return false;
        }
    }

    public GameObject FindNearestTarget(GameObject unit, int rangeX, int rangeY, Team team)
    {

       // Debug.Log("Try to find nearest Target");

        List<GameObject> possibleTargets = new List<GameObject>();
        PlayingGridPosition unitPos = unit.GetComponent<PlayMakerFSM>().FsmVariables.GetFsmGameObject("GridPos").Value.GetComponent<PlayingGridPosition>();
        //get direction depening on team

        float distance = float.MaxValue;
        GameObject closestTarget = null;


        //positive moving direction?
        if (team.movingDirection == Team.direction.moveright)
        {

            //Debug.Log("Unit position: " + unitPos.GetPosX() + "/" + unitPos.GetPosY());

            for (int x = unitPos.GetPosX(); x <= unitPos.GetPosX() + rangeX; x++)
            {
                for (int y = unitPos.GetPosY() - rangeY; y <= unitPos.GetPosY() + rangeY; y++)
                {

                    //Debug.Log(x + " / " + y);

                    //Is point on grid?
                    if (x >= 0 && x < nColumns_X && y >= 0 && y < nRows_Y)
                    {
                        PlayingGridPosition checkPos = gridPositions[x, y];
                        //is point occupied? Is occupying object shootable? Is occupying object in enemy team?
                        if (checkPos.GetOccupyingObject() != null && checkPos.GetOccupyingObject().CompareTag("shootable") && checkPos.GetOccupyingObject().GetComponent<PlayMakerFSM>().FsmVariables.GetFsmObject("Team").Value != team)
                        {

                            //Debug.Log("Possible target to the right found");
                            possibleTargets.Add(checkPos.GetOccupyingObject());
                        }
                    }
                }
            }
        } else
        {
            for (int x = unitPos.GetPosX(); x >= unitPos.GetPosX() - rangeX; x--)
            {
                for (int y = unitPos.GetPosY() - rangeY; y <= unitPos.GetPosY() + rangeY; y++)
                {
                    //Is point on grid?
                    if (x >= 0 && x < nColumns_X && y >= 0 && y < nRows_Y)
                    {
                        PlayingGridPosition checkPos = gridPositions[x, y];
                        //is point occupied? Is occupying object shootable? Is occupying object in enemy team?
                        if (checkPos.GetOccupyingObject() != null && checkPos.GetOccupyingObject().CompareTag("shootable") && checkPos.GetOccupyingObject().GetComponent<PlayMakerFSM>().FsmVariables.GetFsmObject("Team").Value != team)
                        {
                            //Debug.Log("Possible target to the left found");
                            possibleTargets.Add(checkPos.GetOccupyingObject());
                        }
                    }
                }
            }
        }

        //get all objects in range that are shootable (Units, obstacles);

        //iterate over all possible fields in range, add relevant objects to list
        if (possibleTargets.Count >= 0)
        {


            foreach (GameObject posTarget in possibleTargets)
            {
                int dX = posTarget.GetComponent<PlayMakerFSM>().FsmVariables.GetFsmInt("PosX").Value - unit.GetComponent<PlayMakerFSM>().FsmVariables.GetFsmInt("PosX").Value;
                int dY = posTarget.GetComponent<PlayMakerFSM>().FsmVariables.GetFsmInt("PosY").Value - unit.GetComponent<PlayMakerFSM>().FsmVariables.GetFsmInt("PosY").Value;


                float testDistance = Mathf.Pow(dX, 2) + Mathf.Pow(dY, 2);
                if (testDistance < distance)
                {
                    distance = testDistance;
                    closestTarget = posTarget;
                }


            }
        }

        //Get nearest object, but prioritize x axis
        return closestTarget;
    }


    public PlayingGridPosition AttemptToMoveStep(PlayingGridPosition startPos, bool moveRight)
    {
        if (moveRight)
        {
            if (startPos.GetPosX() + 1 < nColumns_X && gridPositions[startPos.GetPosX() + 1, startPos.GetPosY()].GetOccupyingObject() == null)
            {

                // Debug.Log("Can Take step right!");
                return gridPositions[startPos.GetPosX() + 1, startPos.GetPosY()];
            }
            else
            {
                // Debug.Log("Cant Take step right!");
                return startPos;
            }
        }
        else
        {
            if (startPos.GetPosX() - 1 >= 0 && gridPositions[startPos.GetPosX() - 1, startPos.GetPosY()].GetOccupyingObject() == null)
            {

                // Debug.Log("Can Take step left!");
                return gridPositions[startPos.GetPosX() - 1, startPos.GetPosY()];
            }
            else
            {
                //Debug.Log("Cant Take step right!");
                return startPos;
            }
        }


    }


    public GameObject AttemptToMoveStepRight(GameObject startPos)
    {
        // Debug.Log("Message received. Stepping right");


        if (startPos.GetComponent<PlayingGridPosition>() != null)
        {
            //Debug.Log("Return pos: " + startPos.GetComponent<PlayingGridPosition>().GetPosX());
            return AttemptToMoveStepRight(startPos.GetComponent<PlayingGridPosition>()).gameObject;
        }
        else
        {
            //Debug.Log("Return null");
            return startPos;
        }
    }



    public GameObject AttemptToMoveStepLeft(GameObject startPos)
    {
        // Debug.Log("Message received. Stepping right");


        if (startPos.GetComponent<PlayingGridPosition>() != null)
        {
            //Debug.Log("Return pos: " + startPos.GetComponent<PlayingGridPosition>().GetPosX());
            return AttemptToMoveStepLeft(startPos.GetComponent<PlayingGridPosition>()).gameObject;
        }
        else
        {
            //Debug.Log("Return null");
            return startPos;
        }
    }

    public PlayingGridPosition AttemptToMoveStepRight(PlayingGridPosition startPos)
    {
        return AttemptToMoveStep(startPos, true);
    }

    public PlayingGridPosition AttemptToMoveStepLeft(PlayingGridPosition startPos)
    {
        return AttemptToMoveStep(startPos, false);
    }


    //called once at the beginning. Creates playing grid to dimensions.
    public void CreatePlayingGrid()
    {
        gridPositions = new PlayingGridPosition[nColumns_X, nRows_Y];

        for (int x = 0; x < nColumns_X; x++)
        {
            for (int y = 0; y < nRows_Y; y++)
            {
                GameObject newGridPos_obj = GameObject.Instantiate(gridPositionPrefab);
                PlayingGridPosition newGridPos = newGridPos_obj.GetComponent<PlayingGridPosition>();

                newGridPos.SetPosX(x);
                newGridPos.SetPosY(y);

                newGridPos.transform.parent = transform;
                gridPositions[x, y] = newGridPos;

                newGridPos.TransformToPosition();
            }
        }

        Debug.Log("Created PlayingGrid");
    }

    // Use this for initialization
    void Start()
    {

    }


    void Awake()
    {
        CreatePlayingGrid();
    }

    // Update is called once per frame
    void Update()
    {

    }
}

