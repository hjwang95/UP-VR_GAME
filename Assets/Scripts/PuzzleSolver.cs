using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PuzzleSolver : MonoBehaviour
{
    private Vector2 spaceGrid;
    
    private GameObject[] tileArray;

    //input from ViveInput
    //public ViveInput input;
    private int direction; //1:down; 2:up; 3:left; 4:right;
    

    private void Awake()
    {
        spaceGrid = new Vector2(0.0f, 2.0f);
    }

    private void Start()
    {
        ViveInput.touchpadDirection += OnTouchpadDirection;

        tileArray = GameObject.FindGameObjectsWithTag("SlidingTile");
    }

    private void OnTouchpadDirection(object sender, EventArgs e)
    {
        int direction = ViveInput.touchpadDirectionValue;

        switch (direction){
            //Down
            case 1:
                if (spaceGrid.y < 2)
                {
                    spaceGrid.y += 1;
                    findTile(spaceGrid);
                }
                break;
            //Up
            case 2:
                if (spaceGrid.y > 0)
                {
                    spaceGrid.y -= 1;
                    findTile(spaceGrid);
                }
                break;
            //left
            case 3:
                if (spaceGrid.x > 0)
                {
                    spaceGrid.x -= 1;
                    findTile(spaceGrid);
                }
                break;
            //right
            case 4:
                if (spaceGrid.x < 2)
                {
                    spaceGrid.x += 1;
                    findTile(spaceGrid);
                }
                break;
        }
    }

    private void findTile(Vector2 pos)
    {

        foreach (GameObject tile in tileArray)
        {
            if(tile.GetComponent<PuzzleTile>().GridLocation == pos)
            {
                tile.GetComponent<PuzzleTile>().Move();
            }
        }
    }
}
