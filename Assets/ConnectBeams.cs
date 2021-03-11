using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//sing UnityEngine.UIElements;
using UnityEngine.UI;

public class ConnectBeams : MonoBehaviour
{
    private LineRenderer lines;
    private Camera sceneCamera;
    [HideInInspector]public HashSet<GameObject> selectedObjectsHash;
    //private HashSet<GameObject> selectedSwitchers;
    [HideInInspector]public List<GameObject> selectedObjectsList;
    private List<GameObject> selectedCircles;
    private List<GameObject> circlesInGridList;
    public GameObject circle;
    public Transform Canvas;
    public CanvasManager canvasManager;
    public GameObject gridElements;
    public GameObject circleOnGrid;
    private GridLayoutGroup gridLayout;
    private RectTransform gridRectTransform;
    private GameObject onMouseGameObject;
    public AnimationCurve pointDistributionGraph;
    public GameObject textBonusAfterDestroy;
    //private List<Vector3> pointsToRender;
    private int smoothPoint = 4;
    private int enterColorCode;
    private Vector2 offsetGridElements;
    
    //private bool isFreePoint;
    
    void Start()
    {
        enterColorCode = -1;
        
        GameObject obj = new GameObject();
        onMouseGameObject = obj;

        selectedObjectsList = new List<GameObject>();
        selectedObjectsHash = new HashSet<GameObject>();
        selectedCircles = new List<GameObject>();
        circlesInGridList = new List<GameObject>();
        //selectedSwitchers = new HashSet<GameObject>();
        lines = this.GetComponent<LineRenderer>();
        lines.positionCount = 0;
        sceneCamera = FindObjectOfType<Camera>();

        gridLayout = gridElements.GetComponent<GridLayoutGroup>();
        canvasManager = Canvas.GetComponent<CanvasManager>();
        float center = Screen.width/2f ;
        gridRectTransform = gridElements.GetComponent<RectTransform>();
        offsetGridElements = new Vector2(-gridLayout.cellSize.x / 2f, 0f);
        gridRectTransform.anchoredPosition = offsetGridElements;


    }

    void Update()
    {
        List<Vector3> pointsToRender = new List<Vector3>();

        //Debug.Log(selectedCircles.Count + "  " + selectedObjectsList.Count + "  " + selectedObjectsHash.Count);
        //Debug.Log(Screen.width/2f);

        if (Input.GetMouseButtonDown(0))
        {
            RayToOnjects();
            pointsToRender = InterpolatedCurve();
        }

        if (Input.GetMouseButton(0))
        {
            RayToOnjects();
            pointsToRender = InterpolatedCurve();
            AddSelectedCircle();
        }

        if (Input.GetMouseButtonUp(0))
        {
            lines.positionCount = 0;
            foreach (GameObject currentObj in selectedObjectsList)
            {
                if (currentObj.GetHashCode() == onMouseGameObject.GetHashCode())
                {
                    continue;
                }
                StartCoroutine(currentObj.GetComponent<InfoPacket>().Destroy());
            }
            enterColorCode = -1;

            SpawnBonusTextAfterDestroy();

            selectedObjectsHash.Clear();
            selectedObjectsList.Clear();

            foreach (GameObject currentObj in selectedCircles)
            {
                Destroy(currentObj);
            }
            
            foreach (GameObject currentObj in circlesInGridList)
            {
                Destroy(currentObj);
            }
            offsetGridElements = new Vector2(-gridLayout.cellSize.x / 2f, 0f);
            gridRectTransform.anchoredPosition = offsetGridElements;
            circlesInGridList.Clear();
            selectedCircles.Clear();

            canvasManager.ActionUnSelected();
            
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
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            int currentColorCode = hit.transform.GetComponent<InfoPacket>().selectedType.colorCode;
            //Debug.Log(enterColorCode);
            if ( SelectedColorCode(currentColorCode, hit.transform.gameObject) == true)
            {
                
                selectedObjectsHash.Add(hit.transform.gameObject);
                if (selectedObjectsList.Find(obj => obj.GetHashCode() == onMouseGameObject.GetHashCode()) != null)
                {
                    selectedObjectsList.Remove(onMouseGameObject);
                }

                selectedObjectsList.Add(hit.transform.gameObject);

                GameObject circleSpawn = Instantiate(circle, Canvas);
                circleSpawn.SetActive(true);
                selectedCircles.Add(circleSpawn);

                SpawnCirclesInGridCanvas(hit.transform.gameObject);
                canvasManager.ActionSelectedObjects();
                
            }
            else
            {
                AddPointMouse();
            }
        }
        else
        {
            AddPointMouse();
        }
    }

