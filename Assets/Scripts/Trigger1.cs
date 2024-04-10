using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger1 : MonoBehaviour
{
    [SerializeField] private GameObject blocker;
    
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        blocker.SetActive(true);
        Destroy(this);
    }
}
