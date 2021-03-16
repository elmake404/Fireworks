using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CollectCouroutine : MonoBehaviour
{
    //public float launchTime;
    public float fallTime;
    public AnimationCurve speedAtTime;
    public AnimationCurve speedAtFall;

    public IEnumerator LaunchOneType(GameObject gameObj,Transform[] launchPos, Vector3[] targetPos, float launchDuration)
    {
        
        for (int i = 0; i < 4; i++)
        {

            StartCoroutine(PerLaunch(gameObj,launchPos[i].transform.position, targetPos[i], launchDuration));
            yield return new WaitForSeconds(0.5f);
        }
        yield return null;
    }

    public IEnumerator LaunchRandomType(GameObject[] gameObj, Transform[] launchPos, Vector3[] targetPos,  float launchDuration)
    {
        for (int i = 0; i < 4; i++)
        {

            StartCoroutine(PerLaunch(gameObj[i], launchPos[i].transform.position, targetPos[i], launchDuration));
            yield return new WaitForSeconds(0.5f);
        }


        yield return null;
    }

    private IEnumerator PerLaunch(GameObject gameObj, Vector3 launchPos, Vector3 currentTarget, float launchDuration)
    {
        
        gameObj = Instantiate(gameObj, launchPos, Quaternion.identity);
        for (float i = 0f; i < launchDuration; i += 1f * Time.deltaTime)
        {
            
            if (IsReferenceNull(gameObj) == true) break;
            Vector3 move = Vector3.Lerp(launchPos, currentTarget, speedAtTime.Evaluate(i / launchDuration));
            gameObj.transform.position = move;
            yield return new WaitForEndOfFrame();
        }
        if (IsReferenceNull(gameObj) == false)
        {
            StartCoroutine(AfterLaunchFall(gameObj));
        }
        yield return null;
    }

    private IEnumerator AfterLaunchFall(GameObject gameObj)
    {
        
        if (IsReferenceNull(gameObj) == false)
        {
            StartCoroutine(gameObj.GetComponentInChildren<InfoPacket>().DyingFlare(fallTime));
        }
        
        Vector3 fallStartPos = gameObj.transform.position;
        Vector3 targetPos = new Vector3(fallStartPos.x, fallStartPos.y - 2f, fallStartPos.z);

        for (float i = 0f; i < fallTime; i += 1f * Time.deltaTime)
        {
            if (IsReferenceNull(gameObj) == true) break;
            Vector3 move = Vector3.Lerp(fallStartPos, targetPos, speedAtFall.Evaluate(i / fallTime));
            gameObj.transform.position = move;
            yield return new WaitForEndOfFrame();
        }
        yield return null;
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

    private bool IsReferenceNull(GameObject obj)
    {
        if (obj == null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
