using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : BaseController
{
    private Rigidbody rb;

    private Animator shootSliderAnim;

    public GameObject bulletPrefab;
    public Transform bulletStartPoint;
    public ParticleSystem shootFX;

    [HideInInspector] public bool canShoot;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        shootSliderAnim =GameObject.Find("FireBar").GetComponent<Animator>();

        GameObject.Find("ShootButton").GetComponent<Button>().onClick.AddListener(ShootingControl);
        canShoot =true;
    }

    void Update()
    {
        Movement();
        ChangeRotation();
    }

    void FixedUpdate()
    {
        MoveTank();   
    }

    void MoveTank()
    {
        rb.MovePosition(rb.position + speed * Time.deltaTime);
    }

    void Movement()
    {
        // Left
        if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            MoveLeft();
        }
        // ReleaseKey
        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A))
        {
            MoveStraight();
        }

        // Right
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            MoveRight();
        }
        // ReleaseKey
        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D))
        {
            MoveStraight();
        }

        // Up
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            MoveFast();
        }
        // ReleaseKey
        if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W))
        {
            MoveNormal();
        }

        // Down
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            MoveSlow();
        }
        // ReleaseKey
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            MoveNormal();
        }
    }

    void ChangeRotation()
    {
        if (speed.x > 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, maxAngle, 0f), Time.deltaTime * rotationSpeed);
        }
        else if(speed.x < 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f,-maxAngle, 0f), Time.deltaTime * rotationSpeed);
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f,0f, 0f), Time.deltaTime * rotationSpeed);
        }
    }

    public void ShootingControl()
    {
        if (Time.timeScale != 0)
        {
            if (canShoot)
            {
                GameObject bullet = Instantiate(bulletPrefab, bulletStartPoint.position, Quaternion.identity);
                bullet.GetComponent<BulletScripts>().Move(2000f);
                shootFX.Play();

                canShoot = false;

                shootSliderAnim.Play("Fill");


            }
           
        }
    }
}
