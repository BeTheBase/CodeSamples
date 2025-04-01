using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DBManager : MonoBehaviour
{
    public UnityEngine.UI.Text PlayerDisplay;


    private void Start()
    {
        if (LoggedIn)
        {
            PlayerDisplay.text = "Player: " + UserName;
        }
    }

    public static string UserName;
    public static string Score;
    public static bool LoggedIn { get { return UserName != null; } }

    public static void LogOut()
    {
        UserName = null;
    }
}
