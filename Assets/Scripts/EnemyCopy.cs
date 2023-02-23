using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCopy : MonoBehaviour
{
    public float speed = 5f;
    public Rigidbody2D enemyRb;
    public Animator animator;
    Vector2 moviment;
    Transform initPos;
    public Transform enemy;

    // Start is called before the first frame update
    void Start()
    {
        initPos = enemy.transform;
    }

    // Update is called once per frame
    void Update()
    {
        //moviment.x = Input.GetAxisRaw("Horizontal");
        //moviment.y = Input.GetAxisRaw("Vertical");
        if(initPos.position.x - enemy.position.x > 0)
        {
            moviment.x = 1;
        }
        else if(initPos.position.x - enemy.position.x < 0)
        {
            moviment.x = -1;
        }
        else
        {
            moviment.x = 1;
        }
        if(initPos.position.y - enemy.position.y > 0)
        {
            moviment.y = 1;
        }
        else if(initPos.position.y - enemy.position.y < 0)
        {
            moviment.y = -1;
        }
        else
        {
            moviment.y = 1;
        }

        animator.SetFloat("Horizontal", moviment.x);
        animator.SetFloat("Vertical", moviment.y);
        animator.SetFloat("Speed", moviment.sqrMagnitude);

        if (moviment != Vector2.zero)
        {
            animator.SetFloat("HorIdle", moviment.x);
            animator.SetFloat("VertIdle", moviment.y);
        }

    }

    private void FixedUpdate()
    {
        enemyRb.MovePosition(enemyRb.position + moviment * speed * Time.fixedDeltaTime);
    }
}
