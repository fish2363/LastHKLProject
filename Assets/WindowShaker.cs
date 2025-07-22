using System;
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;

public class WindowShaker : MonoBehaviour
{
    [DllImport("user32.dll")]
    static extern IntPtr GetActiveWindow();

    [DllImport("user32.dll")]
    static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

    [DllImport("user32.dll")]
    static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        public int Left, Top, Right, Bottom;
    }

    public float shakeDuration = 0.5f;
    public float shakeMagnitude = 10f;

    public void ShakeWindow()
    {
        StartCoroutine(ShakeCoroutine());
    }

    IEnumerator ShakeCoroutine()
    {
        IntPtr windowHandle = GetActiveWindow();

        GetWindowRect(windowHandle, out RECT rect);
        int width = rect.Right - rect.Left;
        int height = rect.Bottom - rect.Top;

        float timer = 0f;
        Vector2 originalPos = new Vector2(rect.Left, rect.Top);

        while (timer < shakeDuration)
        {
            float offsetX = UnityEngine.Random.Range(-shakeMagnitude, shakeMagnitude);
            float offsetY = UnityEngine.Random.Range(-shakeMagnitude, shakeMagnitude);

            MoveWindow(windowHandle, (int)(originalPos.x + offsetX), (int)(originalPos.y + offsetY), width, height, true);
            timer += Time.deltaTime;
            yield return null;
        }

        // 원래 위치로 복구
        MoveWindow(windowHandle, (int)originalPos.x, (int)originalPos.y, width, height, true);
    }
}
