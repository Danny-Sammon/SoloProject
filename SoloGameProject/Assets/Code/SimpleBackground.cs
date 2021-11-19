using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBackground : MonoBehaviour
{
    Material material;
    Vector2 offset;

    public float xVelocity, yVelocity;
    // Start is called before the first frame update
    private void Awake()
    {

        material = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        offset = new Vector2(xVelocity, yVelocity);

        material.mainTextureOffset += offset * Time.deltaTime;

    }
}