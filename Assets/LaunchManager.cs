using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LevelDifficult : int
{
    Level_0 = 0,
    Level_1 = 1,
    Level_2 = 2,
    Level_3 = 3,
    Level_4 = 4,
    Level_5 = 5,
    Level_6 = 6,
}

public class LaunchManager : MonoBehaviour
{
    public Transform[] launchPositions;
    private PoimtsManager pointsManager;
    private CollectCouroutine collectCouroutine;
    public List<TypeOfPacket> listPackets;
    public static int numOfSpawnedPacket;

    private GameObject[] casePackets_1;
    private GameObject[] casePackets_2;
    private GameObject[] casePackets_3;


    private void Awake()
    {
        /*arrayPackets = new List<TypeOfPacket>();
        arrayPackets[1] = redPacket;
        arrayPackets[2] = greenPacket;*/

        pointsManager = FindObjectOfType<PoimtsManager>();
        collectCouroutine = FindObjectOfType<CollectCouroutine>();
    }
    private void Start()
    {
        
        casePackets_1 = new GameObject[] { listPackets[1].packetPrefab, listPackets[2].packetPrefab };
        casePackets_2 = new GameObject[] { listPackets[1].packetPrefab, listPackets[2].packetPrefab, listPackets[3].packetPrefab };
        casePackets_3 = new GameObject[] { listPackets[1].packetPrefab, listPackets[2].packetPrefab, listPackets[3].packetPrefab, listPackets[4].packetPrefab };

         SelectDifficult(LevelDifficult.Level_0);
    }

    private void Update()
    {
        //Debug.Log(numOfSpawnedPacket);
    }
    private void SelectDifficult(LevelDifficult difficult)
    {
        switch (difficult)
        {
            case LevelDifficult.Level_0:

                StartCoroutine(LevelDifficult_0(waitPerLaunchMin: 1f, waitPerLaunchMax: 2f));
                break;

            case LevelDifficult.Level_1:

                StartCoroutine(LevelDifficult_1(waitPerLaunchMin:1f, waitPerLaunchMax: 2f));
                break;

            case LevelDifficult.Level_2:

                StartCoroutine(LevelDifficult_2(waitPerLaunchMin : 1f, waitPerLaunchMax: 2f));
                break;

            case LevelDifficult.Level_3:

                StartCoroutine(LevelDifficult_3(waitPerLaunchMin: 1f, waitPerLaunchMax: 2f));
                break;

            case LevelDifficult.Level_4:

                StartCoroutine(LevelDifficult_4(waitPerLaunchMin: 1f, waitPerLaunchMax: 2f));
                break;

            case LevelDifficult.Level_5:

                StartCoroutine(LevelDifficult_5(waitPerLaunchMin: 0f, waitPerLaunchMax: 2f));
                break;

            case LevelDifficult.Level_6:

                StartCoroutine(LevelDifficult_6(waitPerLaunchMin: 0f, waitPerLaunchMax: 2f));
                break;
        }
    }




    IEnumerator LevelDifficult_0(float waitPerLaunchMin, float waitPerLaunchMax)
    {


        yield return new WaitForSeconds(1f);
        for (int i = 0; i < 2; i++)
        {
            StartCoroutine(collectCouroutine.LaunchOneType(listPackets[1].packetPrefab, launchPositions, pointsManager.GetRow(), preLaunchTime: 0f, launchPeriod: 0f, percentToUse: 0f, launchDuration: 10f));
        }

        yield return new WaitForSeconds(5f);
        for (int i = 0; i < 2; i++)
        {
            StartCoroutine(collectCouroutine.LaunchOneType(listPackets[2].packetPrefab, launchPositions, pointsManager.GetRow(), preLaunchTime: 0f, launchPeriod: 0f, percentToUse: 0f, launchDuration: 10f));

        }

        yield return new WaitForSeconds(4f);
        for (int i = 0; i < 2; i++)
        {
            StartCoroutine(collectCouroutine.LaunchOneType(listPackets[3].packetPrefab, launchPositions, pointsManager.GetRow(), preLaunchTime: 0f, launchPeriod: 0f, percentToUse: 0f, launchDuration: 10f));

        }
        yield return new WaitForSeconds(4f);
        for (int i = 0; i < 2; i++)
        {
            StartCoroutine(collectCouroutine.LaunchOneType(listPackets[4].packetPrefab, launchPositions, pointsManager.GetRow(), preLaunchTime: 0f, launchPeriod: 0f, percentToUse: 0f, launchDuration: 10f));

        }
        yield return new WaitForSeconds(4f);
        SelectDifficult(LevelDifficult.Level_1);

        yield return null;
    }

