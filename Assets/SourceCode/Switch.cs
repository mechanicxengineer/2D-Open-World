using UnityEngine;

public class Switch : MonoBehaviour
{
	public bool active;
	public BoolValue storedValue;
	public Sprite activeSprite;
	public Door door;
	private SpriteRenderer spriteRenderer;

	void Start()
	{
		active = storedValue.runtimeValue;
		spriteRenderer = GetComponent<SpriteRenderer>();
		if (active) { ActivateSwitch(); }
	}

	public void ActivateSwitch()
	{
		active = !active;
		storedValue.runtimeValue = active;
		door.Open();
		spriteRenderer.sprite = activeSprite;
	}
	
	public void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			ActivateSwitch();
		}
	}

}