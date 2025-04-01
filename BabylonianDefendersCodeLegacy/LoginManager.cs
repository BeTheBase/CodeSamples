using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour
{
    public InputField NameField;
    public InputField PasswordField;

    public Button SubmitButton;

    public void CallLogin()
    {
        StartCoroutine(Login());
    }

    IEnumerator Login()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", NameField.text);
        form.AddField("password", PasswordField.text);
        UnityWebRequest www = UnityWebRequest.Post("https://studenthome.hku.nl/~bas.dekoningh/login2.php", form);

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            DBManager.UserName = NameField.text;
            DBManager.Score = www.downloadHandler.text.Split('\t')[1];
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
    }
}
