using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyCollision : MonoBehaviour
{

    GameManager refToManager;
    PlayerControls refToControls;
    float errorMargine, playerZ;
    Bounds offset;
    void Start()
    {
        refToManager = FindObjectOfType<GameManager>();
        refToControls = FindObjectOfType<PlayerControls>();
        errorMargine = 0.5f; //the lower the better
        playerZ = refToControls.Player.transform.position.z;
        offset = this.GetComponent<SpriteRenderer>().bounds;
        offset.Expand(1.1f);
    }

    void Update()
    {
        if ((refToControls.Player.GetComponent<SpriteRenderer>().bounds.min.y >= (this.GetComponent<SpriteRenderer>().bounds.max.y - errorMargine)))
        {
            print("botActiveM"); //debugging
            //Y-Max Mesh
            if (refToControls.Player.GetComponent<SpriteRenderer>().bounds.Intersects(offset))
            {
                print("moving");
                refToControls.Player.transform.parent = this.transform;
                refToControls.Player.transform.position = new Vector3(refToControls.Player.transform.position.x, GetComponent<SpriteRenderer>().bounds.max.y + 0.86f, playerZ);
                refToControls.canJump = true;
                refToControls.gravitySpeed = 0;
            }
        }
        else// if (!this.GetComponent<SpriteRenderer>().bounds.Intersects(offset))
        {
            refToControls.Player.transform.parent = null;
            refToControls.gravitySpeed = 0.2f;
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
