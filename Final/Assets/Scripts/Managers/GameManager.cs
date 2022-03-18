using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public string currentScene; //Entrance, Lava jump
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
    }

    private void levelOrder()
    {

    }

    public void camManager()
    {

    }
}
