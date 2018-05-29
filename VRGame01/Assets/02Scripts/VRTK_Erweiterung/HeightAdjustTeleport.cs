using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using NewtonVR;

namespace VRTK_Erweiterung
{
	public class HeightAdjustTeleport : VRTK_HeightAdjustTeleport
	{
		[Header("NewtonVR Settings")]
		public NVRPlayer Player;

		Vector3 relPosition;

		protected override void DoTeleport(object sender, DestinationMarkerEventArgs e)
		{
			// ErweiterungStart ---
			relPosition = Player.LeftHand.transform.position;
			// ErweiterungEnde ----

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
				relPosition = Player.LeftHand.transform.position - relPosition;
				TeleportInteractables ();
				// ErweiterungEnde ----
			}
		}

		void TeleportInteractables()
		{
			NVRInteractable LHandInteractable = Player.LeftHand.CurrentlyInteracting;
			NVRInteractable RHandInteractable = Player.RightHand.CurrentlyInteracting;

			if (LHandInteractable != null)
			{
				DoNotTeleportObject LTO = LHandInteractable.transform.gameObject.GetComponent<DoNotTeleportObject> ();
				if (LTO == null || LTO.DoTeleport)
				{
					if(LTO != null && LTO.IsBeschleuniger)
					{
						Beschleuniger b = LHandInteractable.transform.gameObject.GetComponentInChildren<Beschleuniger> ();

						if (b.munition != null)
						{
							for(int i = 0; i < b.munition.Length; i++)
							{
								b.munition[i].rigidbody.MovePosition(b.munition[i].rigidbody.position + relPosition);
							}
							//b.munition.MovePosition(b.munition.position + relPosition);
							//b.munition.velocity = Vector3.zero;
						}
					}
					LHandInteractable.transform.position += relPosition;
				}
			}
			if (RHandInteractable != null)
			{
				DoNotTeleportObject RTO = RHandInteractable.transform.gameObject.GetComponent<DoNotTeleportObject> ();
				if (RTO == null || RTO.DoTeleport)
				{
					if(RTO != null && RTO.IsBeschleuniger)
					{
						Beschleuniger b = RHandInteractable.transform.gameObject.GetComponentInChildren<Beschleuniger> ();

						if (b.munition != null)
						{
							for(int i = 0; i < b.munition.Length; i++)
							{
								b.munition[i].rigidbody.MovePosition(b.munition[i].rigidbody.position + relPosition);
							}
							//b.munition.MovePosition(b.munition.position + relPosition);
							//b.munition.velocity = Vector3.zero;
						}
					}
					//RHandInteractable.transform.position = Player.RightHand.transform.position;
					RHandInteractable.transform.position += relPosition;
				}
			}
		}
	}
}