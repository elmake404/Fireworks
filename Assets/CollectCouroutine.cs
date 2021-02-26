using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Launch 1", menuName = "Couroutine Launch 1")]
public class CollectCouroutine : ScriptableObject
{
    public float launchTime;
    public Vector3 startPos;
    public GameObject missleObject;
    public IEnumerator Launch_1()
    {
        GameObject newInstance = Instantiate(missleObject, startPos, Quaternion.identity);
        Vector3 targetPos = LocatePlayerArea.GetRandomPos();
        for (float i = 0f; i < launchTime; i += 5f * Time.deltaTime)
        {
            newInstance.transform.position = Vector3.MoveTowards(newInstance.transform.position, targetPos, i / 200f);
            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }
}
