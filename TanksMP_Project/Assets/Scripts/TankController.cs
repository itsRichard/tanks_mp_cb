using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class TankController :  MonoBehaviour
{
	[Header("Movement Variables")]
	[SerializeField] float movementSpeed = 5.0f;	//The speed of the object
	[SerializeField] float turnSpeed = 45.0f;		//The turn speed of the object
	[Header("Camera Position Variables")]
	[SerializeField] float cameraDistance = 16f;	//Distance from the tank that the camera should be
	[SerializeField] float cameraHeight = 16f;		//The height off of the ground that the camera should be

	Rigidbody localRigidBody;	//Cache the reference to the rigidbody
	Transform mainCamera;		//Reference to the scene's main camera
	Vector3 cameraOffset;		//A vector3 containing how far back and up the camera should be from the tank

	void Start()
	{
		//Get the reference to the object's rigidbody since we will be using it a lot
		localRigidBody = GetComponent<Rigidbody> ();

		//Set up the camera offset for future use
		cameraOffset = new Vector3(0f, cameraHeight, -cameraDistance);

		//Find the main scene camera and move it into the correct position
		mainCamera = Camera.main.transform;
		MoveCamera ();
	}

	void FixedUpdate()
	{
		//Get the horizontal and vertical input. Note that we can get input for any platform
		//using the CrossPlatformInput class
		float turnAmount = CrossPlatformInputManager.GetAxis("Horizontal");
		float moveAmount= CrossPlatformInputManager.GetAxis("Vertical"); 

		//Calculate and apply the new position
		Vector3 deltaTranslation = transform.position + transform.forward * movementSpeed * moveAmount * Time.deltaTime;
		localRigidBody.MovePosition (deltaTranslation);

		//Calculate and apply the new rotation
		Quaternion deltaRotation = Quaternion.Euler (turnSpeed * new Vector3 (0, turnAmount, 0) * Time.deltaTime);
		localRigidBody.MoveRotation (localRigidBody.rotation * deltaRotation);

		//Update the camera's position
		MoveCamera ();
	}

	//This method moves the camera to the correct spot behind the player
	void MoveCamera()
	{
		mainCamera.position = transform.position;	//...position the camera on the tank
		mainCamera.rotation = transform.rotation;	//...align the camera with the tank
		mainCamera.Translate (cameraOffset);		//...move the camera up and away from the tank
		mainCamera.LookAt(transform);				//...make the camera look at the tank
	}
}
