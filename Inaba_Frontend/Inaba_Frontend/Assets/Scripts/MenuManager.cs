using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Diagnostics;

public class MenuManager : MonoBehaviour
{

    [SerializeField] GameObject settingMenuObj;
    [SerializeField] GameObject mainConsoleObj;
    [SerializeField] GameObject MainButtonA;
    [SerializeField] GameObject MainButtonB;

    bool isConsoleOpen;



    public void SwitchConsole()
    {
        if (!isConsoleOpen)
        {
            mainConsoleObj.SetActive(true);
            MainButtonA.SetActive(false);
            MainButtonB.SetActive(true);
            isConsoleOpen = true;
            return;
        }else{
            mainConsoleObj.SetActive(false);
            MainButtonA.SetActive(true);
            MainButtonB.SetActive(false);
            isConsoleOpen = false;
            return;
        }
        
    }


    public void OpenSetting()
    {
        settingMenuObj.SetActive(true);
    }

    public void CloseSetting()
    {
        settingMenuObj.SetActive(false);
    }

}
