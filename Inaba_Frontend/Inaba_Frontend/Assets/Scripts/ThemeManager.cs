using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Diagnostics;

public class ThemeManager : MonoBehaviour
{
    [SerializeField] UnityEngine.UI.Image[] ButtonImages;
    [SerializeField] UnityEngine.UI.Image[] WindowBase;
    [SerializeField] UnityEngine.UI.Image[] TextArea;
    [SerializeField] UnityEngine.UI.Image[] SettingBase;
    [SerializeField] UnityEngine.UI.Image[] SettingAccent;
    [SerializeField] UnityEngine.UI.Image[] WindowTitle;
    [SerializeField] UnityEngine.UI.Image[] ScrollArea;
    [SerializeField] UnityEngine.UI.Image[] ScrollBar;
    [SerializeField] TextMeshProUGUI[] BlackBaseTexts;
    [SerializeField] TextMeshProUGUI[] WhiteBaseTexts;
    public int currentThemeID;
    // Start is called before the first frame update
    void Start()
    {
        if (currentThemeID == 0){
            ThemeSet0();
        }else if(currentThemeID == 1)
        {
            ThemeSet1();
        }else if(currentThemeID == 2)
        {
            ThemeSet2();
        }else if(currentThemeID == 3)
        {
            ThemeSet3();
        }else if(currentThemeID == 4)
        {
            ThemeSet4();
        }else if(currentThemeID == 5)
        {
            ThemeSet5();
        }else if(currentThemeID == 6)
        {
            ThemeSet6();
        }else if(currentThemeID == 7)
        {
            ThemeSet7();
        }else if(currentThemeID == 8)
        {
            ThemeSet8();
        }else if(currentThemeID == 9)
        {
            ThemeSet9();
        }
        else
        {
            ThemeSet0();
        }
    }

    public void ThemeSet0() //デフォルト黒系
    {
        currentThemeID = 0;
        for (int i = 0; i < ButtonImages.Length; i++)
        {
            ButtonImages[i].color = new Color32(235,235,235,235); //色指定
        }
        for (int i = 0; i < WindowBase.Length; i++)
        {
            WindowBase[i].color = new Color32(0,0,0,200); //色指定
        }
        for (int i = 0; i < TextArea.Length; i++)
        {
            TextArea[i].color = new Color32(19,19,19,240); //色指定
        }
        for (int i = 0; i < SettingBase.Length; i++)
        {
            SettingBase[i].color = new Color32(31,31,31,255); //色指定
        }
        for (int i = 0; i < SettingAccent.Length; i++)
        {
            SettingAccent[i].color = new Color32(55,55,55,255); //色指定
        }
        for (int i = 0; i < WindowTitle.Length; i++)
        {
            WindowTitle[i].color = new Color32(60,60,60,255); //色指定
        }
        for (int i = 0; i < ScrollArea.Length; i++)
        {
            ScrollArea[i].color = new Color32(0,0,0,250); //色指定
        }
        for (int i = 0; i < ScrollBar.Length; i++)
        {
            ScrollBar[i].color = new Color32(65,65,65,240); //色指定
        }
        for (int i = 0; i < BlackBaseTexts.Length; i++)
        {
            BlackBaseTexts[i].color = new Color32(0,0,0,255); //色指定
        }
        for (int i = 0; i < WhiteBaseTexts.Length; i++)
        {
            WhiteBaseTexts[i].color = new Color32(255,255,255,255); //色指定
        }
    }

    public void ThemeSet1() //白系
    {
        currentThemeID = 1;
        for (int i = 0; i < ButtonImages.Length; i++)
        {
            ButtonImages[i].color = new Color32(25,25,25,200); //色指定
        }
        for (int i = 0; i < WindowBase.Length; i++)
        {
            WindowBase[i].color = new Color32(250,250,250,160); //色指定
        }
        for (int i = 0; i < TextArea.Length; i++)
        {
            TextArea[i].color = new Color32(230,230,230,180); //色指定
        }
        for (int i = 0; i < SettingBase.Length; i++)
        {
            SettingBase[i].color = new Color32(225,225,225,255); //色指定
        }
        for (int i = 0; i < SettingAccent.Length; i++)
        {
            SettingAccent[i].color = new Color32(200,200,200,255); //色指定
        }
        for (int i = 0; i < WindowTitle.Length; i++)
        {
            WindowTitle[i].color = new Color32(192,192,192,255); //色指定
        }
        for (int i = 0; i < ScrollArea.Length; i++)
        {
            ScrollArea[i].color = new Color32(252,252,252,220); //色指定
        }
        for (int i = 0; i < ScrollBar.Length; i++)
        {
            ScrollBar[i].color = new Color32(160,160,160,190); //色指定
        }
        for (int i = 0; i < BlackBaseTexts.Length; i++)
        {
            BlackBaseTexts[i].color = new Color32(255,255,255,255); //色指定
        }
        for (int i = 0; i < WhiteBaseTexts.Length; i++)
        {
            WhiteBaseTexts[i].color = new Color32(0,0,0,255); //色指定
        }
    }

