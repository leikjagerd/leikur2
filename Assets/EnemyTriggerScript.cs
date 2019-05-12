using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTriggerScript : MonoBehaviour
{

	public EnemyController controller;
	public Score score;
    public PlayerController player;
    
    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "PlayerBullet"){
            controller.health -= Random.Range(10, 40);
            if(controller.health <= 0){
            	score.score += 1;
            	controller.clone.SetActive(false);
            	controller.SpawnEnemy();
        	}
            Destroy(other.gameObject);
        } else if(other.gameObject.tag == "Player"){
            player.health -= 37;
        }
    }
}
