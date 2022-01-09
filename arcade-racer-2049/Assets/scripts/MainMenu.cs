using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private bool isPlaySelected;
    private TextMesh playTextMesh;
    private TextMesh quitTextMesh;

    void Awake()
    {
        playTextMesh = GameObject.Find("mainPlay").GetComponent<TextMesh>();
        quitTextMesh = GameObject.Find("mainQuit").GetComponent<TextMesh>();
    }

    private void Start()
    {
        playTextMesh.color = Color.red;
        isPlaySelected = true;
    }

    void Update()
    {
        if (Input.GetKey("down"))
        {
            if (isPlaySelected)
            {
                isPlaySelected = false;
                playTextMesh.color = Color.white;
                quitTextMesh.color = Color.red;
            }
        }

        if (Input.GetKey("up"))
        {
            if (!isPlaySelected)
            {
                isPlaySelected = true;
                playTextMesh.color = Color.red;
                quitTextMesh.color = Color.white;
            }
        }

        if (Input.GetKeyDown("return") || Input.GetKeyDown(KeyCode.Return))
        {
            if (!isPlaySelected)
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
