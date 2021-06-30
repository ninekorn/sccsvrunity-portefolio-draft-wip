using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootableBox : MonoBehaviour {

    public int currentHealth = 3;


    public void Damage (int damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth <=0)
        {
            gameObject.SetActive(false);
        }
    }


	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
