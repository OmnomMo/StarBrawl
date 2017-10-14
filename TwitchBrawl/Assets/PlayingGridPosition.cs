using System.Collections;
using System.Collections.Generic;
using UnityEngine;



    public class PlayingGridPosition : MonoBehaviour
    {
        [SerializeField]
        private int posX;
        [SerializeField]
        private int posY;
        [SerializeField]
        private GameObject occupyingObject;

        public GameObject GetOccupyingObject()
        {
            return occupyingObject;
        }

        public void SetOccupyingObject(GameObject obj)
        {
            occupyingObject = obj;
        }


        public int GetPosX()
        {
            return posX;
        }
        
        public int GetPosY()
        {
            return posY;
        }

        public Transform GetTransform()
        {
            return transform;
        }

        public void SetPosX(int x)
        {
            posX = x;
        }

        public void SetPosY(int y)
        {
            posY = y; 
        }


        public void TransformToPosition()
        {
            transform.position = new Vector3(posX, 0, posY);
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

