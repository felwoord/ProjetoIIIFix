using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SmoothCameraFollow : MonoBehaviour {

	// The position that that camera will be following.
	public Transform target;            
	// The speed with which the camera will be following.
	[Range(0,1)]
	public float smoothing = 1f;       
	// The initial offset from the target.
	Vector3 offset;                     
	// Boolean to indicate we are locked on to the target and moving with it. Can set to false if we want alternate camera movement
	public bool LockedOn = false;

	void Start()
	{
		Scene sce = SceneManager.GetActiveScene ();
		if (sce.name == "1Tutorial") {
			Invoke ("StartCameraFollow", 2.1f);
		} else {
			StartCameraFollow ();
		}
	}

	void FixedUpdate()
	{
		if(LockedOn)
		{
			// Create a postion the camera is aiming for based on the offset from the target.
			Vector3 targetCamPos = target.localPosition + offset;

			// Smoothly interpolate between the camera's current position and it's target position.
			transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing);
		}
	}

	private void StartCameraFollow(){
		target = GameObject.Find ("Player").transform;
		offset = transform.localPosition - target.localPosition;
		LockedOn = true;
	}
}