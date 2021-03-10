using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject pointOfInterst;
    private Vector3 targetPos;

    void Start()
    {
        InvokeRepeating("RandomTarget", 1f, 3f);
    }


    private void LateUpdate()
    {

        Vector3 lerPos = Vector3.MoveTowards(this.transform.position, targetPos, 2f*Time.deltaTime);
        lerPos.z = -10f;
        this.transform.position = lerPos;
        this.transform.LookAt(pointOfInterst.transform);

    }

    private void RandomTarget()
    {
        //Debug.Log("Time");
        targetPos = new Vector3(Random.insideUnitSphere.x * 3f, Random.insideUnitSphere.y * 5f, 0f);
    }
}
