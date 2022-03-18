using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot1 : MonoBehaviour
{
    PlayerControls reftoControls;
    public string CurrentState;//Empty, Set, *object Name*
    //string SortState;//Idle, Pickup, Sort
    public GameObject Current;

    // Start is called before the first frame update
    void Start()
    {
        CurrentState = "Empty";
        reftoControls = FindObjectOfType<PlayerControls>();
    }

    // Update is called once per frame
    void Update()
    {
        //Resetting
        if (reftoControls.SortState == "ResetRock")
        {
            if (CurrentState == "Rock")
            {
                CurrentState = "Empty";
                reftoControls.SortState = "Idle";
            }
        }
        if (reftoControls.SortState == "ResetBranch")
        {
            if (CurrentState == "Branch")
            {
                CurrentState = "Empty";
                reftoControls.SortState = "Idle";
            }
        }

        if (CurrentState == "Empty")
        {
            Current = GameObject.Find("Default");
        }
        if (CurrentState == "Set")
        {
            print("set");
            Current = GameObject.Find("" + reftoControls.ItemName);
            reftoControls.SortState = "Idle";
            CurrentState = "" + reftoControls.ItemName;
        }

        if (CurrentState != "Set" && CurrentState != "Empty")
        {
            print("In Inventory");
            Current.transform.position = new Vector3(reftoControls.myCamera.transform.position.x - 8, reftoControls.myCamera.transform.position.y + 4, 0);
        }

        if (reftoControls.SlotSelected == 1)
        {
            reftoControls.CurrentSlot = ("Holding" + CurrentState);
        }
    }

}
