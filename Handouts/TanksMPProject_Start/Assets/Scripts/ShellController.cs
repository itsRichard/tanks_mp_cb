using UnityEngine;

public class ShellController : MonoBehaviour 
{
	[SerializeField] float shellLifetime = 2f;	//How long does the shell live
	[SerializeField] bool canKill = false;		//Can the shell damage the players?

	bool isLive = true;							//Is the shell able to explode?
	float age;									//How long has the shell been alive
	ParticleSystem explosionParticles;			//The shell explosion effect
	MeshRenderer shellRenderer;					//The shell model renderer


	void Start ()
	{
		//Get references to the components we need
		explosionParticles = GetComponentInChildren<ParticleSystem> ();
		shellRenderer = GetComponent<MeshRenderer> ();
	}
		
	void Update () 
	{	
		//If the shell has been alive too long...
		age += Time.deltaTime;
		if( age > shellLifetime )
		{	
			//...Destroy it
			Destroy(gameObject);
		}
	}

	//When the shell hits something
	void OnCollisionEnter(Collision other)
	{
		//If the shell isn't live, leave
		if (!isLive)
			return;

		//The shell is going to explode and is no longer live
		isLive = false;

		//Hide the shell body
		shellRenderer.enabled = false;
		//Show the explosion particle effect
		explosionParticles.Play (true);

		//If the shell isn't lethal or it didn't hit a player, leave
		if(!canKill || other.gameObject.tag != "Player")
			return;

		//Get a reference to the hit object's Tank Health script and tell it to take a point of damage
		TankHealth health = other.gameObject.GetComponent<TankHealth> ();

		if(health != null)
			health.TakeDamage (1);
	}
}
