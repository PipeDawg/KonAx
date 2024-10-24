using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private float duration = 1;
    private float _elapsedTime;

    private Vector3 _startPos;
    private Vector3 _endPos;
    private Quaternion _startRot;
    private Quaternion _endRot;



    //Below makes singleton
    private static CameraMover _instance;
    public static CameraMover Instance => _instance;

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this);
        }
        else
        {
            _instance = this;
        }
    }
    //Singleton thing stops here

    void Start()
    {
        _startPos = transform.position;
        _endPos = transform.position;
        _startRot = transform.rotation;
        _endRot = transform.rotation;
    }

    //MoveCamera is called by the move camera triggers to move the camera to a desired destination
    public void MoveCamera(Transform cameraDestination)
    {
        _startPos = transform.position;
        _startRot = transform.rotation;

        _endPos = cameraDestination.position;
        _endRot = cameraDestination.rotation;
    }

    void Update()
    {
        if (transform.position != _endPos && transform.rotation != _endRot)
        {
            _elapsedTime += Time.deltaTime;
            float percentageComplete = _elapsedTime / duration;

            transform.position = Vector3.Lerp(_startPos, _endPos, percentageComplete);
            transform.rotation = Quaternion.Lerp(_startRot, _endRot, percentageComplete);
        }
        else
        {
            _elapsedTime = 0;
        }
    }
}