using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCopy : MonoBehaviour
{
    public float speed = 5f;
    public Rigidbody2D enemyRb;
    public Animator animator;
    Vector2 moviment;
    public Transform enemy, player;
    public Pathfinding pt;

    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        //pathfind
        pt.FindPlayer(enemy, player);

        Vector3 posAtual = this.transform.position;
        Vector3 nextPos = pt.grid.pathFinded[0].truePosFromWorld;

        if (posAtual.y < nextPos.y && posAtual.x < nextPos.x)
        {
            moviment.y = 1;
            moviment.x = 1;
        } 
        if (posAtual.y < nextPos.y && posAtual.x == nextPos.x)
        {
            moviment.y = 1;
            moviment.x = 0;
        }
        if (posAtual.y > nextPos.y && posAtual.x > nextPos.x)
        {
            moviment.y = -1;
            moviment.x = -1;
        } 
        if (posAtual.y > nextPos.y && posAtual.x == nextPos.x)
        {
            moviment.y = -1;
            moviment.x = 0;
        }
        if(posAtual.y == nextPos.y && posAtual.x == nextPos.x)
        {
            moviment.y = 0;
            moviment.x = 0;
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
        //enemyRb.MovePosition(enemyRb.position + moviment * speed * Time.fixedDeltaTime);
    }
}
