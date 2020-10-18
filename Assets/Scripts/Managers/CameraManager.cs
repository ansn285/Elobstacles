using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraManager : MonoBehaviour
{
    // Main Cam pos: -.327 .861 -4.653 ; rot: 9.40 -2.498 0
    // Cam pos: -.327 .861 -4.653 ; rot: 9.40 -2.498 0
    // LookAt pos: -.53 .09 0

    public static CameraManager cameraInstance;

    private static GameObject cam, lookAt, renderCam;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (cameraInstance == null)
        {
            cameraInstance = this;
        }

        else if (cameraInstance != this)
        {
            Destroy(gameObject);
        }

        cam = transform.Find("Cam").gameObject;
        lookAt = transform.Find("CameraLookAt").gameObject;
        renderCam = transform.Find("Main Camera").gameObject;
    }

    private void Update()
    {
        cam = transform.Find("Cam").gameObject;
        lookAt = transform.Find("CameraLookAt").gameObject;
        renderCam = transform.Find("Main Camera").gameObject;

        //if (SceneManager.GetActiveScene().name == "MainMenu")
        //{
        //    cam.transform.localPosition = new Vector3(-.327f, .861f, -4.653f);
        //    lookAt.transform.localPosition = new Vector3(-.53f, .09f, 0);
        //    renderCam.transform.localPosition = new Vector3(-.327f, .861f, -4.653f);

        //    GameObject.Find("Timelines/MenuToLevel").GetComponent<LevelStartManager>().SetObjects();
        //}
    }

    public static GameObject GetCamera()
    {
        return cam;
    }

    public static GameObject GetLookAt()
    {
        return lookAt;
    }

    public static GameObject GetRenderCamera()
    {
        return renderCam;
    }
}
