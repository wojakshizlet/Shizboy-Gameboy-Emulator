using RedRunner;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class SoundManager : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource footsteps;
    public AudioSource jumping;
    //public AudioSource pausedDialogue;
    //public GameManager gameManager; 

    //if gamerunning.m_gamerunning, play audio
    //else dont play audio

    Boolean flag = false;
    float footstepTime = 0.2f;
    float timeDuration = 0.0f;
    Boolean isGrounded = true;

    // Update is called once per frame
    void Update()
    {

        timeDuration = Time.deltaTime;
        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            if (isGrounded) {
                StopAllCoroutines();

                StartCoroutine(jump());
            }

        }

        if (CrossPlatformInputManager.GetAxis("Horizontal") > 0 || CrossPlatformInputManager.GetAxis("Horizontal") < 0) {

            if (!flag)
            {
                StartCoroutine(feet());
                timeDuration -= footstepTime;
            }


            //            StartCoroutine(feet());

            //if (CrossPlatformInputManager.GetButtonDown("Jump"))
            //{
            //    Jump();
            //}
        }

        //if (gameManager.m_GameRunning) {
        //    pausedDialogue.Play();
        //}
    }

    IEnumerator feet() {
        footsteps.time = 0;
        float random = UnityEngine.Random.value + 0.5f;
        footsteps.pitch = random;

        footsteps.Play();
        flag = true;

        yield return new WaitForSeconds(0.25f);
        footsteps.Stop();
        flag = false;
    }

    IEnumerator jump() {
        isGrounded = false;
        flag = true;

        footsteps.Stop();
        jumping.Play();
        yield return new WaitForSeconds(1.0f);

        flag = false;
        isGrounded = true;
    }
}
