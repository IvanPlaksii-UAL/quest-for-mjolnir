using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class STAT
{
    //public string currentScene;






}

public class GameManager : MonoBehaviour
{
    public string currentScene;
    public int Health;

    void Start()
    {
        currentScene = "Menu";
    }

    void Update()
    {
        LevelSet();
        levelOrder();
    }

    void LevelSet()
    {
        if (currentScene == "Menu")
        {

        }
        if (currentScene == "Entrance")
        {

        }
        if (currentScene == "LavaJump")
        {

        }
        if (currentScene == "Boss")
        {

        }
        if (currentScene == "HillSlide")
        {

        }
        if (currentScene == "MinePuzzle")
        {

        }
        if (currentScene == "Boss")
        {

        }
    }

    private void levelOrder()
    {

    }

    public void camManager()
    {

    }
}
