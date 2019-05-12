using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerController : MonoBehaviour
{

	float rotationSpeed = 0;

    [HideInInspector]
    public int health;

	public GameObject flame;
	public GameObject bullet;
    public Text healthText;

	private static float Abs(float a){
		return a < 0 ? -a : a;
	}

	private static float LimitTo(float a, float b){
		return Abs(a) <= b ? a : (a < 0 ? -b : b);
	}

	private static float DegreesToRadian(float deg){
		return (float)(Math.PI * deg / 180.0);
	}

    [HideInInspector]
   	public Rigidbody2D rigid;
   	Transform transform;
    

    public void OnDamage(){
        if(health <= 0){
            //Destroy(gameObject);
            Application.LoadLevel(2);
        }
    }

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        transform = GetComponent<Transform>();
        health = 100;

    }

    private static bool zoomed = false;

    void FixedUpdate()
    {
    	float ver = Input.GetAxis("Vertical");
    	float hor = -Input.GetAxis("Horizontal");
    	float angle = (float)DegreesToRadian(transform.rotation.eulerAngles.z);
    	Vector2 vec = new Vector2((float)Math.Sin(angle), (float)Math.Cos(angle));
    	rigid.AddForce(vec * -ver);
    	rotationSpeed = LimitTo(rotationSpeed + (hor / 10), 10);
    	rigid.MoveRotation(rotationSpeed);
    	flame.SetActive(Abs(ver) > 0.3);

        

    	if(Input.GetButtonDown("Shoot")){
    		GameObject clone = Instantiate(bullet, new Vector3(0,0,0), new Quaternion(0,0,0,1));
    		
    		clone.transform.position = new Vector3(transform.position.x,transform.position.y,0);

    	}

        if(Input.GetButtonDown("Zoom")) {
            Camera.main.orthographicSize = zoomed ? 5f : 50f;
            zoomed = !zoomed;
            
        }
        healthText.text = "Health: " + health;
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "EnemyBullet"){
            health -= UnityEngine.Random.Range(3, 15);
            OnDamage();
        }
    }
}
