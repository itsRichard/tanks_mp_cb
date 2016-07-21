using UnityEngine;
using System.Collections.Generic;

public class DeathMatchManager 
{
	static List<TankHealth_DM> tanks = new List<TankHealth_DM>();	//List of tanks playing in the match

	public static void AddTank(TankHealth_DM tank)
	{
		//Add tank to the list
		tanks.Add (tank);
	}

	public static bool RemoveTankAndCheckWinner(TankHealth_DM tank)
	{
		//Remove the tank that died
		tanks.Remove (tank);

		//If there is only one tank left, return true which means there is a winner
		if (tanks.Count == 1)
			return true;

		//Otherwise, return false as there are multiple tanks left
		return false;
	}

	public static TankHealth_DM GetWinner()
	{
		//Check to make sure that there is one winner
		if (tanks.Count != 1)
			return null;

		//Return the last tank: the winner
		return tanks [0];
	}
}
