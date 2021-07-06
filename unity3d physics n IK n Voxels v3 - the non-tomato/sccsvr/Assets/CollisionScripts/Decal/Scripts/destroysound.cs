using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroysound : MonoBehaviour
{
    
    int soundframelengthcounter = 0;
    AudioSource thisaudiotodestroy;

	void Start ()
    {
        thisaudiotodestroy = GetComponent<AudioSource>();

    }
	
	void LateUpdate()
    {
        if (soundframelengthcounter > 9 && soundframelengthcounter >= 999)
        {
            if (!thisaudiotodestroy.isPlaying)
            {
                Destroy(this.gameObject);
            }
        }
        soundframelengthcounter++;
    }
}
