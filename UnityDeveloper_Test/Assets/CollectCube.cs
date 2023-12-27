using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCube : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.points++;
            Debug.Log(GameManager.Instance.points);
            Destroy(gameObject);
        }
    }
}