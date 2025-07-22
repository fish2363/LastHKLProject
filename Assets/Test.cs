using UnityEngine;

public class Test : MonoBehaviour
{
    private void Start()
    {
        Screen.fullScreen = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FindObjectOfType<WindowShaker>()?.ShakeWindow();
        }
    }
}
