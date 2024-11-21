using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public float distance = 6.3f;
    public float height = 3.5f;

    public float heightDamping = 3.25f;
    public float rotationDamping = 0.27f;

    void Start()
    {
        target =GameObject.FindGameObjectWithTag("Player").transform;
    }

    
    void LateUpdate()
    {
        FollowPlayer();
    }

    void FollowPlayer()
    {
        float wantedRotation = target.eulerAngles.y;
        float wantedHeight = target.position.y + height;

        float currentRotation = transform.eulerAngles.y;
        currentRotation = Mathf.LerpAngle(currentRotation, wantedRotation, rotationDamping * Time.deltaTime);

        float currentHeight = transform.position.y;
        currentHeight = Mathf.Lerp(currentHeight, wantedHeight,heightDamping* Time.deltaTime);

        Quaternion rotation = Quaternion.Euler(0,currentRotation, 0);

        transform.position =target.position;
        transform.position -= rotation*Vector3.forward *distance;

        transform.position =new Vector3(transform.position.x,currentHeight,transform.position.z);


    }
}
