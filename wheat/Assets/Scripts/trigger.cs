using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

class trigger : MonoBehaviour
{
    public Flowchart flowchart;
    private CoolCameraManager cameraMan;


    private void OnTriggerEnter(Collider other)
    {
        flowchart.ExecuteBlock("Tractor");
    }
}