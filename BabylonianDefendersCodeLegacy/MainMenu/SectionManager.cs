using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SectionManager : MonoBehaviour
{
    public void LoadLevel(int index)
    {
        SceneManager.LoadSceneAsync(index);
    }


}
