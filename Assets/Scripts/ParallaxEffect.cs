using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    public float effectParallax;

    private Transform camara;
    private Vector3 lastCameraPosition;

    private void Start()
    {
        camara = Camera.main.transform;
        lastCameraPosition = camara.position;
    }

    private void LateUpdate()
    {
        Vector3 backgroundmove = camara.position - lastCameraPosition;
        transform.position += new Vector3(backgroundmove.x * effectParallax, backgroundmove.y, 0);
        lastCameraPosition=camara.position;
    }
}
