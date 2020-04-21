using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    private Collider col;
    public Stem stem;

    private void Start()
    {
        AudioManager.PlayBurn();
        col = GetComponent<SphereCollider>();
    }

    private void Spread(GameObject stemObject)
    {
        stem = stemObject.GetComponent<Stem>();
        if (!stem.isOnFire)
        {
            GameManager.ModifyFireCount(1);
            GameObject fire = Instantiate(Prefabs.fireObject, stem.transform.position, Quaternion.identity);
            fire.GetComponent<Fire>().stem = stem;
            stem.LightOnFire(fire);
        }
    }

    private void Update()
    {
        transform.localScale = new Vector3(
            1.5f - (stem.burnTimer / stem.burnEndTime),
            1.5f - (stem.burnTimer / stem.burnEndTime),
            1.5f - (stem.burnTimer / stem.burnEndTime)
            );
    }

    private void OnTriggerStay(Collider other)
    {
        if (Random.Range(0f, Vector3.Distance(transform.position, other.transform.position)) < 0.1f)
        {
            if (other.GetComponent<Stem>() != null)
            {
                if (other.transform.position.y == 0)
                {
                    Spread(other.gameObject);
                }
            }
        }
    }
}
