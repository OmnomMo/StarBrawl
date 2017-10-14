using System.Collections;
using System.Collections.Generic;
using UnityEngine;



    public class gameDebug : MonoBehaviour {

        GameObject spawnedUnit1;
        GameObject spawnedUnit2;

        // Use this for initialization
        void Start() {

            StartCoroutine(DelayedSpawn());

        }

        IEnumerator DelayedSpawn()
        {
            yield return new WaitForSeconds(0.2f);
            spawnedUnit1 = SpawnManager.Instance.BuildUnit();
        SpawnManager.Instance.SpawnUnit(spawnedUnit1, PlayingGrid.Instance.GetPlayingGridPosition(1, 1), TeamManager.Instance.teamL);


            spawnedUnit2 = SpawnManager.Instance.BuildUnit();
        SpawnManager.Instance.SpawnUnit(spawnedUnit2, PlayingGrid.Instance.GetPlayingGridPosition(5, 1), TeamManager.Instance.teamR);
        }

        // Update is called once per frame
        void Update() {

        }
    }
