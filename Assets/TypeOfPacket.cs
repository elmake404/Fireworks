using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Packet", menuName = "Packet")]
public class TypeOfPacket : ScriptableObject
{
    public GameObject packetPrefab;
    public GameObject fireWork;
    public int colorCode;
}
