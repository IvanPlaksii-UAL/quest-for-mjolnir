using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    PlayerControls reftoControls;
    public GameObject myCamera, bossHitbox, jointBoss, jointArm, bossArm, weapon, mjolnir, rock, pushField, iceSpikes, earthSpike, bossHP, backdropHP;
    public string levelState, //Start, Fight
                  bossState, //Idle, Active, Dead
                  attackMode, //Melee, Range, Special
                  currentAttack;//
    public int fightCD, bossHealth, attackRan, attackFrame, pushBack;
    private bool timeSet, weaponDamage, itemSpawned, playerIntersect;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        reftoControls = FindObjectOfType<PlayerControls>();
        levelState = "Start";
        currentAttack = "Idle";
        fightCD = 240;
        bossHealth = 1000;
        timeSet = false;
        itemSpawned = false;
        playerIntersect = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (levelState == "Start")
        {
            myCamera.transform.position = new Vector3(reftoControls.Player.transform.position.x, 0, -10);
            Camera.main.orthographicSize = 5;

            if (reftoControls.Player.GetComponent<SpriteRenderer>().bounds.Intersects(mjolnir.GetComponent<SpriteRenderer>().bounds)) levelState = "Fight";
        }

        if (levelState == "Fight")
        {
            if (Camera.main.orthographicSize < 9) Camera.main.orthographicSize += 0.1f;
            if (Camera.main.transform.position.x > 0) Camera.main.transform.position -= new Vector3(0.25f, 0, 0);

            else bossState = "Active";

            if (bossState == "Active")
            {
                BossJoints();
                BossMechanics();
                PlayerEffects();
                UserInterface();
            }
        }

        if (bossHealth <= 0)
        {
            bossState = "Dead";
            levelState = "Complete";
        }
    }

    private void BossMechanics()
    {
        //Type of attacks
        if (Vector3.Distance(reftoControls.Player.transform.position, bossHitbox.transform.position) >= 10)
        {
            attackMode = "Range";
        }
        else if (Vector3.Distance(reftoControls.Player.transform.position, bossHitbox.transform.position) > 7 && Vector3.Distance(reftoControls.Player.transform.position, bossHitbox.transform.position) < 12) attackMode = "Mid";
        else attackMode = "Melee";

        if (fightCD > 0) fightCD--;
        if (attackFrame > 0) attackFrame--;
        if (fightCD > 0 && attackFrame == 0) currentAttack = "Idle";
        if (currentAttack == "Idle")ArmPos(270, 0, 1);

        //Random Attacks
        if (currentAttack == "Idle" && fightCD == 0)
        {
            if (attackMode == "Range")
            {
                attackRan = Random.Range(1, 4);
                //Rock Throw
                if (attackRan == 1) currentAttack = "RockThrow";
                //Sonic Yell
                if (attackRan == 2) currentAttack = "SonicYell";
                //Ground Pound
                if (attackRan == 3) currentAttack = "GroundPound";
            }
            else if (attackMode == "Melee")
            {
                attackRan = Random.Range(1, 4);
                //Basic Hit
                if (attackRan == 1) currentAttack = "BasicHit";
                //Sonic Yell
                if (attackRan == 2) currentAttack = "SonicYell";
                //Ground Pound
                if (attackRan == 3) currentAttack = "GroundPound";
            }
            else if (attackMode == "Mid") currentAttack = "Hook";
        }

        //if (attackFrame == 0) currentAttack = "Idle";
        
        if (currentAttack == "BasicHit")
        {
            if (timeSet == false && attackFrame == 0)
            {
                attackFrame = 200;
                timeSet = true;
            }
            if (attackFrame > 150) ArmPos(210, 120, 1);
            else ArmPos(330, 290, 2);
            if (attackFrame == 1)
            {
                fightCD = 100;
                timeSet = false;
            }
        }
        else if (currentAttack == "RockThrow")
        {
            if (timeSet == false && attackFrame == 0)
            {
                attackFrame = 240;
                timeSet = true;
            }
            if (attackFrame > 150)
            {
                if (itemSpawned == false)
                {
                    Instantiate(rock, new Vector3(bossHitbox.transform.position.x, 4, 0), Quaternion.identity);
                    itemSpawned = true;
                }
                ArmPos(130, 40, 1);
            }
            else ArmPos(180, 200, 2);
            if (attackFrame == 1)
            {
                fightCD = 100;
                timeSet = false;
                itemSpawned = false;
            }
        }
        else if (currentAttack == "GroundPound")
        {
            if (timeSet == false && attackFrame == 0)
            {
                attackFrame = 420;
                timeSet = true;
            }

            if (attackFrame > 240 && attackFrame < 300)
            {
                ArmPos(310, 300, 2);
                if (itemSpawned == false)
                {
                    Instantiate(earthSpike, new Vector3(9, -2.6f, 0), Quaternion.identity);
                    itemSpawned = true;
                }
            }
            else if (attackFrame > 120 && attackFrame < 180)
            {
                ArmPos(310, 300, 2);
                if (itemSpawned == false)
                {
                    Instantiate(earthSpike, new Vector3(9, -2.6f, 0), Quaternion.identity);
                    itemSpawned = true;
                }
            }
            else if (attackFrame > 20 && attackFrame < 80)
            {
                ArmPos(310, 300, 2);
                if (itemSpawned == false)
                {
                    Instantiate(earthSpike, new Vector3(9, -2.6f, 0), Quaternion.identity);
                    itemSpawned = true;
                }
            }
            else
            {
                itemSpawned = false;
                ArmPos(260, 200, 1);
            }

            if (attackFrame == 1)
            {
                fightCD = 200;
                timeSet = false;
                itemSpawned = false;
            }
        }
        else if (currentAttack == "SonicYell")
        {
            ArmPos(220, 220, 1);
            if (timeSet == false && attackFrame == 0)
            {
                attackFrame = 180;
                timeSet = true;
            }
            if (attackFrame == 150)
            {
                if (itemSpawned == false)
                {
                    Instantiate(pushField, new Vector3(11.5f, 1.6f, 0), Quaternion.identity);
                    itemSpawned = true;
                }
            }
            if (attackFrame == 90)
            {
                attackRan = Random.Range(-14, 7);
                Instantiate(iceSpikes, new Vector3(attackRan, 9, 0), Quaternion.identity);
            }
            if (attackFrame == 88)
            {
                attackRan = Random.Range(-14, 7);
                Instantiate(iceSpikes, new Vector3(attackRan, 9, 0), Quaternion.identity);
            }
            if (attackFrame == 86)
            {
                attackRan = Random.Range(-14, 7);
                Instantiate(iceSpikes, new Vector3(attackRan, 9, 0), Quaternion.identity);
            }
            if (attackFrame == 84)
            {
                attackRan = Random.Range(-14, 7);
                Instantiate(iceSpikes, new Vector3(attackRan, 9, 0), Quaternion.identity);
            }
            if (attackFrame == 1)
            {
                fightCD = 120;
                timeSet = false;
                itemSpawned = false;
            }
        }
        else if (currentAttack == "Hook")
        {
            ArmPos(330, 280, 1);
            if (timeSet == false && attackFrame == 0)
            {
                attackFrame = 250;
                timeSet = true;
            }
            if (attackFrame < 200 && attackFrame > 140 && playerIntersect == false)
            {
                if(weapon.transform.localScale.y < 28)weapon.transform.localScale += new Vector3(0, 2, 0);
            }
            if (attackFrame < 140 || playerIntersect == true)
            {
                if (weapon.transform.localScale.y > 3.5) weapon.transform.localScale -= new Vector3(0, 3, 0);
            }
            if (weapon.GetComponent<SpriteRenderer>().bounds.Intersects(reftoControls.Player.GetComponent<SpriteRenderer>().bounds))
            {
                //deal damage
                playerIntersect = true;
                reftoControls.Player.transform.position += new Vector3(2, 0, 0);
            }
            if (attackFrame == 1)
            {
                fightCD = 120;
                timeSet = false;
                playerIntersect = false;
                weapon.transform.localScale = new Vector3(0.5f,3.5f,1);
            }
        }

    }

    private void BossJoints()
    {
        bossArm.transform.position = new Vector3(jointBoss.transform.position.x, jointBoss.transform.position.y, 0);
        weapon.transform.position = new Vector3(jointArm.transform.position.x, jointArm.transform.position.y, 0);
    }

    private void ArmPos(int _armAngle, int _weaponAngle, float _speedMultiplier)
    {
        if (bossArm.transform.eulerAngles.z > _armAngle+10)
        {
            bossArm.transform.eulerAngles -= new Vector3(0, 0, 10 * _speedMultiplier);
        }
        else if (bossArm.transform.eulerAngles.z < _armAngle-10)
        {
            bossArm.transform.eulerAngles += new Vector3(0, 0, 10 * _speedMultiplier);
        }
        if (weapon.transform.eulerAngles.z < _weaponAngle-10)
        {
            weapon.transform.eulerAngles += new Vector3(0, 0, 10 * _speedMultiplier);
        }
        else if (weapon.transform.eulerAngles.z > _weaponAngle+10)
        {
            weapon.transform.eulerAngles -= new Vector3(0, 0, 10 * _speedMultiplier);
        }
    }

    private void PlayerEffects()
    {
        //Throwing player back
        if (pushBack > 0)
        {
            reftoControls.Player.transform.position -= new Vector3(3f, 0, 0);
            pushBack--;
        }

        //Immunity to damage
    }

    private void UserInterface()
    {
        if (backdropHP.transform.position.y > 8) backdropHP.transform.position -= new Vector3(0, 0.05f, 0);
        bossHP.transform.position = new Vector3 (-12.5f, backdropHP.transform.position.y,1);
        bossHP.transform.localScale = new Vector3(1,(bossHealth/40),1);
    }
}
