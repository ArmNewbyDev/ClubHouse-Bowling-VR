using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBallDroptoBlackHole : MonoBehaviour
{

    [SerializeField] private CheckPins _checkPin;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ball")
        {
            _checkPin.ResetBall();
            if (_checkPin._round > 1)
            {
                _checkPin.ResetPins();
            }
        }
    }
}
