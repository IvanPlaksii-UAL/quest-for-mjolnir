using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntranceManager : MonoBehaviour
{
    PlayerControls reftoControls;
    public GameObject Enemy1, Enemy2, Enemy3, Branch, Rock, Bush, Target, Hatch, FallTrigger;
    string CurveDirection; //Set, Left, Right
    public string BranchState; //Set, Falling, Interactable, Perched
    public bool enemyAgro = false, TargetSet = false, curve = false, RockHolding = false;
    float ThrowSpeedX, ThrowSpeedY;
    public int FallTimer, RotateTimer;
    Vector3 SetTarget;
    Bounds offset;
    Color thisColor;

    void Start()
    {
        Application.targetFrameRate = 60;
        reftoControls = FindObjectOfType<PlayerControls>();
        thisColor = Bush.GetComponent<SpriteRenderer>().color;
        ThrowSpeedX = 0.4f;
        ThrowSpeedY = 0.4f;
        CurveDirection = "Set";
        BranchState = "Set";
        offset = Hatch.GetComponent<SpriteRenderer>().bounds;
        offset.Expand(2);
        RotateTimer = 17;
        STAT.CURRENTLVL = "Entrance";
    }

    void Update()
    {
        CameraMovement();
        EnemyBehaviour();
        RockThrowing();
        InventoryManagement();
        BranchInteraction();
        HatchInteraction();
        BushLogic();
    }

    private void CameraMovement()
    {
        if (reftoControls.Player.transform.position.x > -9f && reftoControls.Player.transform.position.x < 14f)//Camera Bounds
        {
            Camera.main.transform.position = new Vector3(reftoControls.Player.transform.position.x, 0, -10);//Following Player
        }
    }

    private void EnemyBehaviour()
    {
        if (reftoControls.Player.transform.position.x > -2)//Movement Start
        {
            enemyAgro = true;
        }

        if (enemyAgro == true)//Walking towards player
        {
            Enemy1.transform.position -= new Vector3(0.07f, 0, 0);
            Enemy2.transform.position -= new Vector3(0.07f, 0, 0);
        }

        if (reftoControls.Player.GetComponent<SpriteRenderer>().bounds.Intersects(Enemy1.GetComponent<SpriteRenderer>().bounds) || reftoControls.Player.GetComponent<SpriteRenderer>().bounds.Intersects(Enemy1.GetComponent<SpriteRenderer>().bounds))
        {
            if (reftoControls.Hidden == false)
            {
                //reset level
            }
        }
    }

    private void BushLogic()
    {
        if (reftoControls.Player.GetComponent<SpriteRenderer>().bounds.Intersects(Bush.GetComponent<SpriteRenderer>().bounds))
        {
            reftoControls.Hidden = true;
            thisColor.a = 0.5f;
            Bush.GetComponent<SpriteRenderer>().color = thisColor;
        }
        else
        {
            reftoControls.Hidden = false;
            thisColor.a = 1;
            Bush.GetComponent<SpriteRenderer>().color = thisColor;
        }
    }

    private void RockThrowing()
    {
        if (Rock.transform.position.y > -3.2 && TargetSet == false) //Gravity
        {
            Rock.transform.position -= new Vector3(0, 0.3f, 0);
        }
        if (Rock.transform.position.y <= -3.2) //Ball Bounds Y
        {
            CurveDirection = "Set";
            curve = false;
            ThrowSpeedX = 0.4f;
        }
        if (Rock.transform.position.x < -17.5f || Rock.transform.position.x > 18)
        {
            curve = false;
        }
        
        if (TargetSet == true) //Target Follow
        {
            if (Rock.transform.position.y < Target.transform.position.y)
            {
                Rock.transform.position += new Vector3(0, ThrowSpeedY, 0);
            }
            if (Rock.transform.position.y > Target.transform.position.y)
            {
                Rock.transform.position -= new Vector3(0, ThrowSpeedY, 0);
            }
            if (Rock.transform.position.x < Target.transform.position.x)
            {
                Rock.transform.position += new Vector3(ThrowSpeedX, 0, 0);
            }
            if (Rock.transform.position.x > Target.transform.position.x)
            {
                Rock.transform.position -= new Vector3(ThrowSpeedX, 0, 0);
            }
        }

        //Aiming rock
        if(reftoControls.CurrentSlot == "HoldingRock")
        {
            if (TargetSet == false && curve == false)
            {
                Target.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10);
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    RockHolding = false;
                    reftoControls.SortState = "ResetRock";
                    SetTarget = new Vector3(Target.transform.position.x, Target.transform.position.y, 0);
                    Rock.transform.position = new Vector3(reftoControls.Player.transform.position.x, reftoControls.Player.transform.position.y, 0);
                    TargetSet = true;
                    print("1");
                }
            }
        }
        else
        {
            Target.transform.position = new Vector3(-19, 2, 0);
        }

        if (TargetSet == true && curve == false)
        {
            Target.transform.position = SetTarget;

            //Followthrough Direction
            if (reftoControls.Player.transform.position.x > Target.transform.position.x)
            {
                CurveDirection = "Left";
            }
            if (reftoControls.Player.transform.position.x < Target.transform.position.x)
            {
                CurveDirection = "Right";
            }
        }
        //target collision
        if (Rock.GetComponent<SpriteRenderer>().bounds.Intersects(Target.GetComponent<SpriteRenderer>().bounds) && TargetSet == true)
        {
            Target.transform.position = new Vector3(-19, 2, 0);
            TargetSet = false;
            curve = true;
        }
        if (curve == true)//Rock Curve
        {
            if (CurveDirection == "Left")
            {
                Rock.transform.position -= new Vector3(ThrowSpeedX, 0, 0);
            }
            else if (CurveDirection == "Right")
            {
                Rock.transform.position += new Vector3(ThrowSpeedX, 0, 0);
            }
            if (ThrowSpeedX > 0)
            {
                ThrowSpeedX -= 0.02f;
            }
        }
    }

    private void InventoryManagement()
    {
        if (Rock.GetComponent<SpriteRenderer>().bounds.Intersects(reftoControls.Player.GetComponent<SpriteRenderer>().bounds) && TargetSet == false)//Pick Up
        {
            if (Input.GetKeyDown(KeyCode.F) && RockHolding == false)
            {
                RockHolding = true;
                reftoControls.ItemName = "" + Rock.transform.name;
                reftoControls.SortState = "Pickup";
            }
        }
        if (BranchState == "Interactable" && Input.GetKeyDown(KeyCode.F) && reftoControls.Player.GetComponent<SpriteRenderer>().bounds.Intersects(Branch.GetComponent<SpriteRenderer>().bounds))
        {
            reftoControls.ItemName = "" + Branch.transform.name;
            reftoControls.SortState = "Pickup";
        }
    }

    private void BranchInteraction()
    {
        if (Rock.GetComponent<SpriteRenderer>().bounds.Intersects(Branch.GetComponent<SpriteRenderer>().bounds) && BranchState == "Set" && RockHolding == false)
        {
            BranchState = "Falling";
        }
        if (BranchState == "Falling")
        {
            Branch.transform.position -= new Vector3(0, 0.17f, 0);
            Branch.transform.Rotate(0, 0, -1);
        }
        if (Branch.transform.position.y < -3 && BranchState == "Falling")
        {
            BranchState = "Interactable";
        }

        if (BranchState == "Perched")
        {
            if (reftoControls.Player.GetComponent<SpriteRenderer>().bounds.Intersects(Branch.GetComponent<SpriteRenderer>().bounds) && FallTimer > 0)
            {
                BranchState = "Launched";
            }
        }
    }

    private void HatchInteraction()
    {
        if (reftoControls.Player.GetComponent<SpriteRenderer>().bounds.Intersects(offset))//Bounds of player and hatch
        {
            print("hit");
            if (reftoControls.CurrentSlot == "HoldingBranch" && Input.GetKeyDown(KeyCode.F))
            {
                BranchState = "Perched";
                reftoControls.SortState = "ResetBranch";
            }
        }

        if (BranchState == "Perched")
        {
            Branch.transform.position = new Vector3(5.6f, -3.2f, 0);
            Branch.transform.eulerAngles = new Vector3(0, 0, 30);
        }

        if (BranchState == "Launched")
        {
            Branch.transform.position += new Vector3(-1f, 1f, 0);
            Branch.transform.Rotate(0, 0, 20);

            if (Hatch.transform.eulerAngles.z > -180 && RotateTimer > 0)
            {
                Hatch.transform.Rotate(0, 0, -10);
                RotateTimer--;
            }
        }

        if (reftoControls.Player.GetComponent<SpriteRenderer>().bounds.Intersects(FallTrigger.GetComponent<SpriteRenderer>().bounds))
        {
            FallTimer = 50;
        }

        if (FallTimer > 0)
        {
            FallTimer--;
        }

        if (BranchState == "Launched" && reftoControls.Player.transform.position.y < -6)
        {
            //start next level
        }
    }
}
