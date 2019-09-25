using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransaction : MonoBehaviour {
    private int direction; //1:down; 2:up; 3:left; 4:right;

    
     void Start()
    {
        ViveInput.touchpadDirection += OnTouchpadDirection;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    private void OnTouchpadDirection(object sender, EventArgs e)
    {
        int direction = ViveInput.touchpadDirectionValue;
        Debug.Log("Start");

        switch (direction)
        {
            ////Down
            //case 1:
                //if (spaceGrid.y < 2)
                //{
                //    spaceGrid.y += 1;
                //    findTile(spaceGrid);
                //}
                //break;
            //Up
            case 2:
                this.StartGame();
                break;
            ////left
            //case 3:
            //    if (spaceGrid.x > 0)
            //    {
            //        spaceGrid.x -= 1;
            //        findTile(spaceGrid);
            //    }
            //    break;
            ////right
            //case 4:
                //if (spaceGrid.x < 2)
                //{
                //    spaceGrid.x += 1;
                //    findTile(spaceGrid);
                //}
                //break;
        }
    }
}
