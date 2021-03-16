using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkWithArray
{
    public static int FindIndexOfElemrnt(int[] nums, int findIndex)
    {
        return Array.FindIndex(nums, p => p == findIndex);
    }


}
