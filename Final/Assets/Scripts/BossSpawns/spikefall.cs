using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spikefall : MonoBehaviour
{
    PlayerControls reftoControls;
    float fallSpeed;
    // Start is called before the first frame update
    void Start()
    {
        reftoControls = FindObjectOfType<PlayerControls>();
        fallSpeed = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position -= new Vector3(0, fallSpeed, 0);

        if (this.GetComponent<SpriteRenderer>().bounds.Intersects(reftoControls.Player.GetComponent<SpriteRenderer>().bounds))
        {
            //Deal Damage
            Destroy(this.gameObject);
        }
        if (this.transform.position.y < -3) Destroy(this.gameObject);
    }
}
