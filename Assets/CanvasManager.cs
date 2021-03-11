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
    private ConnectBeams connectBeamsScript;
    private Text scoreTextComponent;
    private Text multiplayerValueComponent;
    private float maxScore = 999999999;
    float scorecount;
    float scoreMultiplayer = 1f;
    string formMultiplayer = " X ";

    //public delegate void multiplayerScoreCount();
    //public event  multiplayerScoreCount OnActionMultiplayerScore;

    void Start()
    {
        colorsForSelectedCircles = new Color[] { redPacket, greenPacket, yellowPacket, bluePacket};
        scoreTextComponent = scoreText.GetComponent<Text>();
        multiplayerValueComponent = multiplayerValue.GetComponent<Text>();
        connectBeamsScript = FindObjectOfType<ConnectBeams>();
        //OnActionMultiplayerScore += ActionSelectedObjects;
        multiplayerValueComponent.text = formMultiplayer + scoreMultiplayer;
        StartCoroutine(DefaultCounterScore());
    }


    void Update() 
    {
        scoreTextComponent.text =  Mathf.RoundToInt(scorecount).ToString();
    }

    private IEnumerator DefaultCounterScore()
    {
        for (float i = 0f; i < maxScore; i += 100f * scoreMultiplayer*Time.deltaTime)
        {
            if (scoreMultiplayer != 1f)
            {
                scorecount += 100f * scoreMultiplayer * Time.deltaTime;
            }
            else
            {
                yield return new WaitUntil(()=> scoreMultiplayer!=1);
            }
            yield return new WaitForSeconds(0.05f);
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
