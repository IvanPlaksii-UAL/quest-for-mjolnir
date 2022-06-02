using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingX : MonoBehaviour
{
    public string moveState; //Idle, Left, Right
    PlayerControls refToControls;
    float moveSpeed;
    Vector3 startingPos;
    // Start is called before the first frame update
    void Start()
    {
        refToControls = FindObjectOfType<PlayerControls>();
        moveState = "Idle";
        startingPos = this.transform.position;
        moveSpeed = 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        if (moveState == "Idle" && this.GetComponent<SpriteRenderer>().bounds.max.y + 0.1f > refToControls.Player.GetComponent<SpriteRenderer>().bounds.min.y && this.GetComponent<SpriteRenderer>().bounds.min.x < refToControls.Player.GetComponent<SpriteRenderer>().bounds.max.x && this.GetComponent<SpriteRenderer>().bounds.max.x > refToControls.Player.GetComponent<SpriteRenderer>().bounds.min.x)
        {
            moveState = "Right";
        }

        if (moveState == "Right")
        {
            this.transform.position += new Vector3(moveSpeed, 0, 0);

            if (this.transform.position.x >= startingPos.x + 8) moveState = "Left";
        }

        if (moveState == "Left")
        {
            this.transform.position -= new Vector3(0.2f, 0, 0);

            if (this.transform.position.x <= startingPos.x) moveState = "Right";
        }
    }
}
