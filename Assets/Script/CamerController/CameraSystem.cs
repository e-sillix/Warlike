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

    [SerializeField]private float LimitRadius;
    // private Vector3 followOffset;
    private bool istouchingAllowed=true;
    private float touchStartTime;
    private Vector2 touchStartPos;

    private GameObject TargetForFocus;
    private Coroutine focusRoutine;
    private bool exceptionUIActive;
    private bool cameraExceptionMoveAllowed,UnitOnHold=false;

    // private GameObject targetToFollow;
    // private bool shouldFollow=false;
    private CameraFocus cameraFocus;

    void Start()
    {
        cameraFocus=GetComponent<CameraFocus>();
    }

    public void SetTheUniHold(bool t){
        UnitOnHold=t;
    }
    // void Awake(){
    //     followOffset = cinemachineVirtualCamera.GetCinemachineComponent<
    //     CinemachineTransposer>().m_FollowOffset;
    // }
    public void SetException(bool t){
        //By globalUIManager
        exceptionUIActive=t;
    }
    private void Update() {
        
        if(Input.touchCount >0){
            istouchingAllowed=!globalUIManager.IsUIInterfering();
        }
        if(istouchingAllowed){
            // ResetFollow();
        if(exceptionUIActive){
            // ExceptionUITouchMech();
            if (Input.touchCount == 1)
            {
                Touch touch = Input.GetTouch(0);
                if(touch.phase==TouchPhase.Began){
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                int layerMask = LayerMask.GetMask("Blue", "Ground"); // Allow only "Blue" and "Ground"

                if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
                {
                    BluePrint detectedBlueprint = hit.collider.GetComponentInParent<BluePrint>();
                    TowerBluePrint BTower=hit.collider.GetComponentInParent<TowerBluePrint>();
                    if (detectedBlueprint||BTower) {
                        cameraExceptionMoveAllowed=false;
                        Debug.Log("Clicked a blueprint");
                    //    return;
                    // movingAllowed=true;
                    }
                    else{
                        cameraExceptionMoveAllowed=true;
                         Debug.Log("Not Clicked a blueprint");
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
        // if(TargetForFocus){
        //     FocusingOnTarget();
        //     // followOffset = cinemachineVirtualCamera.GetCinemachineComponent<
        //     // CinemachineTransposer>().m_FollowOffset;
        // }
    }
    // void ExceptionUITouchMech(){

    // }
    void RefreshingTouch(){
        if(Input.touchCount>0){
            Touch touch = Input.GetTouch(0);
            if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
        {
            return;
        }
        else{
             if (touch.phase == TouchPhase.Began) // Finger touches the screen
        {
            ResetFollow();
            // Debug.Log("Reset Follow by RefreshingTouch");
            }
        }
        }
    }
    void TouchDetector(){
        if(Input.touchCount == 1){
            
            Touch touch = Input.GetTouch(0);
            if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
        {
            // Debug.Log("Tapped on UI, ignoring touch.");
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
                // Debug.Log("Tap detected!");
                // Handle tap action here
                // Debug.Log("Tap Detector here.");
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

            Plane groundPlane = new Plane(Vector3.up, Vector3.zero); // Assuming ground is at y = 0

            if (groundPlane.Raycast(prevTouchRay, out float enterPrev) && groundPlane.Raycast(touchRay, out float enterCurr))
            {
                Vector3 prevPoint = prevTouchRay.GetPoint(enterPrev);
                Vector3 currPoint = touchRay.GetPoint(enterCurr);

                Vector3 moveDir = prevPoint - currPoint; // Reverse direction for a natural feel
                
                // Calculate new position
                Vector3 newPosition = transform.position + moveDir;

                // Define circular boundary
                Vector3 centerPoint = new Vector3(0f, transform.position.y, 0f); // Center of the circle (change if needed)
                // float maxRadius = 10f; // Maximum allowed distance from the center

                // Calculate distance from the center
                Vector3 offset = newPosition - centerPoint;
                if (offset.magnitude > LimitRadius)
                {
                    // Clamp position to the max radius
                    newPosition = centerPoint + offset.normalized * LimitRadius;
                }

                // Apply the limited position
                transform.position = newPosition;
            }
        }
    }
}


   
  
void ResetFollow(){
    cameraFocus.RefreshingFollow();
}
void HandleCameraZoomTouch()
{
    Vector3 followOffset = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>
    ().m_FollowOffset;
    Vector3 zoomDir = followOffset.normalized;

    if (Input.touchCount == 2) // Detect two-finger touch
    {
        Touch touch0 = Input.GetTouch(0);
        Touch touch1 = Input.GetTouch(1);

        float prevDistance = (touch0.position - touch0.deltaPosition - (touch1.position - 
        touch1.deltaPosition)).magnitude;
        float currentDistance = (touch0.position - touch1.position).magnitude;

        float pinchAmount = (currentDistance - prevDistance) * 0.01f; // Scale pinch sensitivity

        // **Apply Clamp Immediately to Avoid Overshooting**
        float newMagnitude = Mathf.Clamp(followOffset.magnitude - pinchAmount * zoomAmount, 
        followOffsetMin, followOffsetMax);
        followOffset = zoomDir * newMagnitude;

        // Apply Zoom Smoothly
        cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset =
            Vector3.Lerp(
                cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>().
                m_FollowOffset,
                followOffset,
                Time.deltaTime * zoomSpeed
            );
    }
}


     public void SetFocusOnHome(){
        //called by global ui to focus on.
        cameraFocus.SetFocusOn(Home,1);//code 1 for base
    }
    

    public void SetFocusOnPoint(Vector3 targetPoint){
        //called by global ui to focus on. when we are zoomed out.and click any ground
        cameraFocus.SetFocusOn(null,3,targetPoint);//code 3 for point on ground.
    }


    public void SetFocusOn(GameObject target){
        //called by global ui to focus on.
        cameraFocus.SetFocusOn(target,2);//code 2 for mine and creep.
    }

    public void FollowTheTarget(GameObject target){
    //called when click any unit to follow.
    cameraFocus.FollowTheTarget(target);
    }
   
}
