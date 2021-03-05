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
        StartCoroutine(collectCouroutine.LaunchOneType(listPackets[1].packetPrefab, launchPositions, pointsManager.GetRow(), preLaunchTime : 1f, launchPeriod : 0.5f, percentToUse : 1f));
        StartCoroutine(collectCouroutine.LaunchOneType(listPackets[2].packetPrefab, launchPositions, pointsManager.GetRow(), preLaunchTime : 1f, launchPeriod : 0.5f, percentToUse : 1f));
        StartCoroutine(collectCouroutine.LaunchOneType(listPackets[3].packetPrefab, launchPositions, pointsManager.GetRow(), preLaunchTime : 1f, launchPeriod : 0.5f, percentToUse : 1f));
        StartCoroutine(collectCouroutine.LaunchOneType(listPackets[4].packetPrefab, launchPositions, pointsManager.GetRow(), preLaunchTime : 1f, launchPeriod : 0.5f, percentToUse : 1f));
        StartCoroutine(collectCouroutine.LaunchOneType(listPackets[0].packetPrefab, launchPositions, pointsManager.GetRow(), preLaunchTime : 1f, launchPeriod : 0.5f, percentToUse : 1f));
        //Debug.Log("Length percent" + 10f + "  " + Mathf.RoundToInt(Mathf.Lerp(0, 10f, 0.5f)));
    }
}
