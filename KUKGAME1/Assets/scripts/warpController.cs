using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class warpController : MonoBehaviour
{
    public string sceneName;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Invoke("LoadNextScene", 1);
            /*if(warpSound != null)
            {
                warpSound.play();
            }*/
        }
    }
    void LoadNextScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}

