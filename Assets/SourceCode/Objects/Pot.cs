using System;
using System.Collections;
using UnityEngine;

public class Pot : MonoBehaviour
{
	private Animator animator;
	// Start is called before the first frame update
	void Start()
	{
		animator = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void Smash()
	{
		animator.SetBool("smash", true);
		StartCoroutine(breakCo());
	}

	IEnumerator breakCo()
	{
		yield return new WaitForSeconds(.5f);
		this.gameObject.SetActive(false);
	}
}