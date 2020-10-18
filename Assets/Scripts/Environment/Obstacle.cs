using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public ParticleSystem particles;
    private Player player;
    private Animator animator;
    public AudioClip destructionSound;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        animator = GetComponent<Animator>();

        if (GameController.sfx)
        {
            GetComponent<AudioSource>().Play();
        }
    }


    private void OnParticleCollision(GameObject other)
    {
        // Checking if particle that collided is water and obstacle is fire
        if ((other.CompareTag("Water") && gameObject.CompareTag("Fire")) ||
            (other.CompareTag("Earth") && gameObject.CompareTag("Fire")) ||
            (other.CompareTag("Earth") && gameObject.CompareTag("Water")) ||
            (other.CompareTag("Air") && gameObject.CompareTag("Earth"))
            )
        {

            if (GameController.tutorial)
            {
                GameObject.Find("TutorialTrigger").GetComponent<Tutorial>().ObstacleDestroyed();
            }

            else
            {
                player.IncreaseCoins();
            }
            
            if (GameController.sfx && destructionSound)
            {
                GetComponent<AudioSource>().clip = destructionSound;
                GetComponent<AudioSource>().Play();
            }

            animator.Play(gameObject.name.Replace("(Clone)", ""));
        }

    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }

    public void PlayParticles()
    {
        Instantiate(particles, transform.position, Quaternion.identity);
    }

}
