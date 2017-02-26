﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Paraphernalia.Components;

public class PlayerController : MonoBehaviour {
    
    public float moveSpeed;
    public float slowDownSpeed;
    public float maxSpeed;
    public float primaryFireRate;
    public float secondaryFireRate;
    public float invisFramesTime;
    public float turnSpeed = 1;
    public int lives;
    public MoveForward shotPrefab;
    public Transform turretTransform;
    public Animator robotAnimator;
    public Transform shotSpawn;
    public LineRenderer laserLineRenderer;
    public float laserStartWidth;
    public float laserEndWidth;
    
    private Vector3 targetForward;

    public float maxEnergy;
    public float energy;
    private float _deltaInvisFrames;
    public float deltaInvisFrames {
        get { return _deltaInvisFrames; }
    }
    private Vector3 _laserPointPosition;
    public Vector3 laserPointPosition {
        get { return _laserPointPosition; }
    }
    private bool _laserPointerActive;
    public bool laserPointerActive {
        get { return _laserPointerActive; }
    }
    private Color originalColor;
    private int floorMask;
    private bool mouseDown;

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
        // originalColor = GetComponent<Renderer>().material.color;
        floorMask = LayerMask.GetMask("Ground");
        maxEnergy = 100;

        Vector3[] initLaserPositions = new Vector3[2] { Vector3.zero, Vector3.zero };
        laserLineRenderer.SetPositions(initLaserPositions);
        laserLineRenderer.startWidth = laserStartWidth;
        laserLineRenderer.endWidth = laserEndWidth;
    }
	
	void Update() {
        if (Input.GetMouseButtonDown(0) && Time.time > primaryFireRate) {
            MoveForward shot = Instantiate(shotPrefab, shotSpawn.position, shotSpawn.rotation) as MoveForward;
            shot.transform.forward = shotSpawn.transform.forward;
            AudioManager.PlayVariedEffect("heavyLaser");
        }

        if (deltaInvisFrames > 0) {
            _deltaInvisFrames -= Time.deltaTime;
            if (deltaInvisFrames < 0)
                _deltaInvisFrames = 0;
            float alpha = deltaInvisFrames*2;
            while (alpha > 1) alpha--;
            // GetComponent<Renderer>().material.color = 
            //     new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
        } else {
            // GetComponent<Renderer>().material.color = 
            //     new Color(originalColor.r, originalColor.g, originalColor.b, 1);
        }

        LookAtMouse();

        drawLaserPointer();

        Move();

    }

    void drawLaserPointer() {
        if (Input.GetMouseButtonDown(1)) {
            mouseDown = true;
        }
        if (Input.GetMouseButtonUp(1))
        {
            mouseDown = false;
        }

        if (mouseDown && energy > 0) {
            energy -= Time.deltaTime * 10;
            if (energy < 0) energy = 0;
            Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit floorHit;            
            if (Physics.Raycast(camRay, out floorHit, 300, floorMask)) {
                _laserPointPosition = floorHit.point;
            }

            laserLineRenderer.SetPosition(0, shotSpawn.position);
            laserLineRenderer.SetPosition(1, _laserPointPosition);

            laserLineRenderer.enabled = true;
            _laserPointerActive = true;
        } else {
            laserLineRenderer.enabled = false;
            _laserPointerActive = false;
        }
    }

    void LookAtMouse() {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;
        if (Physics.Raycast(camRay, out floorHit, Mathf.Infinity, floorMask)) {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = transform.position.y;
            targetForward = playerToMouse.normalized;
        }
    }

	void Move () {
        float moveUp = 0;        
        float moveHorizontal = Input.GetAxis("Horizontal") * moveSpeed;
        float moveForward = Input.GetAxis("Vertical") * moveSpeed;

        if (moveHorizontal > maxSpeed) moveHorizontal = maxSpeed;
        if (moveHorizontal < -maxSpeed) moveHorizontal = -maxSpeed;
        if (moveForward > maxSpeed) moveForward = maxSpeed;
        if (moveForward < -maxSpeed) moveForward = -maxSpeed;

        movement = new Vector3(moveHorizontal, moveUp, moveForward);

        if (moveHorizontal == 0 && moveForward == 0) {
            body.velocity = movement * slowDownSpeed;
        } else {
            body.velocity = movement * moveSpeed;
            transform.forward = Vector3.Lerp(transform.forward, targetForward, turnSpeed * Time.deltaTime);
        }
        
        Vector3 dir = new Vector3(moveHorizontal, 0, moveForward);
        float speed = dir.normalized.magnitude;
        float forward = Vector3.Dot(dir, transform.forward);
        float right = Vector3.Dot(dir, transform.right);
        robotAnimator.SetFloat("speed", speed);
        robotAnimator.SetFloat("forward", forward);
        robotAnimator.SetFloat("right", right);

        turretTransform.rotation = Quaternion.LookRotation(targetForward);  
    }

    public void getHit() {
        Debug.Log("GET HIT");
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
        gameObject.SetActive(false);
    }
}
