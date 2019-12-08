using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject player;
    public AudioSource mSource;
    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;

    private Vector3 offset;

    private static int playerScore;


    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.transform.position;
        mSource.clip = musicClipOne;
        mSource.Play();
        
    }

        void Update()
    {

        playerScore = PlayerScript.score;
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }


        if (playerScore >= 8)
        {
            if (mSource.clip == musicClipOne)
            {

                mSource.clip = musicClipTwo;

                mSource.Play();

            }
        }
    }


    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}
