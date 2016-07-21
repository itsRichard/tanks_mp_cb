using UnityEngine;
using UnityEngine.Networking;

//This script causes the camera to rotate around the scene before the game starts. It is 
//meant to demonstrate how to extend the NetworkManager
public class NetworkManager_CameraControl : NetworkManager
{
	[Header("Scene Camera Properties")]
	[SerializeField] Transform sceneCamera;				//The scene camera
	[SerializeField] float cameraRotationRadius = 24f;	//The radius of the camera's rotation
	[SerializeField] float cameraRotationSpeed = 3f;	//The speed of the camera's rotation
	[SerializeField] bool canRotate = true;				//Can the camera be rotated?

	float rotation;		//Current rotation of the camera

	public override void OnStartClient( NetworkClient client ) 
	{
		canRotate = false;
	}

	public override void OnStartHost() 
	{
		canRotate = false;
	}

	public override void OnStopClient () 
	{
		canRotate = true;
	}

	public override void OnStopHost () 
	{
		canRotate = true;
	}

	void Update()
	{
		//If we can't rotate, leave
		if (!canRotate)
			return;

		//Calculate the new rotation and make sure it isn't larger than 360 degrees
		rotation += cameraRotationSpeed * Time.deltaTime;
		if (rotation >= 360f)
			rotation -= 360f;

		//Rotate the camera around the center of the scene
		sceneCamera.position = Vector3.zero;
		sceneCamera.rotation = Quaternion.Euler (0f, rotation, 0f);
		sceneCamera.Translate (0f, cameraRotationRadius, -cameraRotationRadius);
		sceneCamera.LookAt (Vector3.zero);
	}
}
