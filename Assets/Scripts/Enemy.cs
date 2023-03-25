using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 leftbounds = new(-115f, 0f, 0f);
    private Vector3 rightbounds = new(115f, 0f, 0f);

    public Transform EnemyTransform;
    public Vector3 EnemyMovement = new Vector3(0f, 0f, 0f);
    public Vector3 EnemySpeed = new Vector3(1f,0f,0f);
    public float FloatingMaxspeed = 25f;

    private System.Random RNG = new System.Random(Guid.NewGuid().GetHashCode());
    public float ChangeDirectionTime = 4;
    private float TimeTillRNG = 0;
    public float CurrentRN;

    public GameObject Bullet;
    public Vector3 AttackMovement = new Vector3(4f, 0f, 0f);
    public float AttackingMaxspeed = 100;
    public float AttackDelay = 4;
    private float TimeTillAttack = 0;
    public float[] AttackPatternDelay = {0.01f,0.5f,1f,2f,2.5f};
    private bool AttackStartReached = false;
    private bool AttackisRunning = false;

    public int BulletAmount = 2;
    private int ShotsFired = 0;
    private int CurrentAttackNum = -1;
    private int lifes = 10;



    void Start()
    {
        CurrentRN = RNG.Next(200);
    }

    // Update is called once per frame
    void Update()
    {
        if(AttackisRunning == false)
        {
            SmoothMove();
            Attack();
            Debug.Log("Movement loop");
        }
        else
        {
            ContinueAttack();
            Debug.Log(" In Continue Loop");
        }
      //  Debug.Log(EnemyTransform.position.x);
    }

    private void SmoothMove()
    {
        TimeTillRNG += Time.deltaTime;

        if (TimeTillRNG >= ChangeDirectionTime)
        {
            CurrentRN = RNG.Next(200);
            TimeTillRNG = 0;
        }
        if (CurrentRN >= 100)
        {
            if (EnemyMovement.x < FloatingMaxspeed)
            {
                EnemyMovement += EnemySpeed;
            }
            if (EnemyTransform.position.x + EnemyMovement.x * Time.deltaTime >= rightbounds.x)
            {
                EnemyMovement.x = 0;
                CurrentRN = 50;
            }
            else
            {
                EnemyTransform.Translate(-EnemyMovement * Time.deltaTime);
            }
        }
        else
        {
            if (EnemyMovement.x > (-FloatingMaxspeed))
            {
                EnemyMovement -= EnemySpeed;
            }
            if (EnemyTransform.position.x + EnemyMovement.x * Time.deltaTime <= leftbounds.x)
            {
                EnemyMovement.x = 0;
                CurrentRN = 150;
            }
            else
            {
                EnemyTransform.Translate(-EnemyMovement * Time.deltaTime);
            }
        }
    }

    public void Attack()
    {
        TimeTillAttack += Time.deltaTime;
        
        if (TimeTillAttack > AttackDelay && AttackisRunning == false)
        {
            Debug.Log("Attack triggered");
            switch (RNG.Next(1100))
            {
                case int i when (i < 500):
                    {
                        //Attackpattern1();
                        AttackisRunning = true;
                        CurrentAttackNum = 7;
                        break;
                    }
                case int i when (i > 500 && i < 600):
                    {
                          AttackisRunning = true;
                          CurrentAttackNum = 2;
                        break;
                    }
                case int i when (i > 600 && i < 700):
                    {
                        AttackisRunning = true;
                        CurrentAttackNum = 3;
                        break;
                    }
                case int i when (i > 700 && i < 800):
                    {
                           AttackisRunning = true;
                           CurrentAttackNum = 4;
                        break;
                    }
                case int i when (i > 800 && i < 900):
                    {
                         AttackisRunning = true;
                         CurrentAttackNum = 5;
                        break;
                    }
                case int i when (i > 900 && i <= 1000):
                    {
                        AttackisRunning = true;
                        CurrentAttackNum = 6;
                        break;
                    }
                case int i when (i > 1000 && i <= 1100):
                    {
                        AttackisRunning = true;
                        CurrentAttackNum = 7;
                        break;
                    }
                    
            }
            TimeTillAttack = 0;
        }
    }

    public void ContinueAttack()
    {
        switch(CurrentAttackNum)
        {
            case 0:
                {
                    Debug.Log("No Attack is currently running");
                    break;
                }
            case 1:
                {
                    Debug.Log("Attack is instant. AttackisRunning Variable should not be 1");
                    break;
                }
            case 2:
                {
                    Debug.Log("Attack 2 triggered");
                    Attackpattern2();
                    break;
                }
            case 3:
                {
                    Debug.Log("Attack 3 triggered");
                    Attackpattern3();
                    break;
                }
            case 4:
                {
                    Debug.Log("Attack 4 triggered");
                    Attackpattern4();
                    break;
                }
            case 5:
                {
                    Debug.Log("Attack 5 triggered");
                    Attackpattern5();
                    break;
                }
            case 6:
                {
                    Attackpattern6();
                    break;
                }
            case 7:
                {
                    Attackpattern7();
                    break;
                }
        }
    }


    public void Attackpattern1()
    {
        Instantiate(Bullet, EnemyTransform.position, Quaternion.identity).transform.Rotate(0, 0, 160f);
        Instantiate(Bullet, EnemyTransform.position, Quaternion.identity).transform.Rotate(0, 0, 180f);
        Instantiate(Bullet, EnemyTransform.position, Quaternion.identity).transform.Rotate(0, 0, 200f);
    }    
    public void Attackpattern2()
    {


        EnemyMovement.x = -AttackingMaxspeed;
        if (AttackStartReached == false)
        {
            if (EnemyTransform.position.x + EnemyMovement.x * Time.deltaTime <= leftbounds.x)
            {
                EnemyMovement.x = 0;
                AttackStartReached = true;
            }
            else
            {
                EnemyTransform.Translate(-EnemyMovement * Time.deltaTime);
            }
        }
        else
        {
            EnemyMovement.x = AttackingMaxspeed * 0.5f;
            if (EnemyTransform.position.x + EnemyMovement.x * Time.deltaTime >= rightbounds.x)
            {
                EnemyMovement.x = 0;
                AttackisRunning = false;
                AttackStartReached = false;
            }
            else
            {
                EnemyTransform.Translate(-EnemyMovement * Time.deltaTime);
                TimeTillAttack += Time.deltaTime;
                if(TimeTillAttack >= AttackPatternDelay[0])
                {
                    Instantiate(Bullet, EnemyTransform.position, new Quaternion(0f, 0f, 180f, 0f));
                    TimeTillAttack = 0;
                }
            }

        }
    }    
    public void Attackpattern3()
    {
        EnemyMovement.x = AttackingMaxspeed;
        if (AttackStartReached == false)
        {
            if (EnemyTransform.position.x + EnemyMovement.x * Time.deltaTime >= rightbounds.x)
            {
                EnemyMovement.x = 0;
                AttackStartReached = true;
            }
            else
            {
                EnemyTransform.Translate(-EnemyMovement * Time.deltaTime);
            }
        }
        else
        {
            EnemyMovement.x = -AttackingMaxspeed * 0.5f;
            if (EnemyTransform.position.x + EnemyMovement.x * Time.deltaTime <= leftbounds.x)
            {
                EnemyMovement.x = 0;
                AttackisRunning = false;
                AttackStartReached = false;
            }
            else
            {
                EnemyTransform.Translate(-EnemyMovement * Time.deltaTime);
                TimeTillAttack += Time.deltaTime;
                if (TimeTillAttack >= AttackPatternDelay[0])
                {
                    Instantiate(Bullet, EnemyTransform.position, new Quaternion(0f, 0f, 180f, 0f));
                    TimeTillAttack = 0;
                }
            }

        }
    }    
    public void Attackpattern4()
    {
        if(AttackStartReached == false)
        {
            MoveCenter();
        }
        else
        {
            TimeTillAttack += Time.deltaTime;
            if (TimeTillAttack >= AttackPatternDelay[1])
            {
                if(ShotsFired < BulletAmount)
                {
                    Instantiate(Bullet, EnemyTransform.position, Quaternion.identity).transform.Rotate(0, 0, 160);
                    Instantiate(Bullet, EnemyTransform.position, Quaternion.identity).transform.Rotate(0, 0, 180);
                    Instantiate(Bullet, EnemyTransform.position, Quaternion.identity).transform.Rotate(0, 0, 200);
                    Instantiate(Bullet, EnemyTransform.position, Quaternion.identity).transform.Rotate(0, 0, 220);
                    Instantiate(Bullet, EnemyTransform.position, Quaternion.identity).transform.Rotate(0, 0, 140);
                    TimeTillAttack = 0;
                    ShotsFired++;
                }
                else
                {
                    AttackisRunning = false;
                    AttackStartReached = false;
                    ShotsFired = 0;
                    EnemyMovement.x = 0;
                }

            }
        }

    }    
    public void Attackpattern5()
    {
        if (AttackStartReached == false)
        {
            MoveCenter();
        }
        else
        {
            TimeTillAttack += Time.deltaTime;
            if (TimeTillAttack >= AttackPatternDelay[1])
            {
                if (ShotsFired < BulletAmount)
                {
                    Instantiate(Bullet, new Vector3(EnemyTransform.position.x + 1f *(leftbounds.x/5f) , EnemyTransform.position.y, 0f), Quaternion.identity).transform.Rotate(0, 0, 180);
                    Instantiate(Bullet, new Vector3(EnemyTransform.position.x + 2f *(leftbounds.x/5f) , EnemyTransform.position.y, 0f), Quaternion.identity).transform.Rotate(0, 0, 180);
                    Instantiate(Bullet, new Vector3(EnemyTransform.position.x + 3f *(leftbounds.x/5f) , EnemyTransform.position.y, 0f), Quaternion.identity).transform.Rotate(0, 0, 180);
                    Instantiate(Bullet, new Vector3(EnemyTransform.position.x + 4f *(leftbounds.x/5f) , EnemyTransform.position.y, 0f), Quaternion.identity).transform.Rotate(0, 0, 180);
                    Instantiate(Bullet, new Vector3(EnemyTransform.position.x + 4.5f *(leftbounds.x/5f) , EnemyTransform.position.y, 0f), Quaternion.identity).transform.Rotate(0, 0, 180);

                    Instantiate(Bullet, new Vector3(EnemyTransform.position.x - 1f *(leftbounds.x/5f) , EnemyTransform.position.y, 0f), Quaternion.identity).transform.Rotate(0, 0, 180);
                    Instantiate(Bullet, new Vector3(EnemyTransform.position.x - 2f *(leftbounds.x/5f) , EnemyTransform.position.y, 0f), Quaternion.identity).transform.Rotate(0, 0, 180);
                    Instantiate(Bullet, new Vector3(EnemyTransform.position.x - 3f *(leftbounds.x/5f) , EnemyTransform.position.y, 0f), Quaternion.identity).transform.Rotate(0, 0, 180);
                    Instantiate(Bullet, new Vector3(EnemyTransform.position.x - 4f *(leftbounds.x/5f) , EnemyTransform.position.y, 0f), Quaternion.identity).transform.Rotate(0, 0, 180);
                    Instantiate(Bullet, new Vector3(EnemyTransform.position.x - 4.5f *(leftbounds.x/5f) , EnemyTransform.position.y, 0f), Quaternion.identity).transform.Rotate(0, 0, 180);
                    TimeTillAttack = 0;
                    ShotsFired++;
                }
                else
                {
                    AttackisRunning = false;
                    AttackStartReached = false;
                    ShotsFired = 0;
                    EnemyMovement.x = 0;
                }

            }
        }
    }

    private void Attackpattern6()
    {
        if (AttackStartReached == false)
        {
            MoveCenter();
        }   
        else
        {
            TimeTillAttack += Time.deltaTime;
            if (TimeTillAttack >= AttackPatternDelay[1])
            {
                if (ShotsFired < BulletAmount)
                {
                    Instantiate(Bullet, new Vector3(EnemyTransform.position.x + 1f * (leftbounds.x / 5f), EnemyTransform.position.y, 0f), Quaternion.identity).transform.Rotate(0, 0, 205);
                    Instantiate(Bullet, new Vector3(EnemyTransform.position.x + 2f * (leftbounds.x / 5f), EnemyTransform.position.y, 0f), Quaternion.identity).transform.Rotate(0, 0, 215);
                    Instantiate(Bullet, new Vector3(EnemyTransform.position.x + 3f * (leftbounds.x / 5f), EnemyTransform.position.y, 0f), Quaternion.identity).transform.Rotate(0, 0, 225);
                    Instantiate(Bullet, new Vector3(EnemyTransform.position.x + 4f * (leftbounds.x / 5f), EnemyTransform.position.y, 0f), Quaternion.identity).transform.Rotate(0, 0, 235);
                    Instantiate(Bullet, new Vector3(EnemyTransform.position.x + 4.5f * (leftbounds.x / 5f), EnemyTransform.position.y, 0f), Quaternion.identity).transform.Rotate(0, 0, 245);

                    Instantiate(Bullet, new Vector3(EnemyTransform.position.x - 1f * (leftbounds.x / 5f), EnemyTransform.position.y, 0f), Quaternion.identity).transform.Rotate(0, 0, 180);
                    Instantiate(Bullet, new Vector3(EnemyTransform.position.x - 2f * (leftbounds.x / 5f), EnemyTransform.position.y, 0f), Quaternion.identity).transform.Rotate(0, 0, 180);
                    Instantiate(Bullet, new Vector3(EnemyTransform.position.x - 3f * (leftbounds.x / 5f), EnemyTransform.position.y, 0f), Quaternion.identity).transform.Rotate(0, 0, 180);
                    Instantiate(Bullet, new Vector3(EnemyTransform.position.x - 4f * (leftbounds.x / 5f), EnemyTransform.position.y, 0f), Quaternion.identity).transform.Rotate(0, 0, 180);
                    Instantiate(Bullet, new Vector3(EnemyTransform.position.x - 4.5f * (leftbounds.x / 5f), EnemyTransform.position.y, 0f), Quaternion.identity).transform.Rotate(0, 0, 180);
                    TimeTillAttack = 0;
                    ShotsFired++;
                }
                else
                {
                    AttackisRunning = false;
                    AttackStartReached = false;
                    ShotsFired = 0;
                    EnemyMovement.x = 0;
                }
            }
        }
    }
    private void Attackpattern7()
    {
        if (AttackStartReached == false)
        {
            MoveCenter();
        }
        else
        {
            TimeTillAttack += Time.deltaTime;
            if (TimeTillAttack >= AttackPatternDelay[1])
            {
                if (ShotsFired < BulletAmount)
                {
                    Instantiate(Bullet, new Vector3(EnemyTransform.position.x + 1f * (leftbounds.x / 5f), EnemyTransform.position.y, 0f), Quaternion.identity).transform.Rotate(0, 0, 180);
                    Instantiate(Bullet, new Vector3(EnemyTransform.position.x + 2f * (leftbounds.x / 5f), EnemyTransform.position.y, 0f), Quaternion.identity).transform.Rotate(0, 0, 180);
                    Instantiate(Bullet, new Vector3(EnemyTransform.position.x + 3f * (leftbounds.x / 5f), EnemyTransform.position.y, 0f), Quaternion.identity).transform.Rotate(0, 0, 180);
                    Instantiate(Bullet, new Vector3(EnemyTransform.position.x + 4f * (leftbounds.x / 5f), EnemyTransform.position.y, 0f), Quaternion.identity).transform.Rotate(0, 0, 180);
                    Instantiate(Bullet, new Vector3(EnemyTransform.position.x + 4.5f * (leftbounds.x / 5f), EnemyTransform.position.y, 0f), Quaternion.identity).transform.Rotate(0, 0, 180);

                    Instantiate(Bullet, new Vector3(EnemyTransform.position.x - 1f * (leftbounds.x / 5f), EnemyTransform.position.y, 0f), Quaternion.identity).transform.Rotate(0, 0, 155);
                    Instantiate(Bullet, new Vector3(EnemyTransform.position.x - 2f * (leftbounds.x / 5f), EnemyTransform.position.y, 0f), Quaternion.identity).transform.Rotate(0, 0, 145);
                    Instantiate(Bullet, new Vector3(EnemyTransform.position.x - 3f * (leftbounds.x / 5f), EnemyTransform.position.y, 0f), Quaternion.identity).transform.Rotate(0, 0, 135);
                    Instantiate(Bullet, new Vector3(EnemyTransform.position.x - 4f * (leftbounds.x / 5f), EnemyTransform.position.y, 0f), Quaternion.identity).transform.Rotate(0, 0, 125);
                    Instantiate(Bullet, new Vector3(EnemyTransform.position.x - 4.5f * (leftbounds.x / 5f), EnemyTransform.position.y, 0f), Quaternion.identity).transform.Rotate(0, 0, 115);
                    TimeTillAttack = 0;
                    ShotsFired++;
                }
                else
                {
                    AttackisRunning = false;
                    AttackStartReached = false;
                    ShotsFired = 0;
                    EnemyMovement.x = 0;
                }
            }
        }
    }

    private void MoveCenter()
    {

            if (EnemyTransform.position.x > 0)
            {
                EnemyMovement.x = AttackingMaxspeed;
            }
            else
            {
                EnemyMovement.x = -AttackingMaxspeed;
            }

            EnemyTransform.Translate(EnemyMovement * Time.deltaTime);

            if (EnemyTransform.position.x > -2 && EnemyTransform.position.x < 2)
            {
                AttackStartReached = true;
            }
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag.Equals("PlayerBullet"))
        {
            lifes--;
            if(lifes==0)
                Destroy(gameObject);
        }
    }
}