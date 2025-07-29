using UnityEngine;

public class Test : MonoBehaviour
{
    private void Start()
    {
        Screen.fullScreen = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            //FindAnyObjectByType<MouseAutomationRoutine>().RunRoutine();
            FindObjectOfType<WindowShaker>()?.ShakeWindow();
        }
    }
}
