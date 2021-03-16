
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public enum LaunchPatterns : int
{
    One_1 = 0,
    One_2 = 1,
    One_3 = 2,
    One_4 = 3,
    Random = 4,
    Random_plus_0 = 5,

}

public class LaunchManager : MonoBehaviour
{
    public Transform[] launchPositions;
    private PoimtsManager pointsManager;
    private CollectCouroutine collectCouroutine;
    public List<TypeOfPacket> listPackets;
    public static int numOfSpawnedPacket;

    private int[] RandomChance;
    private int[] Chanches;
    private int[] unEditChanches;
    public int initialPercentOne_1;
    public int initialPercentOne_2;
    public int initialPercentOne_3;
    public int initialPercentOne_4;
    public int initialPercentRandom;
    public int initialPercentRandom_plus_One;
    [HideInInspector] public bool isGameRunning;
    private int nextLaunchCounter;
    private float defaultTime = 5f;
    private GameObject[] casePackets;
        

    private void Awake()
    {


        pointsManager = FindObjectOfType<PoimtsManager>();
        collectCouroutine = FindObjectOfType<CollectCouroutine>();
    }
    private void Start()
    {
        isGameRunning = true;
        nextLaunchCounter = 0;
        Chanches = new int[] { initialPercentOne_1, initialPercentOne_2, initialPercentOne_3, initialPercentOne_4, initialPercentRandom, initialPercentRandom_plus_One };
        unEditChanches = new int[] { initialPercentOne_1, initialPercentOne_2, initialPercentOne_3, initialPercentOne_4, initialPercentRandom, initialPercentRandom_plus_One };
        RandomChance = new int[100];

        RewriteRandomArray(RandomChance, Chanches);


        casePackets = new GameObject[] { listPackets[1].packetPrefab, listPackets[2].packetPrefab, listPackets[3].packetPrefab, listPackets[4].packetPrefab };

        


        StartCoroutine(PreGameStart());
        
    }


    private void SelectDifficult(LaunchPatterns difficult)
    {
        switch (difficult)
        {
            case LaunchPatterns.One_1:

                StartCoroutine(collectCouroutine.LaunchOneType(listPackets[1].packetPrefab, launchPositions, pointsManager.GetRow(), 10f));

                break;

            case LaunchPatterns.One_2:

                StartCoroutine(collectCouroutine.LaunchOneType(listPackets[2].packetPrefab, launchPositions, pointsManager.GetRow(), 10f));
                break;

            case LaunchPatterns.One_3:

                StartCoroutine(collectCouroutine.LaunchOneType(listPackets[3].packetPrefab, launchPositions, pointsManager.GetRow(), 10f));
                break;

            case LaunchPatterns.One_4:

                StartCoroutine(collectCouroutine.LaunchOneType(listPackets[4].packetPrefab, launchPositions, pointsManager.GetRow(), 10f));
                break;

            case LaunchPatterns.Random:

                StartCoroutine(collectCouroutine.LaunchRandomType(GetRandomFourPackets(casePackets), launchPositions, pointsManager.GetRow(), 10f));
                break;

            case LaunchPatterns.Random_plus_0:

                StartCoroutine(collectCouroutine.LaunchRandomType(GetRandomFourPackets(casePackets, listPackets[0].packetPrefab), launchPositions, pointsManager.GetRow(), 10f));
                break;

        }
    }


    private GameObject[] GetRandomFourPackets(GameObject[] caseObjects, GameObject rainbowObject = null)
    {
        GameObject[] copyCaseObjects = caseObjects;
        
        int randomIndexInCase = Random.Range(0, copyCaseObjects.Length - 1);
        int randomIndex = randomIndexInCase;
        
        if (rainbowObject != null)
        {
            while (randomIndex != randomIndexInCase)
            {
                randomIndex = Random.Range(0, 4);
            }
            
        }
        GameObject[] outGameobjects = new GameObject[4];

        for (int i = 0; i < 4; i++)
        {
            if (rainbowObject != null & i == randomIndex)
            {
                outGameobjects[i] = rainbowObject;
            }
            else
            {

                
                outGameobjects[i] = copyCaseObjects[Random.Range(0,copyCaseObjects.Length-1)];
                copyCaseObjects = DeleteAnotherElementArray(randomIndexInCase, copyCaseObjects);
            }

        }

        return outGameobjects;
    }

