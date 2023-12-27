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

    bool canChangeGravity;

    Quaternion targetRotation;

    private void Awake()
    {
        canChangeGravity = false;
        DisableHolograms();
        Debug.Log(Physics.gravity);
        targetRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow)) //calculate player's forward direction
        {
            DisableHolograms();
            holoLocations[0].SetActive(true);
            location = Child.transform.forward * 9.81f;
            Debug.Log(location);
            canChangeGravity = true;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow)) //calculate player's Right direction
        {
            DisableHolograms();
            holoLocations[1].SetActive(true);
            location = Child.transform.right * 9.81f;
            Debug.Log(location);
            canChangeGravity = true;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow)) //calculate player's Left direction
        {
            DisableHolograms();
            holoLocations[3].SetActive(true);
            location = Child.transform.right * -9.81f;
            Debug.Log(location);
            canChangeGravity = true;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow)) //calculate player's Backward direction
        {
            DisableHolograms();
            holoLocations[2].SetActive(true);
            location = Child.transform.forward * -9.81f;
            Debug.Log(location);
            canChangeGravity = true;
        }
        if (Input.GetKeyDown(KeyCode.LeftControl)) //Apply gravity changes
        {
            if (canChangeGravity)
            {
                x = y = z = 0;
                x = location.x;
                y = location.y;
                z = location.z;

                targetRotation = Quaternion.Euler(0, 0, 0);

                //calculate direction on which gravity shuld apply
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



                Physics.gravity = new Vector3(x, y, z);

                Debug.Log(Physics.gravity);
                DisableHolograms();
                canChangeGravity = false;
            }
        }
        transform.rotation = targetRotation; //Rotate player according to direction of gravity
    }

    private void DisableHolograms()
    {
        foreach (GameObject game in holoLocations)
        {
            game.SetActive(false);
        }
    }
}
