using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stem : MonoBehaviour
{
    private float heightGrowthRate = 30;
    private float thicknessGrowthRate = 30;
    private float heightGrowthInhibitor = 10;
    private float thicknessGrowthInhibitor = 100;

    private float nodeGrowthTimer;
    private float nodeGrowthGoal;
    private float nodeGrowthMin = 0.4f;
    private float nodeGrowthMax = 1f;

    private float stemAngleMin = -50f;
    private float stemAngleMax = 50f;

    private GameObject fire;
    public bool isOnFire;
    private float minBurnTime = 4;
    private float maxBurnTime = 10;
    public float burnTimer;
    public float burnEndTime;

    private float age;
    private float lifeSpan;
    private bool growing = true;

    private List<GameObject> nodes = new List<GameObject>();

    private void Awake()
    {
        nodeGrowthGoal = Random.Range(nodeGrowthMin, nodeGrowthMax);
    }

    void Update()
    {
        if (growing)
        {
            IncreaseScale();
            AdvanceNodeTimer();
            Age();
        }

        if (isOnFire)
        {
            Burn();
        }
    }

    private void IncreaseScale()
    {
        transform.localScale = new Vector3(
            transform.localScale.x + (thicknessGrowthRate   / thicknessGrowthInhibitor  * (1 - (age / lifeSpan)) * Time.deltaTime),
            transform.localScale.y + (heightGrowthRate      / heightGrowthInhibitor     * (1 - (age / lifeSpan)) * Time.deltaTime),
            transform.localScale.z + (thicknessGrowthRate   / thicknessGrowthInhibitor  * (1 - (age / lifeSpan)) * Time.deltaTime));
    }

    private void AdvanceNodeTimer()
    {
        nodeGrowthTimer += Time.deltaTime;
        if (nodeGrowthTimer >= nodeGrowthGoal)
        {
            CreateNode();
            nodeGrowthTimer = 0;
            nodeGrowthGoal = Random.Range(nodeGrowthMin, nodeGrowthMax);
        }
    }

    private void Age()
    {
        age += Time.deltaTime;
        if (age >= lifeSpan)
        {
            growing = false;
        }
    }

    private void Burn()
    {
        burnTimer += Time.deltaTime;
        if (burnTimer > burnEndTime)
        {
            Destroy(gameObject);
            AudioManager.PlayDie();
        }
    }

    private void CreateNode()
    {
        Vector3 addDistanceToDirection = transform.rotation * transform.InverseTransformDirection(transform.up) * (transform.localScale.y * 2);

        Vector3 node = transform.position + addDistanceToDirection;

        GameObject obj;

        if (Random.Range(0f, 1f) < 0.3f)
        {
            obj = CreateStem(node);
        }
        else
        {
            obj = CreateLeaf(node);
        }

        nodes.Add(obj);
    }

    private GameObject CreateStem(Vector3 pos)
    {
        Quaternion rotation = Quaternion.Euler(new Vector3(
            transform.rotation.x + Random.Range(stemAngleMin, stemAngleMax),
            transform.rotation.y,
            transform.rotation.z + Random.Range(stemAngleMin, stemAngleMax)
            ));

        GameObject stem = Instantiate(Prefabs.stemObject, pos, rotation);

        stem.transform.localScale = new Vector3(
            transform.localScale.x / 2,
            0.01f,
            transform.localScale.z / 2
            );

        stem.GetComponent<Stem>().SetLifespan(Random.Range(lifeSpan / 4, lifeSpan / 3 * 2));

        return stem;
    }

    private GameObject CreateLeaf(Vector3 pos)
    {
        Quaternion rotation = Quaternion.Euler(new Vector3(
            0f,
            Random.Range(0f, 360f),
            0f
            ));

        GameObject leaf = Instantiate(Prefabs.leafObject, pos, rotation);

        leaf.transform.localScale = new Vector3(
            0.01f,
            0.01f,
            0.01f
            );

        return leaf;
    }

    public void SetLifespan(float lifespan)
    {
        this.lifeSpan = lifespan;
    }

    private void OnDestroy()
    {
        if (fire != null)
        {
            Destroy(fire);
            GameManager.ModifyFireCount(-1);
            GameManager.ModifyPlantCount(-1);
        }
        
        foreach (GameObject obj in nodes)
        {
            Destroy(obj);
        }
    }

    public void LightOnFire(GameObject fire)
    {
        isOnFire = true;
        this.fire = fire;
        burnEndTime = Random.Range(minBurnTime, maxBurnTime);
    }
}