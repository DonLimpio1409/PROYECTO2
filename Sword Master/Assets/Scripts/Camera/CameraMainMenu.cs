using UnityEngine;
using DG.Tweening;

public class CameraMainMenu : MonoBehaviour
{ 
    private Camera cam;
    [SerializeField] private Transform[] cameraPositions = new Transform[4];

    private float transitionDuration = 1f;
    private Ease easeType = Ease.OutQuad;

    private void Start()
    {
        cam = Camera.main;
    }

    public void MoveToWaypoint(int index)
    {
        if (cam == null) 
        {
            cam = Camera.main;
        }

        Transform target = cameraPositions[index];
        cam.transform.DOKill();
        cam.transform.DOMove(target.position, transitionDuration).SetEase(easeType).SetUpdate(true);
        cam.transform.DORotateQuaternion(target.rotation, transitionDuration).SetEase(easeType).SetUpdate(true);
    }
}