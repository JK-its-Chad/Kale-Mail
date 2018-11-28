using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvelopeSpawner : MonoBehaviour {

    [SerializeField] Transform holder1;
    [SerializeField] Transform holder2;
    [SerializeField] Transform holder3;

    [SerializeField] GameObject envelope;

    PlayerMove player;
    float timer = 0;
    float timerMax = 1;

	void Start ()
    {
        player = GameObject.Find("Player").GetComponent<PlayerMove>();
        MoreEnve();
    }

    void Update()
    {
        timer -= Time.deltaTime;
    }


    void OnTriggerEnter(Collider other)
    {
        if(player.score > 0 && other.tag == "Button" && timer <= 0)
        {
            timer = timerMax;
            player.score -= 10;
            MoreEnve();
        }
    }

    void MoreEnve()
    {
        Instantiate(envelope, holder1.position, Quaternion.Euler(90, 0, -90));
        Instantiate(envelope, holder2.position, Quaternion.Euler(90, 0, -90));
        Instantiate(envelope, holder3.position, Quaternion.Euler(90, 0, -90));
    }

}
