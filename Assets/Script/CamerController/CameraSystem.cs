using UnityEngine;
using Cinemachine;
using UnityEngine.EventSystems;
using System.Collections;


public class CameraSystem : MonoBehaviour
{
    [SerializeField]private CinemachineVirtualCamera cinemachineVirtualCamera;
    [SerializeField]private GlobalUIManager globalUIManager;
    [SerializeField] private float followOffsetMax,followOffsetMin,zoomSpeed,zoomAmount,moveSpeed
    ,touchSensitivity,tapTime,tapDistance,FocusingTimeLimit,heightMovementSpeed,normalObjectZoom
    ,HomeZoom;
    // private float FOV=50;
    [SerializeField] private GameObject Home;
    private Vector3 followOffset;
    private bool istouchingAllowed=true;
    private float touchStartTime;
    private Vector2 touchStartPos;

    private GameObject TargetForFocus;
    private Coroutine focusRoutine;
    private bool exceptionUIActive;
    private bool cameraExceptionMoveAllowed,UnitOnHold=false;

    private GameObject targetToFollow;
    private bool shouldFollow=false;
    

    public void SetTheUniHold(bool t){
        UnitOnHold=t;
    }
    void Awake(){
        followOffset = cinemachineVirtualCamera.GetCinemachineComponent<
        CinemachineTransposer>().m_FollowOffset;
    }
    public void SetException(bool t){
        exceptionUIActive=t;
    }
    private void Update() {
        if(shouldFollow){
            if(targetToFollow!=null){
                transform.position=targetToFollow.transform.position;
            }
            else{
                shouldFollow=false;
            }
        }else if(targetToFollow!=null){    
            targetToFollow=null;
        }
        // HandleCameraZoomFOV();
        // HandleCameraZoom();
        // HandleCameraMovement();
        if(Input.touchCount >0){
            istouchingAllowed=!globalUIManager.IsUIInterfering();
        }
        if(istouchingAllowed){
            // ResetFollow();
        if(exceptionUIActive){
            if (Input.touchCount == 1)
            {
                Touch touch = Input.GetTouch(0);
                if(touch.phase==TouchPhase.Began){
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                int layerMask = LayerMask.GetMask("Blue", "Ground"); // Allow only "Blue" and "Ground"

                if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
                {
                    BluePrint detectedBlueprint = hit.collider.GetComponentInParent<BluePrint>();
                    if (detectedBlueprint) {
                        cameraExceptionMoveAllowed=false;
                    //    return;
                    // movingAllowed=true;
                    }
                    else{
                        cameraExceptionMoveAllowed=true;
                    }
                    }}
                if (cameraExceptionMoveAllowed ){
                    HandleCameraTouchMovement();
                }
                    }
               
            // HandleCameraTouchMovement();
        return;
        }
        if(UnitOnHold){
            return;
        }
        RefreshingTouch();
        HandleCameraTouchMovement();
        TouchDetector();
        HandleCameraZoomTouch();
        }
        if(TargetForFocus){
            FocusingOnTarget();
            // followOffset = cinemachineVirtualCamera.GetCinemachineComponent<
            // CinemachineTransposer>().m_FollowOffset;
        }
    }
    void RefreshingTouch(){
        if(Input.touchCount>0){
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
        {
            return;
        }
        else{
            ResetFollow();
            Debug.Log("Reset Follow by RefreshingTouch");
        }
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
            // isTouching = true;
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

            // isTouching = false;
            }
            }
    }
    void HandleCameraTouchMovement()
    {
    if (Input.touchCount == 1)
    {
        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Moved)
        {
            Ray touchRay = Camera.main.ScreenPointToRay(touch.position);
            Ray prevTouchRay = Camera.main.ScreenPointToRay(touch.position - touch.deltaPosition);

            Plane groundPlane = new Plane(Vector3.up, Vector3.zero); // Assuming the ground is flat at y=0

            if (groundPlane.Raycast(prevTouchRay, out float enterPrev) && groundPlane.
            Raycast(touchRay, out float enterCurr))
            {
                Vector3 prevPoint = prevTouchRay.GetPoint(enterPrev);
                Vector3 currPoint = touchRay.GetPoint(enterCurr);
                
                Vector3 moveDir = prevPoint - currPoint; // Reverse direction for natural feel
                transform.position += moveDir ;
            }
        }
    }}

    public void SetFocusOnPoint(Vector3 targetPoint){
        //called by global ui to focus on. when we are zoomed out.and click any ground
        // TargetForFocus=target;
        if (focusRoutine != null)
        {
            StopCoroutine(focusRoutine);
        }
        TargetForFocus = null;
        focusRoutine = StartCoroutine(FocusingOnPoint(targetPoint));
    }
    private IEnumerator FocusingOnPoint(Vector3 targetPosition)
{
    float timeElapsed = 0f;
    Vector3 startPos = transform.position;
    
    // Get the current zoom offset
    Vector3 startZoomOffset = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>()
        .m_FollowOffset;
    Vector3 targetZoomOffset = startZoomOffset.normalized * Mathf.Clamp(
        normalObjectZoom, followOffsetMin, followOffsetMax); // Adjust this factor for zoom strength

    while (timeElapsed < FocusingTimeLimit)
    {
        float t = timeElapsed / FocusingTimeLimit;
        t = t * t * (3f - 2f * t); // SmoothStep function for smooth start and end

        // Move camera toward target smoothly
        transform.position = Vector3.Lerp(startPos, targetPosition, t);

        // Zoom-in smoothly
        cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset =
            Vector3.Lerp(startZoomOffset, targetZoomOffset, t);

        timeElapsed += Time.deltaTime;
        followOffset = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset;
        yield return null; // Wait for next frame
    }

    // Ensure final position and zoom
    transform.position = targetPosition;
    cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = targetZoomOffset;

    focusRoutine = null; // Reset coroutine reference
    Debug.Log("Focus Done.");
}

    public void SetFocusOn(GameObject target){
        //called by global ui to focus on.
        // TargetForFocus=target;
        if (focusRoutine != null)
        {
            StopCoroutine(focusRoutine);
        }
        TargetForFocus = target;
        focusRoutine = StartCoroutine(FocusingOnTarget());
    }
   private IEnumerator FocusingOnTarget()
{
    float timeElapsed = 0f;
    Vector3 startPos = transform.position;
    Vector3 targetPos = TargetForFocus.transform.position;

    // Get the current zoom offset
    Vector3 startZoomOffset = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>().
    m_FollowOffset;
    Vector3 targetZoomOffset = startZoomOffset.normalized * Mathf.Clamp(
        normalObjectZoom, followOffsetMin, followOffsetMax);; // Adjust this factor for desired 
    //zoom strength

    while (timeElapsed < FocusingTimeLimit)
    {
        float t = timeElapsed / FocusingTimeLimit;
        t = t * t * (3f - 2f * t); // SmoothStep function for smooth start and end

        // Move camera toward target smoothly
        transform.position = Vector3.Lerp(startPos, targetPos, t);

        // Zoom-in smoothly
        cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset =
            Vector3.Lerp(startZoomOffset, targetZoomOffset, t);

        timeElapsed += Time.deltaTime;
        followOffset = cinemachineVirtualCamera.GetCinemachineComponent<
        CinemachineTransposer>().m_FollowOffset;
        yield return null; // Wait for next frame
    }

    // Ensure final position and zoom
    transform.position = targetPos;
    cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = 
    targetZoomOffset;

    TargetForFocus = null;
    focusRoutine = null; // Reset coroutine reference
    Debug.Log("Focus Done.");
}

