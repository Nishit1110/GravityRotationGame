using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class GravityControll : MonoBehaviour
{
    [SerializeField] GameObject[] holoLocations;
    [SerializeField] Transform hologram;
    [SerializeField] Transform Child;
    Vector3 location;

    private void Awake()
    {
        DisableHolograms();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            DisableHolograms();
            holoLocations[0].SetActive(true);
            hologram = holoLocations[0].transform;
            location = Child.transform.forward * 9.81f;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            DisableHolograms();
            holoLocations[1].SetActive(true);
            hologram = holoLocations[1].transform;
            location = Child.transform.right * 9.81f;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            DisableHolograms();
            holoLocations[3].SetActive(true);
            hologram = holoLocations[3].transform;
            location = Child.transform.right * -9.81f;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            DisableHolograms();
            holoLocations[2].SetActive(true);
            hologram = holoLocations[2].transform;
            location = Child.transform.forward * -9.81f;
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Physics.gravity = location;
            DisableHolograms();
            Quaternion combinedRotation = Child.transform.rotation * hologram.rotation;

            transform.rotation = combinedRotation;
            transform.SetPositionAndRotation(hologram.position, hologram.rotation);
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
