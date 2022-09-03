using System.Collections;
using UnityEngine;

public class HeadRotator : MonoBehaviour
{
    [SerializeField]
    private float _angle;
    [SerializeField]
    private float _duration;

    private void Start()
    {
        StartCoroutine(Rotate());
    }

    private IEnumerator Rotate()
    {
        while (true)
        {
            yield return RotateAroundY(_angle, _duration / 4);
            yield return RotateAroundY(-2 * _angle, _duration / 2);
            yield return RotateAroundY(_angle, _duration / 4);
        }
    }

    private IEnumerator RotateAroundY(float angle, float duration)
    {
        var waitForEndOfFrame = new WaitForEndOfFrame();

        Vector3 startDirection = transform.forward;
        Vector3 targetDirection = Quaternion.Euler(0, angle, 0) * startDirection;
        float time = 0;
        while (time <= duration)
        {
            Vector3 currentDirection = Vector3.Lerp(startDirection, targetDirection, time / duration);
            transform.forward = currentDirection;

            time += Time.deltaTime;
            yield return waitForEndOfFrame;
        }

        transform.forward = targetDirection;
    }
}
