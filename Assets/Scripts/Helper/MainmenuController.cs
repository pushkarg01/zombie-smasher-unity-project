using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainmenuController : MonoBehaviour
{
    public Animator cameraAnim;

    public void PlayGame()
    {
        cameraAnim.Play("Camera1");
    }
}
