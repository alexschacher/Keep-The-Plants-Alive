using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame : MonoBehaviour
{
    private float heightChange;
    private float heightGoal;
    private float heightChangeRate = 0.07f;

    void Update()
    {
        heightGoal = Random.Range(0.8f, 1.2f);

        if (heightGoal > transform.localScale.y)
        {
            heightChange = heightChangeRate;
        }
        else
        {
            heightChange = -heightChangeRate;
        }

        transform.localScale = new Vector3(
            transform.localScale.x,
            transform.localScale.y + heightChange,
            transform.localScale.z);

        transform.rotation = Quaternion.Euler(new Vector3(1f, Random.Range(0f, 360f), 1f));
    }
}
