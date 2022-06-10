using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class STAT
{
    public static string CURRENTLVL;



}

public class GameManager : MonoBehaviour
{
    public string currentScene;
    public int Health;

    void Start()
    {
        STAT.CURRENTLVL = "Menu";
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
