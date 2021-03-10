using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [HideInInspector]public Color[] colorsForSelectedCircles;
    public Color redPacket;
    public Color greenPacket;
    public Color yellowPacket;
    public Color bluePacket;
    public AnimationCurve speedOffsetGridElements;

    void Start()
    {
        colorsForSelectedCircles = new Color[] { redPacket, greenPacket, yellowPacket, bluePacket};
    }


    void Update()
    {
        
    }
}
