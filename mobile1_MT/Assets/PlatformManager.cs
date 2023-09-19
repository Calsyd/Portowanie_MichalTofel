using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    private void Awake()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            Debug.Log("Initial google");
        }
        if (Application.platform == RuntimePlatform.WindowsPlayer)
        {
            Debug.Log("Initial steam");
        }
    }
}
