using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour {
    private float ySpeed = 0.3f;
    public Material mat;
    Vector2 offset = Vector2.zero;
    // Use this for initialization
    void Start()
    {
        mat = GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        offset.y += ySpeed * Time.deltaTime;
        if (offset.y > 0.1f)
            offset.y -= 0.9f;
        else if (offset.y < -0.1f)
            offset.y += 0.9f;
        mat.mainTextureOffset = offset;
    }
}
