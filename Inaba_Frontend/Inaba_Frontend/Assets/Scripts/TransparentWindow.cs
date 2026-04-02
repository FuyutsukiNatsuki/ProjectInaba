using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.EventSystems; // ★追加：UIを検知するための魔法の呪文！

public class TransparentWindow : MonoBehaviour
{
    private struct MARGINS
    {
        public int cxLeftWidth;
        public int cxRightWidth;
        public int cyTopHeight;
        public int cyBottomHeight;
    }

    [DllImport("user32.dll")]
    private static extern IntPtr GetActiveWindow();

    [DllImport("user32.dll")]
    private static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);

    [DllImport("user32.dll")]
    private static extern uint GetWindowLong(IntPtr hWnd, int nIndex);

    [DllImport("user32.dll")]
    private static extern int SetWindowPos(IntPtr hwnd, IntPtr hwndInsertAfter, int x, int y, int cx, int cy, uint uFlags);

    [DllImport("Dwmapi.dll")]
    private static extern uint DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS margins);

    const int GWL_STYLE = -16;
    const int GWL_EXSTYLE = -20;
    const uint WS_POPUP = 0x80000000;
    const uint WS_VISIBLE = 0x10000000;
    const uint WS_EX_LAYERED = 0x00080000;
    const uint WS_EX_TRANSPARENT = 0x00000020;
    const int HWND_TOPMOST = -1;
    const uint SWP_NOSIZE = 0x0001;
    const uint SWP_NOMOVE = 0x0002;

    private IntPtr hWnd;

    void Start()
    {
        Application.runInBackground = true;

#if !UNITY_EDITOR
        hWnd = GetActiveWindow();

        SetWindowLong(hWnd, GWL_STYLE, WS_POPUP | WS_VISIBLE);
        
        uint exStyle = GetWindowLong(hWnd, GWL_EXSTYLE);
        SetWindowLong(hWnd, GWL_EXSTYLE, exStyle | WS_EX_LAYERED);

        MARGINS margins = new MARGINS { cxLeftWidth = -1 };
        DwmExtendFrameIntoClientArea(hWnd, ref margins);

        SetWindowPos(hWnd, new IntPtr(HWND_TOPMOST), 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE);
#endif
    }

    void Update()
    {
#if !UNITY_EDITOR
        // ★追加：マウスがUI（ボタンやパネルなど）の上にあるかチェック！
        bool isPointerOverUI = EventSystem.current != null && EventSystem.current.IsPointerOverGameObject();

        // 従来の「2Dオブジェクト（ムツキちゃん等）」のチェック！
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
        bool isPointerOver2D = hit.collider != null;

        uint exStyle = GetWindowLong(hWnd, GWL_EXSTYLE);

        // 「UIの上」または「2Dキャラの上」なら、クリックをUnityで受け取る（透過解除）！
        if (isPointerOverUI || isPointerOver2D)
        {
            if ((exStyle & WS_EX_TRANSPARENT) != 0)
            {
                SetWindowLong(hWnd, GWL_EXSTYLE, exStyle & ~WS_EX_TRANSPARENT);
            }
        }
        else // 何もない透明な部分なら、クリックを裏のウィンドウに流す（透過オン）！
        {
            if ((exStyle & WS_EX_TRANSPARENT) == 0)
            {
                SetWindowLong(hWnd, GWL_EXSTYLE, exStyle | WS_EX_TRANSPARENT);
            }
        }
#endif
    }
}