using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prefabs : MonoBehaviour
{
    public static GameObject stemObject;
    public static GameObject leafObject;
    public static GameObject fireObject;

    [SerializeField]
    private GameObject stemPrefab;
    [SerializeField]
    private GameObject leafPrefab;
    [SerializeField]
    private GameObject firePrefab;

    private void Awake()
    {
        stemObject = stemPrefab;
        leafObject = leafPrefab;
        fireObject = firePrefab;
    }
}
