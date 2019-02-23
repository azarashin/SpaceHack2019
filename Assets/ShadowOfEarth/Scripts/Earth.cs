using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earth : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float speed = 1.0f;
        transform.Rotate(new Vector3(0, Time.deltaTime * speed, 0)); 
    }
}