    public void ThemeSet2() //パステルブルー
    {
        currentThemeID = 2;
        for (int i = 0; i < ButtonImages.Length; i++)
        {
            ButtonImages[i].color = new Color32(239,249,254,235); //色指定
        }
        for (int i = 0; i < WindowBase.Length; i++)
        {
            WindowBase[i].color = new Color32(225,231,241,200); //色指定
        }
        for (int i = 0; i < TextArea.Length; i++)
        {
            TextArea[i].color = new Color32(200,223,235,240); //色指定
        }
        for (int i = 0; i < SettingBase.Length; i++)
        {
            SettingBase[i].color = new Color32(190,200,215,255); //色指定
        }
        for (int i = 0; i < SettingAccent.Length; i++)
        {
            SettingAccent[i].color = new Color32(200,210,225,255); //色指定
        }
        for (int i = 0; i < WindowTitle.Length; i++)
        {
            WindowTitle[i].color = new Color32(150,164,192,255); //色指定
        }
        for (int i = 0; i < ScrollArea.Length; i++)
        {
            ScrollArea[i].color = new Color32(240,247,254,250); //色指定
        }
        for (int i = 0; i < ScrollBar.Length; i++)
        {
            ScrollBar[i].color = new Color32(180,190,220,240); //色指定
        }
        for (int i = 0; i < BlackBaseTexts.Length; i++)
        {
            BlackBaseTexts[i].color = new Color32(0,0,0,255); //色指定
        }
        for (int i = 0; i < WhiteBaseTexts.Length; i++)
        {
            WhiteBaseTexts[i].color = new Color32(0,0,0,255); //色指定
        }
    }

        public void ThemeSet3() //ライラック
    {
        currentThemeID = 3;
        for (int i = 0; i < ButtonImages.Length; i++)
        {
            ButtonImages[i].color = new Color32(231,215,245,235); //色指定
        }
        for (int i = 0; i < WindowBase.Length; i++)
        {
            WindowBase[i].color = new Color32(214,184,227,200); //色指定
        }
        for (int i = 0; i < TextArea.Length; i++)
        {
            TextArea[i].color = new Color32(191,179,205,240); //色指定
        }
        for (int i = 0; i < SettingBase.Length; i++)
        {
            SettingBase[i].color = new Color32(191,179,205,255); //色指定
        }
        for (int i = 0; i < SettingAccent.Length; i++)
        {
            SettingAccent[i].color = new Color32(201,183,217,255); //色指定
        }
        for (int i = 0; i < WindowTitle.Length; i++)
        {
            WindowTitle[i].color = new Color32(194,159,204,255); //色指定
        }
        for (int i = 0; i < ScrollArea.Length; i++)
        {
            ScrollArea[i].color = new Color32(238,211,251,250); //色指定
        }
        for (int i = 0; i < ScrollBar.Length; i++)
        {
            ScrollBar[i].color = new Color32(194,170,210,240); //色指定
        }
        for (int i = 0; i < BlackBaseTexts.Length; i++)
        {
            BlackBaseTexts[i].color = new Color32(0,0,0,255); //色指定
        }
        for (int i = 0; i < WhiteBaseTexts.Length; i++)
        {
            WhiteBaseTexts[i].color = new Color32(0,0,0,255); //色指定
        }
    }

