using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScripts : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    public void Move(float speed)
    {
        rb.AddForce(transform.forward.normalized *speed);
        Invoke("DeactivateGO",4f);
    }

    void DeactivateGO()
    {
        gameObject.SetActive(false);    
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            gameObject.SetActive(false);
        }
    }
}
