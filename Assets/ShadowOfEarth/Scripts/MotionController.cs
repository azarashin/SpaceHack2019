using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class MotionController : MonoBehaviour
{
    [SerializeField]
    GameObject _controller; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        LineRenderer lr = GetComponent<LineRenderer>();
        if (lr != null)
        {
            lr.startWidth = 0.1f; 
            lr.endWidth = 0.05f; 
            // 頂点の数
            lr.positionCount=2;
            lr.SetPosition(0, _controller.transform.position);
            lr.SetPosition(1, _controller.transform.position + _controller.transform.forward * 100.0f);
        }
        Debug.Log(gameObject.transform.position);

    }

    public void UpdateTransform(SteamVR_Behaviour_Pose p, SteamVR_Input_Sources srcs)
    {
    }
}
