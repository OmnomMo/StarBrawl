using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team : MonoBehaviour {


    public List<GameObject> AllPlayers;
    public List<GameObject> AllUnits;

    public Material teamMat;

    public enum direction { moveleft, moveright };

    public direction movingDirection;



    // Use this for initialization
    void Start () {

        AllPlayers = new List<GameObject>();
        AllUnits = new List<GameObject>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
