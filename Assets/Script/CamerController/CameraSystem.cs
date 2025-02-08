using UnityEngine;
using Cinemachine;
using UnityEngine.EventSystems;

public class CameraSystem : MonoBehaviour
{
    [SerializeField]private CinemachineVirtualCamera cinemachineVirtualCamera;
    [SerializeField]private GlobalUIManager globalUIManager;
    [SerializeField] private float followOffsetMax,followOffsetMin,zoomSpeed,zoomAmount,moveSpeed
    ,touchSensitivity,tapTime,tapDistance;
    private float FOV=50;
    private Vector3 followOffset;
    private bool isTouching = false;
    private float touchStartTime;
    private Vector2 touchStartPos;

    private bool istouchingAllowed=true;
    
    void Awake(){
        followOffset = cinemachineVirtualCamera.GetCinemachineComponent<
        CinemachineTransposer>().m_FollowOffset;
    }
    private void Update() {
        // HandleCameraZoomFOV();
        // HandleCameraZoom();
        // HandleCameraMovement();
        if(Input.touchCount >0){
            istouchingAllowed=!globalUIManager.IsUIInterfering();
        }
        if(istouchingAllowed){

        HandleCameraTouchMovement();
        TouchDetector();
        HandleCameraZoomTouch();
        }
    }
    void TouchDetector(){
        if(Input.touchCount == 1){
            
            Touch touch = Input.GetTouch(0);
            if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
        {
            Debug.Log("Tapped on UI, ignoring touch.");
            return; // Don't process game actions
        }
        if (touch.phase == TouchPhase.Began) // Finger touches the screen
        {
            isTouching = true;
            touchStartTime = Time.time;
            touchStartPos = touch.position;
        }
        else if (touch.phase == TouchPhase.Ended) // Finger lifted
        {
            float touchDuration = Time.time - touchStartTime;
            float touchDistance = (touch.position - touchStartPos).magnitude;

            // Check if it was a quick tap (not a swipe)
            if (touchDuration < tapTime && touchDistance < tapDistance) 
            {
                Debug.Log("Tap detected!");
                // Handle tap action here
                globalUIManager.TapAction();
            }

            isTouching = false;
            }
            }
    }
    void HandleCameraTouchMovement()
    {
    // float moveSpeed = 50f; // Movement speed
    Vector2 touchDelta;

    if (Input.touchCount == 1) // Only move if one finger is touching
    {
        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Moved) // Detect swipe movement
        {
            touchDelta = touch.deltaPosition;
            // Invert movement direction to match intuitive controls
            Vector3 moveDir = -transform.forward * touchDelta.y - transform.right * touchDelta.x;

            // Apply movement
            transform.position += moveDir * Time.deltaTime * moveSpeed*touchSensitivity
            ; // Scale down movement
        }
}}

void HandleCameraZoomTouch(){
        Vector3 zoomDir = followOffset.normalized;
    
    if (Input.touchCount == 2) // Detect two-finger touch
    {
        Touch touch0 = Input.GetTouch(0);
        Touch touch1 = Input.GetTouch(1);

        // Get current and previous distance between fingers
        float prevDistance = (touch0.position - touch0.deltaPosition - (touch1.position - touch1.deltaPosition)).magnitude;
        float currentDistance = (touch0.position - touch1.position).magnitude;

        float pinchAmount = (currentDistance - prevDistance) * 0.01f; // Scale pinch sensitivity

        followOffset -= zoomDir * pinchAmount * zoomAmount;

        // Clamp Zoom Limits
        if (followOffset.magnitude < followOffsetMin)
            followOffset = zoomDir * followOffsetMin;
        if (followOffset.magnitude > followOffsetMax)
            followOffset = zoomDir * followOffsetMax;
    

    // Apply Zoom Smoothly
    cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset =
        Vector3.Lerp(
            cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset, 
            followOffset, 
            Time.deltaTime * zoomSpeed
        );
    }}
    // void HandleCameraMovement(){
    //     Vector3 inputDir = new Vector3(0, 0, 0);
    //     if (Input.GetKey(KeyCode.W)) inputDir.z = +1f;
    //     if (Input.GetKey(KeyCode.S)) inputDir.z = -1f;
    //     if (Input.GetKey(KeyCode.A)) inputDir.x = -1f;
    //     if (Input.GetKey(KeyCode.D)) inputDir.x = +1f;

    //     Vector3 moveDir = transform. forward * inputDir.z + transform.right * inputDir.x;

    //     // float moveSpeed = 50f;
    //     transform.position += moveDir * moveSpeed * Time.deltaTime;
    // }
    

    // void HandleCameraZoom(){
    //     Vector3 zoomDir = followOffset.normalized;

    //     if (Input.mouseScrollDelta.y > 0) {
    //         followOffset -=  zoomDir * zoomAmount;}

    //     if (Input.mouseScrollDelta.y < 0) {
    //         followOffset +=  zoomDir * zoomAmount;}

    //     if (followOffset.magnitude < followOffsetMin) {
    //         followOffset = zoomDir * followOffsetMin;}

    //     if (followOffset.magnitude > followOffsetMax) {
    //         followOffset = zoomDir * followOffsetMax;}

    //     // float zoomSpeed = 10f;
    //     cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset =
    //     Vector3.Lerp(cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>()
    //     .m_FollowOffset, followOffset,Time.deltaTime*zoomSpeed);
    // }
    
}
