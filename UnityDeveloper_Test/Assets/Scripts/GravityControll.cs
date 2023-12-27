using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class GravityControll : MonoBehaviour
{
    [SerializeField] GameObject[] holoLocations;
    [SerializeField] Transform hologram;
    [SerializeField] Transform Child;
    Vector3 location;
    float x, y, z;

    Quaternion targetRotation;

    private void Awake()
    {
        DisableHolograms();
        Debug.Log(Physics.gravity);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            DisableHolograms();
            holoLocations[0].SetActive(true);
            location = Child.transform.forward * 9.81f;
            Debug.Log(location);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            DisableHolograms();
            holoLocations[1].SetActive(true);
            location = Child.transform.right * 9.81f;
            Debug.Log(location);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            DisableHolograms();
            holoLocations[3].SetActive(true);
            location = Child.transform.right * -9.81f;
            Debug.Log(location);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            DisableHolograms();
            holoLocations[2].SetActive(true);
            location = Child.transform.forward * -9.81f;
            Debug.Log(location);
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            x = y = z = 0;
            x = location.x;
            y = location.y;
            z = location.z;

            targetRotation = Quaternion.Euler(0, 0, 0);
            
            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                if (Mathf.Abs(x) > Mathf.Abs(z))
                {
                    if (x > 0)
                    {
                        x = 9.81f;
                        targetRotation = Quaternion.Euler(0f, 0f, 90f);
                    }
                    else
                    {
                        x = -9.81f;
                        targetRotation = Quaternion.Euler(0f, 0f, -90f);
                    }
                    y = 0;
                    z = 0;
                } //X is largest
                else
                {
                    if (z > 0)
                    {
                        z = 9.81f;
                        targetRotation = Quaternion.Euler(-90, 0f, 0f);
                    }
                    else
                    {
                        z = -9.81f;
                        targetRotation = Quaternion.Euler(90, 0f, 0f);
                    }
                    x = 0;
                    y = 0;
                } //Z is largest
            }
            else if (Mathf.Abs(y) > Mathf.Abs(z))
            {
                if (y > 0)
                {
                    y = 9.81f;
                    targetRotation = Quaternion.Euler(180f, 0f, 0f);
                }
                else
                {
                    y = -9.81f;
                    targetRotation = Quaternion.Euler(0f, 0f, 0f);
                }
                x = 0;
                z = 0;
            } //Y is largest
            else
            {
                if (z > 0)
                {
                    z = 9.81f;
                    targetRotation = Quaternion.Euler(-90, 0f, 0f);
                }
                else
                {
                    z = -9.81f;
                    targetRotation = Quaternion.Euler(90, 0f, 0f);
                }
                x = 0;
                y = 0;
            } //Z is largest
            
            

            Physics.gravity = new Vector3(x,y,z);

            Debug.Log(Physics.gravity);
            DisableHolograms();
            
            transform.rotation = targetRotation;
        }
    }

    private void DisableHolograms()
    {
        foreach (GameObject game in holoLocations)
        {
            game.SetActive(false);
        }
    }
}
