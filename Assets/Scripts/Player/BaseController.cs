using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    public Vector3 speed;
    public float horizontalSpeed = 8f, forwardSpeed = 15f;
    public float acceleratedSpeed = 15f, deacceleratedSpeed = 10f;

    protected float rotationSpeed = 10f;
    protected float maxAngle = 10f;

    // public float lowSoundPitch, normalSoundPitch, highSoundPitch;
    public AudioSource audioSource;
    // public AudioClip engineOnSound, engineOffSound;

    private bool isSlow;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        speed = new Vector3(0, 0, forwardSpeed);
    }

    protected void MoveLeft()
    {
        speed = new Vector3(-horizontalSpeed / 2f, 0, speed.z);
    }
    protected void MoveRight()
    {
        speed = new Vector3(horizontalSpeed / 2f, 0, speed.z);
    }

    protected void MoveStraight()
    {
        speed = new Vector3(0, 0, speed.z);
    }


    protected void MoveNormal()
    {
        if (isSlow)
        {
            isSlow = false;
        }
        speed = new Vector3(speed.x, 0, forwardSpeed);
    }

    protected void MoveSlow()
    {
        if (!isSlow)
        {
            isSlow = true;
            // audioSource.Stop();
            // audioSource.PlayOneShot(engineOffSound, 0.3f);
        }
        speed = new Vector3(speed.x, 0, deacceleratedSpeed);
    }

    protected void MoveFast()
    {
        speed = new Vector3(speed.x, 0, acceleratedSpeed);

       // audioSource.Stop();
       // audioSource.PlayOneShot(engineOnSound, 0.1f);
    }

}
