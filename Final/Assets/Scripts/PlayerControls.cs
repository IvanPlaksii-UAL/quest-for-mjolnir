using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public bool canJump = true, Hidden = false, armMovingUp;
    public int jumpTimer, SlotSelected;//1-4
    public  float moveSpeed, gravitySpeed;
    public GameObject Player, arm, armJoint, invSelector;
    string playerState; // FacingLeft, FacingRight
    public string SortState;//Idle, Pickup, Sort, Set, ResetRock
    EntranceManager reftoEntrance;
    GameManager reftoManager;
    public Slot1 inv1;
    public Slot2 inv2;
    public Slot3 inv3;
    public Slot4 inv4;
    public Slot5 inv5;
    public Camera myCamera;
    public string ItemName;//Set, *name of object*
    public string CurrentSlot;
    Color thisColor;

    void Start()
    {
        moveSpeed = 0.15f;
        gravitySpeed = 0.2f;
        SortState = "Idle";
        ItemName = "Set";
        SlotSelected = 1;
        reftoEntrance = FindObjectOfType<EntranceManager>();
        reftoManager = FindObjectOfType<GameManager>();
        if (STAT.CURRENTLVL == "Entrance")
        {
            inv1 = GameObject.Find("Inventory").GetComponent<Slot1>();
            inv2 = GameObject.Find("Inventory").GetComponent<Slot2>();
            inv3 = GameObject.Find("Inventory").GetComponent<Slot3>();
            inv4 = GameObject.Find("Inventory").GetComponent<Slot4>();

            inv1.Current = GameObject.Find("Default");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        //Gravity
        Player.transform.position -= new Vector3(0, gravitySpeed, 0);

        if (Hidden == true)
        {

        }

        Movement();
        Inventory();

        //arm.transform.position = new Vector3(armJoint.transform.position.x, armJoint.transform.position.y, 0);
    }

    private void Movement()
    {
        if (Input.GetKey(KeyCode.D))
        {
            Player.transform.position += new Vector3(moveSpeed, 0, 0);
            //armMovement();
        }
        if (Input.GetKey(KeyCode.A))
        {
            Player.transform.position -= new Vector3(moveSpeed, 0, 0);
            //armMovement();
        }

        //Jump
        if (Input.GetKey(KeyCode.Space) && canJump == true)
        {
            canJump = false;
            jumpTimer = 14;
        }
        if (canJump == false && jumpTimer > 0)
        {
            Player.transform.position += new Vector3(0, 0.45f, 0);
            jumpTimer--;
        }
    }

    private void Inventory()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SlotSelected = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SlotSelected = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SlotSelected = 3;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SlotSelected = 4;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SlotSelected = 5;
        }

        if (STAT.CURRENTLVL == "Entrance")
        {
            if (SlotSelected == 1)
            {
                invSelector.transform.position = new Vector3(myCamera.transform.position.x - 8, myCamera.transform.position.y + 4, 1);
            }
            if (SlotSelected == 2)
            {
                invSelector.transform.position = new Vector3(myCamera.transform.position.x - 7, myCamera.transform.position.y + 4, 1);
            }
            if (SlotSelected == 3)
            {
                invSelector.transform.position = new Vector3(myCamera.transform.position.x - 6, myCamera.transform.position.y + 4, 1);
            }
            if (SlotSelected == 4)
            {
                invSelector.transform.position = new Vector3(myCamera.transform.position.x - 5, myCamera.transform.position.y + 4, 1);
            }
        }
        

        //Manager
        if (SortState == "Idle")
        {

        }
        if (SortState == "Pickup")
        {
            //Checking if slot is free
            if (inv1.CurrentState == "Empty")
            {
                inv1.CurrentState = "Set";
                SortState = "Sort";
            }
            else if (inv2.CurrentState == "Empty")
            {
                inv2.CurrentState = "Set";
                SortState = "Sort";
            }
            else if (inv3.CurrentState == "Empty")
            {
                inv3.CurrentState = "Set";
                SortState = "Sort";
            }
            else if (inv4.CurrentState == "Empty")
            {
                inv4.CurrentState = "Set";
                SortState = "Sort";
            }
            else if (inv5.CurrentState == "Empty")
            {
                inv5.CurrentState = "Set";
                SortState = "Sort";
            }
            else
            {
                print ("No Space");
            }
        }
    }

    private void armMovement()
    {
        if (armMovingUp == true)
        {
            arm.transform.eulerAngles += new Vector3(0, 0, 0.5f);
            if (arm.transform.eulerAngles.z > 75) armMovingUp = false;
        }

        if (armMovingUp == false)
        {
            arm.transform.eulerAngles -= new Vector3(0, 0, 0.5f);
            if (arm.transform.eulerAngles.z < 65) armMovingUp = true;
        }
    }
}
