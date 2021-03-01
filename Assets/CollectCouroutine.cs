using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Launch 1", menuName = "Couroutine Launch 1")]
public class CollectCouroutine : ScriptableObject
{
    public float launchTime;
    public AnimationCurve speedAtTime;
    public Vector3 startPos;
    public GameObject missleObject;

   /* public IEnumerator Launch_1()
    {
        GameObject newInstance = Instantiate(missleObject, startPos, Quaternion.identity);
        Vector3 targetPos = LocatePlayerArea.GetRandomPos();
        for (float i = 0f; i < launchTime; i += 1f)
        {
            newInstance.transform.position = Vector3.Lerp(newInstance.transform.position, targetPos, speedAtTime.Evaluate(i/launchTime));
            yield return new WaitForFixedUpdate();
        }

        yield return null;
    }*/
}
