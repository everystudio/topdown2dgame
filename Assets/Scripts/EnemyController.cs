using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int hp = 3;
    public float speed = 0.5f;
    public float reactionDistance = 4.0f;

    public string idleAnime = "EnemyIdle";
    public string upAnime = "EnemyUp";
    public string downAnime = "EnemyDown";
    public string rightAnime = "EnemyRight";
    public string leftAnime = "EnemyLeft";
    public string deadAnime = "EnemyDead";

    private string nowAnimation = "";
    private string oldAnimation = "";

    private float axisH;
    private float axisV;
    private Rigidbody2D rbody;

    private bool isActive = false;
    public int arrangeId = 0;

    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        GameObject objPlayer = GameObject.FindGameObjectWithTag("Player");
        if (objPlayer != null)
        {
            if (isActive)
            {
                Vector3 deltaPos = objPlayer.transform.position - transform.position;
                float rad = Mathf.Atan2(deltaPos.y, deltaPos.x);
                float angle = rad * Mathf.Rad2Deg;
                if(-45.0f <angle && angle <= 45.0f)
                {
                    nowAnimation = rightAnime;
                }
                else if( 45.0f < angle && angle <= 135.0f)
                {
                    nowAnimation = upAnime;
                }
                else if( -135.0f < angle && angle <= -45.0f)
                {
                    nowAnimation = downAnime;
                }
                else
                {
                    nowAnimation = leftAnime;
                }
                axisH = Mathf.Cos(rad) * speed;
                axisV = Mathf.Sin(rad) * speed;
            }
            else
            {
                float dist = Vector2.Distance(transform.position, objPlayer.transform.position);
                if( dist < reactionDistance)
                {
                    isActive = true;
                }
            }
        }
        else if( isActive)
        {
            isActive = false;
            rbody.velocity = Vector2.zero;
        }
    }
    private void FixedUpdate()
    {
        if( isActive && 0 < hp)
        {
            rbody.velocity = new Vector2(axisH, axisV);
            if(oldAnimation != nowAnimation)
            {
                Animator animator = GetComponent<Animator>();
                animator.Play(nowAnimation);
                oldAnimation = nowAnimation;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Arrow")
        {
            hp -= 1;
            if( hp <= 0)
            {
                GetComponent<CircleCollider2D>().enabled = false;
                rbody.velocity = Vector2.zero;
                GetComponent<Animator>().Play(deadAnime);
                Destroy(gameObject, 0.5f);
            }
        }
    }
}






