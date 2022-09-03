using UnityEngine;
using UnityEngine.Events;

public class LeftRightMover : MonoBehaviour
{
    [SerializeField] private float _speed;

    public event UnityAction Started;
    public event UnityAction Stopped;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Started?.Invoke();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Stopped?.Invoke();
            return;
        }

        if (Input.GetMouseButton(0) == false)
            return;

        float offsetMultiplayer = Camera.main.ScreenToViewportPoint(Input.mousePosition).x * 2 - 1;
        transform.Translate(Vector3.right * Input.GetAxis("Mouse X") * _speed * Time.deltaTime, Space.Self);
    }

    public void Stop()
    {
        enabled = false;
    }
}
