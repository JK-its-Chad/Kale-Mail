using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvelopeScript : MonoBehaviour {

    public bool isFull = false;
    [SerializeField] GameObject content;
    [SerializeField] Transform lid;
    [SerializeField] GameObject writing;

    [SerializeField] LayerMask mailbox;

    PlayerMove player;

    enum ColorWorth { white, green, yellow, orange, red, purple, blue }

    public float value = 0f;

	void Start ()
    {
        player = GameObject.Find("Player").GetComponent<PlayerMove>();
	}
	
	void Update ()
    {
		if(isFull)
        {
            lid.localRotation = Quaternion.Euler(180, 0, 0);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<KaleChipScript>() && !isFull)
        {
            isFull = true;
            value = other.GetComponent<KaleChipScript>().value;
            //content.color = value
            content.SetActive(true);
            Destroy(other.gameObject);
        }

        if(other.tag == "MailBox")
        {
            getAddressWorth();
            player.score += value;
            Destroy(gameObject, 1f);
        }
    }

    void getAddressWorth()
    {
        foreach (MeshRenderer mesh in writing.GetComponentsInChildren<MeshRenderer>())
        {
            if(mesh.material.color == Color.green)
            {
                value += 1;
            }
            if (mesh.material.color == Color.yellow)
            {
                value += 2;
            }
            if (mesh.material.color == Color.red)
            {
                value += 3;
            }
            if (mesh.material.color == Color.blue)
            {
                value += 4;
            }
            if (mesh.material.color == Color.black)
            {
                value += 5;
            }
        }
    }
}
