using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private float minZoom = -60f;
    private float maxZoom = -110f;

    private float scrollSensitivity = 75f;
    private float mouseDragRotateSensitivity = 0.2f;
    private float mouseDragZoomSensitivity = 1f;
    private Vector3 prevMousePos;

    [SerializeField]
    private GameObject cam;

    void Update()
    {
        Scroll();
        RightClickDrag();
        SavePrevMousePos();
    }

    private void Scroll()
    {
        Rotate(Input.GetAxis("Mouse ScrollWheel") * scrollSensitivity);
    }

    private void RightClickDrag()
    {
        if (Input.GetMouseButton(1))
        {
            Rotate((Input.mousePosition.x - prevMousePos.x) * mouseDragRotateSensitivity);
        }
    }

    private void SavePrevMousePos()
    {
        prevMousePos = Input.mousePosition;
    }

    private void Rotate(float amount)
    {
        transform.rotation = Quaternion.Euler(new Vector3(
            transform.eulerAngles.x,
            transform.eulerAngles.y + amount,
            transform.eulerAngles.z
            ));
    }

    private void Zoom(float amount)
    {
        float zoomPos = cam.transform.localPosition.z + amount;

        if (zoomPos < minZoom && zoomPos > maxZoom)
        {
            cam.transform.localPosition = new Vector3(
                cam.transform.localPosition.x,
                cam.transform.localPosition.y,
                zoomPos
                );
        }
    }
}
