using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlSpawn : MonoBehaviour
{
    public GameObject packet;
    private InfoPacket getInfo;
    private ParticleSystem currentParticles;
    void Start()
    {
        getInfo = packet.GetComponentInChildren<InfoPacket>();
        currentParticles = this.GetComponent<ParticleSystem>();
    }

    void Update()
    {
        float velocity = getInfo.velocityPacket;
        /*if (velocity > 50f)
        {
            velocity = 0f;
        }*/
        var emission = currentParticles.emission;
        emission.rateOverTime = 2f + velocity * 4f;
    }
}
