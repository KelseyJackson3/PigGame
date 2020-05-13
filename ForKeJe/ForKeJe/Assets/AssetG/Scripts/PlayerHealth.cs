using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayerController))]
public class PlayerHealth : MonoBehaviour
{
    #region  Variables
    //Refernce to script holding majority of the variables
    public PlayerController playerController;

    //used to call Die function only once upon death
    private bool isDead = false;
    #endregion

    #region Variable Initialization
    //Called on script load
    void Awake()
    {
        //Reset variables
        playerController = gameObject.GetComponent<PlayerController>();
        playerController.health = playerController.maxhealth;
        isDead = false;

        if (playerController == null)
            playerController = GetComponent<PlayerController>();
    }
    #endregion

    //Called once per frame
    void Update()
    {
        //Check to see if the player health is lower than 0
        if (playerController.health <= 0 && isDead == false)
        {
            Die();
        }
    }

    #region Collision Trigger Checks
    //Called when collided with an object set to Trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject colGO = collision.gameObject;

        //Check to see if the player has reached the goal
        if (colGO.tag == "GOAL")
        {
            print("Player reached Goal");

            int currentScene = SceneManager.GetActiveScene().buildIndex;
            print("Current scene index is " + currentScene);
            StartCoroutine(LoadSceneWithDelay(currentScene + 1, 3));
        }

        //check to see if the player went out of the bounds
        else if (colGO.tag == "Bounds")
        {
            print("player hit bounds");

            Die();
        }
    }
    #endregion

    #region  Goomba Effect
    void OnCollisionEnter2D(Collision2D other)
    {
        //Check to see if the player has been hit by an enemy or hit an enemy
        if (other.gameObject.tag == "Enemy")
        {
            foreach (ContactPoint2D point in other.contacts)
            {
                Enemy enemyController = other.gameObject.GetComponent<Enemy>();

                Debug.Log(point.normal);
                Debug.DrawLine(point.point, point.point + point.normal, Color.red, 10);

                if (point.normal.y >= 0.9f)
                {
                    Vector2 velocity = playerController.rb.velocity;
                    velocity.y = playerController.jumpForce;
                    playerController.rb.velocity = velocity;
                    enemyController.TakeDamage(playerController.goombaDmg);

                    print("player hit enemy");
                }
                else
                {
                    TakeDamage(enemyController.damage);
                    print("Enemy hit Player");
                }
            }


        }
    }
    #endregion

    #region Scene Loading
    //Called to load a scene with a delay
    public IEnumerator LoadSceneWithDelay(int sceneIndex, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        print("Level Completed");
        SceneManager.LoadScene(sceneIndex);
    }

    #endregion

    #region  Damage & Death
    //Called to inflict damage upon the player
    public void TakeDamage(float amount)
    {
        print("damage taken");
        playerController.health -= amount;
    }

    //Called upon death
    public void Die()
    {
        isDead = true;

        if (playerController.lives < 0)
        {
            //this.gameObject.SetActive(true);
            //Respawn
            Instantiate(playerController.player, playerController.spawnPoint.transform.position, playerController.spawnPoint.transform.rotation);
            //Destroy(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);

            int currentScene = SceneManager.GetActiveScene().buildIndex;
            playerController.lives = playerController.startingLives;
            StartCoroutine(LoadSceneWithDelay(currentScene, 3));
        }

        if (playerController.deatheffect != null)
        {
            playerController.lives--;
            GameObject effect = Instantiate(playerController.deatheffect, transform.position, Quaternion.identity);
            Destroy(effect, 5);
        }
    }
    #endregion
}
