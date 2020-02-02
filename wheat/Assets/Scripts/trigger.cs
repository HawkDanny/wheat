using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

class trigger : MonoBehaviour
{
    public Flowchart flowchart;
    private CoolCameraManager cameraMan;
    public AudioManager audioMan;

    private void Start()
    {
        cameraMan = GameObject.FindGameObjectWithTag("CameraManager").GetComponent<CoolCameraManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        cameraMan.ChangeCamera(CoolCameraManager.Cam.Tractor);
        flowchart.ExecuteBlock("Tractor");
    }

    private void OnTriggerExit(Collider other)
    {
        cameraMan.ChangeCamera(CoolCameraManager.Cam.Main);
    }

    public void PlayOutro()
    {
        print("outro played");
        audioMan.Play("outro");
    }
}