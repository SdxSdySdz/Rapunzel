using Sources;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Follower))]
[RequireComponent(typeof(LeftRightMover))]
public class Rapunzel : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Hair _hair;

    private Follower _follower;
    private LeftRightMover _leftRightMover;
    
    public event UnityAction Finished;

    private void Awake()
    {
        _follower = GetComponent<Follower>();
        _leftRightMover = GetComponent<LeftRightMover>();
    }

    private void Start()
    {
        Stop();
    }

    private void OnEnable()
    {
        _leftRightMover.Started += Walk;
        _leftRightMover.Stopped += Stop;
    }

    private void OnDisable()
    {
        _leftRightMover.Started -= Walk;
        _leftRightMover.Stopped -= Stop;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PickableHair hair))
            PickUp(hair);
        else if (other.gameObject.TryGetComponent(out FinishLine _))
        {
            LieDown();
            Finished?.Invoke();
        }
    }

    private void LieDown()
    {
        _leftRightMover.Stop();
        _follower.Stop();
        _animator.Play("Laying");
    }
    
    private void Walk()
    {
        _follower.Follow();
        _animator.Play("Walking");
    }

    private void Stop()
    {
        _follower.Stop();
        _animator.Play("Idle");
    }

    private void PickUp(PickableHair hair)
    {
        _hair.ChangeColor(hair.Color);
        _hair.Lengthen();
        Destroy(hair.gameObject);
    }
}
