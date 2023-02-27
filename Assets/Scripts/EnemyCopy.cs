using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCopy : MonoBehaviour
{
    [HideInInspector]
    public float speed = 5f;

    public Rigidbody2D enemyRb;
    public Animator animator;
    Vector2 moviment;
    public Transform enemy, player;

    float dist;

    [HideInInspector]
    public Pathfinding pt;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        dist = (enemy.transform.position - player.transform.position).magnitude;
        if (dist <= 0.4)
        {
            Player.alive = false;
        }
        else
        {
            //pathfind
            pt.FindPlayer(enemy, player);

            //---controle do animator do inimigo---
            Vector3 nextPos = pt.grid.pathFinded[1].truePosFromWorld;
            Vector3 posAtual = pt.grid.pathFinded[0].truePosFromWorld;

            if (posAtual.y < nextPos.y && posAtual.x < nextPos.x)
            {
                moviment.y = 1;
                moviment.x = 1;
            }
            else if (posAtual.y < nextPos.y && posAtual.x == nextPos.x)
            {
                moviment.y = 1;
                moviment.x = 0;
            }
            else if (posAtual.y == nextPos.y && posAtual.x < nextPos.x)
            {
                moviment.y = 0;
                moviment.x = 1;
            }
            else if (posAtual.y > nextPos.y && posAtual.x > nextPos.x)
            {
                moviment.y = -1;
                moviment.x = -1;
            }
            else if (posAtual.y > nextPos.y && posAtual.x == nextPos.x)
            {
                moviment.y = -1;
                moviment.x = 0;
            }
            else if (posAtual.y == nextPos.y && posAtual.x > nextPos.x)
            {
                moviment.y = 0;
                moviment.x = -1;
            }
            else if (posAtual.y == nextPos.y && posAtual.x == nextPos.x)
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
    }

    private void FixedUpdate()
    {
        //enemyRb.MovePosition(enemyRb.position + moviment * speed * Time.fixedDeltaTime);
    }
}
