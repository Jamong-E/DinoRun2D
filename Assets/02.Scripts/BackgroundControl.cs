using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundControl : MonoBehaviour
{
    float backgroundSpeedX = 0.5f;
    float speedVar = 0f;
    // Start is called before the first frame update
    void Start()
    {
        System.Random random = new System.Random();
        transform.Translate(12 - transform.position.x, (float)random.NextDouble() * 4, 0);
        speedVar = 1.0f + (float)random.NextDouble();
        backgroundSpeedX *= speedVar;
        transform.localScale = new Vector3 (speedVar, speedVar, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(-5 * Time.deltaTime * backgroundSpeedX, 0, 0);
        if (transform.position.x < -12) { Destroy(gameObject); }
    }
}
