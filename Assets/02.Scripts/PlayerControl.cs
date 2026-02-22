using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    Animator ani;
    new BoxCollider2D collider;
    Rigidbody2D rb;
    float vert = 0;
    float jumpPower = 0.25f;
    bool jump = false;

    public GameObject PrefabBird;
    public GameObject PrefabCactus1;
    public GameObject PrefabCactus2;
    public GameObject PrefabCactus3;
    public GameObject PrefabCactus4;
    float time = 0.0f;
    float spawntime = 2.0f;
    System.Random rand = new System.Random();

    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
        collider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
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
        else if (transform.position.y > -2) {
            ani.SetTrigger("Jump");
            collider.offset = new Vector2(-0.03f, 0.05f);
            collider.size = new Vector2(1.1f, 1.3f);
        }
        if (!Input.GetKey(KeyCode.DownArrow) && transform.position.y <= -2) {
            ani.SetTrigger("Run");
            collider.offset = new Vector2(-0.03f, 0.05f);
            collider.size = new Vector2(1.1f, 1.3f);
        }
        if (Input.GetKey(KeyCode.DownArrow) && transform.position.y <= -2) {
            ani.SetTrigger("Down");
            collider.offset = new Vector2(-0.03f, -0.225f);
            collider.size = new Vector2(1.6f, 0.75f);
        }

        // Obstacle Spawner
        time += Time.deltaTime;
        if (time > spawntime)
        {
            time = 0;
            spawntime *= 0.9f;
            GameObject obstacle;
            int target = rand.Next(1000);
            if (target < 125) { obstacle = Instantiate(PrefabCactus1); obstacle.transform.position = new Vector2(11f, -2.1f); }
            else if (target < 250) { obstacle = Instantiate(PrefabCactus2); obstacle.transform.position = new Vector2(11f, -2f); }
            else if (target < 375) { obstacle = Instantiate(PrefabCactus3); obstacle.transform.position = new Vector2(11f, -2f); }
            else if (target < 500) { obstacle = Instantiate(PrefabCactus4); obstacle.transform.position = new Vector2(11f, -2f); }
            else {
                obstacle = Instantiate(PrefabBird);
                float y = -1.1f;
                if (target % 3 == 1) { y = -0.8f; }
                else if (target % 3 == 2) { y = -0.5f; }
                obstacle.transform.position = new Vector2(11f, y);
            }
        }

        // Raycast Visualization; 충돌 판정 제작
        Debug.DrawRay(new Vector2(transform.position.x + collider.offset.x - collider.size.x / 2, transform.position.y + collider.offset.y - collider.size.y / 2), new Vector2(collider.size.x, 0), Color.red);
        Debug.DrawRay(new Vector2(transform.position.x + collider.offset.x - collider.size.x / 2, transform.position.y + collider.offset.y + collider.size.y / 2), new Vector2(collider.size.x, 0), Color.green);
        Debug.DrawRay(new Vector2(transform.position.x + collider.offset.x - collider.size.x / 2, transform.position.y + collider.offset.y - collider.size.y / 2), new Vector2(0, collider.size.y), Color.blue);
        Debug.DrawRay(new Vector2(transform.position.x + collider.offset.x + collider.size.x / 2, transform.position.y + collider.offset.y - collider.size.y / 2), new Vector2(0, collider.size.y), Color.white);

        if (Physics2D.Raycast(new Vector2 (transform.position.x + collider.offset.x - collider.size.x / 2, transform.position.y + collider.offset.y - collider.size.y / 2), new Vector2 (1, 0), collider.size.x, (1 << 7)) ||
            Physics2D.Raycast(new Vector2 (transform.position.x + collider.offset.x - collider.size.x / 2, transform.position.y + collider.offset.y + collider.size.y / 2), new Vector2 (1, 0), collider.size.x, (1 << 7)) ||
            Physics2D.Raycast(new Vector2 (transform.position.x + collider.offset.x - collider.size.x / 2, transform.position.y + collider.offset.y - collider.size.y / 2), new Vector2 (0, 1), collider.size.y, (1 << 7)) ||
            Physics2D.Raycast(new Vector2 (transform.position.x + collider.offset.x + collider.size.x / 2, transform.position.y + collider.offset.y - collider.size.y / 2), new Vector2 (0, 1), collider.size.y, (1 << 7)))
        { Debug.Log("bam"); }
    }
}
