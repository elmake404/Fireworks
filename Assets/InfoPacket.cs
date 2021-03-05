using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class InfoPacket : MonoBehaviour
{
    public TypeOfPacket selectedType;
    private MeshRenderer meshRenderer;
    private SphereCollider sphereColider;
    private TrailRenderer trailRenderer;
    //private LaunchManager launchManager;

    private void OnEnable()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        sphereColider = GetComponent<SphereCollider>();
        trailRenderer = GetComponent<TrailRenderer>();
        //launchManager = FindObjectOfType<LaunchManager>();

        
    }

    /*public IEnumerator Destroy()
    {
        meshRenderer.enabled = false;
        sphereColider.enabled = false;
        trailRenderer.enabled = false;

        GameObject obj = GameObject.Instantiate(launchManager.listPackets[selectedType.colorCode].fireWork);
        obj.transform.position = gameObject.transform.position;
        obj.GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(obj.GetComponent<ParticleSystem>().main.duration);
        Destroy(gameObject);
    }*/
    public IEnumerator Destroy()
    {
        meshRenderer.enabled = false;
        sphereColider.enabled = false;
        trailRenderer.enabled = false;

        GameObject obj = GameObject.Instantiate(selectedType.fireWork);
        obj.transform.position = gameObject.transform.position;
        obj.GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(obj.GetComponent<ParticleSystem>().main.duration);
        Destroy(gameObject);
    }
}
