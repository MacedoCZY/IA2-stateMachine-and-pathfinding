using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMS : MonoBehaviour
{
    private FieldOfView fov;
    private EnemyCopy EnemyCopy;
    private float timer;
    public GameObject Enemy;
    private Animator anim;

    void Start()
    {
        fov = Enemy.GetComponent<FieldOfView>();
        EnemyCopy = Enemy.GetComponent<EnemyCopy>();
    	anim = Enemy.GetComponent<Animator>();
    }

    void Update()
    {
        if (fov.playerInSight)
        {
            anim.enabled = true;
            EnemyCopy.enabled = true;
            timer = Time.time;
        }

        if (Time.time - timer > 5)
        {
            anim.enabled = false; 
	    EnemyCopy.enabled = false;
        }

        
    }
}
