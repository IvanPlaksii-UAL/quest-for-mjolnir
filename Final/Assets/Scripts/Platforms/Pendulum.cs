using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendulum : MonoBehaviour
{
    public GameObject Platform, Joint;
    public string swingState; //Left, Right
    public int swingCD, target;
    public float speed, currentAngle;

    // Start is called before the first frame update
    void Start()
    {
        swingState = "Left";
        target = 30;
        currentAngle = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Platform.transform.position = Joint.transform.position;

        speed = Mathf.Abs((target / currentAngle) / 2);
        if (swingCD == 0)
        {
            if (currentAngle >= target)
            {
                swingState = "Left";
                //swingCD = 40;
            }

            if (currentAngle <= -target)
            {
                swingState = "Right";
                //swingCD = 40;
            }

            if (swingState == "Left")
            {
                this.transform.Rotate(0, 0, -speed);
                currentAngle--;
            }
            if (swingState == "Right")
            {
                this.transform.Rotate(0, 0, speed);
                currentAngle++;
            }
        }
        else swingCD--;
    }
}
