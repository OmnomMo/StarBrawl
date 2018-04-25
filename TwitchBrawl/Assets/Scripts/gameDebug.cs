using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class gameDebug : MonoBehaviour
{

    GameObject spawnedUnit1;
    GameObject spawnedUnit2;
    GameObject spawnedObstacle1;
    GameObject spawnedObstacle2;

    public float obstacleSpawnChance;

    public Twitch_Player testPlayer1;
    public Twitch_Player testPlayer2;
    public Twitch_Player testPlayerNeutral;

    // Use this for initialization
    void Start()
    {

        StartCoroutine(DelayedSpawn());

    }

    IEnumerator DelayedSpawn()
    {
        yield return new WaitForSeconds(0.2f);

        //scatter Obstacles
        for (int x = 2; x < PlayingGrid.Instance.nColumns_X -2; x++)
        {
            for (int y = 0; y < PlayingGrid.Instance.nRows_Y; y++)
            {

                

                float nRand = Random.Range(0f, 1f);

                if (nRand <= obstacleSpawnChance)
                {
                    GameObject spawnedObstacle = SpawnManager.Instance.BuildObstacle();
                    SpawnManager.Instance.SpawnUnit(spawnedObstacle, PlayingGrid.Instance.GetPlayingGridPosition(x, y), testPlayerNeutral.team, testPlayerNeutral);
                }
            }
        }

        ////spawnedUnit1 = SpawnManager.Instance.BuildUnit();
        ////SpawnManager.Instance.SpawnUnit(spawnedUnit1, PlayingGrid.Instance.GetPlayingGridPosition(0, 1), testPlayer1.team, testPlayer1);

        //spawnedObstacle1 = SpawnManager.Instance.BuildObstacle();
        //SpawnManager.Instance.SpawnUnit(spawnedObstacle1, PlayingGrid.Instance.GetPlayingGridPosition(5, 1),testPlayerNeutral.team, testPlayerNeutral);

        ////spawnedUnit2 = SpawnManager.Instance.BuildUnit();
        ////SpawnManager.Instance.SpawnUnit(spawnedUnit2, PlayingGrid.Instance.GetPlayingGridPosition(6, 3), testPlayer2.team, testPlayer2);

        //spawnedObstacle2 = SpawnManager.Instance.BuildObstacle();
        //SpawnManager.Instance.SpawnUnit(spawnedObstacle2, PlayingGrid.Instance.GetPlayingGridPosition(1, 3), testPlayerNeutral.team, testPlayerNeutral);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
