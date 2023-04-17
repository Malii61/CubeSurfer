using System;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    public enum PositionClamp
    {
        left,
        right,
        none
    }
    private PositionClamp positonClamp = PositionClamp.none;
    public static CubeController Instance { get; private set; }
    [SerializeField] private float leftAndRightSpeed;
    [SerializeField] private float forwardSpeed;
    [SerializeField] CollectorCube collector;

    private float senstivityMultiplier = 0.007f;

    private Touch _touch;

    private bool _dragStarted;
    //#if UNITY_ANDROID && !UNITY_EDITOR
    //    public static AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
    //    public static AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
    //    public static AndroidJavaObject vibrator = currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");
    //#else
    //    public static AndroidJavaClass unityPlayer;
    //    public static AndroidJavaObject currentActivity;
    //    public static AndroidJavaObject vibrator;
    //#endif
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        collector.OnCubeCollected += Collector_OnCubeCollected;
        collector.OnCubeDropped += Collector_OnCubeDropped;

    }
    //public void Vibrate(long milliseconds)
    //{
    //    if (isAndroid())
    //        vibrator.Call("vibrate", milliseconds);
    //    else
    //        Handheld.Vibrate();
    //}

    private void Collector_OnCubeDropped(object sender, EventArgs e)
    {
        UpdatePositions();
    }
    private void Collector_OnCubeCollected(object sender, CollectorCube.OnCubeCollectedEventArgs e)
    {
        UpdatePositions();
    }
    internal void UpdatePositions()
    {
        int childCount = transform.childCount;
        transform.position = new Vector3(transform.position.x, childCount - 1, transform.position.z);
        transform.GetChild(1).localPosition = new Vector3(0, -(childCount - 2), 0);
        for (int i = 2; i < childCount; i++)
        {
            transform.GetChild(i).localPosition = new Vector3(0, -(i - 1), 0);
        }
    }


    private void OnDestroy()
    {
        collector.OnCubeCollected -= Collector_OnCubeCollected;
        collector.OnCubeDropped -= Collector_OnCubeDropped;
    }

    private void LateUpdate()
    {
        if (Input.touchCount > 0)
        {
            _touch = Input.GetTouch(0);
            if (_touch.phase == TouchPhase.Began)
            {
                _dragStarted = true;
            }
        }
        if (_dragStarted && !PauseMenuUI.isPauseMenuActive)
        {
            if (_touch.phase == TouchPhase.Moved)
            {
                float x = _touch.deltaPosition.x * senstivityMultiplier;
                Move(x);
            }
            else
            {
                Move(0f);
            }
        }
    }
    private void Move(float moveValue)
    {
        float adjustedHorizontalMove = CheckPositionClamper(moveValue);
        transform.Translate(-forwardSpeed * Time.deltaTime, 0, adjustedHorizontalMove);
    }

    private float CheckPositionClamper(float horizontalMove)
    {
        bool isGreaterThanZero = horizontalMove > 0;
        if ((positonClamp == PositionClamp.right && isGreaterThanZero) || (positonClamp == PositionClamp.left && !isGreaterThanZero))
            return 0f;
        return horizontalMove;
    }
    public void SetSensitivityMultiplier(int value)
    {
        senstivityMultiplier = 0.006f + 0.001f * value;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.TryGetComponent(out MagnetManager magnet))
        {
            magnet.Use();
        }

        if (other.TryGetComponent(out WallObstacle obstacle))
        {
            collector.OnCollidedWithObstacle();
        }
        else if (other.TryGetComponent(out GoldMultiplier goldMultiplier))
        {
            collector.OnCollidedWithGoldMultiplier();
        }
    }
    public void SetClampPosition(PositionClamp clamp)
    {
        positonClamp = clamp;
    }
    //    public bool isAndroid()
    //    {
    //#if UNITY_ANDROID && !UNITY_EDITOR
    //	return true;
    //#else
    //        return false;
    //#endif
    //    }
}