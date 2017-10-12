using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TwitchBrawl
{
    public class Hitpoints : MonoBehaviour
    {

        public int maxHitpoints;
        int hitPoints;

        // Use this for initialization
        void Start()
        {
            hitPoints = maxHitpoints;
        }

        public void DealDamage(int damage)
        {
            hitPoints -= damage;
            //Debug.Log(gameObject.ToString() + "got hit! Now at " + hitPoints + "life points.");

            if (hitPoints < 0)
            {
                hitPoints = 0;

                Death();
            }
        }

        public void Death()
        {
            Debug.Log(gameObject.ToString() + "died!");
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
