using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectBeams : MonoBehaviour
{
    private LineRenderer lines;
    private Camera sceneCamera;
    private HashSet<GameObject> selectedObjectsHash;
    private List<GameObject> selectedObjectsList;
    private GameObject onMouseGameObject;
    
    //private bool isFreePoint;
    
    void Start()
    {
        //GameObject.CreatePrimitive(PrimitiveType.Cube);
        GameObject obj = new GameObject();
        onMouseGameObject = obj;

        selectedObjectsList = new List<GameObject>();
        selectedObjectsHash = new HashSet<GameObject>();
        lines = this.GetComponent<LineRenderer>();
        lines.positionCount = 0;
        sceneCamera = FindObjectOfType<Camera>();
        Debug.Log(onMouseGameObject.name);
    }

    void Update()
    {
        

        if (Input.GetMouseButtonDown(0))
        {


        }

        if (Input.GetMouseButton(0))
        {
            RayToOnjects();

        }

        if (Input.GetMouseButtonUp(0))
        {
            lines.positionCount = 0;
            selectedObjectsHash.Clear();
            selectedObjectsList.Clear();
            

        }


        if (lines.positionCount != 0)
        {
            for (int i = 0; i < selectedObjectsList.Count; i++)
            {
                lines.SetPosition(i, selectedObjectsList[i].transform.position);
            }
        }
        /*Debug.Log(selectedObjectsHash.Count);
        Debug.Log(selectedObjectsList.Count);
        Debug.Log(lines.positionCount);*/

    }
    private void RayToOnjects()
    {
        Ray ray = sceneCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (selectedObjectsHash.Add(hit.transform.gameObject))
            {
                if (selectedObjectsList.Find(obj => obj.GetHashCode() == onMouseGameObject.GetHashCode()) != null)
                {
                    lines.positionCount -= 1;
                    selectedObjectsList.Remove(onMouseGameObject);
                }

                lines.positionCount = selectedObjectsHash.Count;
                selectedObjectsList.Add(hit.transform.gameObject);
               
            }

        }
        else
        {
            Vector3 pointOutTarget = sceneCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, sceneCamera.nearClipPlane - sceneCamera.transform.position.z));
            onMouseGameObject.transform.position = pointOutTarget;
            if (selectedObjectsList.Find(obj => obj.GetHashCode() == onMouseGameObject.GetHashCode()) == null)
            {
                lines.positionCount += 1;
                selectedObjectsList.Add(onMouseGameObject);
            }
            

        }
    }
}
