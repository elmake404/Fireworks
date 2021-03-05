using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoimtsManager : MonoBehaviour
{

    //public GameObject instance;
    private SaveData loadData;
    private int numPointsY_Axis;
    private int numPointsX_Axis;
    private Vector3 xOffset;
    private Vector3 yOffset;
    private Vector3 rectPos;
    private Vector3[,] MatrixPoints;
    private GenerateMatrixInfo matrixInfo = new GenerateMatrixInfo();
    private int[] randomSortedYIndex;
    private int numCallGetRow;
    


    private void Awake()
    {
        numCallGetRow = 0;

        loadData = FindObjectOfType<SaveData>();
        matrixInfo = JsonUtility.FromJson<GenerateMatrixInfo>(loadData.Load());
        numPointsY_Axis = matrixInfo.ySizeArray;
        numPointsX_Axis = matrixInfo.xSizeArray;
        xOffset = matrixInfo.xOffset;
        yOffset = matrixInfo.yOffset;
        rectPos = matrixInfo.rectPos;

        MatrixPoints = new Vector3[numPointsY_Axis, numPointsX_Axis];
        SpawnPos();

        randomSortedYIndex = new int[numPointsY_Axis];
        for (int i = 0; i < numPointsY_Axis; i++)
        {
            randomSortedYIndex[i] = i;
        }

        GetRow();

    }

    public Vector3[] GetRow()
    {
        Vector3[] rowXPositions = new Vector3[numPointsX_Axis];

        if (numCallGetRow == 0)
        {
            SetRandomRowIndex();
        }

        if (numCallGetRow == numPointsY_Axis - 1)
        {
            numCallGetRow = 0;
            SetRandomRowIndex();
        }

        for (int i = 0; i < numPointsX_Axis; i++)
        {
            Vector2 randomOffset = Random.insideUnitSphere/2f;
            Vector3 addPos = MatrixPoints[randomSortedYIndex[numCallGetRow], i];
            rowXPositions[i] = new Vector3(addPos.x + randomOffset.x, addPos.y + randomOffset.y, 0f);
            
        }

        numCallGetRow += 1;

        return rowXPositions;
    }

    private void SetRandomRowIndex()
    {
        for (int i = numPointsY_Axis - 1; i >= 1; i--)
        {
            int j = Random.Range(0, i + 1);
            var temp = randomSortedYIndex[j];
            randomSortedYIndex[j] = randomSortedYIndex[i];
            randomSortedYIndex[i] = temp;
        }
    }


    private void SpawnPos()
    {
        for (int y = 0; y < numPointsY_Axis; y++)
        {
            for (int x = 0; x < numPointsX_Axis; x++)
            {
                Vector3 point = new Vector3(xOffset.x / 2f + rectPos.x + xOffset.x * x, yOffset.y / 2f + rectPos.y + yOffset.y * y, 0f);
                MatrixPoints[y, x] = point;

                
            }
        }
    }
}
