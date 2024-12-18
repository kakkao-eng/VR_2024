using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour
{
    public float speed = 50f;
    public float lifeTime = 5f;

    private Rigidbody rb;
    private ScoreManager _scoreManager;
    
    // Start is called before the first frame update
    void Start()
    {
        _scoreManager = FindObjectOfType<ScoreManager>();
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Target"))
        {
            if (_scoreManager != null)
            {
                _scoreManager.AddScore(1);
            }
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }

}
