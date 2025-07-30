using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    [DllImport("user32.dll")]
    static extern bool SetCursorPos(int X, int Y);

    [DllImport("user32.dll")]
    static extern bool GetCursorPos(out POINT lpPoint);

    [StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
        public int X;
        public int Y;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct INPUT
    {
        public uint type;
        public MOUSEINPUT mi;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MOUSEINPUT
    {
        public int dx;
        public int dy;
        public uint mouseData;
        public uint dwFlags;
        public uint time;
        public System.IntPtr dwExtraInfo;
    }

    [DllImport("user32.dll")]
    static extern uint SendInput(uint nInputs, INPUT[] pInputs, int cbSize);

    const uint INPUT_MOUSE = 0;
    const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
    const uint MOUSEEVENTF_LEFTUP = 0x0004;

    public Vector2[] movePath;

    public bool isRepeat;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isRepeat)
            StartCoroutine(MoveMouseSequence());
    }

    IEnumerator MoveMouseSequence()
    {
        isRepeat = true;
        POINT p;
        GetCursorPos(out p);
        Vector2 currentPos = new Vector2(p.X, p.Y);

        if (movePath.Length > 0)
        {
            yield return MoveMouseSmoothly(currentPos, movePath[0], 0.5f);
            Click();
            yield return new WaitForSeconds(0.2f);
        }

        for (int i = 0; i < movePath.Length - 1; i++)
        {
            Vector2 from = movePath[i];
            Vector2 to = movePath[i + 1];

            yield return MoveMouseSmoothly(from, to, 0.5f);
            Click();
            yield return new WaitForSeconds(0.2f);
        }
        StartCoroutine(MoveMouseSequence());
    }

    IEnumerator MoveMouseSmoothly(Vector2 start, Vector2 end, float duration)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);

            float x = Mathf.Lerp(start.x, end.x, t);
            float y = Mathf.Lerp(start.y, end.y, t);

            SetCursorPos((int)x, (int)y);
            yield return null;
        }

        SetCursorPos((int)end.x, (int)end.y);
    }

    public static void Click()
    {
        INPUT[] inputs = new INPUT[2];

        inputs[0].type = INPUT_MOUSE;
        inputs[0].mi.dwFlags = MOUSEEVENTF_LEFTDOWN;

        inputs[1].type = INPUT_MOUSE;
        inputs[1].mi.dwFlags = MOUSEEVENTF_LEFTUP;

        SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(INPUT)));
    }
}