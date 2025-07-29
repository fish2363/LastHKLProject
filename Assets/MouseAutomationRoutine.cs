using UnityEngine;
using System.Collections;
using DG.Tweening;

public class MouseAutomationRoutine : MonoBehaviour
{
    //public RectTransform targetUI; // 타겟 UI를 직접 참조할 수 있게
    //public float duration = 1.0f;
    //public float jitterAmount = 3f;

    //private bool isRunning = false;

    //public void RunRoutine()
    //{
    //    if (!isRunning)
    //        StartCoroutine(MoveAndClickRoutine());
    //}

    //IEnumerator MoveAndClickRoutine()
    //{
    //    isRunning = true;

    //    // 현재 실제 마우스 위치
    //    Vector2 start = MouseController.GetMousePosition();

    //    // 목표 UI 위치를 OS 좌표계로 변환
    //    Vector2 uiScreenPos = RectTransformUtility.CalculateRelativeRectTransformBounds(
    //        targetUI.root, targetUI).center;

    //    Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(null, targetUI.position);
    //    screenPos.y = Screen.height - screenPos.y; // OS 좌표로 변환

    //    Vector2 targetPosition = screenPos;

    //    // 부드럽게 이동
    //    float time = 0f;
    //    while (time < duration)
    //    {
    //        time += Time.deltaTime;
    //        float t = time / duration;

    //        float jitterX = Mathf.PerlinNoise(time * 2f, 0f) * jitterAmount - jitterAmount / 2f;
    //        float jitterY = Mathf.PerlinNoise(0f, time * 2f) * jitterAmount - jitterAmount / 2f;

    //        Vector2 interpolated = Vector2.Lerp(start, targetPosition, Mathf.SmoothStep(0f, 1f, t));
    //        Vector2 withJitter = interpolated + new Vector2(jitterX, jitterY);

    //        MouseController.MoveMouse(withJitter);
    //        yield return null;
    //    }

    //    // 정확한 위치 클릭
    //    MouseController.MoveMouse(targetPosition);
    //    yield return new WaitForSeconds(0.05f);

    //    MouseController.Click();
    //    isRunning = false;
    //}
}