public void FollowTheTarget(GameObject target){
    //called when click any unit to follow.
    targetToFollow=target;
    shouldFollow=true;
}
void ResetFollow(){
    targetToFollow=null;
    shouldFollow=false;
}
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

     public void SetFocusOnHome(){
        //called by global ui to focus on.
        // TargetForFocus=target;
        if (focusRoutine != null)
        {
            StopCoroutine(focusRoutine);
        }
        TargetForFocus = Home;
        focusRoutine = StartCoroutine(FocusOnHome());
    }
    IEnumerator FocusOnHome(){
        float timeElapsed = 0f;
    Vector3 startPos = transform.position;
    Vector3 targetPos = TargetForFocus.transform.position;

    // Get the current zoom offset
    Vector3 startZoomOffset = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>().
    m_FollowOffset;
    Vector3 targetZoomOffset = startZoomOffset.normalized * Mathf.Clamp(
        HomeZoom, followOffsetMin, followOffsetMax);; // Adjust this factor for desired 
    //zoom strength

    while (timeElapsed < FocusingTimeLimit)
    {
        float t = timeElapsed / FocusingTimeLimit;
        t = t * t * (3f - 2f * t); // SmoothStep function for smooth start and end

        // Move camera toward target smoothly
        transform.position = Vector3.Lerp(startPos, targetPos, t);

        // Zoom-in smoothly
        cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset =
            Vector3.Lerp(startZoomOffset, targetZoomOffset, t);

        timeElapsed += Time.deltaTime;
        followOffset = cinemachineVirtualCamera.GetCinemachineComponent<
        CinemachineTransposer>().m_FollowOffset;
        yield return null; // Wait for next frame
    }

    // Ensure final position and zoom
    transform.position = targetPos;
    cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = 
    targetZoomOffset;

    TargetForFocus = null;
    focusRoutine = null; // Reset coroutine reference
    Debug.Log("Focus Done.");
    }

   
}
