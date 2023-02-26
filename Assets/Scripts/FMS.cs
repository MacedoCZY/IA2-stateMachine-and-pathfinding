using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMS : MonoBehaviour
{
    private FieldOfView fov;
    private EnemyCopy EnemyCopy;
    private float timer;
    public GameObject Enemy;

    void Start()
    {
        fov = Enemy.GetComponent<FieldOfView>();
        EnemyCopy = Enemy.GetComponent<EnemyCopy>();
    }

    void Update()
    {
        if (fov.playerInSight)
        {
            EnemyCopy.enabled = true;
            timer = Time.time;
        }

        if (Time.time - timer > 5)
        {
            EnemyCopy.enabled = false;
        }

        
    }
}
