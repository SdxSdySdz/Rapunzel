using PathCreation;
using UnityEngine;

public class Follower : MonoBehaviour
{
    [SerializeField] private PathCreator pathCreator;
    [SerializeField] private EndOfPathInstruction endOfPathInstruction;
    [SerializeField] private float speed = 5;
    [SerializeField] private bool _followX;
    [SerializeField] private bool _followY;
    [SerializeField] private bool _followZ;
    [SerializeField] private Vector3 _offset;

    private bool _isMoving;
    private float distanceTravelled;

    private void Start()
    {
        pathCreator.pathUpdated += OnPathChanged;
        transform.position = pathCreator.path.GetPointAtTime(1, EndOfPathInstruction.Stop);
        _isMoving = true;
    }

    private void Update()
    {
        if (_isMoving == false)
            return;

        distanceTravelled += speed * Time.deltaTime;
        Vector3 TargetPosition = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
        // transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);

        Vector3 position = transform.position;

        if (_followX)
            position = new Vector3(TargetPosition.x, position.y, position.z);

        if (_followY)
            position = new Vector3(position.x, TargetPosition.y, position.z);

        if (_followZ)
            position = new Vector3(position.x, position.y, TargetPosition.z);

        transform.position = position + _offset;
    }

    public void Follow()
    {
        enabled = true;
    }

    public void Stop()
    {
        enabled = false;
    }

    private void OnPathChanged()
    {
        distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
    }
}
