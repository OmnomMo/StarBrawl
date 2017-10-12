using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TwitchBrawl
{
    public class SimpleUnitSpawner : MonoBehaviour
    {

        public GameObject unitPrefab;


        public GameObject Spawn(bool spawnLToR, string unitName, float power)
        {

            GameObject newUnit = GameObject.Instantiate(unitPrefab);

            newUnit.transform.position = transform.position;

            newUnit.GetComponent<SimpleUnit>().OnSpawn(spawnLToR, unitName, power);

            return newUnit;
        }


        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
