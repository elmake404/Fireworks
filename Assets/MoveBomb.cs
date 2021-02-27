using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBomb : MonoBehaviour
{
    public CollectCouroutine launch;
    public GameObject bomb;
    public List<Transform> launchPlaces = new List<Transform>();
    private Vector3 targetPos;
    private int numberOfArray;


    private void Start()
    {
        numberOfArray = LocatePlayerArea.originalPosGrid.Count;
        targetPos = LocatePlayerArea.GetRandomPos();
        Debug.Log(numberOfArray);
        launch.missleObject = bomb;
        launch.startPos = launchPlaces[0].transform.position;
        StartCoroutine( SpawnPeriodic());
        //Debug.Log( LocatePlayerArea.originalPosGrid.Count);
        
        
    }

    IEnumerator SpawnPeriodic()
    {
        for (int i = 0; i < numberOfArray-1; i++)
        {
            IEnumerator newLaunch = launch.Launch_1();
            StartCoroutine(newLaunch);
            yield return new WaitForSeconds(0.2f);
        }
        yield return null;
    }



}
