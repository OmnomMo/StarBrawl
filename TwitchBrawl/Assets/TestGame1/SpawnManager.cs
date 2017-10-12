using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TwitchBrawl
{
    public class SpawnManager : MonoBehaviour
    {

        public GameObject spawnerL;
        public GameObject spawnerR;


        // Use this for initialization
        void Start()
        {

        }

        public GameObject SpawnUnit(bool spawnL, string unitName, float power)
        {
            if (spawnL)
            {
               return spawnerL.GetComponent<SimpleUnitSpawner>().Spawn(true, unitName, power);
            }
            else
            {
               return spawnerR.GetComponent<SimpleUnitSpawner>().Spawn(false, unitName, power);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
