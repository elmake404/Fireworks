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
    private GameObject obj;

    private void OnEnable()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        sphereColider = GetComponent<SphereCollider>();
        trailRenderer = GetComponent<TrailRenderer>();
    }

    public IEnumerator Destroy()
    {
        meshRenderer.enabled = false;
        sphereColider.enabled = false;
        trailRenderer.enabled = false;

        obj = Instantiate(selectedType.fireWork);
        obj.transform.position = gameObject.transform.position;
        obj.GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(obj.GetComponent<ParticleSystem>().main.duration);
        Destroy(obj);
        
        DestroyImmediate(gameObject);
    }
}
