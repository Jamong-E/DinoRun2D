using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    Animator ani;
    float vert = 0;
    float jumpPower = 0.25f;
    bool jump = false;
    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !jump) { jump = true; vert = jumpPower; }
        if (jump)
        {
            transform.Translate(0, vert, 0);
            vert -= 0.015625f;
            if (transform.position.y <= -2) { jump = false; }
        }

        if (transform.position.y < -2) { transform.Translate(0, -2 - transform.position.y, 0); }
        else if (transform.position.y > -2) { ani.SetTrigger("Jump"); }
        if (!Input.GetKey(KeyCode.DownArrow) && transform.position.y <= -2) { ani.SetTrigger("Run"); }
        if (Input.GetKey(KeyCode.DownArrow) && transform.position.y <= -2) { ani.SetTrigger("Down"); }
    }
}
