using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pushback : MonoBehaviour
{
    BossManager reftoBoss;
    PlayerControls reftoControls;
    int selfDestruct;
    // Start is called before the first frame update
    void Start()
    {
        reftoBoss = FindObjectOfType<BossManager>();
        reftoControls = FindObjectOfType<PlayerControls>();
        selfDestruct = 150;
    }

    // Update is called once per frame
    void Update()
    {
        if (reftoControls.Player.GetComponent<SpriteRenderer>().bounds.Intersects(this.GetComponent<SpriteRenderer>().bounds))
        {
            reftoBoss.pushBack = 5;
        }

        selfDestruct--;
        if (selfDestruct <= 0) Destroy(this.gameObject);
    }
}
