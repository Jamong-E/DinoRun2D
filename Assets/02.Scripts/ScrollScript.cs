using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollScript : MonoBehaviour
{
    public GameObject CloudPrefab;
    public float scrollSpeedX = 2.0f;
    private Renderer quadRenderer;
    float time = 0f;
    float nextCloud = 0.1f;
    System.Random random = new System.Random();
    // Start is called before the first frame update
    void Start()
    {
        quadRenderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > nextCloud)
        {
            time = 0f;
            GameObject Cloud = Instantiate(CloudPrefab);
            Cloud.transform.SetParent(transform.parent);
            float seed = (float)random.NextDouble();
            nextCloud = 0.5f + 2 * seed;  // square SEED if you want clustered clouds
        }

        float offsetX = Time.time * scrollSpeedX;
        quadRenderer.material.mainTextureOffset = new Vector2(offsetX, 0f);
    }
}
