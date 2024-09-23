using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float torqueAmount = 1f;
    [SerializeField] float boostspeed = 30f;
    [SerializeField] float basespeed = 10f;
    bool canmove = true;
    private Vector3 respawnPoint;
    Rigidbody2D rb2d;
    SurfaceEffector2D surfaceEffector2D;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        surfaceEffector2D = FindObjectOfType<SurfaceEffector2D>();
        respawnPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(canmove)
        {
            rotateplayer();
            respondtoboost();
        }

    }
    public void disablecontrols()
    {
        canmove = false;
    }
    public void enablecontrols()
    {
        canmove = true;
    }

    void respondtoboost()
    {
        if(Input.GetKey(KeyCode.UpArrow))
        {
            surfaceEffector2D.speed = boostspeed;
        }
        else
        {
            surfaceEffector2D.speed = basespeed;
        }
    }

    void rotateplayer()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {

            rb2d.AddTorque(torqueAmount);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            rb2d.AddTorque(-torqueAmount);
        }
    }
}
