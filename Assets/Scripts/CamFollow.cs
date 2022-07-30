using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    [Header("CAMERA FOLLOW STATS")]
    public float mouseSensivity = 10;
    public Transform target;
    public Vector2 pitchMinMax = new Vector2(-40, 85);
    public float rotationSoomthTime = .12f;

    [Header("CAMERA ZOOM")]
    public float distanceFromTarget = 2f;
    public float minDistance = 1f;
    public float maxDistance = 3f;

    [Header("CAMERA COLLISION")]
    public float ajusteCam;
    RaycastHit hit = new RaycastHit();
    Vector3 rotationSmoothVelocity;
    Vector3 currentRotation;
    
    float yaw;
    float pitch;
    
    public Transform currentTarget;
    public string tagName = "enemy";
    
    GameObject[] AllEnemys;
    public bool lockON = false;







    //
	private Transform player;
	
	private Transform rcam;
	private Transform pcam;
	
	private float rotation = 0.0f;
	private float distance = 4.5f;
	
	private float zTargetDistance = 0.0f;
	private float zTargetRotation = 4.5f;

	public bool isTargeting = false;
	private AudioSource[] zTargetSounds;
	private GameObject oldEnemy = null;
    //

void Start()
{
    Cursor.lockState = CursorLockMode.Locked;
    Cursor.visible = false;
    AllEnemys = GameObject.FindGameObjectsWithTag(tagName);
}

void LateUpdate()
{
    if(lockON == false)
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            lockON = true;
        }
    }
    else if(lockON == true)
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            lockON = false;
        }
    }
        if(lockON == false)
    {
        FollowPlayer();
    }
}

private void FixedUpdate()
{
    if (lockON == true)
    {
        GetTarget();
            Vector3 dir = currentTarget.position - transform.position;
            transform.rotation = Quaternion.LookRotation(dir, Vector3.up);
            transform.position = target.position - transform.forward * distanceFromTarget;
        }
}

void FollowPlayer()
{
    yaw += Input.GetAxis("Mouse X") * mouseSensivity;
    pitch += Input.GetAxis("Mouse Y") * mouseSensivity;
    pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);
    currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw), ref rotationSmoothVelocity, rotationSoomthTime);
    transform.eulerAngles = currentRotation;
    transform.position = target.position - transform.forward * distanceFromTarget;
    ZoomCam();
    CamCollision();
}

void CamCollision()
{
    if (Physics.Linecast(target.position, transform.position, out hit))
    {
        transform.position = hit.point + transform.forward * ajusteCam * -distanceFromTarget;
    }
}

void ZoomCam()
{
    if (Input.GetAxis("Mouse ScrollWheel") < distanceFromTarget)
    {
        distanceFromTarget -= Input.GetAxis("Mouse ScrollWheel");
    }
    if (Input.GetAxis("Mouse ScrollWheel") > distanceFromTarget)
    {
        distanceFromTarget += Input.GetAxis("Mouse ScrollWheel");
    }
    if(distanceFromTarget <= minDistance)
    {
        distanceFromTarget = minDistance;
    }
    if(distanceFromTarget >= maxDistance)
    {
        distanceFromTarget = maxDistance;
    }
}

void GetTarget()
{
    Transform tMin = null;
    float minDist = Mathf.Infinity;
    
    foreach (var enemy in AllEnemys)
    {
        float dist = (enemy.transform.position - transform.position).magnitude;
        if (dist < minDist)
        {
            tMin = enemy.transform;
            minDist = dist;
        }
    }
    currentTarget = tMin;
 }


}