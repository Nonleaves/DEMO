using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{

    [RuntimeInitializeOnLoadMethod]
    static void Initialize()
    {
        if (SceneManager.GetActiveScene().name == "LoginScene")
        {
            return;
        }
        SceneManager.LoadScene("LoginScene");
    }
}
