using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    private Camera Camera;

    void Start()
    {
        Camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    void Update()
    {
        Vector3 v = Camera.transform.position - transform.position;
        v.x = v.z = 0.0f;
        transform.LookAt(Camera.transform.position - v);
        transform.Rotate(0, 180, 0);
    }
}
