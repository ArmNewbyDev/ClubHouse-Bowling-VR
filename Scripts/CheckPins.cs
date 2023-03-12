using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using TMPro;

public class CheckPins : MonoBehaviour
{
    [Header("TestResetBall")]
    [SerializeField] private GameObject[] wallForResetBall;

    private GameObject[] balls;
    private GameObject[] pins;
    private bool[] AddScore = new bool[10];
    private int _scoreNow;
    private int _scoreTotal;
    public int _round = 0;
    private bool GameEnd = false;
    private int _match = 0;
    private bool _thrown = false;

    bool Finish = false;
    bool Strike = false;
    bool Spare = false;
    bool StopSound = false;

    private Vector3[] _positionsPins, _positionsBall;

    public AudioClip End;
    public AudioClip StrikeSound;
    public AudioClip SpareSound;
    public AudioClip ReBallSound;

    private AudioSource _audioSource;

    [SerializeField] TextMeshProUGUI WhatMatch;
    [SerializeField] TextMeshProUGUI TotalScore;
    [SerializeField] TextMeshProUGUI[] ScoreMatch = new TextMeshProUGUI[10];


    // Start is called before the first frame update
    void Start()
    {
        pins = GameObject.FindGameObjectsWithTag("Pin");
        balls = GameObject.FindGameObjectsWithTag("Ball");

        _audioSource = GetComponent<AudioSource>();
        _scoreTotal = 0;
        _scoreNow = 0;
        _round = 0;
        _match = 0;
        _positionsPins = new Vector3[pins.Length];
        _positionsBall = new Vector3[balls.Length];

        for (int i = 0; i < pins.Length; i++)
        {
            _positionsPins[i] = pins[i].transform.position;
        }
        for (int i = 0; i < balls.Length; i++)
        {
            _positionsBall[i] = balls[i].transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {

        CountPinsDown();
        if (_match>9)
        {
            if (!GameEnd) {
                _audioSource.PlayOneShot(End);
            }
            GameEnd = true;
        }

        //Test Reset Pins
        if (Input.GetKeyUp(KeyCode.R))
        {
            ResetPins();
        }

        //Test Reset Balls
        if (Input.GetKeyUp(KeyCode.F))
        {
            ResetGame();
        }
    }

    IEnumerator Wait(int i)
    {
        yield return new WaitForSeconds(1);

        pins[i].SetActive(false);
    }

    //Count Pins Down and Add Score
    private void CountPinsDown()
    {
        for (int i = 0; i < pins.Length; i++)
        {


            if (pins[i].transform.eulerAngles.z > 10 && pins[i].transform.eulerAngles.z < 350 && pins[i].activeSelf)
            {
                if (!AddScore[i])
                {
                    if (!GameEnd)
                    {
                        _scoreNow++;
                        ScoreMatch[_match].text = _scoreNow.ToString();

                    }
                    
                    StartCoroutine(Wait(i)); 

                    AddScore[i] = true;
                }

                //Add Score
                    //_scoreNow++;
                //Disable Pin
                    //pins[i].SetActive(false);
            }
        }

        Debug.Log("Score : " + _scoreTotal);
        _thrown = true;

    }


    //Reset Pins
    public void ResetPins()
    {
        if (!Finish)
        {
           
            StartCoroutine(JustWaitForBall());
            Finish = true;
        }
        

    }
    public void JustResetBall()
    {

        for (int i = 0; i < balls.Length; i++)
        {
            balls[i].transform.position = _positionsBall[i];
            balls[i].GetComponent<Rigidbody>().velocity = Vector3.zero;
            balls[i].GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            balls[i].transform.rotation = Quaternion.identity;
        }
    }
    IEnumerator JustWaitForBall()
    {
        yield return new WaitForSeconds(2);

        for (int i = 0; i < pins.Length; i++)
        {
            pins[i].SetActive(true);
            pins[i].transform.position = _positionsPins[i];
            pins[i].GetComponent<Rigidbody>().velocity = Vector3.zero;
            pins[i].GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            pins[i].transform.rotation = Quaternion.identity;
            AddScore[i] = false;
        }
        if (Strike)
        {
            _scoreTotal += 10;
            Strike = false;
        }
        else if (Spare)
        {
            Spare = false;
        }
        _scoreTotal += _scoreNow;
        _scoreNow = 0;
        _round = 0;
        _match++;
        if (_match < 10) {

                WhatMatch.text = "Round: " + (_match + 1);
        }
        else if(_match >= 10) { WhatMatch.text = "Round: End"; }
        
        TotalScore.text = _scoreTotal.ToString();
        Finish = false;
        StopSound = false;
    }

    //Reset Balls
    public void ResetBall()
    {
        for (int i = 0; i < balls.Length; i++)
        {
            balls[i].transform.position = _positionsBall[i];
            balls[i].GetComponent<Rigidbody>().velocity = Vector3.zero;
            balls[i].GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            balls[i].transform.rotation = Quaternion.identity;
        }
        _audioSource.PlayOneShot(ReBallSound);
        if (!GameEnd) { _round++;
            ScoreCalculate();
        }
        
    }
    public void ResetGame()
    {
        _scoreTotal = 0;
        _scoreNow = 0;
        _round = 0;
        _match = 0;
        TotalScore.text = _scoreTotal.ToString();
        WhatMatch.text = "Round: " + (_match + 1);
        Finish = false;
        Strike = false;
        Spare = false;
        StopSound = false;
        for (int i = 0; i < pins.Length; i++)
        {
            pins[i].SetActive(true);
            pins[i].transform.position = _positionsPins[i];
            pins[i].GetComponent<Rigidbody>().velocity = Vector3.zero;
            pins[i].GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            pins[i].transform.rotation = Quaternion.identity;
            AddScore[i] = false;
        }
        for (int i = 0; i < ScoreMatch.Length; i++)
        {
            ScoreMatch[i].text = "0";
        }

        _audioSource.PlayOneShot(ReBallSound);
    }
    private void ScoreCalculate()
    {
        if (_round == 1 && _scoreNow == 10)
        {
            Debug.Log("Strike");
            Strike = true;
            if (!StopSound)
            {
                _audioSource.PlayOneShot(StrikeSound);
                ScoreMatch[_match].text = "X";
                StopSound = true;
            }
            
            ResetPins();
        }
        if (_round == 2 && _scoreNow == 10)
        {
            Debug.Log("Spare");
            Spare = true;
            if (!StopSound)
            {
                _audioSource.PlayOneShot(SpareSound);
                ScoreMatch[_match].text = "/";
                StopSound =true;
            }
            
            ResetPins();
        }
        else if (_round == 2 && _scoreNow < 10)
        {
            Debug.Log("Score : " + _scoreTotal);
            ResetPins();
        }

    }
}
