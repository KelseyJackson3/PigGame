using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        print("Triggered");
        if (other.gameObject.tag == "Player")
        {
            print("Player reached Goal");

            int currentScene = SceneManager.GetActiveScene().buildIndex;

            print("Current scene index is " + currentScene);

            StartCoroutine(LoadSceneWithDelay(currentScene + 1, 3));
        }
    }

    public IEnumerator LoadSceneWithDelay(int sceneIndex, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        print("Level Completed");

        SceneManager.LoadScene(sceneIndex);
    }
}
