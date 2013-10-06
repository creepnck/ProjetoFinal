using UnityEngine;
using System.Collections;

public class DoorTrigger : MonoBehaviour
{
	private Transform roomFloor;
	
	void Start()
	{
		roomFloor =  transform.parent.FindChild("RoomFloor");
	}
	
	void OnTriggerEnter (Collider other)
	{
		if (other.CompareTag("Player"))
		{
			Debug.Log("Door has been hit.");
			Vector3 nextRoomPosition = Vector3.zero;
			Vector3 nextPlayerPosition = Vector3.zero;
			Room roomApi = transform.parent.GetComponent<GameRoom>().room;
			Room nextRoom = null;
			float worldX;
			float worldZ;
			switch (this.gameObject.transform.name.ToUpper()) {
				case "NORTHDOOR":

					worldX = roomApi.GetTop().worldX;
					worldZ = roomApi.GetTop().worldZ;
					
					nextRoomPosition = new Vector3(worldX, 0, worldZ);

					nextPlayerPosition = nextRoomPosition + Vector3.forward * (-(roomFloor.localScale.z/2) + (transform.localScale.z/2) + (other.transform.localScale.z/2) + 0.5f);
					nextPlayerPosition = new Vector3(other.transform.position.x, nextPlayerPosition.y, nextPlayerPosition.z);

					nextRoom = roomApi.GetTop();
					
					break;
				
				case "SOUTHDOOR":	
				
					worldX = roomApi.GetBottom().worldX;
					worldZ = roomApi.GetBottom().worldZ;
					
					nextRoomPosition = new Vector3(worldX, 0, worldZ);
					nextPlayerPosition = nextRoomPosition + Vector3.back * (-(roomFloor.localScale.z/2) + (transform.localScale.z/2) + (other.transform.localScale.z/2) + 0.5f);
					nextPlayerPosition = new Vector3(other.transform.position.x, nextPlayerPosition.y, nextPlayerPosition.z);
					nextRoom = roomApi.GetBottom();
				
					break;
				
				case "EASTDOOR":
				
					worldX = roomApi.GetRight().worldX;
					worldZ = roomApi.GetRight().worldZ;
					
					nextRoomPosition = new Vector3(worldX, 0, worldZ);
					nextPlayerPosition = nextRoomPosition + Vector3.right * (-(roomFloor.localScale.x/2) + (transform.localScale.x/2) - (other.transform.localScale.x/2));
					nextPlayerPosition = new Vector3(nextPlayerPosition.x, nextPlayerPosition.y, other.transform.position.z);
					nextRoom = roomApi.GetRight();
				
					break;
				
				case "WESTDOOR":
				
					worldX = roomApi.GetLeft().worldX;
					worldZ = roomApi.GetLeft().worldZ;
					
					nextRoomPosition = new Vector3(worldX, 0, worldZ);
					nextPlayerPosition = nextRoomPosition + Vector3.left * (-(roomFloor.localScale.x/2) + (transform.localScale.x/2) - (other.transform.localScale.x/2));
					nextPlayerPosition = new Vector3(nextPlayerPosition.x, nextPlayerPosition.y, other.transform.position.z);
				
					nextRoom = roomApi.GetLeft();
				
					break;
			}
			
			roomApi.setActiveTrap(false);
			nextRoom.setActiveTrap(true);
			
			Camera.main.GetComponent<CameraBehaviour>().snapToPosition(nextRoomPosition);
			other.GetComponent<PlayerMovement>().snapToPosition(nextPlayerPosition);
		}
	}
}

