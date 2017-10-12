using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TwitchBrawl
{
    public class GameVariables : MonoBehaviour
    {

        private static GameVariables _instance;
        public static GameVariables Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = GameObject.FindObjectOfType<GameVariables>();

                    if (_instance == null)
                    {
                        GameObject container = new GameObject("GameVariables");
                        _instance = container.AddComponent<GameVariables>();
                    }
                }

                return _instance;
            }
        }

        public float brawlPointsPerSecond;
        public float unitSpeed;
        

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
