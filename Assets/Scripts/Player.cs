using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{ 
    public float speed = 5f;
    public Rigidbody2D playerRb;
    public Animator animator;
    Vector2 moviment;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moviment.x = Input.GetAxisRaw("Horizontal");
        moviment.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("horizontal", moviment.x);
        animator.SetFloat("vertical", moviment.y);
        animator.SetFloat("speed", moviment.sqrMagnitude);

        if(moviment != Vector2.zero){
            animator.SetFloat("horiIdle", moviment.x);
            animator.SetFloat("vertIdle", moviment.y);
        }

    }

    private void FixedUpdate(){
        playerRb.MovePosition(playerRb.position + moviment * speed * Time.fixedDeltaTime);
    }
}
