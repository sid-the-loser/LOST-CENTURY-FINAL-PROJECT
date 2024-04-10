using System;
using System.Collections;
using System.Collections.Generic;
using TarodevController;
using UnityEngine;

public class SwordPedestal : MonoBehaviour
{
    [SerializeField] private GameObject[] turnOff;
    [SerializeField] private GameObject[] turnOn;
    [SerializeField] private bool isTutorial;

    

    private bool used;
    private Collider2D cldr;
    
    // Start is called before the first frame update
    void Start()
    {
        cldr = GetComponent<Collider2D>();
        cldr.isTrigger = true;
        foreach (var obj in turnOff)
        {
            obj.SetActive(true);
        }
        foreach (var obj in turnOn)
        {
            obj.SetActive(true);
        }
        FindObjectOfType<PlayerController>().SetSwordInhand(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !used)
        {
            foreach (var obj in turnOff)
            {
                obj.SetActive(false);
                FindObjectOfType<PlayerController>().SetSwordInhand(true);

                if (isTutorial)
                {
                    FindObjectOfType<PlayerController>().TimeTravel();

                }
            }

            used = true;
            cldr.isTrigger = false;
        }
    }
}
