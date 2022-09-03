using System.Collections;
using UnityEngine;

public class FromToMover : MonoBehaviour
{
    [SerializeField] private Transform _from;
    [SerializeField] private Transform _to;
    [SerializeField] private float _duration;

    private void Start()
    {
        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        Vector3 from = _from.position;
        Vector3 to = _to.position;

        transform.position = from;
        while (true)
        {
            yield return Move(to);
            yield return Move(from);
        }
    }

    private IEnumerator Move(Vector3 target)
    {
        var waitForEndOfFrame = new WaitForEndOfFrame();

        Vector3 startPosition = transform.position;
        float time = 0;
        while (time <= _duration)
        {
            transform.position = Vector3.Lerp(startPosition, target, time / _duration);
            time += Time.deltaTime;
            yield return waitForEndOfFrame;
        }
    }
}
