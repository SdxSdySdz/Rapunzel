using System.Collections;
using Sources;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

[RequireComponent(typeof(Follower))]
[RequireComponent(typeof(LeftRightMover))]
public class Rapunzel : MonoBehaviour
{
    [SerializeField] private Transform _lyingPoint;
    [SerializeField] private Animator _animator;
    [SerializeField] private Hair _hair;

    private Follower _follower;
    private LeftRightMover _leftRightMover;
    private Camera _camera;
    
    public event UnityAction Finished;

    private void Awake()
    {
        _follower = GetComponent<Follower>();
        _leftRightMover = GetComponent<LeftRightMover>();
        _camera = Camera.main;
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
            Finish();
            Finished?.Invoke();
        }
    }

    private void Finish()
    {
        _leftRightMover.Stop();
        _follower.Stop();
        _camera.transform.SetParent(null);
        StartCoroutine(PrepareLying());
    }

    private IEnumerator PrepareLying()
    {
        transform.DOMove(_lyingPoint.position, 2.5f);
        yield return new WaitForSeconds(2.5f);
        
        transform.Rotate(0, 180, 0);
        
        _animator.Play("Lying");
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
