using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.XR;

public class TestController : MonoBehaviour
{
    public string controllerName = "LeftHand";
    private InputDevice targetDevice;
    
    void TryInitializeDevice()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevices(devices);
        Debug.Log(devices);
        foreach (var device in devices)
        {
            Debug.Log(device.name);
        }
    }
    void Start()
    {
        TryInitializeDevice();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
