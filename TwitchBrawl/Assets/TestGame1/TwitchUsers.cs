using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TwitchBrawl {

    public class TwitchUsers : MonoBehaviour {

        public LinkedList<User> allTwitchUsers;

        // Use this for initialization
        void Start() {
            allTwitchUsers = new LinkedList<User>();
        }

        public void AddUser(string userName)
        {
            allTwitchUsers.AddLast(new User(userName));
        }

        public User getUser(string userName)
        {

            User foundUser = null;

            if (allTwitchUsers.Count == 0)
            {
                return null;
            }

            foreach (User us in allTwitchUsers)
            {
                if (us.userName == userName)
                {
                    foundUser = us;
                    break;
                }
            }

            return foundUser;

        }

        // Update is called once per frame
        void Update() {

        }
    }

    public class User {


        public string userName { get; set; }
        public float brawlPoints { get; set; }
        public float timePointsUpdated {get; set; }
        public float startTime { get; set; }

        public User(string userName_)
        {
            userName = userName_;
            startTime = Time.time;
            timePointsUpdated = Time.time;
            brawlPoints = 1;
        }

        public float UpdateBrawlPoints()
        {
            //Debug.Log(GameVariables.Instance.brawlPointsPerSecond);
            //Debug.Log("brawl points added: " + ((Time.time - timePointsUpdated) * GameVariables.Instance.brawlPointsPerSecond));
            brawlPoints += ((Time.time - timePointsUpdated) * GameVariables.Instance.brawlPointsPerSecond);
            timePointsUpdated = Time.time;

            Debug.Log("Update brawlPoints for " + userName + ": " + brawlPoints);
            return brawlPoints;
        }

        public float PayPoints(float paidPoints)
        {
            float p;
            UpdateBrawlPoints();
            if (paidPoints > brawlPoints)
            {
                p = brawlPoints;
                brawlPoints = 0;
                return p;
            } else
            {
                brawlPoints -= paidPoints;
                return brawlPoints;
            }
        }

        public float PayAllPoints()
        {
            UpdateBrawlPoints();
            float p;
            p = brawlPoints;
            brawlPoints = 0;
            return p;
        }





    }

        }
