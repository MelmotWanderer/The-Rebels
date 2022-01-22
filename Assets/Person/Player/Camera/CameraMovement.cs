using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    
    Animator _camAnimator;
    void Start()
    {
        _camAnimator = GetComponent<Animator>();
    }
    public void Move(float speed, bool isMoved)
    {
        _camAnimator.speed = speed;
        _camAnimator.SetBool("isMoved", isMoved);
    }
    public void Shake(float duration, float magnitude, float noize)
    {
        StartCoroutine(ShakeCor(duration, magnitude, noize));
    }
    public void ShakeRotate(float duration, float angleDeg, Vector2 direction)
    {
        StartCoroutine(ShakeRotateCor(duration, angleDeg, direction));

    }

    private IEnumerator ShakeRotateCor(float duration, float angleDeg, Vector2 direction)
    {
        //Счетчик прошедшего времени
        float elapsed = 0f;
        //Запоминаем начальное вращение камеры по аналогии с вибрацией камеры
        Quaternion startRotation = transform.localRotation;

        //Для удобства добавляем переменную середину нашего таймера
        //Ибо сначала отклонение будет идти на увеличение, а затем на уменьшение
        float halfDuration = duration / 2;
        //Приводим направляющий вектор к единичному вектору, дабы не портить вычисления
        direction = direction.normalized;
        while (elapsed < duration)
        {
            //Сохраняем текущее направление ибо мы будем менять данный вектор
            Vector2 currentDirection = direction;
            //Подсчёт процентного коэффициента для функции Lerp[0..1]
            //До середины таймера процент увеличивается, затем уменьшается
            float t = elapsed < halfDuration ? elapsed / halfDuration : (duration - elapsed) / halfDuration;
            //Текущий угол отклонения
            float currentAngle = Mathf.Lerp(0f, angleDeg, t);
            //Вычисляем длину направляющего вектора из тангенса угла.
            //Недостатком данного решения будет являться то
            //Что угол отклонения должен находится в следующем диапазоне (0..90)
            currentDirection *= Mathf.Tan(currentAngle * Mathf.Deg2Rad);
            //Сумма векторов - получаем направление взгляда на текущей итерации
            Vector3 resDirection = ((Vector3)currentDirection + Vector3.forward).normalized;
            //С помощью Quaternion.FromToRotation получаем новое вращение
            //Изменяем локальное вращение, дабы во время вращения, если игрок будет управлять камерой
            //Все работало корректно
            transform.localRotation = Quaternion.FromToRotation(Vector3.forward, resDirection);

            elapsed += Time.deltaTime;
            yield return null;
        }
        //Восстанавливаем вращение
        transform.localRotation = startRotation;
    }
    private IEnumerator ShakeCor(float duration, float magnitude, float noize)
    {
        float elapsed = 0f;
        Vector2 startPosition = transform.localPosition;
        Vector2 noizeStartPoint0 = Random.insideUnitCircle * noize;
        Vector2 noizeStartPoint1 = Random.insideUnitCircle * noize;
        while(elapsed < duration)
        {
            Vector2 currentNoizePoint0 = Vector2.Lerp(noizeStartPoint0, Vector2.zero, elapsed / duration);
            Vector2 currentNoizePoint1 = Vector2.Lerp(noizeStartPoint1, Vector2.zero, elapsed / duration);
            Vector2 cameraPositionDelta = new Vector2(Mathf.PerlinNoise(currentNoizePoint0.x, currentNoizePoint0.y), Mathf.PerlinNoise(currentNoizePoint1.x, currentNoizePoint1.y));
            cameraPositionDelta *= magnitude;
            transform.localEulerAngles = startPosition + (Vector2)cameraPositionDelta;
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = startPosition;
        
    }
}
