using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float viewRadius = 5f;
    [Range(0, 360)]public float viewAngle = 45f;
    public LayerMask targetMask;
    public LayerMask obstacleMask;

    public GameObject player;
    public bool playerInSight { get; private set; }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FDECheck());
    }

    private IEnumerator FDECheck()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FindVisibleTargets();
        }
    }

    private void FindVisibleTargets()
    {
        Collider2D[] rangeChecks = Physics2D.OverlapCircleAll(transform.position, viewRadius, targetMask);

        if(rangeChecks.Length > 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector2 dirToTarget = (target.position - transform.position).normalized;
            
            if(Vector2.Angle(transform.up, dirToTarget) < viewAngle / 2)
            {
                float dstToTarget = Vector2.Distance(transform.position, target.position);

                if(!Physics2D.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                {
                    playerInSight = true;
                }
                else
                {
                    playerInSight = false;
                }
            }
        }
        else if(playerInSight)
        {
            playerInSight = false;
        }
    
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        //UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, viewRadius);

        Vector3 angle01 = AngleView(-transform.eulerAngles.z, -viewAngle / 2);
        Vector3 angle02 = AngleView(-transform.eulerAngles.z, viewAngle / 2);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + angle01 * viewRadius);
        Gizmos.DrawLine(transform.position, transform.position + angle02 * viewRadius);

        if(playerInSight)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, player.transform.position);
        }
    }

    private Vector2 AngleView(float angleY, float angleD)
    {
        angleD += angleY;

        return new Vector2(Mathf.Sin(angleD * Mathf.Deg2Rad), Mathf.Cos(angleD * Mathf.Deg2Rad));
    }
}
