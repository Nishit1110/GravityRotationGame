using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSCrpipt : MonoBehaviour
{
    [SerializeField] Transform game;

    // Update is called once per frame
    void Update()
    {
        Debug.Log(game);
    }
}