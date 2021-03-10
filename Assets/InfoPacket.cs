using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class InfoPacket : MonoBehaviour
{
    public bool isObjectSelected;
    public GameObject linkToThisPrefab;
    public TypeOfPacket selectedType;
    [HideInInspector]public float velocityPacket;
    public GameObject ownParticlesTrail;
    public GameObject particleAfterDying;
    private MeshRenderer meshRenderer;
    private SphereCollider sphereColider;
    private TrailRenderer trailRenderer;
    private GameObject obj;
    private Vector3 previousPos;
    private ParticleSystem particles;
    private bool isDestroy;
    

    private void OnEnable()
    {
        LaunchManager.numOfSpawnedPacket += 1;
        isObjectSelected = false;
        isDestroy = false;
        meshRenderer = GetComponent<MeshRenderer>();
        sphereColider = GetComponent<SphereCollider>();
        trailRenderer = GetComponent<TrailRenderer>();
        particles = ownParticlesTrail.GetComponent<ParticleSystem>();
        particleAfterDying.SetActive(false);
    }

    private void Update()
    {
        velocityPacket = (this.transform.position - previousPos).magnitude/Time.deltaTime;
        previousPos = this.transform.position;
    }

    public IEnumerator Destroy()
    {
        isDestroy = true;
        
        meshRenderer.enabled = false;
        sphereColider.enabled = false;
        trailRenderer.enabled = false;

        ownParticlesTrail.SetActive(false);

        obj = Instantiate(selectedType.fireWork);
        obj.transform.position = gameObject.transform.position;
        obj.GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(obj.GetComponent<ParticleSystem>().main.duration);
        Destroy(obj);
        Destroy(linkToThisPrefab);
        Destroy(ownParticlesTrail);
    }
    public IEnumerator DyingFlare(float timeToDying)
    {
        var emission = particles.emission;
        float startParticles = 5f;
        
        for (float i = 0f; i < timeToDying; i += 1f * Time.deltaTime)
        {
            if (isDestroy == true) { break; }
            if (isObjectSelected == true) { break; }
            float setParticles = Mathf.Lerp(startParticles, 0.5f, i / timeToDying);
            emission.rateOverTime = setParticles;
            yield return new WaitForEndOfFrame();
        }
        if (isObjectSelected == false)
        {
            if (isDestroy == false)
            {
                ownParticlesTrail.SetActive(false);
                GameObject particlesAfter = Instantiate(particleAfterDying);
                particlesAfter.transform.position = this.transform.position;
                particlesAfter.SetActive(true);
                yield return new WaitForSeconds(0.5f);
                Destroy(particlesAfter);
                Destroy(obj);
                Destroy(linkToThisPrefab);
                Destroy(ownParticlesTrail);
            }
        }
        
        
        yield return null;
    }

    private void OnDisable()
    {
        LaunchManager.numOfSpawnedPacket -= 1;
    }
}
