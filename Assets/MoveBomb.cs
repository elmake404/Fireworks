using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBomb : MonoBehaviour
{
    public CollectCouroutine launch;
    public GameObject bomb;
    public List<Transform> launchPlaces = new List<Transform>();
    private Vector3 targetPos;
    //private int numberOfArray;


    private void Start()
    {
        //numberOfArray = LocatePlayerArea.originalPosGrid.Count-1;
        //targetPos = LocatePlayerArea.GetRandomPos();
        //Debug.Log(numberOfArray);
        launch.missleObject = bomb;
        launch.startPos = launchPlaces[0].transform.position;
        //StartCoroutine( SpawnPeriodic());
        //Debug.Log( LocatePlayerArea.originalPosGrid.Count);


        //AssetBundle.Instantiate(launch)

    }

    /*IEnumerator SpawnPeriodic()
    {
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < 5; i++)
        {

            //IEnumerator newLaunch = AssetBundle.Instantiate(launch).Launch_1();
            IEnumerator ddsfvbdf = launch.Launch_1();
            StartCoroutine(ddsfvbdf);


            yield return new WaitForSeconds(1f);
        }
        yield return null;
    }*/



}
