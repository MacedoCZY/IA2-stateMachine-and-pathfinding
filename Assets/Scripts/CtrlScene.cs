using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CtrlScene : MonoBehaviour
{
    public GameObject box, exit, button;
    Rigidbody2D rb;
    SpriteRenderer spr;
    public Sprite sprTwo;
    GameObject player;
    float dist, dist2;
    public MeshRenderer txt, noOpen;
    bool open = false;

    void Start()
    {
        rb = box.GetComponent<Rigidbody2D>();
        spr = exit.GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        dist = (box.transform.position - button.transform.position).magnitude;
        if (dist <= 0.08)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            spr.sprite = sprTwo;
            open = true;
        }

        dist2 = (player.transform.position - exit.transform.position).magnitude;
        if (dist2 <= 0.7)
        {
            txt.enabled = true;
            if (open)
            {
                if(Input.GetKeyDown(KeyCode.F))
                    SceneManager.LoadScene("SampleScene");
            }
            else
            {
                txt.enabled = false;
                noOpen.enabled = true;
            }
        }
        else
        {   
            noOpen.enabled = false; 
            txt.enabled = false;
        }
    }
}
