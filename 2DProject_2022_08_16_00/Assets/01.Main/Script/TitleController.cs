using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour
{
    public void GoNextScene()
    {
        LoadSceneManager.Instance.LoadSceneAsync(LoadSceneManager.SceneState.Game);
    }
   
    void Start()
    {
       
    }
}
