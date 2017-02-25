using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public GameObject player;
    public float moveSpeed;
    public float slowDownSpeed;
    public float maxSpeed;
    public float invisFramesTime;

    public int lives;
    private float _deltaInvisFrames;
    public float deltaInvisFrames {
        get { return _deltaInvisFrames; }
    }
    private Color originalColor;

    private Rigidbody rb;
    public Rigidbody body {
        get {
            if (rb == null) {
                rb = GetComponent<Rigidbody>();
            }
            return rb;
        }
    }
    private Vector3 movement;

	// Use this for initialization
	void Start () {
        movement = new Vector3(0, 0, 0);
        _deltaInvisFrames = 0;
        originalColor = GetComponent<Renderer>().material.color;
    }
	
	void Update() {
        if (deltaInvisFrames > 0) {
            _deltaInvisFrames -= Time.deltaTime;
            if (deltaInvisFrames < 0)
                _deltaInvisFrames = 0;
            float alpha = deltaInvisFrames*2;
            while (alpha > 1) alpha--;
            GetComponent<Renderer>().material.color = 
                new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
        } else {
            GetComponent<Renderer>().material.color = 
                new Color(originalColor.r, originalColor.g, originalColor.b, 1);
        }
        
    }

	void FixedUpdate () {
        float moveUp = 0;
        if (Input.GetKeyDown(KeyCode.Space) && rb.velocity.y == 0) {
            //moveUp = jumpSpeed;
        }
        
        float moveHorizontal = Input.GetAxis("Horizontal") * moveSpeed;
        float moveVertical = Input.GetAxis("Vertical") * moveSpeed;

        if (moveHorizontal > maxSpeed) moveHorizontal = maxSpeed;
        if (moveHorizontal < -maxSpeed) moveHorizontal = -maxSpeed;
        if (moveVertical > maxSpeed) moveVertical = maxSpeed;
        if (moveVertical < -maxSpeed) moveVertical = -maxSpeed;

        movement = new Vector3(moveHorizontal, moveUp, moveVertical);

        if (moveHorizontal == 0 && moveVertical == 0) {
            body.velocity = movement * slowDownSpeed;
        } else {
            body.velocity = movement * moveSpeed;
        }
        
        
        
    }

    public void getHit() {
        if (this.deltaInvisFrames == 0) {
            if (this.lives > 0) {
                this.lives -= 1;
                _deltaInvisFrames += invisFramesTime;
            } else {
                this.die();
            }
        }
    }

    private void die() {
        Destroy(player.gameObject);
    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b) {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }
}
