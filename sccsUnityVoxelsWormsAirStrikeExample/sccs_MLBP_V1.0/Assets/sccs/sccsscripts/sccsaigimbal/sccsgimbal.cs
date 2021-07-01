//by steve chassé aka ninekorn

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace SCCoreSystems
{
    public class sccsgimbal : MonoBehaviour
	{
        sccsaiguess[] SC_AI4LR = new sccsaiguess[maxPerceptronInstances];

        public Transform northpole; //2d game target direction bullseye
        public Transform compass; //2d game drone turret
        GameObject compassneedleobject;
        GameObject northpoleobject;

        public const int maxPerceptronInstances = 10;
        public const int maxPerceptronInstancesNeurons = 3; // 3 minimum i think
        float perceptronLearningRate = 0.001f;

        public float needle_rotation_speed = 10f;
        public int swtchwaypointtype = 0;

        int totalRight = 0;
        int totalLeft = 0;
        int frame4RandomRorL = 0;

        public float totalDotgoalRL = 0;
        float dottogoal = 0;

        Vector3 playerlocation = Vector2.zero;
        Vector3 directionright = Vector2.zero;
        Vector3 dirturrettoplayer = Vector2.zero;
        Vector3 dirturrettoenemy = Vector2.zero;

        public Vector3 needleScale = new Vector3(0.8f, 4, 0.8f);
        public Vector3 needlePosition = new Vector3(0, 2, 0);
        public sccscrypto sccscryptoclass;

        public int sccscryptoframeperformanceadjusterswtch = 0;
        public int sccscryptoframeperformanceadjustercounterR = 0;
        public int sccscryptoframeperformanceadjustercounterL = 0;
        public int sccscryptoframeperformanceadjustermaxR = 9;
        public int sccscryptoframeperformanceadjustermaxL = 9;

        void Start()
        {
            compassneedleobject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            compassneedleobject.transform.localScale = needleScale;
            compass = compassneedleobject.transform;

            needlePosition.y += transform.position.y;
            compass.position = needlePosition;

            compass.parent = this.transform;

            if (swtchwaypointtype == 0)
            {
                compass.GetComponent<MeshRenderer>().material.color = Color.blue;
            }
            else if (swtchwaypointtype == 1)
            {
                compass.GetComponent<MeshRenderer>().material.color = Color.green;
            }
            else if (swtchwaypointtype == 2)
            {
                compass.GetComponent<MeshRenderer>().material.color = Color.red;
            }

            /*northpoleobject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            northpoleobject.transform.localScale = new Vector3(2, 2, 2);
            northpole = northpoleobject.transform;
            northpole.transform.position = new Vector3(6, 6, 0);*/

            for (int i = 0; i < SC_AI4LR.Length; i++)
            {
                SC_AI4LR[i] = new sccsaiguess(compass, northpole, maxPerceptronInstancesNeurons, perceptronLearningRate);
                SC_AI4LR[i].SC_anglesNumber = 360;
                SC_AI4LR[i].SC_Angle_Divider = 10;
                SC_AI4LR[i].weightsNumber = 10;
                SC_AI4LR[i].inputsNumber = 20; // minimum 3 for the Trainer class
                SC_AI4LR[i].errormargin = 5;
                SC_AI4LR[i].swtchwaypointtype = swtchwaypointtype;
            }

            sccscryptoclass = gameObject.GetComponent<sccscrypto>();

        }

        public int drawrayspeedframeperformancecounterx = 0;
        public int drawrayspeedframeperformancecountery = 0;
        public int drawrayspeedframeperformancecounterz = 0;

        public int drawrayspeedframeperformancecountermaxx = 0;
        public int drawrayspeedframeperformancecountermaxy = 0;
        public int drawrayspeedframeperformancecountermaxz = 0;


        void Update()
        {
            if (drawrayspeedframeperformancecounterx >= drawrayspeedframeperformancecountermaxx)
            {
                if (swtchwaypointtype == 0) // X AXIS ROTATION
                {
                    Debug.DrawRay(this.transform.position, this.transform.up, Color.blue);
                }
                drawrayspeedframeperformancecounterx = 0;
            }
            if (drawrayspeedframeperformancecountery >= drawrayspeedframeperformancecountermaxy)
            {
                if (swtchwaypointtype == 1) // Y AXIS ROTATION
                {
                    Debug.DrawRay(this.transform.position, this.transform.right, Color.green);
                }
                drawrayspeedframeperformancecountery = 0;
            }
            if (drawrayspeedframeperformancecounterz >= drawrayspeedframeperformancecountermaxz)
            {
                if (swtchwaypointtype == 2) // Z AXIS ROTATION
                {
                    Debug.DrawRay(this.transform.position, this.transform.up, Color.red);
                }
                drawrayspeedframeperformancecounterz = 0;
            }





            totalRight = 0;
            totalLeft = 0;
            totalDotgoalRL = 0;

            for (int i = 0; i < SC_AI4LR.Length; i++)
            {
                if (SC_AI4LR[i] != null)
                {
                    SC_AI4LR[i].UpdatePerceptron();
                    totalRight += SC_AI4LR[i]._guessedCorrectRight;
                    totalLeft += SC_AI4LR[i]._guessedCorrectLeft;
                    totalDotgoalRL += SC_AI4LR[i]._dotGoal;
                }
                else
                {
                    SC_AI4LR[i].UpdatePerceptron();
                    totalRight += SC_AI4LR[i]._guessedCorrectRight;
                    totalLeft += SC_AI4LR[i]._guessedCorrectLeft;
                    totalDotgoalRL += SC_AI4LR[i]._dotGoal;
                }
            }
            totalDotgoalRL /= SC_AI4LR.Length;

            if (swtchwaypointtype == 0)
            {
                //Vector3 eulerAngles = transform.eulerAngles;
                //eulerAngles.x = 0;
                //eulerAngles.y = gearLR.transform.eulerAngles.y;
                //transform.eulerAngles = eulerAngles;

                //Debug.Log("dot: " + totalDotgoalRL); 
                if (totalDotgoalRL < -0.0025f || totalDotgoalRL > 0.0025f)
                {
                    if (totalRight > totalLeft)
                    {
                        //Debug.Log("north pole is right");
                        transform.Rotate(new Vector3(0, 0, -needle_rotation_speed * Mathf.Abs(totalDotgoalRL)), Space.World);
                        if (sccscryptoframeperformanceadjustercounterR > sccscryptoframeperformanceadjustermaxR)
                        {
                            sccscryptoclass.sccscryptoworktesting();
                            sccscryptoframeperformanceadjustercounterR = 0;
                        }

                    }
                    else if (totalRight < totalLeft)
                    {
                        //Debug.Log("north pole is left");
                        transform.Rotate(new Vector3(0, 0, needle_rotation_speed * Mathf.Abs(totalDotgoalRL)), Space.World);
                        if (sccscryptoframeperformanceadjustercounterR > sccscryptoframeperformanceadjustermaxL)
                        {
                            sccscryptoclass.sccscryptoworktesting();
                            sccscryptoframeperformanceadjustercounterL = 0;
                        }
                    }
                    else
                    {
                        if (frame4RandomRorL == 0)
                        {
                            transform.Rotate(new Vector3(0, 0, -needle_rotation_speed * Mathf.Abs(totalDotgoalRL)), Space.World);
                            if (sccscryptoframeperformanceadjustercounterR > sccscryptoframeperformanceadjustermaxR)
                            {
                                sccscryptoclass.sccscryptoworktesting();
                                sccscryptoframeperformanceadjustercounterR = 0;
                            }
                        }
                        else
                        {
                            transform.Rotate(new Vector3(0, 0, needle_rotation_speed * Mathf.Abs(totalDotgoalRL)), Space.World);
                            if (sccscryptoframeperformanceadjustercounterR > sccscryptoframeperformanceadjustermaxL)
                            {
                                sccscryptoclass.sccscryptoworktesting();
                                sccscryptoframeperformanceadjustercounterL = 0;
                            }
                        }
                    }
                }
                else
                {
                    //Debug.Log("found north pole / bullseye");
                }
            }
            else if (swtchwaypointtype == 1)
            {
                Vector3 eulerAngles = transform.eulerAngles;

                eulerAngles.x = 0;
                eulerAngles.z = 0;

                transform.eulerAngles = eulerAngles;

                //Debug.Log("dot: " + totalDotgoalRL); 
                if (totalDotgoalRL < -0.0025f || totalDotgoalRL > 0.0025f)
                {
                    if (totalRight > totalLeft)
                    {
                        //Debug.Log("north pole is right");
                        transform.Rotate(new Vector3(0, -needle_rotation_speed * Mathf.Abs(totalDotgoalRL), 0), Space.World);
                        if (sccscryptoframeperformanceadjustercounterR > sccscryptoframeperformanceadjustermaxR)
                        {
                            sccscryptoclass.sccscryptoworktesting();
                            sccscryptoframeperformanceadjustercounterR = 0;
                        }

                    }
                    else if (totalRight < totalLeft)
                    {
                        //Debug.Log("north pole is left");
                        transform.Rotate(new Vector3(0, needle_rotation_speed * Mathf.Abs(totalDotgoalRL), 0), Space.World);
                        if (sccscryptoframeperformanceadjustercounterL > sccscryptoframeperformanceadjustermaxL)
                        {
                            sccscryptoclass.sccscryptoworktesting();
                            sccscryptoframeperformanceadjustercounterL = 0;
                        }
                    }
                    else
                    {
                        if (frame4RandomRorL == 0)
                        {
                            transform.Rotate(new Vector3(0, -needle_rotation_speed * Mathf.Abs(totalDotgoalRL), 0), Space.World);
                            if (sccscryptoframeperformanceadjustercounterR > sccscryptoframeperformanceadjustermaxR)
                            {
                                sccscryptoclass.sccscryptoworktesting();
                                sccscryptoframeperformanceadjustercounterR = 0;
                            }
                        }
                        else
                        {
                            transform.Rotate(new Vector3(0, needle_rotation_speed * Mathf.Abs(totalDotgoalRL), 0), Space.World);
                            if (sccscryptoframeperformanceadjustercounterL > sccscryptoframeperformanceadjustermaxL)
                            {
                                sccscryptoclass.sccscryptoworktesting();
                                sccscryptoframeperformanceadjustercounterL = 0;
                            }
                        }
                    }
                }
                else
                {
                    //Debug.Log("found north pole / bullseye");
                }
            }
            else if (swtchwaypointtype == 2)
            {

                //Debug.Log("dot: " + totalDotgoalRL); 
                if (totalDotgoalRL < -0.0025f || totalDotgoalRL > 0.0025f)
                {
                    if (totalRight > totalLeft)
                    {
                        //Debug.Log("north pole is right");
                        transform.Rotate(new Vector3(-needle_rotation_speed * Mathf.Abs(totalDotgoalRL), 0, 0), Space.World);
                        if (sccscryptoframeperformanceadjustercounterR > sccscryptoframeperformanceadjustermaxR)
                        {
                            sccscryptoclass.sccscryptoworktesting();
                            sccscryptoframeperformanceadjustercounterR = 0;
                        }

                    }
                    else if (totalRight < totalLeft)
                    {
                        //Debug.Log("north pole is left");
                        transform.Rotate(new Vector3(needle_rotation_speed * Mathf.Abs(totalDotgoalRL), 0, 0), Space.World);
                        if (sccscryptoframeperformanceadjustercounterL > sccscryptoframeperformanceadjustermaxL)
                        {
                            sccscryptoclass.sccscryptoworktesting();
                            sccscryptoframeperformanceadjustercounterL = 0;
                        }
                    }
                    else
                    {
                        if (frame4RandomRorL == 0)
                        {
                            transform.Rotate(new Vector3(-needle_rotation_speed * Mathf.Abs(totalDotgoalRL), 0, 0), Space.World);
                            if (sccscryptoframeperformanceadjustercounterR > sccscryptoframeperformanceadjustermaxR)
                            {
                                sccscryptoclass.sccscryptoworktesting();
                                sccscryptoframeperformanceadjustercounterR = 0;
                            }
                        }
                        else
                        {
                            transform.Rotate(new Vector3(needle_rotation_speed * Mathf.Abs(totalDotgoalRL), 0, 0), Space.World);
                            if (sccscryptoframeperformanceadjustercounterL > sccscryptoframeperformanceadjustermaxL)
                            {
                                sccscryptoclass.sccscryptoworktesting();
                                sccscryptoframeperformanceadjustercounterL = 0;
                            }
                        }
                    }
                }
                else
                {
                    //Debug.Log("found north pole / bullseye");
                }
            }
            frame4RandomRorL++;
            sccscryptoframeperformanceadjustercounterR++;
            sccscryptoframeperformanceadjustercounterL++;
        }
    }
}