    IEnumerator LevelDifficult_1(float waitPerLaunchMin, float waitPerLaunchMax)
    {

        Debug.Log("Level2Start");
     
            StartCoroutine(collectCouroutine.LaunchOneType(listPackets[1].packetPrefab, launchPositions, pointsManager.GetRow(), preLaunchTime: 0f, launchPeriod: 0f, percentToUse: 1f, launchDuration: 10f));


        yield return new WaitForSeconds(5f);

            StartCoroutine(collectCouroutine.LaunchRandomType(casePackets_2, launchPositions, pointsManager.GetRow(), preLaunchTime: 0f, launchPeriod: 0f, percentToUse: 1f, launchDuration: 10f));

        yield return new WaitForSeconds(6f);

            StartCoroutine(collectCouroutine.LaunchRandomType(casePackets_2, launchPositions, pointsManager.GetRow(), preLaunchTime: 0f, launchPeriod: 0f, percentToUse: 1f, launchDuration: 10f));

        yield return new WaitForSeconds(6f);

            StartCoroutine(collectCouroutine.LaunchRandomType(casePackets_2, launchPositions, pointsManager.GetRow(), preLaunchTime: 0f, launchPeriod: 0f, percentToUse: 1f, launchDuration: 10f));

        yield return new WaitForSeconds(6f);

        SelectDifficult(LevelDifficult.Level_2);

        yield return null;
    }
    
    IEnumerator LevelDifficult_2(float waitPerLaunchMin, float waitPerLaunchMax)
    {
        for (int i = 0; i < 20; i++)
        {
            StartCoroutine(collectCouroutine.LaunchOneType(casePackets_2[Random.Range(0,3)], launchPositions, pointsManager.GetRow(), preLaunchTime: 0f, launchPeriod: 0f, percentToUse: 0f, launchDuration: 10f));
            if (i % 3 == 0 & i != 20 & i != 0) { yield return new WaitForSeconds(4f); }
        }

        yield return new WaitForSeconds(4f);
        SelectDifficult(LevelDifficult.Level_3);

        yield return null;
    }
    
    IEnumerator LevelDifficult_3(float waitPerLaunchMin, float waitPerLaunchMax)
    {
        for (int i = 0; i < 20; i++)
        {
            if (i % 4 == 0 & i!=0)
            {
                StartCoroutine(collectCouroutine.LaunchOneType(listPackets[0].packetPrefab, launchPositions, pointsManager.GetRow(), preLaunchTime:0, launchPeriod:0, percentToUse: 0f, launchDuration: 10f));
            }
            
            StartCoroutine(collectCouroutine.LaunchOneType(casePackets_3[Random.Range(0,4)], launchPositions, pointsManager.GetRow(), preLaunchTime: 0, launchPeriod: 0, percentToUse: 0f, launchDuration: 10f));

            if (i % 3 == 0 & i != 20 & i!=0) { yield return new WaitForSeconds(5f); }
        }

        yield return new WaitForSeconds(4f);
        SelectDifficult(LevelDifficult.Level_4);

        yield return null;
    }    

    IEnumerator LevelDifficult_4(float waitPerLaunchMin, float waitPerLaunchMax)
    {
        for (int i = 0; i < 20; i++)
        {
            if (i % 4 == 0 & i!= 0)
            {
                StartCoroutine(collectCouroutine.LaunchOneType(listPackets[0].packetPrefab, launchPositions, pointsManager.GetRow(), preLaunchTime: 0f, launchPeriod: 0f, percentToUse: 0f, launchDuration: 10f));
            }

            StartCoroutine(collectCouroutine.LaunchOneType(casePackets_3[Random.Range(0, 4)], launchPositions, pointsManager.GetRow(), preLaunchTime: 0, launchPeriod: 0, percentToUse: 0f, launchDuration: 10f));

            if (i % 3 == 0 & i != 20 & i != 0) { yield return new WaitForSeconds(4f); }
        }

        yield return new WaitForSeconds(1f);
        SelectDifficult(LevelDifficult.Level_5);

        yield return null;
    }

    IEnumerator LevelDifficult_5(float waitPerLaunchMin, float waitPerLaunchMax)
    {
        for (int i = 0; i < 30; i++)
        {
            if (i % 4 == 0 & i != 0)
            {
                StartCoroutine(collectCouroutine.LaunchOneType(listPackets[0].packetPrefab, launchPositions, pointsManager.GetRow(), preLaunchTime: 0f, launchPeriod: 0f, percentToUse: 0f, launchDuration: 10f));
            }

            StartCoroutine(collectCouroutine.LaunchRandomType(casePackets_3, launchPositions, pointsManager.GetRow(), preLaunchTime: 0, launchPeriod: 0, percentToUse: 0f, launchDuration: 10f));

            if (i % 4 == 0 & i != 30 & i != 0) { yield return new WaitForSeconds(6f); }
        }

        yield return new WaitForSeconds(4f);
        SelectDifficult(LevelDifficult.Level_6);


        yield return null;
    }

    IEnumerator LevelDifficult_6(float waitPerLaunchMin, float waitPerLaunchMax)
    {
        for (int i = 0; i < 40; i++)
        {
            if (i % 4 == 0 & i != 0)
            {
                StartCoroutine(collectCouroutine.LaunchOneType(listPackets[0].packetPrefab, launchPositions, pointsManager.GetRow(), preLaunchTime: 0f, launchPeriod: 0f, percentToUse: 0f, launchDuration: 10f));
            }

            StartCoroutine(collectCouroutine.LaunchRandomType(casePackets_3, launchPositions, pointsManager.GetRow(), preLaunchTime: 0, launchPeriod: 0, percentToUse: 0f, launchDuration: 10f));

            if (i % 4 == 0 & i != 30 & i != 0) { yield return new WaitForSeconds(5f); }
        }

        yield return new WaitForSeconds(4f);
        SelectDifficult(LevelDifficult.Level_0);

        yield return null;
    }

    
}
