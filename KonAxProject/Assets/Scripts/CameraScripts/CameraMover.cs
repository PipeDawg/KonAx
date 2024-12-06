using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private float duration = 1;
    private float _elapsedTime;

    [SerializeField] GameObject[] cameraPositions;
    public enum CameraPositionEnum
    {
        StartRoom,
        JailRoom,
        StorageRoom,
        BarRoom,
        BridgeRoom1,
        BridgeRoom2,
        ThroneRoom
    };
    [SerializeField] CameraPositionEnum _enumCameraStartPosition;

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
        switch (_enumCameraStartPosition)
        {
            case CameraPositionEnum.StartRoom:
                transform.position = cameraPositions[0].transform.position;
                transform.rotation = cameraPositions[0].transform.rotation;
                break;
            case CameraPositionEnum.JailRoom:
                transform.position = cameraPositions[1].transform.position;
                transform.rotation = cameraPositions[1].transform.rotation;
                break;
            case CameraPositionEnum.StorageRoom:
                transform.position = cameraPositions[2].transform.position;
                transform.rotation = cameraPositions[2].transform.rotation;
                break;
            case CameraPositionEnum.BarRoom:
                transform.position = cameraPositions[3].transform.position;
                transform.rotation = cameraPositions[3].transform.rotation;
                break;
            case CameraPositionEnum.BridgeRoom1:
                transform.position = cameraPositions[4].transform.position;
                transform.rotation = cameraPositions[4].transform.rotation;
                break;
            case CameraPositionEnum.BridgeRoom2:
                transform.position = cameraPositions[5].transform.position;
                transform.rotation = cameraPositions[5].transform.rotation;
                break;
            case CameraPositionEnum.ThroneRoom:
                transform.position = cameraPositions[6].transform.position;
                transform.rotation = cameraPositions[6].transform.rotation;
                break;
            default:
                transform.position = cameraPositions[0].transform.position;
                transform.rotation = cameraPositions[0].transform.rotation;
                break;
        }

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