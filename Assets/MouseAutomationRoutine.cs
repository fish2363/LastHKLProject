using UnityEngine;
using System.Collections;
using DG.Tweening;

public class MouseAutomationRoutine : MonoBehaviour
{
    //public RectTransform targetUI; // Ÿ�� UI�� ���� ������ �� �ְ�
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

    //    // ���� ���� ���콺 ��ġ
    //    Vector2 start = MouseController.GetMousePosition();

    //    // ��ǥ UI ��ġ�� OS ��ǥ��� ��ȯ
    //    Vector2 uiScreenPos = RectTransformUtility.CalculateRelativeRectTransformBounds(
    //        targetUI.root, targetUI).center;

    //    Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(null, targetUI.position);
    //    screenPos.y = Screen.height - screenPos.y; // OS ��ǥ�� ��ȯ

    //    Vector2 targetPosition = screenPos;

    //    // �ε巴�� �̵�
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

    //    // ��Ȯ�� ��ġ Ŭ��
    //    MouseController.MoveMouse(targetPosition);
    //    yield return new WaitForSeconds(0.05f);

    //    MouseController.Click();
    //    isRunning = false;
    //}
}