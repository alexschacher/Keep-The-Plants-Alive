using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaf : MonoBehaviour
{
    private float growthRate;
    private float growthSlowdown;

    void Start()
    {
        growthRate = Random.Range(1f, 1.2f);
        growthSlowdown = Random.Range(0.35f, 0.36f);
        GetComponentInChildren<Renderer>().material.color = new Color(
            2/5f,
            1/15f,
            Random.Range(0, 2/5f)
            );
    }

    void Update()
    {
        if (growthRate < 0.1f) return;

        growthRate -= growthSlowdown * Time.deltaTime;

        Debug.Log(growthRate);
        Debug.Log("time:" + Time.deltaTime);

        transform.localScale = new Vector3(
            transform.localScale.x + (growthRate * Time.deltaTime),
            transform.localScale.y + (growthRate * Time.deltaTime),
            transform.localScale.z + (growthRate * Time.deltaTime));
    }
}