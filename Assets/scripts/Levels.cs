using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Levels : MonoBehaviour
{
    public void loadLevel1()
    {
        SceneManager.LoadScene(2);
    }

    public void loadLevel2()
    {
        SceneManager.LoadScene(3);

    }
    public void loadLevel3()
    {
        SceneManager.LoadScene(4);
    }
}