    private void RewriteRandomArray(int[] array, int[] ArrayChanches)
    {

        int step = 0;
        for (int i = 0; i < ArrayChanches.Length; i++)
        {
            for (int j = 0; j < ArrayChanches[i]; j++)
            {

                array[step] = i;
                step++;


            }
        }


    }

    private void ChangeChanceProportion(int usedIndexChance, int[] ArrayChanches, int step = 1)
    {
        int calcNewValue = ArrayChanches[usedIndexChance] - (ArrayChanches.Length - 1) * step;
        ArrayChanches[usedIndexChance] = Mathf.Clamp(calcNewValue, 0, 100);

        int numPlus = ArrayChanches.Length - 1;

        if (calcNewValue < 0)
        {
            numPlus = ArrayChanches.Length - Mathf.Abs(calcNewValue) - 1;
        }


        string text = "";
        for (int i = 0; i < ArrayChanches.Length; i++)
        {
            if (i == usedIndexChance)
            {
                continue;
            }
            else
            {
                if (numPlus == 0) { break; }
                ArrayChanches[i] += step;
                numPlus--;
            }

            text += " " + ArrayChanches[i];
        }

    }

    private int GetIndexInRandom()
    {
        int random = Random.Range(0, 99);
        int selectRandomInArray = RandomChance[random];
        ChangeChanceProportion(selectRandomInArray, Chanches);
        RewriteRandomArray(RandomChance, Chanches);
        return selectRandomInArray;
    }


    private IEnumerator PreGameStart()
    {
        yield return new WaitForSeconds(3f);
        SelectDifficult(LaunchPatterns.One_1);

        yield return new WaitForSeconds(6f);
        SelectDifficult(LaunchPatterns.One_1);

        yield return new WaitForSeconds(5f);
        SelectDifficult(LaunchPatterns.One_2);

        yield return new WaitForSeconds(4f);
        SelectDifficult(LaunchPatterns.One_3);

        yield return new WaitForSeconds(4f);
        SelectDifficult(LaunchPatterns.One_4);

        yield return new WaitForSeconds(4f);
        SelectDifficult(LaunchPatterns.Random);

        yield return new WaitForSeconds(3f);
        SelectDifficult(LaunchPatterns.Random_plus_0);

        StartCoroutine(GameStart());

        yield return null;
    }

    private IEnumerator GameStart()
    {
        yield return new WaitForSeconds(7f);
        int counterToResetRandom = 0;
        while (isGameRunning)
        {
            if (counterToResetRandom > 15)
            {
                counterToResetRandom = 0;
                RewriteRandomArray(RandomChance, unEditChanches);
            }
            SelectDifficult((LaunchPatterns)GetIndexInRandom());
            yield return new WaitForSeconds(GetTimeToNextLaunch());
            //Debug.Log(counterToResetRandom);
            counterToResetRandom ++;
        }
        yield return null;
    }

    private float GetTimeToNextLaunch()
    {
        
        if (nextLaunchCounter > 3)
        {
            defaultTime -= 0.3f;
            nextLaunchCounter = 0;
        }
        defaultTime = Mathf.Clamp(defaultTime, 2f, 99f);
        nextLaunchCounter++;
        return defaultTime;
    }
    private GameObject[] DeleteAnotherElementArray(int usedIndexElement, GameObject[] elements)
    {
        
        if (elements.Length == 2)
        {
            return elements;
        }
        else
        {
            GameObject[] newElements = new GameObject[elements.Length - 1];
            int indexToCopy = 0;

            for (int i = 0; i < newElements.Length; i++)
            {
                if (i == usedIndexElement)
                {
                    indexToCopy++;
                }

                newElements[i] = elements[indexToCopy];
                indexToCopy++;
            }
            newElements[Random.Range(0, newElements.Length - 1)] = elements[usedIndexElement];

            return newElements;
        }
        
    }
}
