using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace starBrawl {

    public class PlayingGrid : MonoBehaviour {

        [SerializeField]
        private int nColumns_X;
        [SerializeField]
        private int nRows_Y;

        public GameObject gridPositionPrefab;

        [SerializeField]
         private PlayingGridPosition[,] gridPositions;

        public Vector2 GetPlayingGridDimensions()
        {
            Vector2 dim = new Vector2(nColumns_X, nRows_Y);

            return dim;
        }

        public PlayingGridPosition GetPlayingGridPosition(int x, int y)
        {
            return gridPositions[x, y];
        }
        
        
        public PlayingGridPosition AttemptToMoveStep (PlayingGridPosition startPos, bool moveRight)
        {
            if (moveRight)
            {
                if (startPos.GetPosX() +1 < nColumns_X     &&    gridPositions[startPos.GetPosX() +1, startPos.GetPosY()].GetOccupyingObject() != null)
                {
                    return gridPositions[startPos.GetPosX() + 1, startPos.GetPosY()];
                } else
                {
                    return startPos;
                }
            } else
            {
                if (startPos.GetPosX() - 1 < nColumns_X && gridPositions[startPos.GetPosX() - 1, startPos.GetPosY()].GetOccupyingObject() != null)
                {
                    return gridPositions[startPos.GetPosX() - 1, startPos.GetPosY()];
                }  else
                {
                    return startPos;
                }
            }


        }

        //called once at the beginning. Creates playing grid to dimensions.
        public void CreatePlayingGrid()
        {
            gridPositions = new PlayingGridPosition[nColumns_X, nRows_Y];

            for (int x = 0; x < nColumns_X; x++)
            {
                for (int y = 0; y < nRows_Y; y++)
                {
                    GameObject newGridPos_obj = GameObject.Instantiate(gridPositionPrefab);
                    PlayingGridPosition newGridPos = newGridPos_obj.GetComponent<PlayingGridPosition>();

                    newGridPos.SetPosX(x);
                    newGridPos.SetPosY(y);

                    newGridPos.transform.parent = transform;
                    gridPositions[x, y] = newGridPos;

                    newGridPos.TransformToPosition();
                }
            }

            Debug.Log("Created PlayingGrid");
        }

	// Use this for initialization
	void Start() {
            CreatePlayingGrid();
        }

        // Update is called once per frame
        void Update() {

        }
    }


}