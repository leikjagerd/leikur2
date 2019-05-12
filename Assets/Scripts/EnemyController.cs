using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class EnemyController : MonoBehaviour
{
	public GameObject ship;
    public GameObject player;
    public GameObject bullet;
    public GameObject arrow;


    Rigidbody2D rigid;

    [HideInInspector] // við viljum ekki sjá þetta í inspector en samt viljum við hafa þetta á public til að aðrar scriptur geti fiktað með þetta
    public GameObject clone;

    public float slow = 3;

    public int health = 100;

    private float ticksToShoot, ticks = 0;

    public Text distanceText;
    void Start()
    {
        SpawnEnemy();
    }


    public void SpawnEnemy(){
        ticksToShoot = UnityEngine.Random.Range(30f, 90f);
        float range = 5f;
        float min = 20f;
    	//ship.transform.position = new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0);
    	clone = Instantiate(ship, new Vector3(player.transform.position.x + UnityEngine.Random.Range(-range, range) + min,player.transform.position.y + UnityEngine.Random.Range(-range,range) + min,0), new Quaternion(0,0,0,1));
        clone.transform.position = new Vector3(UnityEngine.Random.Range(-range, range) + min, UnityEngine.Random.Range(-range, range) + min, 0);
        rigid = clone.GetComponent<Rigidbody2D>();
        ship.SetActive(false);
        clone.SetActive(true);
        health = 100;

    }

    IEnumerator NextSpawn(){
    	yield return new WaitForSeconds(1);
    	SpawnEnemy();
    }

    void RandomShoot(){


        GameObject b = Instantiate(bullet, new Vector3(0,0,0), new Quaternion(0,0,0,1));
        b.gameObject.tag = "EnemyBullet";
        b.transform.position = new Vector3((float)clone.transform.position.x, (float)clone.transform.position.y, 0);
    }

    private void UpdatePointer(){
        Vector2 vec = new Vector2(player.transform.position.x - clone.transform.position.x, player.transform.position.y - clone.transform.position.y);
        float ang = (float)Math.Atan(vec.y / vec.x);
        float x = (float) (player.transform.position.x + (vec.x < 0 ? Math.Cos(ang) :- Math.Cos(ang)));
        float y = (float) (player.transform.position.y + (vec.x < 0 ? Math.Sin(ang) :- Math.Sin(ang)));
        float deg = (float)((ang * 180) / Math.PI);
        arrow.transform.position = new Vector3((float) x, (float)y, 0);
        arrow.transform.eulerAngles = new Vector3(0, 0, (vec.x < 0 ? deg : deg + 180));
    }

    void FixedUpdate()
    {
        UpdatePointer();

        Vector2 distance = new Vector2(player.transform.position.x - clone.transform.position.x, player.transform.position.y - clone.transform.position.y);
        float distance_l = (float)Math.Sqrt(distance.x*distance.x + distance.y*distance.y);
        distanceText.text = "Distance: " + distance_l + "m";
        Vector2 force = new Vector2(distance.x / distance_l, distance.y / distance_l);
        rigid.AddForce(force / slow);
        ticks++;
        if(ticks >= ticksToShoot){
            ticksToShoot = UnityEngine.Random.Range(30f, 90f);
            ticks = 0;
            RandomShoot();
        }
    }

}
