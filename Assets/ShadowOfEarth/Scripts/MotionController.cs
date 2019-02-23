using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class MotionController : MonoBehaviour
{
    [SerializeField]
    GameObject _controller;

    [SerializeField]
    LayerMask mask;

    [SerializeField]
    Material _earthMat;

    [SerializeField]
    float _range; 

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
            lr.startWidth = 0.01f; 
            lr.endWidth = 0.005f;
            lr.startColor = Color.green;
            lr.endColor = Color.red; 
            // 頂点の数
            lr.positionCount=2;
            lr.SetPosition(0, _controller.transform.position);
            lr.SetPosition(1, _controller.transform.position + _controller.transform.forward * 100.0f);
        }

        Ray(); 
    }

    void Ray()
    {
        float r = _earthMat.GetFloat("_Range"); 

        // 1.
        // Rayの作成
        Ray ray = new Ray(_controller.transform.position, _controller.transform.forward);
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        // 2.		
        // Rayが衝突したコライダーの情報を得る
        RaycastHit hit;
        // Rayが衝突したかどうか
        if (Physics.Raycast(ray, out hit, r + (100.0f - r) * 0.1f, mask))
        {
            _earthMat.SetFloat("_Range", _range);
            _earthMat.SetColor("_Center", new Color(hit.point.x, hit.point.y, hit.point.z, 0.0f));
            //hit.point
        } else
        {
            _earthMat.SetFloat("_Range", r + (0.0f - r) * 0.01f); 
        }


    }

    public void UpdateTransform(SteamVR_Behaviour_Pose p, SteamVR_Input_Sources srcs)
    {
    }
}
