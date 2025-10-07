using Unity.Burst.Intrinsics;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
	public Transform target;
	public float smoothSpeed;
	public Vector2 minPosition;
	public Vector2 maxPosition;

	void Start()
	{
		transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
	}

	void FixedUpdate()
	{
		if (transform.position != target.position)
		{
			Vector3 desiredPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
			desiredPosition.x = Mathf.Clamp(desiredPosition.x, minPosition.x, maxPosition.x);
			desiredPosition.y = Mathf.Clamp(desiredPosition.y, minPosition.y, maxPosition.y);
			transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
		}
	}
}