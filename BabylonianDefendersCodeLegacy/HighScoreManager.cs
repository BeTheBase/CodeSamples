using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;
using SimpleJSON;
using UnityEngine.SceneManagement;

public interface IHighScore
{
    void SetScore(int score);
    int GetScore();

    void SetHighScoresInDataBase();
    void GetHighScoresFromDataBase();

    void UpdateUsername(string name);
}

[System.Serializable]
public struct HighScoreData
{
    public string username;
    public int score;
}

[System.Serializable]
public struct HighScoreDataWrapp
{
    public HighScoreData[] datas;
}

public class HighScoreManager : GenericSingleton<HighScoreManager, IHighScore>, IHighScore
{
    [SerializeField]
    private int localScore = 0;
    [SerializeField]
    private TextMeshProUGUI localScoreText;
    [SerializeField]
    private string username;
    [SerializeField]
    private string rootURL = "https://studenthome.hku.nl/~bas.dekoningh/"; //Path where php files are located
    [SerializeField]
    private string errorMessage = "";
    [SerializeField]
    private TextMeshProUGUI UsersTextField;
    [SerializeField]
    private TextMeshProUGUI ScoresTextField;
    public GameObject VisualParent;
    public GameObject VisualLayoutUsersAndScoresObject;


    private void Start()
    {
        DontDestroyOnLoad(this);

        localScoreText.text = localScore.ToString();
    }

    public override void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if(FindObjectOfType<PlayerScore>())
        {
            localScoreText = FindObjectOfType<PlayerScore>().ScoreField;
        }
        localScoreText.text = localScore.ToString();
    }

    public void UpdateUsername(string name)
    {
        username = name;
    }

    public void SetHighScoresInDataBase()
    {
        StartCoroutine(UpdateHighScoreEnumerator());
    }

    public void GetHighScoresFromDataBase()
    {
        StartCoroutine(ReceiveHighScores());
    }

    public void SetScore(int score)
    {
        localScore += score;

        if(localScoreText != null)
            localScoreText.text = localScore.ToString();
    }

    public int GetScore()
    {
        return localScore;
    }

    IEnumerator UpdateHighScoreEnumerator()
    {
        bool isWorking = true;
        bool registrationCompleted = false;

        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("score", localScore);

        using (UnityWebRequest www = UnityWebRequest.Post(rootURL + "UpdateHighScore.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                errorMessage = www.error;
            }
            else
            {
                string responseText = www.downloadHandler.text;

                if (responseText.StartsWith("Success"))
                {
                    Debug.Log("Updatehighscore");
                    registrationCompleted = true;                    
                }
                else
                {
                    errorMessage = responseText;
                }
            }
        }

        isWorking = false;
    }


    IEnumerator ReceiveHighScores()
    {
        string responseText = "";
        bool registrationCompleted = false;
        bool isWorking = false;
        using (UnityWebRequest www = UnityWebRequest.Get(rootURL + "ReceiveHighScore.php"))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                errorMessage = www.error;
            }
            else
            {
                responseText = www.downloadHandler.text;
                List<byte> rt = new List<byte>();
                foreach(byte b in responseText)
                {
                    rt.Add(b);
                }
                Debug.Log(responseText);
                //string jsonFormated = JsonHelper.FromJsonList<string>(rt);


                string output = "{\"datas\":" + responseText + "}";
                Debug.Log(output);
                //var coolJson = JsonHelper.FromJsonList<List<HighScoreData>>(output);
                HighScoreDataWrapp jsonOuts = JsonUtility.FromJson<HighScoreDataWrapp>(output);

                foreach(HighScoreData highScoreData in jsonOuts.datas)
                {
                    GameObject go = GameObject.Instantiate(VisualLayoutUsersAndScoresObject, VisualParent.transform.position, Quaternion.identity);
                    go.transform.parent = VisualParent.transform;
                    go.transform.position = Vector3.zero;
                    go.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "User :: " +highScoreData.username;
                    go.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Score :: " + highScoreData.score.ToString();

                    //go.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = highScoreData.score.ToString();
                }

                //wrapperd = JsonUtility.FromJson<HighScoreDataWrapp>(responseText);



                // JsonUtility.FromJsonOverwrite(responseText, jsonOuts);
                //string finalFormated = JsonHelper.ToJsonList<HighScoreData>(jsonOuts);
                //Debug.Log(finalFormated);
                //foreach(HighScoreData json in jsonOuts)
                //{
                // Debug.Log("Usernam: " + json.Username + ": with score: " + json.Score);
                //}
                /*
                
            
                JSONNode jsonNode = JSON.Parse(responseText);
                int resultQty = int.Parse(jsonNode["</br>"]);

                for (int i = 0; i < resultQty; i++)
                {
                    string name = jsonNode["username"][i]["score"];
                    Debug.Log(name);
                    // And so on... 
                }*/
                if (responseText.StartsWith("Success"))
                {
                    registrationCompleted = true;
                }
                else
                {
                    errorMessage = responseText;
                }
            }
        }

        isWorking = false;
    }


}
