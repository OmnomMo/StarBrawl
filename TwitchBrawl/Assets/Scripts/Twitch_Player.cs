using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Twitch_Player : MonoBehaviour {

    public string name;

    public Team team;

    public GameObject activeUnit;
    

    //public GameObject SpawnNewUnit()
    //{
    //    GameObject spawnedUnit = SpawnManager.Instance.BuildUnit();
    //    PlayingGridPosition spawnPoint = team.GetRandomEligibleSpawnPoint();

    //        if (spawnPoint != null) {
    //        SpawnManager.Instance.SpawnUnit(spawnedUnit, spawnPoint, team, this);
    //    }

    //    activeUnit = spawnedUnit;


    //    return spawnedUnit;
    //}

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