        public void ThemeSet4() //パステルグリーン
    {
        currentThemeID = 4;
        for (int i = 0; i < ButtonImages.Length; i++)
        {
            ButtonImages[i].color = new Color32(205,235,221,235); //色指定
        }
        for (int i = 0; i < WindowBase.Length; i++)
        {
            WindowBase[i].color = new Color32(183,225,205,200); //色指定
        }
        for (int i = 0; i < TextArea.Length; i++)
        {
            TextArea[i].color = new Color32(161,201,183,240); //色指定
        }
        for (int i = 0; i < SettingBase.Length; i++)
        {
            SettingBase[i].color = new Color32(161,201,183,240); //色指定
        }
        for (int i = 0; i < SettingAccent.Length; i++)
        {
            SettingAccent[i].color = new Color32(183,231,211,255); //色指定
        }
        for (int i = 0; i < WindowTitle.Length; i++)
        {
            WindowTitle[i].color = new Color32(161,231,215,255); //色指定
        }
        for (int i = 0; i < ScrollArea.Length; i++)
        {
            ScrollArea[i].color = new Color32(205,253,235,250); //色指定
        }
        for (int i = 0; i < ScrollBar.Length; i++)
        {
            ScrollBar[i].color = new Color32(131,168,148,240); //色指定
        }
        for (int i = 0; i < BlackBaseTexts.Length; i++)
        {
            BlackBaseTexts[i].color = new Color32(0,0,0,255); //色指定
        }
        for (int i = 0; i < WhiteBaseTexts.Length; i++)
        {
            WhiteBaseTexts[i].color = new Color32(0,0,0,255); //色指定
        }
    }

        public void ThemeSet5() //ディープオーシャン
    {
        currentThemeID = 5;
        for (int i = 0; i < ButtonImages.Length; i++)
        {
            ButtonImages[i].color = new Color32(230,235,240,235); //色指定
        }
        for (int i = 0; i < WindowBase.Length; i++)
        {
            WindowBase[i].color = new Color32(16,32,64,200); //色指定
        }
        for (int i = 0; i < TextArea.Length; i++)
        {
            TextArea[i].color = new Color32(8,16,48,240); //色指定
        }
        for (int i = 0; i < SettingBase.Length; i++)
        {
            SettingBase[i].color = new Color32(8,16,48,255); //色指定
        }
        for (int i = 0; i < SettingAccent.Length; i++)
        {
            SettingAccent[i].color = new Color32(24,48,96,255); //色指定
        }
        for (int i = 0; i < WindowTitle.Length; i++)
        {
            WindowTitle[i].color = new Color32(16,48,128,255); //色指定
        }
        for (int i = 0; i < ScrollArea.Length; i++)
        {
            ScrollArea[i].color = new Color32(1,2,4,250); //色指定
        }
        for (int i = 0; i < ScrollBar.Length; i++)
        {
            ScrollBar[i].color = new Color32(48,64,96,240); //色指定
        }
        for (int i = 0; i < BlackBaseTexts.Length; i++)
        {
            BlackBaseTexts[i].color = new Color32(0,0,0,255); //色指定
        }
        for (int i = 0; i < WhiteBaseTexts.Length; i++)
        {
            WhiteBaseTexts[i].color = new Color32(255,255,255,255); //色指定
        }
    }

        public void ThemeSet6() //グレープ
    {
        currentThemeID = 6;
        for (int i = 0; i < ButtonImages.Length; i++)
        {
            ButtonImages[i].color = new Color32(245,225,235,235); //色指定
        }
        for (int i = 0; i < WindowBase.Length; i++)
        {
            WindowBase[i].color = new Color32(64,16,50,200); //色指定
        }
        for (int i = 0; i < TextArea.Length; i++)
        {
            TextArea[i].color = new Color32(32,8,24,240); //色指定
        }
        for (int i = 0; i < SettingBase.Length; i++)
        {
            SettingBase[i].color = new Color32(32,8,24,255); //色指定
        }
        for (int i = 0; i < SettingAccent.Length; i++)
        {
            SettingAccent[i].color = new Color32(64,16,55,255); //色指定
        }
        for (int i = 0; i < WindowTitle.Length; i++)
        {
            WindowTitle[i].color = new Color32(103,30,96,255); //色指定
        }
        for (int i = 0; i < ScrollArea.Length; i++)
        {
            ScrollArea[i].color = new Color32(4,2,4,250); //色指定
        }
        for (int i = 0; i < ScrollBar.Length; i++)
        {
            ScrollBar[i].color = new Color32(96,24,80,240); //色指定
        }
        for (int i = 0; i < BlackBaseTexts.Length; i++)
        {
            BlackBaseTexts[i].color = new Color32(0,0,0,255); //色指定
        }
        for (int i = 0; i < WhiteBaseTexts.Length; i++)
        {
            WhiteBaseTexts[i].color = new Color32(255,255,255,255); //色指定
        }
    }

