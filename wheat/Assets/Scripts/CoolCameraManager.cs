using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolCameraManager : MonoBehaviour
{
    public enum Cam {
        Main,
        Tractor
    }

    public GameObject mainCam;
    public GameObject tractorCam;

    private GameObject currentCamGameObject;
    private Cam currentCam;

    private void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }

    private void Start()
    {
        currentCam = Cam.Main;
        currentCamGameObject = mainCam;
    }

    public void ChangeCamera(Cam newCam)
    {
        currentCamGameObject.SetActive(false);
        currentCam = newCam;

        switch(newCam) {
            case Cam.Main:
                currentCamGameObject = mainCam;
            break;
            case Cam.Tractor:
                currentCamGameObject = tractorCam;
            break;
        }
        currentCamGameObject.SetActive(true);
    }
}
