using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMenuManager : MonoBehaviour
{
    public GameObject titleMenuButtonGroup;
    public GameObject instructionsGroup;

    public void StartEditor()
    {
        SceneManager.LoadScene("EditorScreen");
    }

    public void Instructions()
    {
        titleMenuButtonGroup.SetActive(false);
        instructionsGroup.SetActive(true);

    }

    public void BackToTitleScreen()
    {
        instructionsGroup.SetActive(false);
        titleMenuButtonGroup.SetActive(true);
    }
}
