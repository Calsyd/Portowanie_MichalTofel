using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointShield : MonoBehaviour
{
    private Shield shieldIndex;
    private float timerFree = 0; //zapobiega nak³adaniu siê tarcz
    private void Awake()
    {
        InvokeRepeating(nameof(CountTimer), 1, 1);
    }

    public void ImputShield(Shield _shield)
    {
        shieldIndex = _shield;
        timerFree = 7;
    }
    public bool IsFree()
    {
        if (timerFree > 0)
            return false;
        if (shieldIndex == null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private void CountTimer()
    {
        if (timerFree > 0)
            timerFree--;
    }
}
