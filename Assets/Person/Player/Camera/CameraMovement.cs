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
        float elapsed = 0f;
        float halfDuration = duration / 2;
        direction = direction.normalized;
        Quaternion startRotation = transform.localRotation;
        while(elapsed < duration)
        {
            Vector2 currentDirection = direction;
            float t = elapsed < halfDuration ? elapsed / halfDuration : (duration - elapsed) / halfDuration;
            float currentAngle = Mathf.Lerp(0f, angleDeg, t);
            currentDirection *= Mathf.Tan(currentAngle * Mathf.Deg2Rad);

            Vector3 resDirection = ((Vector3)currentDirection + Vector3.forward).normalized;
            transform.localRotation = Quaternion.FromToRotation(Vector3.forward, resDirection);
            elapsed += Time.deltaTime;
            yield return null;
        }
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
