using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text;


public class SaveData:MonoBehaviour
{
    

    public void Save(GenerateMatrixInfo objClass)
    {
        string save = JsonUtility.ToJson(objClass);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/SaveData.json", save);
    }

    public string Load()
    {
        string load = System.IO.File.ReadAllText(Application.persistentDataPath + "/SaveData.json");
        return load;
    }
    
}


[System.Serializable]
public class GenerateMatrixInfo
{
    public int ySizeArray;
    public int xSizeArray;
    public Vector3 xOffset;
    public Vector3 yOffset;
    public Vector3 rectPos;

}

