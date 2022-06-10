using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaFinish : MonoBehaviour
{
    PlayerControls reftoControls;
    // Start is called before the first frame update
    void Start()
    {
        reftoControls = FindObjectOfType<PlayerControls>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (reftoControls.Player.GetComponent<SpriteRenderer>().bounds.Intersects(this.GetComponent<SpriteRenderer>().bounds))
        {
            STAT.CURRENTLVL = "Boss";
        }
    }
}
