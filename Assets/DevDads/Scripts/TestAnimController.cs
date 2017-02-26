using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAnimController : MonoBehaviour {

	private Animator animator;

	void Start () {
		animator = GetComponent<Animator>();	
	}

	void Update () {
		float x = Input.GetAxis("Horizontal") * 5;
		float y = Input.GetAxis("Vertical") * 5;
		Vector2 dir = new Vector2(x,y);
		animator.SetFloat("speed", dir.normalized.magnitude);
		animator.SetFloat("xAxis", x);
		animator.SetFloat("yAxis", y);
	}
}
