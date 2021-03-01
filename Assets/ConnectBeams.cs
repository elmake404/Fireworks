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
    public AnimationCurve pointDistributionGraph;
    //private List<Vector3> pointsToRender;
    private int smoothPoint = 4;
    
    //private bool isFreePoint;
    
    void Start()
    {
        GameObject obj = new GameObject();
        onMouseGameObject = obj;

        selectedObjectsList = new List<GameObject>();
        selectedObjectsHash = new HashSet<GameObject>();
        lines = this.GetComponent<LineRenderer>();
        lines.positionCount = 0;
        sceneCamera = FindObjectOfType<Camera>();
    }

    void Update()
    {
        List<Vector3> pointsToRender = new List<Vector3>();

        if (Input.GetMouseButtonDown(0))
        {
        }

        if (Input.GetMouseButton(0))
        {
            RayToOnjects();
            pointsToRender = InterpolatedCurve();
        }

        if (Input.GetMouseButtonUp(0))
        {
            lines.positionCount = 0;
            selectedObjectsHash.Clear();
            selectedObjectsList.Clear();
        }

        
        if (pointsToRender != null)
        {
            lines.positionCount = pointsToRender.Count;
            for (int i = 0; i < pointsToRender.Count; i++)
            {
                lines.SetPosition(i, pointsToRender[i]);
            }
        }

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
                    selectedObjectsList.Remove(onMouseGameObject);
                }

                selectedObjectsList.Add(hit.transform.gameObject);
            }
        }
        else
        {
            Vector3 pointOutTarget = sceneCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, sceneCamera.nearClipPlane - sceneCamera.transform.position.z));
            onMouseGameObject.transform.position = pointOutTarget;
            if (selectedObjectsList.Find(obj => obj.GetHashCode() == onMouseGameObject.GetHashCode()) == null)
            {
                selectedObjectsList.Add(onMouseGameObject);
            }
        }
    }

    private List<Vector3> InterpolatedCurve()
    {
        List<Vector3> originalPoints = new List<Vector3>();
        List<Vector3> pointsToRender = new List<Vector3>();
        if (selectedObjectsList.Count >= 2)
        {
            foreach (GameObject currentObj in selectedObjectsList)
            {
                originalPoints.Add(currentObj.transform.position);
            }
        }
        else
        {
            return null;
        }

        for (int currPoint = 0; currPoint < originalPoints.Count; currPoint++)
        {
            float t = 0.0f;
            for (int currCurvedPoint = 0; currCurvedPoint < smoothPoint; currCurvedPoint++)
            {
                if (originalPoints.Count - 1 < currPoint + 1)
                {
                    continue;
                }
                else
                {
                    t = Mathf.InverseLerp(0, smoothPoint - 1, currCurvedPoint);
                    float tEvaluate = pointDistributionGraph.Evaluate(t);
                    Vector3 curvedPoint = (1 - tEvaluate) * originalPoints[currPoint] + tEvaluate * originalPoints[currPoint + 1];
                    pointsToRender.Add(curvedPoint);
                }
            }
            
        }
        return pointsToRender;
    }

    /*private void OnDrawGizmos()
    {
        List<Vector3> pointsToRender = new List<Vector3>();
        pointsToRender = InterpolatedCurve();
        if (pointsToRender != null)
        {
            
            for (int i = 0; i < pointsToRender.Count; i++)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(pointsToRender[i],0.4f);
                
            }
        }
    }*/
    
}
    
