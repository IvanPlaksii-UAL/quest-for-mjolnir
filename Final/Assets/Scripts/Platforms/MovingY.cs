using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingY : MonoBehaviour
{
    public string moveState; //Up, Down
    PlayerControls refToControls;
    float moveSpeed;
    Vector3 startingPos;
    // Start is called before the first frame update
    void Start()
    {
        refToControls = FindObjectOfType<PlayerControls>();
        moveState = "Up";
        startingPos = this.transform.position;
        moveSpeed = 0.15f;
    }

    // Update is called once per frame
    void Update()
    {
        if (moveState == "Up")
        {
            this.transform.position += new Vector3(0, moveSpeed, 0);

            if (this.transform.position.y > startingPos.y + 3) moveState = "Down";
        }

        if (moveState == "Down")
        {
            this.transform.position -= new Vector3(0, moveSpeed, 0);

            if (this.transform.position.y < startingPos.y - 3) moveState = "Up";
        }
    }
}
