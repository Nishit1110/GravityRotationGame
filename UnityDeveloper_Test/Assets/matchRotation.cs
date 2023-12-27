using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class matchRotation : MonoBehaviour
{
    [SerializeField] private Transform m_Transform;
    
    // Update is called once per frame
    void Update()
    {
        transform.rotation = m_Transform.rotation;    
    }
}
