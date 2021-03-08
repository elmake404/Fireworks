using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchManager : MonoBehaviour
{
    public Transform[] launchPositions;
    private PoimtsManager pointsManager;
    private CollectCouroutine collectCouroutine;
    public List<TypeOfPacket> listPackets;
    

    private void Awake()
    {
        /*arrayPackets = new List<TypeOfPacket>();
        arrayPackets[1] = redPacket;
        arrayPackets[2] = greenPacket;*/

        pointsManager = FindObjectOfType<PoimtsManager>();
        collectCouroutine = FindObjectOfType<CollectCouroutine>();
    }
    private void Start()
    {
        //GameObject[] obj =  { listPackets[0].packetPrefab, listPackets[1].packetPrefab, listPackets[2].packetPrefab, listPackets[3].packetPrefab, listPackets[4].packetPrefab };
        StartCoroutine(collectCouroutine.LaunchOneType(listPackets[1].packetPrefab, launchPositions, pointsManager.GetRow(), preLaunchTime : 1f, launchPeriod : 1f, percentToUse : 1f, launchDuration: 5f));
        
    }
}
