using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNewScene : MonoBehaviour
{
    public void LoadNS (int sceneNum)
    {
        SceneManager.LoadScene(sceneNum);
    }
}
