using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    
    public float moveSpeed;
    public float slowDownSpeed;
    public float maxSpeed;
    public float primaryFireRate;
    public float secondaryFireRate;
    public float invisFramesTime;
    public int lives;
    public MoveForward shotPrefab;
    private Transform shotSpawn;

    private float _deltaInvisFrames;
    public float deltaInvisFrames {
        get { return _deltaInvisFrames; }
    }
    private Color originalColor;
    private int floorMask;
    private float deltaPrimaryFireRate = 0f;

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
        shotSpawn = transform.GetChild(0);
        floorMask = LayerMask.GetMask("Ground");
    }
	
	void Update() {
        if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time > primaryFireRate) {
            deltaPrimaryFireRate = Time.time + primaryFireRate;
            MoveForward shot = Instantiate(shotPrefab, shotSpawn.position, shotSpawn.rotation) as MoveForward;
            shot.transform.forward = shotSpawn.transform.forward;
        }

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

        // Create a ray from the mouse cursor on screen in the direction of the camera.
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        // Create a RaycastHit variable to store information about what was hit by the ray.
        RaycastHit floorHit;
        // Perform the raycast and if it hits something on the floor layer...
        if (Physics.Raycast(camRay, out floorHit, Mathf.Infinity, floorMask)) {
            // Create a vector from the player to the point on the floor the raycast from the mouse hit.
            Vector3 playerToMouse = floorHit.point - transform.position;

            // Ensure the vector is entirely along the floor plane.
            playerToMouse.y = 0f;

            // Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);

            // Set the player's rotation to this new rotation.
            rb.MoveRotation(newRotation);
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
        Destroy(this.gameObject);
    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b) {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }
}
