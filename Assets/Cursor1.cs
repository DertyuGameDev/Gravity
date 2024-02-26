using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor1 : MonoBehaviour
{
    void Update()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
