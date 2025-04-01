using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerScore : MonoBehaviour
{
    public TextMeshProUGUI ScoreField;

    private void LateUpdate()
    {
        ScoreField.text = "Current points: "+HighScoreManager.Instance.GetScore();    
    }

    private void OnDisable()
    {
        HighScoreManager.Instance.SetHighScoresInDataBase();
    }
}
