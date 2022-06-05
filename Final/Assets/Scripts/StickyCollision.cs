using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyCollision : MonoBehaviour
{

    GameManager refToManager;
    PlayerControls refToControls;
    float errorMargine, playerZ;
    public bool touching;
    void Start()
    {
        refToManager = FindObjectOfType<GameManager>();
        refToControls = FindObjectOfType<PlayerControls>();
        errorMargine = 0.5f; //the lower the better
        playerZ = refToControls.Player.transform.position.z;
        touching = false;
    }

    void Update()
    {
        if (touching == true)
        {
            refToControls.Player.transform.parent = this.transform;
            refToControls.gravitySpeed = 0;
            refToControls.Player.transform.position = new Vector3(refToControls.Player.transform.position.x, this.GetComponent<SpriteRenderer>().bounds.max.y + 0.4f, playerZ);
        }
        if (touching == false)
        {
            refToControls.Player.transform.parent = null;
            refToControls.gravitySpeed = 0.2f;
        }

        
        //print("botActive"); //debugging
        //Y-Max Mesh
        if (this.GetComponent<SpriteRenderer>().bounds.Intersects(refToControls.Player.GetComponent<SpriteRenderer>().bounds))
        {
            print("standing");
                
            touching = true;
            refToControls.canJump = true;
        }
        else
        {
            touching = false;
        }

        if ((refToControls.Player.GetComponent<SpriteRenderer>().bounds.max.y <= (this.GetComponent<SpriteRenderer>().bounds.min.y + errorMargine)))
        {
            print("topActive"); //debugging
            //Y-Min Mesh
            if (this.GetComponent<SpriteRenderer>().bounds.Intersects(refToControls.Player.GetComponent<SpriteRenderer>().bounds))
            {
                print("hithead");
                refToControls.jumpTimer = 0;
                refToControls.Player.transform.position = new Vector3(refToControls.Player.transform.position.x, GetComponent<SpriteRenderer>().bounds.min.y - 0.86f, playerZ);
            }
        }
        if (refToControls.Player.GetComponent<SpriteRenderer>().bounds.min.y < (this.GetComponent<SpriteRenderer>().bounds.max.y - 0.6f) || refToControls.Player.GetComponent<SpriteRenderer>().bounds.max.y > (this.GetComponent<SpriteRenderer>().bounds.min.y + 0.6f))
        {
            print("mid"); //debugging
            {
                //X-Min Mesh
                if (refToControls.Player.GetComponent<SpriteRenderer>().bounds.max.x < (this.GetComponent<SpriteRenderer>().bounds.min.x + errorMargine))
                {
                    if (this.GetComponent<SpriteRenderer>().bounds.Intersects(refToControls.Player.GetComponent<SpriteRenderer>().bounds))
                    {
                        print("left"); //debugging
                        refToControls.Player.transform.position = new Vector3(GetComponent<SpriteRenderer>().bounds.min.x - 0.3f, refToControls.Player.transform.position.y, playerZ);
                    }
                }
                //X-Max Mesh
                if (refToControls.Player.GetComponent<SpriteRenderer>().bounds.min.x > (this.GetComponent<SpriteRenderer>().bounds.max.x - errorMargine))
                {
                    if (this.GetComponent<SpriteRenderer>().bounds.Intersects(refToControls.Player.GetComponent<SpriteRenderer>().bounds))
                    {
                        print("right"); //debugging
                        refToControls.Player.transform.position = new Vector3(GetComponent<SpriteRenderer>().bounds.max.x + 0.3f, refToControls.Player.transform.position.y, playerZ);
                    }
                }
            }
        }
    }
}
