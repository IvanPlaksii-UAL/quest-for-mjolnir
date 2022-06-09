using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthSpike : MonoBehaviour
{
    float moveSpeed;
    PlayerControls reftoControls;
    BossManager reftoBoss;
    // Start is called before the first frame update
    void Start()
    {
        reftoControls = FindObjectOfType<PlayerControls>();
        reftoBoss = FindObjectOfType<BossManager>();
        moveSpeed = 0.4f;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position -= new Vector3(moveSpeed, 0, 0);

        if (this.GetComponent<SpriteRenderer>().bounds.Intersects(reftoControls.Player.GetComponent<SpriteRenderer>().bounds))
        {
            //deal damage
            reftoBoss.pushBack = 2;
            Destroy(this.gameObject);
        }
        if (this.transform.position.x < -16) Destroy(this.gameObject);
    }
}
