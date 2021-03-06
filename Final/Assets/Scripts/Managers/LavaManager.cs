using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaManager : MonoBehaviour
{
    PlayerControls reftoControls;
    public GameObject Pendulum, Cracked, MovingX, MovingY, Lava, Finish;
    public int platformType, platformSpawns;
    private float pendulumDistance, crackedDistance, xDistance, yDistance;
    Vector3 spawnPos, currentPos;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        reftoControls = FindObjectOfType<PlayerControls>();
        platformSpawns = 10;
        pendulumDistance = 10;
        crackedDistance = 6;
        currentPos.x = -1;
        xDistance = 13;
        yDistance = 6;
        spawnPos = reftoControls.Player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Camera.main.transform.position = new Vector3(reftoControls.Player.transform.position.x, 0, -10);//Following Player
        if (reftoControls.Player.GetComponent<SpriteRenderer>().bounds.Intersects(Lava.GetComponent<SpriteRenderer>().bounds)) reftoControls.Player.transform.position = spawnPos;

        LevelBuilder();
    }

    private void LevelBuilder()
    {
        if (platformSpawns > 0)
        {
            platformType = Random.Range(1, 5);

            //Pendulum Spawn
            if (platformType == 1)
            {
                Instantiate(Pendulum, new Vector3(currentPos.x + 2, 2, 0), Quaternion.identity);

                currentPos.x = currentPos.x + pendulumDistance;
                platformSpawns--;
            }
            //Cracked Platform Spawn
            if (platformType == 2)
            {
                Instantiate(Cracked, new Vector3(currentPos.x, -2, 0), Quaternion.identity);

                currentPos.x = currentPos.x + crackedDistance;
                platformSpawns--;
            }
            //X-Moving Platform Spawn
            if (platformType == 3)
            {
                Instantiate(MovingX, new Vector3(currentPos.x, 0, 0), Quaternion.identity);

                currentPos.x = currentPos.x + xDistance;
                platformSpawns--;
            }
            //Y-Moving Platform Spawn
            if (platformType == 4)
            {
                Instantiate(MovingY, new Vector3(currentPos.x, 0, 0), Quaternion.identity);

                currentPos.x = currentPos.x + yDistance;
                platformSpawns--;
            }
        }
        else if (platformSpawns == 0) // Creates finish platform
        {
            Instantiate(Finish, new Vector3(currentPos.x, 0, 0), Quaternion.identity);
        }
    }
}
