using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class UpdateMotion : MonoBehaviour
{
    private Vector3 m_motionPositionEnd;

    private Vector3 pas;
    private float m_motionTime = 1f; 
    void Update()
    {
        if ((m_motionPositionEnd - transform.position).magnitude > 1f){
            transform.position += pas*Time.deltaTime;
        }
        else { Destroy(this); }
    }

    public void Init(Vector3 motionEndPosition, float time )
    {
        m_motionPositionEnd = motionEndPosition;
        m_motionTime = time;
        pas = (motionEndPosition - transform.position)/m_motionTime;

    }
}
