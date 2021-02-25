using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBomb : MonoBehaviour
{
    public List<Transform> launchPlaces = new List<Transform>();
    private Vector3 targetPos;


    private void Start()
    {
        targetPos = LocatePlayerArea.GetRandomPos();
        Debug.Log(targetPos);
        this.transform.position = launchPlaces[0].transform.position;
        //this.transform.position = targetPos;
    }

    private void Update()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, targetPos, Time.deltaTime);

    }

    private void RandomDestination()
    { 
        
    }

    /*IEnumerator Move()
    {
        yield return new WaitForSeconds(1f);
        Vector3 targetPos = LocatePlayerArea.GetRandomPos();
        while (true)
        {
            Vector3.Lerp(this.transform.position, targetPos, 0.1f);
            if (this.transform.position == targetPos)
            {
                break;  
            }
        }
        yield return null;
    }*/


}
