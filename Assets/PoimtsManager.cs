using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoimtsManager : MonoBehaviour
{
    public GameObject instance;
    private SaveData loadData;
    private int numPointsY_Axis;
    private int numPointsX_Axis;
    private Vector3 xOffset;
    private Vector3 yOffset;
    private Vector3 rectPos;
    private Vector3[,] MatrixPoints;
    private GenerateMatrixInfo matrixInfo = new GenerateMatrixInfo();


    private void Awake()
    {
        loadData = FindObjectOfType<SaveData>();
        matrixInfo = JsonUtility.FromJson<GenerateMatrixInfo>(loadData.Load());
        numPointsY_Axis = matrixInfo.ySizeArray;
        numPointsX_Axis = matrixInfo.xSizeArray;
        xOffset = matrixInfo.xOffset;
        yOffset = matrixInfo.yOffset;
        rectPos = matrixInfo.rectPos;

        MatrixPoints = new Vector3[numPointsY_Axis, numPointsX_Axis];
        SpawnPos();

        Debug.Log(rectPos);
        
    }

    private void Start()
    {
        //Debug.Log( importedMatrixPoints.matrixPointsPos.Length);
        
    }

    private void SpawnPos()
    {
        for (int y = 0; y < numPointsY_Axis; y++)
        {
            for (int x = 0; x < numPointsX_Axis; x++)
            {
                Vector3 point = new Vector3(xOffset.x / 2f + rectPos.x + xOffset.x * x, yOffset.y / 2f + rectPos.y + yOffset.y * y, 0f);
                MatrixPoints[y, x] = point;
                GameObject isnt = GameObject.Instantiate(instance);
                isnt.transform.position = point;

            }
        }
    }
}
