using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Fixedselected : MonoBehaviour
{
    public GameObject gameObject;
    private bool gameControllerConnected = false;

    void Awake() {
        
        var allGamepads = Gamepad.current;
        if (allGamepads != null) Cursor.visible = false;

        InputSystem.onDeviceChange +=
        (device, change) =>
        {
            switch (change)
            {
                case InputDeviceChange.Added:
                    gameControllerConnected = true;
                    break;
                case InputDeviceChange.Disconnected:
                    gameControllerConnected = false;
                    break;
                case InputDeviceChange.Reconnected:
                    gameControllerConnected = true;
                    break;
                case InputDeviceChange.Removed:
                    gameControllerConnected = false;
                    break;
                default:
                    var allGamepads = Gamepad.current;
                    if (allGamepads != null) gameControllerConnected = true;
                    break;
            }
        };
    }
    void Update()
    {

        if (gameControllerConnected)
        {
            if (EventSystem.current.currentSelectedGameObject == null)
            {
                EventSystem.current.SetSelectedGameObject(gameObject);
            }
            else
            {
                gameObject = EventSystem.current.currentSelectedGameObject;
            }
            InputSystem.DisableDevice(Mouse.current);
            Cursor.visible = false;
        }
        else {
            EventSystem.current.SetSelectedGameObject(null);
            InputSystem.EnableDevice(Mouse.current);
            Cursor.visible = true;
        }
        
    }
    
}
