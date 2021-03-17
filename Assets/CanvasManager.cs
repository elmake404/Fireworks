using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    [HideInInspector] public Color[] colorsForSelectedCircles;
    public Color redPacket;
    public Color greenPacket;
    public Color yellowPacket;
    public Color bluePacket;
    public AnimationCurve speedOffsetGridElements;
    public GameObject scoreText;
    public GameObject multiplayerValue;
    public GameObject sliderHP;
    public GameObject sliderRedHP;
    public GameObject sliderFillArea;
    public GameObject sliderRedFillArea;
    public GameObject RestartMenu;
    public GameObject restartButton;
    public GameObject newHighScoreText;
    public GameObject currentScoreText;
    public GameObject bestScoreText;

    private Button restartButtonComponent;
    private Text currentScoreTextComponent;
    private Text bestScoreTextComponent;
    private Slider sliderHPComponent;
    private Slider sliderRedHPComponent;
    public float AddBonus = 250f;
    private ConnectBeams connectBeamsScript;
    private Text scoreTextComponent;
    private Text multiplayerValueComponent;
    private float maxScore = 999999999;
    float scorecount;
    float scoreMultiplayer = 1f;
    string formMultiplayer = " x ";
    public int CapacityHP = 5;
    private float currentSliderValue;
    public float DefaultCapacityHP = 1f;
    private bool isSubstractHP;
    private float valueToSubstractHP
    {
        get { return DefaultCapacityHP / CapacityHP; }
    }
    public delegate void SubstracHPValue();
    public static event SubstracHPValue OnSubstractHPValue;

    [HideInInspector] public int bestScore;

    private static bool isGameStart;

    private void Awake()
    {
        if (isGameStart == false)
        {
            isGameStart = true;
            FacebookManager.Instance.GameStart();
        }
        OnSubstractHPValue = null;
    }

    void Start()
    {
        bestScore = PlayerPrefs.GetInt("bestScore");

        RestartMenu.SetActive(false);
        isSubstractHP = false;
        sliderFillArea.SetActive(true);
        sliderHPComponent = sliderHP.GetComponent<Slider>();
        sliderRedHPComponent = sliderRedHP.GetComponent<Slider>();
        currentScoreTextComponent = currentScoreText.GetComponent<Text>();
        bestScoreTextComponent = bestScoreText.GetComponent<Text>();
        restartButtonComponent = restartButton.GetComponent<Button>();

        restartButtonComponent.onClick.AddListener(RestartScene);
        bestScoreTextComponent.text = PlayerPrefs.GetInt("bestScore").ToString();

        sliderHPComponent.value = DefaultCapacityHP;
        colorsForSelectedCircles = new Color[] { redPacket, greenPacket, yellowPacket, bluePacket };
        scoreTextComponent = scoreText.GetComponent<Text>();
        multiplayerValueComponent = multiplayerValue.GetComponent<Text>();
        connectBeamsScript = FindObjectOfType<ConnectBeams>();
        //OnActionMultiplayerScore += ActionSelectedObjects;
        multiplayerValueComponent.text = formMultiplayer + scoreMultiplayer;
        OnSubstractHPValue += StartSubstarctHP;
        StartCoroutine(DefaultCounterScore());
        StartCoroutine(RecoveryHP());
        StartCoroutine(CallRestartMenu());


    }


    void Update()
    {
        scoreTextComponent.text = Mathf.RoundToInt(scorecount).ToString();
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

    public IEnumerator RecoveryHP()
    {
        yield return new WaitForSeconds(0.1f);
        while (true)
        {
            if (sliderHPComponent.value < 0.05f)
            {
                
                break;
            }
            //yield return new WaitForSeconds(0.05f);

            sliderHPComponent.value += Time.deltaTime / 2f;
            currentSliderValue = sliderHPComponent.value;
            yield return new WaitForSeconds(0.1f);
            yield return new WaitWhile(()=>isSubstractHP==true);
        }
        
        yield return null;
    }

    public IEnumerator SubstractHP()
    {
        //CapacityHP -= 1;
        if (CapacityHP == 0) { yield return null ; }

        float substract = valueToSubstractHP / 100;
        StartCoroutine(SubstractRedHP(1.5f));
        for (int i = 0; i < 100; i++)
        {
            isSubstractHP = true;
            if (sliderHPComponent.value < 0.05f) 
            {
                CapacityHP = 0;
                sliderFillArea.SetActive(false);
                break;
            }
            sliderHPComponent.value -= substract;
            currentSliderValue = sliderHPComponent.value;
            yield return new WaitForFixedUpdate();
        }
        
        yield return new WaitForSeconds(3f);
        isSubstractHP = false;
        yield return null;
    }

    public IEnumerator SubstractRedHP(float Delay)
    {

        
        sliderRedHPComponent.value = currentSliderValue;
        yield return new WaitForSeconds(Delay);
        float substract = valueToSubstractHP / 100;
        
        for (int i = 0; i < 100; i++)
        {
            if (sliderRedHPComponent.value < 0.05f)
            {
                sliderRedFillArea.SetActive(false);
                break;
            }
            sliderRedHPComponent.value -= substract;
            yield return new WaitForSeconds(0.01f);
        }

        yield return null;
    }

    private IEnumerator CallRestartMenu()
    {
        yield return new WaitUntil(()=> sliderHPComponent.value<0.05f);
        currentScoreTextComponent.text = Mathf.RoundToInt(scorecount).ToString();
        FacebookManager.Instance.LevelFail(0);

        RestartMenu.SetActive(true);
        newHighScoreText.SetActive(false);
        if ((int)scorecount > bestScore)
        {
            PlayerPrefs.SetInt("bestScore", Mathf.RoundToInt(scorecount));
            bestScoreTextComponent.text = Mathf.RoundToInt(scorecount).ToString();
            newHighScoreText.SetActive(true);
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

    private void RestartScene()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
}
