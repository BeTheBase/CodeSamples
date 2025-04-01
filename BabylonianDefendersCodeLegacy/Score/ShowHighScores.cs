using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHighScores : MonoBehaviour
{
    void Start()
    {
        //get the highscores out of database and add to ui
        HighScoreManager.Instance.SetHighScoresInDataBase();

        HighScoreManager.Instance.GetHighScoresFromDataBase();

        
    }
}
