using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class endScreen : MonoBehaviour
{
    private bool isRetrySelected;
    private TextMesh retryTextMesh;
    private TextMesh quitTextMesh;

    void Awake()
    {
        retryTextMesh = GameObject.Find("endRetry").GetComponent<TextMesh>();
        quitTextMesh = GameObject.Find("endQuit").GetComponent<TextMesh>();
    }

    private void Start()
    {
        retryTextMesh.color = Color.red;
        isRetrySelected = true;
    }

    void Update()
    {
        if (Input.GetKey("down"))
        {
            if (isRetrySelected)
            {
                isRetrySelected = false;
                retryTextMesh.color = Color.white;
                quitTextMesh.color = Color.red;
            }
        }

        if (Input.GetKey("up"))
        {
            if (!isRetrySelected)
            {
                isRetrySelected = true;
                retryTextMesh.color = Color.red;
                quitTextMesh.color = Color.white;
            }
        }

        if (Input.GetKeyDown("return") || Input.GetKeyDown(KeyCode.Return))
        {
            if (!isRetrySelected)
            {
                Application.Quit();
            }
            else
            {
                SceneManager.LoadScene(1, LoadSceneMode.Single);
            }
        }
    }
}
