using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    PlayerControls reftoControls;
    BossManager reftoBoss;
    private Vector3 targetPos;
    float moveSpeed;
    bool active;
    int selfDestruct;
    // Start is called before the first frame update
    void Start()
    {
        reftoBoss = FindObjectOfType<BossManager>();
        reftoControls = FindObjectOfType<PlayerControls>();
        targetPos = new Vector3(reftoControls.Player.transform.position.x, reftoControls.Player.transform.position.y, 0);
        moveSpeed = 0.45f;
        active = false;
        selfDestruct = 100;
    }

    // Update is called once per frame
    void Update()
    {
        //Activates on contact with weapon
        if (this.GetComponent<SpriteRenderer>().bounds.Intersects(reftoBoss.weapon.GetComponent<SpriteRenderer>().bounds)) active = true;

        if (active == true)
        {
            //Movement to player
            if (this.transform.position.x < targetPos.x)
            {
                this.transform.position += new Vector3(moveSpeed, 0, 0);
            }
            if (this.transform.position.x > targetPos.x)
            {
                this.transform.position -= new Vector3(moveSpeed, 0, 0);
            }
            if (this.transform.position.y < targetPos.y)
            {
                this.transform.position += new Vector3(0, moveSpeed/3, 0);
            }
            if (this.transform.position.y > targetPos.y)
            {
                this.transform.position -= new Vector3(0, moveSpeed/3, 0);
            }

            selfDestruct--;
        }

        if (this.GetComponent<SpriteRenderer>().bounds.Intersects(reftoControls.Player.GetComponent<SpriteRenderer>().bounds))
        {
            //Deal damage
            Destroy(this.gameObject);
        }
        else if (selfDestruct <= 0) Destroy(this.gameObject);
    }
}
