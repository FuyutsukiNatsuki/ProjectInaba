using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class TransparentWindow : MonoBehaviour
{
    private struct MARGINS
    {
        public int cxLeftWidth;
        public int cxRightWidth;
        public int cyTopHeight;
        public int cyBottomHeight;
    }

    // ★追加：OSから直接マウス座標をもぎ取るための箱！
    [StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
        public int x;
        public int y;
    }

    [DllImport("user32.dll")]
    private static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);

    [DllImport("user32.dll")]
    private static extern uint GetWindowLong(IntPtr hWnd, int nIndex);

    [DllImport("user32.dll")]
    private static extern int SetWindowPos(IntPtr hwnd, IntPtr hwndInsertAfter, int x, int y, int cx, int cy, uint uFlags);

    [DllImport("Dwmapi.dll")]
    private static extern uint DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS margins);

    // ★追加：OSから直接マウス座標をもぎ取る魔法の呪文！
    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool GetCursorPos(out POINT lpPoint);

    [DllImport("user32.dll")]
    private static extern bool ScreenToClient(IntPtr hWnd, ref POINT lpPoint);

    const int GWL_STYLE = -16;
    const int GWL_EXSTYLE = -20;
    const uint WS_POPUP = 0x80000000;
    const uint WS_VISIBLE = 0x10000000;
    const uint WS_EX_LAYERED = 0x00080000;
    const uint WS_EX_TRANSPARENT = 0x00000020;
    const int HWND_TOPMOST = -1;
    const uint SWP_NOSIZE = 0x0001;
    const uint SWP_NOMOVE = 0x0002;
    const uint SWP_SHOWWINDOW = 0x0040;

    private IntPtr hWnd;

    void Start()
    {
        Application.runInBackground = true;
        Screen.SetResolution(1920, 1080, FullScreenMode.Windowed);

Application.runInBackground = true;
        Screen.SetResolution(1920, 1080, FullScreenMode.Windowed);

#if !UNITY_EDITOR
        hWnd = System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle;

        SetWindowLong(hWnd, GWL_STYLE, WS_POPUP | WS_VISIBLE);
        
        uint exStyle = GetWindowLong(hWnd, GWL_EXSTYLE);
        SetWindowLong(hWnd, GWL_EXSTYLE, exStyle | WS_EX_LAYERED);

        MARGINS margins = new MARGINS { cxLeftWidth = -1 };
        DwmExtendFrameIntoClientArea(hWnd, ref margins);

        // ★特大修正：「動かさない」設定を外して、画面の左上(0,0)にピッタリ張り付けて1920x1080に引き伸ばす！
        SetWindowPos(hWnd, new IntPtr(HWND_TOPMOST), 0, 0, 1920, 1080, SWP_SHOWWINDOW);
#endif
    }

    void Update()
    {
#if !UNITY_EDITOR
        // ★修正：OS直取り（GetCursorPos）をやめて、DPIを自動計算してくれるUnity純正に戻す！
        Vector2 mousePos = Input.mousePosition;

        PointerEventData pointerData = new PointerEventData(EventSystem.current);
        pointerData.position = mousePos;
        System.Collections.Generic.List<RaycastResult> results = new System.Collections.Generic.List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);
        bool isPointerOverUI = results.Count > 0;

        uint exStyle = GetWindowLong(hWnd, GWL_EXSTYLE);

        // UIの上なら透過解除（クリックできる）、それ以外なら透過（裏画面を触れる）！
        if (isPointerOverUI)
        {
            if ((exStyle & WS_EX_TRANSPARENT) != 0)
            {
                SetWindowLong(hWnd, GWL_EXSTYLE, exStyle & ~WS_EX_TRANSPARENT);
            }
        }
        else
        {
            if ((exStyle & WS_EX_TRANSPARENT) == 0)
            {
                SetWindowLong(hWnd, GWL_EXSTYLE, exStyle | WS_EX_TRANSPARENT);
            }
        }
#endif
    }
}