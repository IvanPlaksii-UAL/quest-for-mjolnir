using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cracked : MonoBehaviour
{
    PlayerControls refToControls;
    private int fallTimer, shakeCD;
    public string pillarState; //Idle, Shaking, Falling
    public int shakeState; //1 - Left, 2 - Right
    Vector3 startingPos;
    // Start is called before the first frame update
    void Start()
    {
        refToControls = FindObjectOfType<PlayerControls>();
        fallTimer = 120;
        pillarState = "Idle";
        startingPos = this.transform.position;
        shakeState = 1;
        //shakeCD = 15;
    }

    // Update is called once per frame
    void Update()
    {
        if (pillarState == "Idle" && this.GetComponent<SpriteRenderer>().bounds.max.y + 0.2f < refToControls.Player.GetComponent<SpriteRenderer>().bounds.min.y && this.GetComponent<SpriteRenderer>().bounds.min.x < refToControls.Player.GetComponent<SpriteRenderer>().bounds.max.x && this.GetComponent<SpriteRenderer>().bounds.max.x > refToControls.Player.GetComponent<SpriteRenderer>().bounds.min.x)
        {
            pillarState = "Shaking";
        }

        if (pillarState == "Shaking")
        {
            if (shakeCD > 0) shakeCD--;

            if (shakeCD <= 0)
            {
                if (shakeState == 1)
                {
                    this.transform.position -= new Vector3(0.3f, 0, 0);
                    shakeState = 2;
                    shakeCD = 5;
                }
                else if (shakeState == 2)
                {
                    this.transform.position += new Vector3(0.3f, 0, 0);
                    shakeState = 1;
                    shakeCD = 5;
                }
            }

            fallTimer--;
        }

        if (fallTimer <= 0)
        {
            this.transform.position -= new Vector3(0, 0.7f, 0);
        }

        if (this.transform.position.y < -25)
        {
            Destroy(this.gameObject);
        }
    }
}
