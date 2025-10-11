using System.Collections;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
	[Header("Position variables")]
	public Transform target;
	public float smoothSpeed;
	public Vector2 minPosition;
	public Vector2 maxPosition;

	[Header("Animation variables")]
	public Animator animator;

	[Header("Position Reset")]
	public VectorValue cameraMin;
	public VectorValue cameraMax;

	void Start()
	{
		maxPosition = cameraMax.initialValue;
		minPosition = cameraMin.initialValue;
		animator = GetComponent<Animator>();
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

	public void BeginCameraShake()
	{
		animator.SetBool("shake", true);
		StartCoroutine(ShakeCo());
	}
	
	public IEnumerator ShakeCo()
    {
		yield return null;
		animator.SetBool("shake", false);
    }
}