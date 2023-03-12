using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool GameStart = false;



    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameStartByButton()
    {
        GameStart = true;
    }

    public void GameEnd()
    {
        GameStart = false;
    }
}
