using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionMesh : MonoBehaviour
{
    GameManager refToManager;
    PlayerControls refToPlayer;
    float errorMargine, playerZ;
    Color thisColor;
    void Start()
    {
        refToManager = FindObjectOfType<GameManager>();
        refToPlayer = FindObjectOfType<PlayerControls>();
        errorMargine = 0.5f; //the lower the better
        playerZ = refToPlayer.Player.transform.position.z;
        thisColor = this.GetComponent<SpriteRenderer>().color;
        //thisColor.a = 0;
        this.GetComponent<SpriteRenderer>().color = thisColor;
    }

    void Update()
    {
        if ((refToPlayer.Player.GetComponent<SpriteRenderer>().bounds.min.y >= (this.GetComponent<SpriteRenderer>().bounds.max.y - errorMargine)))
        {
            print("botActive"); //debugging
            //Y-Max Mesh
            if (this.GetComponent<SpriteRenderer>().bounds.Intersects(refToPlayer.Player.GetComponent<SpriteRenderer>().bounds))
            {
                print("standing");
                refToPlayer.Player.transform.position = new Vector3(refToPlayer.Player.transform.position.x, GetComponent<SpriteRenderer>().bounds.max.y + 0.86f, playerZ);
                refToPlayer.canJump = true;
            }
        }
        else if ((refToPlayer.Player.GetComponent<SpriteRenderer>().bounds.max.y <= (this.GetComponent<SpriteRenderer>().bounds.min.y + errorMargine)))
        {
            print("topActive"); //debugging
            //Y-Min Mesh
            if (this.GetComponent<SpriteRenderer>().bounds.Intersects(refToPlayer.Player.GetComponent<SpriteRenderer>().bounds))
            {
                print("hithead");
                refToPlayer.jumpTimer = 0;
                refToPlayer.Player.transform.position = new Vector3(refToPlayer.Player.transform.position.x, GetComponent<SpriteRenderer>().bounds.min.y - 0.86f, playerZ);
            }
        }
        if (refToPlayer.Player.GetComponent<SpriteRenderer>().bounds.min.y < (this.GetComponent<SpriteRenderer>().bounds.max.y - 0.6f) || refToPlayer.Player.GetComponent<SpriteRenderer>().bounds.max.y > (this.GetComponent<SpriteRenderer>().bounds.min.y + 0.6f))
        {
            print("mid"); //debugging
            {
                //X-Min Mesh
                if (refToPlayer.Player.GetComponent<SpriteRenderer>().bounds.max.x < (this.GetComponent<SpriteRenderer>().bounds.min.x + errorMargine))
                {
                    if (this.GetComponent<SpriteRenderer>().bounds.Intersects(refToPlayer.Player.GetComponent<SpriteRenderer>().bounds))
                    {
                        print("left"); //debugging
                        refToPlayer.Player.transform.position = new Vector3(GetComponent<SpriteRenderer>().bounds.min.x - 0.3f, refToPlayer.Player.transform.position.y, playerZ);
                    }
                }
                //X-Max Mesh
                if (refToPlayer.Player.GetComponent<SpriteRenderer>().bounds.min.x > (this.GetComponent<SpriteRenderer>().bounds.max.x - errorMargine))
                {
                    if (this.GetComponent<SpriteRenderer>().bounds.Intersects(refToPlayer.Player.GetComponent<SpriteRenderer>().bounds))
                    {
                        print("right"); //debugging
                        refToPlayer.Player.transform.position = new Vector3(GetComponent<SpriteRenderer>().bounds.max.x + 0.3f, refToPlayer.Player.transform.position.y, playerZ);
                    }
                }
            }
        }
    }
}
