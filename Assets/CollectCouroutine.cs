using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CollectCouroutine : MonoBehaviour
{
    public float launchTime;
    public float fallTime;
    public AnimationCurve speedAtTime;
    public AnimationCurve speedAtFall;

    public IEnumerator LaunchOneType(GameObject gameObj,Transform[] launchPos, Vector3[] targetPos, float preLaunchTime, float launchPeriod, float percentToUse)
    {
        int clampedIndex = Mathf.Clamp(Mathf.RoundToInt(Mathf.Lerp(0, targetPos.Length, percentToUse)), 1, targetPos.Length);
        int[] randomIndexes = GetRandomIndexes(targetPos.Length);

        yield return new WaitForSeconds(preLaunchTime);
        
        for (int i = 0; i < clampedIndex; i++)
        {
            
            StartCoroutine(PerLaunch(gameObj,launchPos[0].transform.position, targetPos[randomIndexes[i]]));
            
            yield return new WaitForSeconds(launchPeriod);
        }
        yield return null;
    }

    private IEnumerator PerLaunch(GameObject gameObj, Vector3 launchPos, Vector3 currentTarget)
    {
        gameObj = GameObject.Instantiate(gameObj);
        for (float i = 0f; i < launchTime; i += 1f * Time.deltaTime)
        {
            Vector3 move = Vector3.Lerp(launchPos, currentTarget, speedAtTime.Evaluate(i / launchTime));
            gameObj.transform.position = move;
            yield return new WaitForEndOfFrame();
        }
        StartCoroutine(AfterPerLaunch(gameObj, gameObj.transform.position));
        yield return null;
    }

    private IEnumerator AfterPerLaunch(GameObject gameObj, Vector3 launchPos)
    {
        yield return new WaitForSeconds(0.5f);
        Vector3 currentTarget = launchPos;
        currentTarget.y -= 10f;
        for (float i = 0f; i < launchTime; i += 1f * Time.deltaTime)
        {
            Vector3 move = Vector3.Lerp(launchPos, currentTarget, speedAtFall.Evaluate(i / launchTime));
            gameObj.transform.position = move;
            yield return new WaitForEndOfFrame();
        }
    }

    private int[] GetRandomIndexes(int numIndexes)
    {
        int[] newRandomIndexes = new int[numIndexes];

        for (int i = 0; i < numIndexes; i++)
        {
            newRandomIndexes[i] = i;
        }

        for (int i = numIndexes - 1; i >= 1; i--)
        {
            int j = Random.Range(0, i + 1);
            var temp = newRandomIndexes[j];
            newRandomIndexes[j] = newRandomIndexes[i];
            newRandomIndexes[i] = temp;
        }

        return newRandomIndexes;
    }
}
