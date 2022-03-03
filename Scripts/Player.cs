using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject player;
    public Image scopeImg;
    public Button scopeButton, fireButton;

    private bool gyroEnabled;
    private Gyroscope gyro;
    private GameObject GyroControl;
    private Quaternion rot;
    private Quaternion adjustrot;

    public void ScopePress()
    {
        scopeImg.enabled = true;
        scopeButton.GetComponent<Image>().enabled = false;
        fireButton.GetComponent<Image>().enabled = true;
    }

    void Start()
    {
        scopeImg.enabled = false;
        player = GetComponent<GameObject>();
        fireButton.GetComponent<Image>().enabled = false;


        GyroControl = new GameObject("Gyro Control");
        GyroControl.transform.position = transform.position;
        transform.SetParent(GyroControl.transform);
        gyroEnabled = EnableGyro();
        adjustrot = Quaternion.Euler(90f, 0f, 0f) * Quaternion.Inverse(gyro.attitude);
    }

    private bool EnableGyro()
    {
        if (SystemInfo.supportsGyroscope)
        {
            gyro = Input.gyro;
            gyro.enabled = true;
            GyroControl.transform.rotation = Quaternion.Euler(90f, -90f, 0f);
            rot = new Quaternion(0, 0, 1, 0);
            return true;
        }
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gyroEnabled)
        {
            if (Input.touchCount == 3)
            {
                adjustrot = Quaternion.Euler(90f, 0f, 0f) * Quaternion.Inverse(gyro.attitude);
            }
            transform.localRotation = adjustrot * gyro.attitude * rot;
        }

    }
}
