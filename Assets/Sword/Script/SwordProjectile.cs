using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class SwordProjectile : MonoBehaviour
{
    public float projAngle;
    public bool collided = false;
    private float launchVel = 20f;
    private float deleteLaSwordTimer = 6f;
    
    [SerializeField] private Rigidbody2D rb;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private IEnumerator DeleteLaSword()
    {
        yield return new WaitForSeconds(deleteLaSwordTimer);
        collided = true;
        print("Sword is back!");
    }

    public void LaunchSword()
    {
        transform.rotation = Quaternion.Euler(0, 0, (projAngle+270));
        //rb.velocity = (transform.forward * launchVel);
        
        float velx = launchVel * Mathf.Cos(projAngle*Mathf.Deg2Rad);
        float vely = launchVel * Mathf.Sin(projAngle*Mathf.Deg2Rad);
        rb.velocity = new Vector2(velx, vely);

        StartCoroutine(DeleteLaSword());
        print(projAngle);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("PlayerRange") || other.CompareTag("SwordIgnore")) return;
        collided = true;
        print(other.tag);
        print("Sword is back!");
    }
}
