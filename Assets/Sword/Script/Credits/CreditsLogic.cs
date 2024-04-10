using System.Collections;
using System.Collections.Generic;
using TarodevController;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CreditsLogic : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textDisplay;

    [SerializeField] private AudioSource logText;
    [SerializeField] private AudioSource errorText;

    private PlayerInputActions _actions;
    private InputAction _timeJump;
    
    private string[] lines = {
        "[Log]: <running function> TheEnd();",
        "[Err]: Function \"TheEnd\" halted! Trying again!",
        "[Err]: Failed reloading function \"TheEnd\"",
        "[Err]: Detected a leak in developer_quotes.bbs file. Printing the error log...",
        "",
        "[Log]: Feel free to skip this message (TimeJump button)...",
        "",
        "[Log]: \"Harrish Chithrangan\": {\"Role\": \"Visuals/SFX & Music\"}",
        "[Log]: Harrish said: \"True...\"",
        "",
        "[Log]: \"Kylan Cingolani\": {\"Role\": \"Level Design\"}",
        "[Log]: Kylan said: \"I think we should add time travel to the game!\"",
        "",
        "[Log]: \"Ryan Cheng\": {\"Role\": \"UI\"}",
        "[Log]: Ryan said: \"Anita Max Wynn!\"",
        "",
        "[Log]: \"Sidharth Suresh\": {\"Role\": \"Programming\"}",
        "[Log]: Sid said: \"This project was a whole lot of spaghetti and duct tape, lol!\"",
        "",
        "[Log]: \"Udey Goraya\": {\"Role\": \"Programming\"}",
        "[Log]: Udey said: \"With banana bread you can do anything!\"",
        "",
        "[Log]: \"Samuel\": {\"Role\": \"Story Writer\"}",
        "[Log]: Samuel said: \"...\"",
        "",
        "[Log]: Error log ended.",
        "[Log]: See ya later...",
        "[Log]: alligator!"
    };

    private void Awake()
    {
        _actions = new PlayerInputActions();
        _timeJump = _actions.Player.TImeJump;
    }
    
    private void OnEnable() => _actions.Enable();

    private void OnDisable() => _actions.Disable();

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartAnimation());
    }

    private IEnumerator StartAnimation()
    {
        for (int i = 0; i < lines.Length; i++)
        {
            textDisplay.text += lines[i]+"\n";
            if (lines[i].Contains("[Err]"))
            {
                errorText.Play();
            }
            else
            {
                logText.Play();
            }
            if (i < lines.Length-1)
            {
                yield return new WaitForSeconds(lines[i].Length*0.07f);
            }
            else
            {
                yield return new WaitForSeconds(2);
                SceneManager.LoadScene("Main menu");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_timeJump.WasPressedThisFrame())
        {
            SceneManager.LoadScene("Main menu");
        }
    }
}
