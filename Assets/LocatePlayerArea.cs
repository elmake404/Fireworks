using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocatePlayerArea : MonoBehaviour
{
    private Camera mainCamera;
    private Vector3 rectPos;
    private Vector3 intervalPerPoint = new Vector3(0.8f, 0.8f, 0f);
    private int numPointsX_Axis;
    private int numPointsY_Axis;
    private Vector3 rectPosXEnd;
    private Vector3 rectPosYEnd;
    private Vector3 xOffset; 
    private Vector3 yOffset; 
    private Vector3[,] MatrixPoints;
    
    public GameObject instanced;  

    private void Awake()
    {

        mainCamera = this.gameObject.GetComponent<Camera>();
        Vector3 centerPos = new Vector3(0f, 1f ,0f);

        Rect onScreenRect = new Rect(Screen.width * ((1f - 0.8f) / 2f), Screen.height * ((1f - 0.5f) / 2f), Screen.width * 0.8f, Screen.height * 0.5f);
        rectPos = mainCamera.ScreenToWorldPoint( new Vector3( onScreenRect.position.x, 3f*onScreenRect.y, mainCamera.nearClipPlane - this.transform.position.z));
        rectPos.z = 0f;

        rectPosXEnd = new Vector3(rectPos.x - (rectPos.x) * 2f, rectPos.y, 0f);
        rectPosYEnd = new Vector3(rectPos.x, centerPos.y - rectPos.y + centerPos.y, 0f);

        numPointsX_Axis = Mathf.RoundToInt(Vector3.Distance(rectPos, rectPosXEnd) /Vector3.Distance(Vector3.zero, new Vector3(intervalPerPoint.x, 0f, 0f)));
        numPointsY_Axis = Mathf.RoundToInt(Vector3.Distance(rectPos, rectPosYEnd) /Vector3.Distance(Vector3.zero, new Vector3(0f, intervalPerPoint.y, 0f)));

        MatrixPoints = new Vector3[numPointsY_Axis, numPointsX_Axis]; 

        xOffset = (rectPosXEnd - rectPos)/numPointsX_Axis;
        yOffset = (rectPosYEnd - rectPos)/numPointsY_Axis;
        Debug.Log(numPointsY_Axis);
        SpawnPos();
    }

    void OnGUI()
    {
        Rect onScreenRect = new Rect(Screen.width * ((1f - 0.8f) / 2f), Screen.height * ((1f - 0.5f) / 2f), Screen.width * 0.8f, Screen.height * 0.5f);

        GUI.Box(onScreenRect, "");
    }


    

    private void SpawnPos()
    {
        for (int y = 0; y < numPointsY_Axis; y++)
        {
            for (int x = 0; x < numPointsX_Axis; x++)
            {
                Vector3 point = new Vector3(xOffset.x/2f + rectPos.x + xOffset.x * x, yOffset.y/2f + rectPos.y + yOffset.y * y, 0f);
                MatrixPoints[y, x] = point;

                GameObject intance = GameObject.Instantiate(instanced);
                instanced.transform.position = point;
            }
        }
    }

    
}
