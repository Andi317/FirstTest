﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using NewtonVR;

namespace VRTK_Erweiterung
{
	public class BasicTeleport : VRTK_BasicTeleport
	{
		[Header("NewtonVR Settings")]
		public NVRPlayer Player;

		protected override void DoTeleport(object sender, DestinationMarkerEventArgs e)
		{
			if (enableTeleport && ValidLocation(e.target, e.destinationPosition) && e.enableTeleport)
			{
				StartTeleport(sender, e);
				Quaternion updatedRotation = SetNewRotation(e.destinationRotation);
				Vector3 newPosition = GetNewPosition(e.destinationPosition, e.target, e.forceDestinationPosition);
				CalculateBlinkDelay(blinkTransitionSpeed, newPosition);
				Blink(blinkTransitionSpeed);
				Vector3 updatedPosition = SetNewPosition(newPosition, e.target, e.forceDestinationPosition);
				ProcessOrientation(sender, e, updatedPosition, updatedRotation);
				EndTeleport(sender, e);

				// ErweiterungStart ---
				TeleportInteractables ();
				// ErweiteringEnd ---
			}
		}

		void TeleportInteractables()
		{
			NVRInteractable LHandInteractable = Player.LeftHand.CurrentlyInteracting;
			NVRInteractable RHandInteractable = Player.RightHand.CurrentlyInteracting;

			if (LHandInteractable != null){
				DoNotTeleportObject LTO = LHandInteractable.transform.gameObject.GetComponent<DoNotTeleportObject> ();
				if (LTO == null || LTO.DoTeleport)
				{
					LHandInteractable.transform.position = Player.LeftHand.transform.position;
				}
			}
			if (RHandInteractable != null) {
				DoNotTeleportObject RTO = RHandInteractable.transform.gameObject.GetComponent<DoNotTeleportObject> ();
				if (RTO == null || RTO.DoTeleport)
				{
					RHandInteractable.transform.position = Player.RightHand.transform.position;
				}
			}
		}
	}
}	