using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvelopeScript : MonoBehaviour {

    public bool isFull = false;
    [SerializeField] GameObject content;
    [SerializeField] Transform lid;
    [SerializeField] GameObject writing;
    List<MeshRenderer> address;

    public float value = 0f;

	void Start ()
    {
        foreach(MeshRenderer mesh in writing.GetComponentsInChildren<MeshRenderer>())
        {
            address.Add(mesh);
        }
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
    }
}