        public void ThemeSet7() //ワインレッド
    {
        currentThemeID = 7;
        for (int i = 0; i < ButtonImages.Length; i++)
        {
            ButtonImages[i].color = new Color32(250,230,230,235); //色指定
        }
        for (int i = 0; i < WindowBase.Length; i++)
        {
            WindowBase[i].color = new Color32(64,12,16,200); //色指定
        }
        for (int i = 0; i < TextArea.Length; i++)
        {
            TextArea[i].color = new Color32(32,6,8,240); //色指定
        }
        for (int i = 0; i < SettingBase.Length; i++)
        {
            SettingBase[i].color = new Color32(32,6,8,255); //色指定
        }
        for (int i = 0; i < SettingAccent.Length; i++)
        {
            SettingAccent[i].color = new Color32(92,8,12,255); //色指定
        }
        for (int i = 0; i < WindowTitle.Length; i++)
        {
            WindowTitle[i].color = new Color32(128,32,48,255); //色指定
        }
        for (int i = 0; i < ScrollArea.Length; i++)
        {
            ScrollArea[i].color = new Color32(16,2,4,250); //色指定
        }
        for (int i = 0; i < ScrollBar.Length; i++)
        {
            ScrollBar[i].color = new Color32(100,20,30,240); //色指定
        }
        for (int i = 0; i < BlackBaseTexts.Length; i++)
        {
            BlackBaseTexts[i].color = new Color32(0,0,0,255); //色指定
        }
        for (int i = 0; i < WhiteBaseTexts.Length; i++)
        {
            WhiteBaseTexts[i].color = new Color32(255,255,255,255); //色指定
        }
    }

        public void ThemeSet8() //抹茶グリーン
    {
        currentThemeID = 8;
        for (int i = 0; i < ButtonImages.Length; i++)
        {
            ButtonImages[i].color = new Color32(215,235,215,235); //色指定
        }
        for (int i = 0; i < WindowBase.Length; i++)
        {
            WindowBase[i].color = new Color32(12,96,12,200); //色指定
        }
        for (int i = 0; i < TextArea.Length; i++)
        {
            TextArea[i].color = new Color32(8,64,8,240); //色指定
        }
        for (int i = 0; i < SettingBase.Length; i++)
        {
            SettingBase[i].color = new Color32(8,64,8,255); //色指定
        }
        for (int i = 0; i < SettingAccent.Length; i++)
        {
            SettingAccent[i].color = new Color32(16,128,16,255); //色指定
        }
        for (int i = 0; i < WindowTitle.Length; i++)
        {
            WindowTitle[i].color = new Color32(24,140,24,255); //色指定
        }
        for (int i = 0; i < ScrollArea.Length; i++)
        {
            ScrollArea[i].color = new Color32(2,16,2,250); //色指定
        }
        for (int i = 0; i < ScrollBar.Length; i++)
        {
            ScrollBar[i].color = new Color32(16,80,16,240); //色指定
        }
        for (int i = 0; i < BlackBaseTexts.Length; i++)
        {
            BlackBaseTexts[i].color = new Color32(0,0,0,255); //色指定
        }
        for (int i = 0; i < WhiteBaseTexts.Length; i++)
        {
            WhiteBaseTexts[i].color = new Color32(255,255,255,255); //色指定
        }
    }

        public void ThemeSet9() //レモン
    {
        currentThemeID = 9;
        for (int i = 0; i < ButtonImages.Length; i++)
        {
            ButtonImages[i].color = new Color32(240,240,225,235); //色指定
        }
        for (int i = 0; i < WindowBase.Length; i++)
        {
            WindowBase[i].color = new Color32(64,64,16,200); //色指定
        }
        for (int i = 0; i < TextArea.Length; i++)
        {
            TextArea[i].color = new Color32(32,32,8,240); //色指定
        }
        for (int i = 0; i < SettingBase.Length; i++)
        {
            SettingBase[i].color = new Color32(32,32,8,255); //色指定
        }
        for (int i = 0; i < SettingAccent.Length; i++)
        {
            SettingAccent[i].color = new Color32(64,64,16,255); //色指定
        }
        for (int i = 0; i < WindowTitle.Length; i++)
        {
            WindowTitle[i].color = new Color32(96,96,24,255); //色指定
        }
        for (int i = 0; i < ScrollArea.Length; i++)
        {
            ScrollArea[i].color = new Color32(16,16,8,250); //色指定
        }
        for (int i = 0; i < ScrollBar.Length; i++)
        {
            ScrollBar[i].color = new Color32(96,96,16,240); //色指定
        }
        for (int i = 0; i < BlackBaseTexts.Length; i++)
        {
            BlackBaseTexts[i].color = new Color32(0,0,0,255); //色指定
        }
        for (int i = 0; i < WhiteBaseTexts.Length; i++)
        {
            WhiteBaseTexts[i].color = new Color32(255,255,255,255); //色指定
        }
    }


}
