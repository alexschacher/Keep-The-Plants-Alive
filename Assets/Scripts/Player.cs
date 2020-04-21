using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float distanceToPlane;
    Ray pointRay;
    private Plane plane = new Plane(Vector3.up, 0);

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            Application.Quit();
        }

        if (!GameManager._instance.canPlant || GameManager._instance.seedsAmount <= 0) return;

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 clickPos = GetClickPosition();
            if (Vector3.Distance(clickPos, Vector3.zero) < 50)
            {
                AudioManager.PlayPlant();
                GameManager.ModifyPlantCount(1);
                GameManager.ModifySeedAmount(-1);
                GameObject stem = Instantiate(Prefabs.stemObject, clickPos, Quaternion.identity);
                stem.GetComponent<Stem>().SetLifespan(Random.Range(3, 7));
            }
        }
    }

    private Vector3 GetClickPosition()
    {
        pointRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        plane.Raycast(pointRay, out distanceToPlane);

        return new Vector3(
            pointRay.GetPoint(distanceToPlane).x,
            pointRay.GetPoint(distanceToPlane).y,
            pointRay.GetPoint(distanceToPlane).z);
    }
}