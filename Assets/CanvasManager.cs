using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    [HideInInspector]public Color[] colorsForSelectedCircles;
    public Color redPacket;
    public Color greenPacket;
    public Color yellowPacket;
    public Color bluePacket;
    public AnimationCurve speedOffsetGridElements;
    public GameObject scoreText;
    public GameObject multiplayerValue;
    public GameObject sliderHP;
    public GameObject sliderFillArea;
    private Slider sliderHPComponent;
    public float AddBonus = 250f;
    private ConnectBeams connectBeamsScript;
    private Text scoreTextComponent;
    private Text multiplayerValueComponent;
    private float maxScore = 999999999;
    float scorecount;
    float scoreMultiplayer = 1f;
    string formMultiplayer = " X ";
    public int CapacityHP = 5;
    public float DefaultCapacityHP = 1f;
    private float valueToSubstractHP
    {
        get { return DefaultCapacityHP / CapacityHP; }
    }
    public delegate void SubstracHPValue();
    public static event  SubstracHPValue OnSubstractHPValue;

    private void Awake()
    {
        OnSubstractHPValue = null;
    }

    void Start()
    {
        sliderFillArea.SetActive(true);
        sliderHPComponent = sliderHP.GetComponent<Slider>();
        sliderHPComponent.value = DefaultCapacityHP;
        colorsForSelectedCircles = new Color[] { redPacket, greenPacket, yellowPacket, bluePacket};
        scoreTextComponent = scoreText.GetComponent<Text>();
        multiplayerValueComponent = multiplayerValue.GetComponent<Text>();
        connectBeamsScript = FindObjectOfType<ConnectBeams>();
        //OnActionMultiplayerScore += ActionSelectedObjects;
        multiplayerValueComponent.text = formMultiplayer + scoreMultiplayer;

        OnSubstractHPValue += StartSubstarctHP;
        StartCoroutine(DefaultCounterScore());
        
    }


    void Update() 
    {
        scoreTextComponent.text =  Mathf.RoundToInt(scorecount).ToString();
    }

    private IEnumerator DefaultCounterScore()
    {
        for (float i = 0f; i < maxScore; i += 1000f * scoreMultiplayer*Time.deltaTime)
        {
            if (scoreMultiplayer != 1f)
            {
                scorecount += 1000f * scoreMultiplayer * Time.deltaTime;
            }
            else
            {
                yield return new WaitUntil(()=> scoreMultiplayer!=1);
            }
            yield return new WaitForSeconds(0.05f);
        }
        yield return null;
    }

    public IEnumerator AddExtraBonus(float number)
    {
        float addBonus = number / 100f;
        //Debug.Log(addBonus);
        for (int i = 0; i<100 ; i++)
        {
            
            scorecount += addBonus;
            yield return new WaitForSeconds(0.01f);
        }

        yield return null;
    }
    public static void InvokeActionSubstractHP()
    {
        OnSubstractHPValue?.Invoke();
    }
    public void StartSubstarctHP()
    {
        if (CapacityHP != 0)
        {
            StartCoroutine(SubstractHP());
        }
        else
        {
            OnSubstractHPValue = null;
        }
        
    }

    public IEnumerator SubstractHP()
    {
        CapacityHP -= 1;
        if (CapacityHP == 0) { yield return null ; }

        float substract = valueToSubstractHP / 100;
        for (int i = 0; i < 100; i++)
        {
            if (sliderHPComponent.value < 0.05f) 
            {
                CapacityHP = 0;
                sliderFillArea.SetActive(false);
                break;
            }
            sliderHPComponent.value -= substract;
            yield return new WaitForFixedUpdate();
        }

        yield return null;
    }

    public void ActionSelectedObjects()
    {
        if (connectBeamsScript.selectedObjectsHash.Count > 1)
        {
            scoreMultiplayer += 1;
        }
        
        multiplayerValueComponent.text = formMultiplayer + scoreMultiplayer.ToString();
    }

    public void ActionUnSelected()
    {
        
        scoreMultiplayer = 1f;
        multiplayerValueComponent.text = formMultiplayer + scoreMultiplayer.ToString();
    }
}
