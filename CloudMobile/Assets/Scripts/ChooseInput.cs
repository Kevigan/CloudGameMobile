using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseInput : MonoBehaviour
{
    public void ChooseTouch()
    {
        GameManager.Main.TouchInput = true;
        GameManager.Main.GyroScopeInput = false;
    }
    public void ChooseGyro()
    {
        GameManager.Main.TouchInput = false;
        GameManager.Main.GyroScopeInput = true;
    }
}
