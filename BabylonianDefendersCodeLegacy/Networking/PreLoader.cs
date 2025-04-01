using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PreLoader : MonoBehaviour
{
    public GameObject ClientPrefab;
    public GameObject ServerPrefab;

    public void OnBothServerClient()
    {
        DontDestroyOnLoad(Instantiate(ClientPrefab.gameObject));
        DontDestroyOnLoad(Instantiate(ServerPrefab.gameObject));

        
    }
    public void OnClientInit()
    {
        DontDestroyOnLoad(Instantiate(ClientPrefab.gameObject));
        //Load lobby
    }

    public void OnServerInit()
    {
        DontDestroyOnLoad(Instantiate(ServerPrefab.gameObject));
        //Load lobby
    }
}