    private void AddPointMouse()
    {
        Vector3 pointOutTarget = sceneCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, sceneCamera.nearClipPlane - sceneCamera.transform.position.z));
        onMouseGameObject.transform.position = pointOutTarget;
        if (selectedObjectsList.Find(obj => obj.GetHashCode() == onMouseGameObject.GetHashCode()) == null)
        {
            selectedObjectsList.Add(onMouseGameObject);
        }
    }

    private void AddSelectedCircle()
    {
        for (int i = 0; i < selectedObjectsHash.Count; i++)
        {
            Vector3 worldToScreen = sceneCamera.WorldToScreenPoint(selectedObjectsList[i].transform.position);
            worldToScreen.z = 0;
            selectedCircles[i].transform.position = worldToScreen;
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

    private bool SelectedColorCode(int currentColorCode, GameObject currentObject)
    {
        if (selectedObjectsHash.Contains(currentObject) == true)
        { return false; }
        if (enterColorCode == -1)
        {
            enterColorCode = currentColorCode;
        }
        /*else if (selectedObjectsList.Count >= 2)
        {
            if (selectedObjectsList[selectedObjectsList.Count - 2].GetComponent<InfoPacket>()?.selectedType.colorCode == 0)
            {
                enterColorCode = currentColorCode;
            }
        }*/
        

        switch (currentColorCode)
        {
            case 0:
                return SwitcherPacket(currentColorCode, currentObject);

            case 1:
                return AddedPacket(currentColorCode, currentObject);
                
            case 2:
                return AddedPacket(currentColorCode, currentObject);

            case 3:
                return AddedPacket(currentColorCode, currentObject);

            case 4:
                return AddedPacket(currentColorCode, currentObject);



            default: throw new System.Exception("no number") ;
        }

        
    }

    private bool SwitcherPacket(int currentColorCode, GameObject currentObject)
    {
        currentObject.GetComponent<InfoPacket>().isObjectSelected = true;

        if (selectedObjectsHash.Contains(currentObject) == false)
        {
            enterColorCode = -1;
            return true;
        }
        else
        {
            //enterColorCode = -1;
            return false;
        }
    }

    private bool AddedPacket(int currentColorCode, GameObject currentObject)
    {
        

        if (enterColorCode == currentColorCode)
        {
            if (selectedObjectsHash.Contains(currentObject) == true)
            {
                return false;
            }

            else
            {
                currentObject.GetComponent<InfoPacket>().isObjectSelected = true;
                return true;
            }
        }

        else
        {
            return false;
        }
    }
    
    private void SpawnCirclesInGridCanvas(GameObject addedObject)
    {
        InfoPacket currentInfo = addedObject.GetComponent<InfoPacket>();
        if (currentInfo.selectedType.colorCode == 0)
        {

        }
        else
        {
            GameObject currentCircleInstance = Instantiate(circleOnGrid, gridElements.transform);
            
            Image currentImage = currentCircleInstance.GetComponent<Image>();
            currentImage.color = canvasManager.colorsForSelectedCircles[currentInfo.selectedType.colorCode-1];
            currentCircleInstance.SetActive(true);

            circlesInGridList.Add(currentCircleInstance);
            if (circlesInGridList.Count > 1)
            {
                StartCoroutine(OffsetGridAnimation(gridLayout.cellSize.x));
            }
            
        }
    }

    private IEnumerator OffsetGridAnimation(float xOffset)
    {
        //gridRectTransform.anchoredPosition
        for (float i = 0f;  i < 1f; i += 3f*Time.deltaTime)
        {
            if (circlesInGridList.Count <= 1) 
            {
                break; 
            }
            float addPosOffset = Mathf.Lerp(0f, xOffset, i)*3f*Time.deltaTime;
            offsetGridElements -= new Vector2(addPosOffset, 0f);

            //Debug.Log(offsetGridElements);
            gridRectTransform.anchoredPosition = offsetGridElements;
            yield return new WaitForEndOfFrame();
        }
        
        yield return null;
    }

    private void SpawnBonusTextAfterDestroy()
    {
        float addBonusSum = 0f;
        for (int i = 0; i < selectedObjectsHash.Count; i++)
        {
            addBonusSum += canvasManager.AddBonus;
            Vector3 posDestroyed = selectedObjectsList[i].transform.position;
            Vector3 spawnPos = sceneCamera.WorldToScreenPoint(posDestroyed);
            GameObject obj = Instantiate(textBonusAfterDestroy,Canvas);
            obj.transform.position = spawnPos;
            obj.SetActive(true);
        }
        Debug.Log(addBonusSum);
        StartCoroutine(canvasManager.AddExtraBonus(addBonusSum));
    }
}
    
