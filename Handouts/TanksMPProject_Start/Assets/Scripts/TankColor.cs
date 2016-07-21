using UnityEngine;
using UnityEngine.Networking;

public class TankColor : NetworkBehaviour 
{
	[SyncVar]
	public Color color;		//The color to change the tanks. The SyncVar attribute means that the value will be shared over the network

	MeshRenderer[] rends;	//Array to store the mesh renderers of the tank

	void Start()
	{
		//Find all mesh renderers on the tank and change their color to the player's chosen color.
		//With the color being a SyncVar, it will be shared with all copies of the player's tank
		//on all clients
		rends = GetComponentsInChildren<MeshRenderer>();
		for(int i = 0; i < rends.Length; i++)
			rends[i].material.color = color;
	}
		
	public void HideTank()
	{
		//Loop through the MeshRenderers and set them to Clear
		for (int i = 0; i < rends.Length; i++)
			rends [i].material.color = Color.clear;
	}
}
