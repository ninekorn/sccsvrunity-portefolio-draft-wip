using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class planetcreator : MonoBehaviour {

    public int widthL = 4;
    public int widthR = 3;

    public int heightB = 4;
    public int heightT = 3;

    public int depthF = 4;
    public int depthB = 3;

    public Transform planetchunkdiv;

    void Start ()
    {
        for (int x = -widthL; x <= widthR; x++)
        {
            for (int y = -heightB; y <= heightT; y++)
            {
                for (int z = -depthF; z <= depthB; z++)
                {
                    Transform planetchunkdiv_ =  Instantiate(planetchunkdiv, new Vector3(x * widthL, y * heightB, z * depthF), Quaternion.identity);
                    planetchunkdiv_.parent = this.transform;

                }
            }
        }
    }

    void Update ()
    {
		
	}
}
