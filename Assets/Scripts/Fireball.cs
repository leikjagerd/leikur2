using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Fireball : MonoBehaviour
{

	public PlayerController player;
    private GameObject enemy;

	Transform bulletTransform;
	Rigidbody2D rigid;

    [HideInInspector]
    public bool fromEnemy;

    public EnemyController enemyController;

    public float speed;


	// Þarf að nota þessi föll til að vinna með Sin Cos og Tan til að reikna stefnur og svona
    private static float DegreesToRadian(float deg){
        return (float)(Math.PI * deg / 180.0);
    }

    private static float RadianToDegreen(float rad){
        return (float)((rad * 180.0) / Math.PI);
    }

    float angle;

    void Start()
    {
        try {
            rigid = GetComponent<Rigidbody2D>();
            bulletTransform = GetComponent<Transform>();
            if(gameObject.tag == "PlayerBullet"){
                angle = (float)DegreesToRadian(-player.transform.rotation.eulerAngles.z+90);
                Vector2 velocity = player.rigid.velocity; //fær hraðann á þínu geimskipi (x og y)
                speed *= 1 + velocity.magnitude; // geri þetta til að skotin glitch'i ekki þegar þú ert á hraðferð (fær hraðann á geimskipi sem float)
            } else if (gameObject.tag == "EnemyBullet"){
                enemy = enemyController.clone;
                float x = player.transform.position.x - enemy.transform.position.x,
                      y = player.transform.position.y - enemy.transform.position.y;
                angle = (float)(Math.Atan(y / x));

                
            }
        } catch (UnassignedReferenceException e){} // til að leikurinn crashi ekki út af fyrsta skotinu í leiknum
       
    }


    void Update()
    {
        Vector3 vec = new Vector3((float)Math.Cos(angle) * speed, (float)Math.Sin(angle) * speed,0); // lætur skotin stefna í rétta átt
        bulletTransform.position -= vec; // hreyfir skotin

    }

}
