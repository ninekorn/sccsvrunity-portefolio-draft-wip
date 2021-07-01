using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace SCCoreSystems
{
    public class sccscompass : MonoBehaviour
    {
        //public GameObject target;
        Shader targetShader;
        Material mat;
        float desired = 5.0f;
        Vector3[] verts;

        SC_AI[] SC_AI4LR = new SC_AI[maxPerceptronInstances];

        public int swtchforresetwaitforseconds = 0;
        public float waitforseconds = 0;

        public Transform compass;
        public Transform northpole;
        public Vector3 northpolePos;

        //GameObject compassneedleobject;
        //GameObject northpoleobject;

        public const int maxPerceptronInstances = 10;
        public const int maxPerceptronInstancesNeurons = 10; // 3 minimum i think
        float perceptronLearningRate = 0.001f;

        public float needle_rotation_speed = 0.5f;

        int totalRight = 0;
        int totalLeft = 0;
        int frame4RandomRorL = 0;

        public float totalDotgoalRL = 0;
        float dottogoal = 0;

        Vector3 playerlocation = Vector2.zero;
        Vector3 directionright = Vector2.zero;
        Vector3 dirturrettoplayer = Vector2.zero;
        Vector3 dirturrettoenemy = Vector2.zero;

        public int swtchwaypointtype = 0;

        public float lastElevation = 0.0f;

        public sccscrypto sccscryptoclass;

        public int sccscryptoframeperformanceadjusterswtch = 0;
        public int sccscryptoframeperformanceadjustercounterR = 0;
        public int sccscryptoframeperformanceadjustercounterL = 0;
        public int sccscryptoframeperformanceadjustermaxR = 9;
        public int sccscryptoframeperformanceadjustermaxL = 9;

        void Start()
        {
            /*compassneedleobject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            compassneedleobject.transform.localScale = new Vector3(0.8f, 4, 0.8f);
            compass = compassneedleobject.transform;
            compass.position = new Vector3(0, 2, 0);
            compass.parent = this.transform;

            northpoleobject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            northpoleobject.transform.localScale = new Vector3(2, 2, 2);
            northpole = northpoleobject.transform;
            northpole.transform.position = new Vector3(6, 6, 0);*/
            northpolePos = northpole.transform.position;
            for (int i = 0; i < SC_AI4LR.Length; i++)
            {
                SC_AI4LR[i] = new SC_AI(compass, northpolePos, maxPerceptronInstancesNeurons, perceptronLearningRate, swtchforresetwaitforseconds, waitforseconds);
                SC_AI4LR[i].SC_anglesNumber = 360;
                SC_AI4LR[i].SC_Angle_Divider = 10;
                SC_AI4LR[i].weightsNumber = 10;
                SC_AI4LR[i].inputsNumber = 3; // minimum 3 for the Trainer class
                SC_AI4LR[i].errormargin = 3;
                SC_AI4LR[i].swtchwaypointtype = swtchwaypointtype;
            }



            //target = GameObject.Find("sccsperkochunk-weightsvalues");
            //targetShader = target.GetComponent<MeshRenderer>().material.shader; //get the shader, this is handy if you want to switch shaders. 
            //mat = target.GetComponent<Renderer>().material;    //and get my material, where I can send my variables to
            //verts = target.GetComponent<MeshFilter>().mesh.vertices;
            sccscryptoclass = gameObject.GetComponent<sccscrypto>();
        }

        void Update()
        {
            northpolePos = northpole.transform.position;
            totalRight = 0;
            totalLeft = 0;
            totalDotgoalRL = 0;
            for (int i = 0; i < SC_AI4LR.Length; i++)
            {
                SC_AI4LR[i].northpoletransform = northpole.transform.position;
                SC_AI4LR[i].UpdatePerceptron(swtchforresetwaitforseconds, waitforseconds);
                totalRight += SC_AI4LR[i]._guessedCorrectRight;
                totalLeft += SC_AI4LR[i]._guessedCorrectLeft;
                totalDotgoalRL += SC_AI4LR[i]._dotGoal;
            }
            totalDotgoalRL /= SC_AI4LR.Length;

            Vector3 dirCompassToNorthPole = northpole.transform.position - compass.transform.position;
            float hypothenuse = dirCompassToNorthPole.magnitude;
            float adjacent = Mathf.Abs(Mathf.Round(northpole.transform.position.y));
            float opp = Mathf.Sqrt((hypothenuse * hypothenuse) - (adjacent * adjacent));
            float angle = sc_maths.RadianToDegree(Mathf.Asin(opp / hypothenuse));

            if (SC_AI4LR[0].swtchwaypointtype == 0)
            {
                float posYtest = Mathf.Abs(Mathf.Round(northpole.transform.position.y * 10) / 10);

                //Debug.Log(northpole.transform.localEulerAngles.x);
                //Debug.Log(totalDotgoalRL);
                //if (lastElevation != posYtest)
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
                            if (sccscryptoframeperformanceadjustercounterR > sccscryptoframeperformanceadjustermaxL)
                            {
                                sccscryptoclass.sccscryptoworktesting();
                                sccscryptoframeperformanceadjustercounterL = 0;
                            }
                        }
                    }
                }
                lastElevation = posYtest;


                //float angleRadian = 0.0f;
                //ProjectileHelper.ComputeElevationToHitTargetWithSpeed(northpole.transform.position.y, hypothenuse, 0.0f, 1.0f, false, out angleRadian);
                //angleRadian = sc_maths.RadianToDegree(angleRadian);
                //Debug.Log(angleRadian);

                //Vector3 dirCompassToNorthPole = northpole.transform.position - compass.transform.position;
                //float hypothenuse = dirCompassToNorthPole.magnitude;
                //float adjacent = Mathf.Abs(northpole.transform.position.y);

                //float angle = 1/Mathf.Cos(hypothenuse / adjacent);
                //float angleDegrerRL = Vector2.Angle(dirCompassToNorthPole, RLGear.forward);
                //var angleDegrerRL = Mathf.Abs(Mathf.Atan2(northpole.transform.up.y, northpole.transform.up.x) - Mathf.Atan2(-dirCompassToNorthPole.y, -dirCompassToNorthPole.x)); ///* 180 Math.PI
                //Debug.Log(angle);

                //sinAngle = opp/hyp

                //if (northpole.transform.position.y != lastElevation)
                {

                }






                //y and x

                //float angle = sc_maths.AngleBetween(transform.position.x,transform.position.y, northpole.position.x, northpole.position.y);
                //Debug.Log(angle);



                /*if (totalDotgoalRL < -0.0025f || totalDotgoalRL > 0.0025f)
                {
                    // i gotta incorporate a tiny change here. the rotation needs to be high at all times except when almost pointing towards the target where it needs to stop instantly... it needs to break and
                    // break fast otherwise the turret will just rotate non-stop. i might use a lerp later on but using the Dot product for the lerp is also a temp solution.
                 
                }
                else
                {
                    //Debug.Log("found north pole / bullseye");
                }*/

            }
            else if (SC_AI4LR[0].swtchwaypointtype == 1)
            {
                //Debug.Log("dot: " + totalDotgoalRL);


                //float clampedAngle = sc_maths.ClampValue(transform.eulerAngles.z, -180, 180);

                //Debug.Log(clampedAngle);


                //if (totalDotgoalRL < 0.99)
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

                /*if (totalDotgoalRL < 0.99)
                {
                    // i gotta incorporate a tiny change here. the rotation needs to be high at all times except when almost pointing towards the target where it needs to stop instantly... it needs to break and
                    // break fast otherwise the turret will just rotate non-stop. i might use a lerp later on but using the Dot product for the lerp is also a temp solution.
                    if (totalRight > totalLeft)
                    {
                        //Debug.Log("north pole is right");
                        transform.Rotate(new Vector3(0, -needle_rotation_speed * Mathf.Abs(totalDotgoalRL), 0), Space.World);
                    }
                    else if (totalRight < totalLeft)
                    {
                        //Debug.Log("north pole is left");
                        transform.Rotate(new Vector3(0, needle_rotation_speed * Mathf.Abs(totalDotgoalRL), 0), Space.World);
                    }
                    else
                    {
                        if (frame4RandomRorL == 0)
                        {
                            transform.Rotate(new Vector3(0, -needle_rotation_speed * Mathf.Abs(totalDotgoalRL), 0), Space.World);
                        }
                        else
                        {
                            transform.Rotate(new Vector3(0, needle_rotation_speed * Mathf.Abs(totalDotgoalRL), 0), Space.World);
                        }
                    }
                }
                else
                {
                    //Debug.Log("found north pole / bullseye");
                }*/





                /*if (totalDotgoalRL < -0.0025f || totalDotgoalRL > 0.0025f)
                //if (totalDotgoalRL > -0.99f || totalDotgoalRL < 0.99f)
                {
                    // i gotta incorporate a tiny change here. the rotation needs to be high at all times except when almost pointing towards the target where it needs to stop instantly... it needs to break and
                    // break fast otherwise the turret will just rotate non-stop. i might use a lerp later on but using the Dot product for the lerp is also a temp solution.
                    if (totalRight > totalLeft)
                    {
                        //Debug.Log("north pole is right");
                        transform.Rotate(new Vector3(0, -needle_rotation_speed * Mathf.Abs(totalDotgoalRL), 0), Space.World);
                    }
                    else if (totalRight < totalLeft)
                    {
                        //Debug.Log("north pole is left");
                        transform.Rotate(new Vector3(0, needle_rotation_speed * Mathf.Abs(totalDotgoalRL), 0), Space.World);
                    }
                    else
                    {
                        if (frame4RandomRorL == 0)
                        {
                            transform.Rotate(new Vector3(0, -needle_rotation_speed * Mathf.Abs(totalDotgoalRL), 0), Space.World);
                        }
                        else
                        {
                            transform.Rotate(new Vector3(0, needle_rotation_speed * Mathf.Abs(totalDotgoalRL), 0), Space.World);
                        }
                    }
                }
                else
                {
                    //Debug.Log("found north pole / bullseye");
                }*/

                /*
                if (totalDotgoalRL < -0.0025f || totalDotgoalRL > 0.0025f)
                {
                    // i gotta incorporate a tiny change here. the rotation needs to be high at all times except when almost pointing towards the target where it needs to stop instantly... it needs to break and
                    // break fast otherwise the turret will just rotate non-stop. i might use a lerp later on but using the Dot product for the lerp is also a temp solution.
                    if (totalRight > totalLeft)
                    {
                        //Debug.Log("north pole is right");
                        transform.Rotate(new Vector3(0, -needle_rotation_speed * Mathf.Abs(totalDotgoalRL), 0), Space.World);
                    }
                    else if (totalRight < totalLeft)
                    {
                        //Debug.Log("north pole is left");
                        transform.Rotate(new Vector3(0, needle_rotation_speed * Mathf.Abs(totalDotgoalRL), 0), Space.World);
                    }
                    else
                    {
                        if (frame4RandomRorL == 0)
                        {
                            transform.Rotate(new Vector3(0, -needle_rotation_speed * Mathf.Abs(totalDotgoalRL), 0), Space.World);
                        }
                        else
                        {
                            transform.Rotate(new Vector3(0, needle_rotation_speed * Mathf.Abs(totalDotgoalRL), 0), Space.World);
                        }
                    }
                }
                else
                {
                    //Debug.Log("found north pole / bullseye");
                }*/
            }

            /*if (displaytextcounter >= 0)
            {
                for (int i = 0; i < SC_AI4LR.Length; i++)
                {
                    for (int j = 0; j < SC_AI4LR[i].saveCurrentWeightForTheCurrentAngleInsideOfUnitsOf360.Length; j++)
                    {
                        if (j < verts.Length)
                        {
                            mat.SetVector("_VertPos", new Vector4(verts[j].x, verts[j].y, verts[j].z, 1));
                            mat.SetFloat("_Weight", SC_AI4LR[i].saveCurrentWeightForTheCurrentAngleInsideOfUnitsOf360[j][0]);
                        }




                        //mat.SetInt("_VertIndex",j);
                        /*for (int w = 0; w < SC_AI4LR[i].saveCurrentWeightForTheCurrentAngleInsideOfUnitsOf360[j].Length; w++)
                        {
                            //UpdateText("ang: " + j + " w: " + SC_AI4LR[i].saveCurrentWeightForTheCurrentAngleInsideOfUnitsOf360[j][w]);
							//float tempVertIndex = mat.GetFloat("_VertIndex");
							//tempVar += Time.deltaTime
							Debug.Log(SC_AI4LR[i].saveCurrentWeightForTheCurrentAngleInsideOfUnitsOf360[j][w]);
							//float tempVar = mat.GetFloat("_Amount"); //ask for the amount of the float
							//Debug.Log( mat.SetFloat("_Amount") );     //Debugging to check
							//mat.SetFloat("_Amount",SC_AI4LR[i].saveCurrentWeightForTheCurrentAngleInsideOfUnitsOf360[j][w]);  //tempVar += Time.deltaTime
                        }
                    }
                }
                displaytextcounter = 1;
            }*/

            displaytextcounter++;
            frame4RandomRorL++;
            sccscryptoframeperformanceadjustercounterR++;
            sccscryptoframeperformanceadjustercounterL++;
        }

        int displaytextcounter = 0;

        /*public Text textComponent;
        void UpdateText(string value)
        {
            //Update the text shown in the text component by setting the `text` variable
            textComponent.text += value + "\n\r";
        }*/









        /*public Texture2D textureToDisplay;
        void OnGUI()
        {
            for (int i = 0; i < SC_AI4LR.Length; i++)
            {

                for (int j = 0; j < SC_AI4LR[i].saveCurrentWeightForTheCurrentAngleInsideOfUnitsOf360.Length; j++)
                {    
                    GUI.Label(new Rect(0, 0, 10, 10), "ang: " + i + " w: " + SC_AI4LR[i].saveCurrentWeightForTheCurrentAngleInsideOfUnitsOf360[j]);
                }
            }
        }*/
    }
}