﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{

    public GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        if (Input.GetKey("escape"))
        {
          Application.Quit();
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        this.transform.position = new Vector3(target.transform.position.x, this.transform.position.y, this.transform.position.z);
    }
}
