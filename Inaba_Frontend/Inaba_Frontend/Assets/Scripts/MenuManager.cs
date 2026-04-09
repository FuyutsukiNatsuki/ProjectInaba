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
using Unity.VisualScripting;

public class MenuManager : MonoBehaviour
{

    [SerializeField] GameObject settingMenuObj;
    [SerializeField] GameObject mainConsoleObj;
    [SerializeField] GameObject MainButtonA;
    [SerializeField] GameObject MainButtonB;
    [SerializeField] TMP_InputField UserNameInput;
    [SerializeField] GameObject generalSettingObj;
    [SerializeField] GameObject LogWindowObj;

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
        generalSettingObj.SetActive(true);
        LogWindowObj.SetActive(false);
    }

    public void CloseSetting()
    {
        settingMenuObj.SetActive(false);
        generalSettingObj.SetActive(false);
        LogWindowObj.SetActive(false);
    }


    public void NameSaveButton()
    {
        NameSave(UserNameInput.text);
    }
    public void NameSave(string UserName)
    {
        PlayerPrefs.SetString("UserName",UserName);
        PlayerPrefs.Save(); // 忘れずにセーブ！
    }
    
    public void OpenLogWindow()
    {
        settingMenuObj.SetActive(true);
        generalSettingObj.SetActive(false);
        LogWindowObj.SetActive(true);
        
    }

    void Start()
    {
        string savedName = PlayerPrefs.GetString("UserName", "");
        UserNameInput.text = savedName;
    }

}
