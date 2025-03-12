using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;
using Cinemachine;

public class CameraFocus : MonoBehaviour
{
    private bool shouldFollow=false;
    private GameObject targetToFollow=null;

    private CameraSystem cameraSystem;

    private Coroutine focusRoutine;
    [SerializeField]private CinemachineVirtualCamera cinemachineVirtualCamera;
    private GameObject TargetForFocus;
    [SerializeField] private float followOffsetMax,followOffsetMin,zoomSpeed,zoomAmount
    ,FocusingTimeLimit,heightMovementSpeed,normalObjectZoom,HomeZoom;

    private int focusID;
    private Vector3 TargetOnGround;
    //  private Vector3 followOffset;
    // Start is called before the first frame update
    void Start()
    {
        cameraSystem=GetComponent<CameraSystem>();   
    }


    void Update()
    {
            if(!shouldFollow&&targetToFollow==null){
                return;
            }
            else if(shouldFollow&&targetToFollow!=null){
                transform.position=targetToFollow.transform.position;
            }
            else{
                //if target dispear or refresh
                if(targetToFollow){
                    targetToFollow=null;
                }else if(shouldFollow){
                    shouldFollow=false;
                }

            }
            
    }
    //focusing on home 
    public void SetFocusOn(GameObject FocusTarget,int Focusid,Vector3 TargetPoint=default){
        //1-base,2-creep&mine,3-Point on Ground.
        focusID=Focusid;
        if (focusRoutine != null)
        {
            StopCoroutine(focusRoutine);
        }
        TargetForFocus = FocusTarget;
        TargetOnGround=TargetPoint;
        focusRoutine = StartCoroutine(FocusCoroutine());
    }
    IEnumerator FocusCoroutine(){
        float timeElapsed = 0f;
    Vector3 startPos = transform.position;
    Vector3 targetPos;
    if(TargetForFocus){

    targetPos = TargetForFocus.transform.position;
    }
    else{

    targetPos = TargetOnGround;
    }

    // Get the current zoom offset
    Vector3 startZoomOffset = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>().
    m_FollowOffset;
    Vector3 targetZoomOffset;
    if(focusID==1){
        targetZoomOffset = startZoomOffset.normalized * Mathf.Clamp(
        HomeZoom, followOffsetMin, followOffsetMax);; // Adjust this factor for desired 
    //zoom strength
    }
    else if(focusID==2){
        targetZoomOffset = startZoomOffset.normalized * Mathf.Clamp(
        normalObjectZoom, followOffsetMin, followOffsetMax);; // Adjust this factor for desired 
    // //zoom strength

    }else{
        targetZoomOffset = startZoomOffset.normalized * Mathf.Clamp(
        HomeZoom, followOffsetMin, followOffsetMax);;
    }
    

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
        // followOffset = cinemachineVirtualCamera.
        // GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset;
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



    //for focusing the Unit army

    public void FollowTheTarget(GameObject target){
        //called by cameraSystem
        //called when click any unit to follow.
        targetToFollow=target;
        shouldFollow=true;
    }

    public void RefreshingFollow()
    {
        targetToFollow=null;
        shouldFollow=false;
    }

}
