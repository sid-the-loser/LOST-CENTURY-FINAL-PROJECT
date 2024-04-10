using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int maxSeals = 3;
    public int sealsBroken;
    
    [SerializeField] private string nextSceneName;

    [SerializeField] private Portal endPortal;
    
    private string currentSceneName;

    private void Start()
    {
        currentSceneName = SceneManager.GetActiveScene().name;
        endPortal = FindObjectOfType<Portal>();
    }

    private void Update()
    {
        if (sealsBroken >= maxSeals && !endPortal.GetUnlocked())
        {
            // print("All seals broken!");
            endPortal.SetUnlocked(true);
        }

        if (Input.GetKeyUp(KeyCode.Escape) || Gamepad.current.startButton.wasPressedThisFrame)
        {
            SceneManager.LoadScene("Main menu");
        }
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ReloadCurrentScene()
    {
        SceneManager.LoadScene(currentSceneName);
    }
    
    public void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
