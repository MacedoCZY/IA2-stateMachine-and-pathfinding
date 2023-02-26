using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [HideInInspector]
    public float speed;

    public Rigidbody2D playerRb;
    public Animator animator;
    Vector2 moviment;
    public static string lastScn;

    [HideInInspector]
    public static bool alive = true;

   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.R))
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        if (alive)
        {
            moviment.x = Input.GetAxisRaw("Horizontal");
            moviment.y = Input.GetAxisRaw("Vertical");
            
            if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                speed = 1.333f;
            }
            else
            {
                speed = 0.666f;
            }

            animator.SetFloat("horizontal", moviment.x);
            animator.SetFloat("vertical", moviment.y);
            animator.SetFloat("speed", moviment.sqrMagnitude);

            if (moviment != Vector2.zero)
            {
                animator.SetFloat("horiIdle", moviment.x);
                animator.SetFloat("vertIdle", moviment.y);
            }
        }
        else
        {
            lastScn = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene("Death");
        }
    }

    private void FixedUpdate(){
        playerRb.MovePosition(playerRb.position + moviment * speed * Time.fixedDeltaTime);
    }

}
