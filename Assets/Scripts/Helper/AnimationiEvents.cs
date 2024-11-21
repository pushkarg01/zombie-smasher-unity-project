using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationiEvents : MonoBehaviour
{
    private PlayerController controller;
    private Animator anim;
    void Start()
    {
        controller=GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        anim =GetComponent<Animator>();
    }

    void ResetShooting()
    {
        controller.canShoot = true;
        anim.Play("Idle");
    }

    void CameraStartGame()
    {
        SceneManager.LoadScene("Game");
    }
}
