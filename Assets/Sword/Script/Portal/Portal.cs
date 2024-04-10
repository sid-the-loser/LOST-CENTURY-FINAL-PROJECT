using System;
using System.Collections;
using System.Collections.Generic;
using TarodevController;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private SpriteRenderer _renderer;
    
    private bool unlocked;

    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (unlocked)
        {
            _renderer.color = Color.white;
        }
        else
        {
            _renderer.color = Color.red;
        }
    }

    public void SetUnlocked(bool value)
    {
        unlocked = value;
    }

    public bool GetUnlocked()
    {
        return unlocked;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && unlocked)
        {
            //Audio
            //other.GetComponent<PlayerController>()._levelEnd.Play();
            // new WaitForSeconds(5f);
            FindObjectOfType<GameManager>().LoadNextScene();
        }
    }
}
