using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocatePlayerArea : MonoBehaviour
{
    private Camera mainCamera;
    private Vector3 centerOfWorld;
    private float radiusToSpawn;
    public float gridSpacing;
    public static HashSet<Vector3> originalPosGrid;    // Зарезервированные, для сброса posGrid - позиции
    public static HashSet<Vector3> posGrid;     //Готовые позиции для вычитания
    //public static HashSet<Vector3> usedPosGrid;     //Испольщованные позиции
    public static Vector3[] toLaunchedPos;
    

    private void Awake()
    {
        posGrid = new HashSet<Vector3>();
        //usedPosGrid = new HashSet<Vector3>();
        mainCamera = this.gameObject.GetComponent<Camera>();
        Vector2 centerOfScreen = new Vector2(mainCamera.pixelWidth / 2, mainCamera.pixelHeight / 2);
        Vector3 centerPos = mainCamera.ScreenToWorldPoint(new Vector3(centerOfScreen.x, centerOfScreen.y, mainCamera.nearClipPlane - mainCamera.transform.position.z));
        centerPos.z = 0f;
        Vector3 leftBorderCamera = mainCamera.ScreenToWorldPoint(new Vector3(0, centerOfScreen.y, mainCamera.nearClipPlane - mainCamera.transform.position.z));
        leftBorderCamera.z = 0f;
        radiusToSpawn = Vector3.Distance(leftBorderCamera, centerPos);
        centerOfWorld = centerPos;
        SpawnPos(posGrid);
        originalPosGrid = posGrid;
        Debug.Log("Original " + posGrid.Count);
        RefreshForRandomArray();
    }
    void Start()
    {
        
    }

    private void FixedUpdate()
    {

    }

    private void SpawnPos(HashSet<Vector3> positions)
    {
        for (float x = -radiusToSpawn; x < radiusToSpawn; x += gridSpacing)
        {
            for (float y = -radiusToSpawn; y < radiusToSpawn; y += gridSpacing)
            {
                Vector3 spawnPosition = new Vector3(centerOfWorld.x + x, centerOfWorld.y + y, 0f) ;
                if (Vector3.Distance(spawnPosition, centerOfWorld) <radiusToSpawn)
                {
                    positions.Add(spawnPosition);
                    
                }
                
            }
        }
    }

    public static void RefreshForRandomArray()
    {
        //toLaunchedPos = null;
        toLaunchedPos = new Vector3[posGrid.Count];
        posGrid.CopyTo(toLaunchedPos);
        //Debug.Log("Length Random Array "+toLaunchedPos.Length);
    }

    public static void RefreshUsedPositions()
    {
        posGrid.Clear();
        posGrid = originalPosGrid;
    }

    public static Vector3 GetRandomPos()
    {
        int randomIndex = Random.Range(0, toLaunchedPos.Length);
        Vector3 pos = toLaunchedPos[randomIndex];
        posGrid.Remove(pos);
        RefreshForRandomArray();

        return pos;
    }
}
