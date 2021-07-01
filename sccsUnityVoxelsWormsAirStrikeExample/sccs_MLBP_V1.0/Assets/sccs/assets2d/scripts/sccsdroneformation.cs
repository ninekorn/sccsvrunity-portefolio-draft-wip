using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace SCCoreSystems
{


    public class sccsdroneformation : MonoBehaviour
    {
        public const int maxPerceptronInstances = 10;
        public const int maxPerceptronInstancesNeurons = 3; // 3 minimum i think
        float perceptronLearningRate = 0.001f;


        public int swtchforresetwaitforseconds = 0;
        public float waitforseconds = 0;


        public Transform player;
        public Transform drone;
        public Transform formationwaypoint;



        public Transform hardpoint;
        public Transform targetTransform;
        public Vector3 targetPos;


        public int counterenginepushBL = 0;
        public int counterenginepushBR = 0;
        public int counterenginepushBRL = 0;

        public int maxenginepushBL = 0;
        public int maxenginepushBR = 0;
        public int maxenginepushBRL = 0;




        Vector2 waypointPos;
        Vector2 coordsPlayer = Vector2.zero;

        Rigidbody playerrigid;
        Rigidbody dronerigid;
        SC_AI[] SC_AI = new SC_AI[10];

        int frame4chooseRorL = 0;

        public float rotspeed = 0.0001f;
        float totalDotgoal = 0;
        float totalDotgoallastframe = 0;

        float speedlastframe;
        float speedcurrentframe;


        int totalRight = 0;
        int totalLeft = 0;

        public float force = 10;
        public float forcethruster = 10;
        public float forcethrusterangular = 10;

        void Start()
        {
            playerrigid = player.GetComponent<Rigidbody>();
            dronerigid = drone.GetComponent<Rigidbody>();

            for (int i = 0; i < SC_AI.Length; i++) // using 10 instances of SC_AI
            {
                SC_AI[i] = new SC_AI(drone, targetPos, maxPerceptronInstancesNeurons, perceptronLearningRate, swtchforresetwaitforseconds, waitforseconds);
                SC_AI[i].swtchwaypointtype = 1;
            }
        }





        void Update()
        {
            //coordsPlayer = new Vector2(player.position.x, player.position.y);
            coordsPlayer = new Vector2(playerrigid.position.x, playerrigid.position.y);

            Vector2 waypointPos = player.right;
            coordsPlayer += waypointPos * 3;
            //waypointPos = getFormationWaypoint(5, 0);
            //formationwaypoint.position = waypointPos;

            formationwaypoint.position = coordsPlayer;
            for (int i = 0; i < SC_AI.Length; i++) // using 10 instances of SC_AI
            {
                SC_AI[i].waypointpos = formationwaypoint.position;
            }

            totalRight = 0;
            totalLeft = 0;
            totalDotgoal = 0;

            for (int i = 0; i < SC_AI.Length; i++)
            {
                SC_AI[i].UpdatePerceptron(swtchforresetwaitforseconds, waitforseconds);
                totalRight += SC_AI[i]._guessedCorrectRight;
                totalLeft += SC_AI[i]._guessedCorrectLeft;
                totalDotgoal += SC_AI[i]._dotGoal;
            }
            totalDotgoal /= SC_AI.Length;
            //Debug.Log(totalDotgoal);




            if (totalDotgoal >= 0.01f || totalDotgoal <= -0.01f)
            {
                if (totalDotgoal <= -0.01f)
                {
                    speedcurrentframe = dronerigid.velocity.magnitude;

                    if (totalDotgoallastframe <= totalDotgoal) // 
                    {
                        //the dot product of the last frame was smaller than the current frame
                        //the totalDotGoal is negative so it means the ship is currently to the top/north of the waypoint in a 2d view and
                        //the ship is getting closer to the waypoint from that position
                        //if the ship is getting closer to the waypoint

                        //var tempclampedspeed = sc_maths.Clamp(speedcurrentframe, 0, 1);
                        var scaledspeed = (speedcurrentframe - 0) / (1 - 0);
                        //Debug.Log(scaledspeed); // goes up when getting closer to the waypoint from the top

                        if (speedcurrentframe <= speedlastframe) //getting closer to the waypoint but the speed is increasing. wrong.
                        {
                            //stop accelerating if it was accelerating.
                            //stop engines push in that direction
                            //invert the engines to the other side instead to push the opposite direction.
                            //totalDotgoal0 = -0.99f to 0.1f
                            Debug.Log("0");
                            //Debug.Log("0: " + (counterenginepushBRL) + " 1: " + scaledspeed);
                            if (counterenginepushBRL > (scaledspeed)) // scaledspeed currently between 0++ to 6++ getting higher when nearer waypoint
                            {
                                var rigiddrone = drone.gameObject.GetComponent<Rigidbody>();
                                rigiddrone.AddForce(rigiddrone.transform.up * force * totalDotgoal, ForceMode.Impulse);
                                counterenginepushBRL = 0;
                            }
                        }
                        else// if (speedcurrentframe > speedlastframe) //getting closer to the waypoint from the north but the speed is decreasing. right.
                        {
                            Debug.Log("1");
                            //Debug.Log("0: " + (counterenginepushBRL) + " 1: " + scaledspeed);
                            /*if (counterenginepushBRL > (scaledspeed)) // scaledspeed currently between 0++ to 6++ getting higher when nearer waypoint
                            {
                                //totalDotgoal getting smaller when near to waypoint. 0.99f--

                                var rigiddrone = drone.gameObject.GetComponent<Rigidbody>();
                                rigiddrone.AddForce(-rigiddrone.transform.up * force * totalDotgoal, ForceMode.Impulse);
                                counterenginepushBRL = 0;
                            }*/

                            //var rigiddrone = drone.gameObject.GetComponent<Rigidbody>();
                            //rigiddrone.AddForce(rigiddrone.transform.up * force * totalDotgoal * scaledspeed, ForceMode.Impulse);


                            //stop accelerating if it was accelerating.
                            //stop engines push in that direction
                            //invert the engines to the other side instead to push the opposite direction.
                            //totalDotgoal0 = -0.99f to 0.1f

                            /*if (maxenginepushBRL - counterenginepushBRL < (scaledspeed))// scaledspeed currently between 0++ to 6++ getting higher when nearer waypoint
                            {
                                var rigiddrone = drone.gameObject.GetComponent<Rigidbody>();
                                rigiddrone.AddForce(-rigiddrone.transform.up * force * totalDotgoal, ForceMode.Impulse);
                                counterenginepushBRL = 0;
                            }*/
                        }
                    }
                    else// if (totalDotgoallastframe > totalDotgoal)
                    {
                        var scaledspeed = (speedcurrentframe - 0) / (1 - 0);
                        //Debug.Log(scaledspeed); // goes up when getting closer to the waypoint from the top

                        if (speedcurrentframe <= speedlastframe) //getting closer to the waypoint but the speed is increasing. wrong.
                        {
                            //stop accelerating if it was accelerating.
                            //stop engines push in that direction
                            //invert the engines to the other side instead to push the opposite direction.
                            //totalDotgoal0 = -0.99f to 0.1f
                            Debug.Log("00");
                            //Debug.Log("0: " + (counterenginepushBRL) + " 1: " + scaledspeed);
                            if (counterenginepushBRL > (scaledspeed)) // scaledspeed currently between 0++ to 6++ getting higher when nearer waypoint
                            {
                                var rigiddrone = drone.gameObject.GetComponent<Rigidbody>();
                                rigiddrone.AddForce(rigiddrone.transform.up * force * totalDotgoal, ForceMode.Impulse);
                                counterenginepushBRL = 0;
                            }
                        }
                        else// if (speedcurrentframe > speedlastframe) //getting closer to the waypoint from the north but the speed is decreasing. right.
                        {
                            if (counterenginepushBRL > (scaledspeed)) // scaledspeed currently between 0++ to 6++ getting higher when nearer waypoint
                            {
                                var rigiddrone = drone.gameObject.GetComponent<Rigidbody>();
                                rigiddrone.AddForce(rigiddrone.transform.up * force * totalDotgoal, ForceMode.Impulse);
                                counterenginepushBRL = 0;
                            }
                            Debug.Log("11");
                            //Debug.Log("0: " + (counterenginepushBRL) + " 1: " + scaledspeed);
                            /*if (counterenginepushBRL > (scaledspeed)) // scaledspeed currently between 0++ to 6++ getting higher when nearer waypoint
                            {
                                //totalDotgoal getting smaller when near to waypoint. 0.99f--

                                var rigiddrone = drone.gameObject.GetComponent<Rigidbody>();
                                rigiddrone.AddForce(-rigiddrone.transform.up * force * totalDotgoal, ForceMode.Impulse);
                                counterenginepushBRL = 0;
                            }*/

                            //var rigiddrone = drone.gameObject.GetComponent<Rigidbody>();
                            //rigiddrone.AddForce(rigiddrone.transform.up * force * totalDotgoal * scaledspeed, ForceMode.Impulse);


                            //stop accelerating if it was accelerating.
                            //stop engines push in that direction
                            //invert the engines to the other side instead to push the opposite direction.
                            //totalDotgoal0 = -0.99f to 0.1f

                            /*if (maxenginepushBRL - counterenginepushBRL < (scaledspeed))// scaledspeed currently between 0++ to 6++ getting higher when nearer waypoint
                            {
                                var rigiddrone = drone.gameObject.GetComponent<Rigidbody>();
                                rigiddrone.AddForce(-rigiddrone.transform.up * force * totalDotgoal, ForceMode.Impulse);
                                counterenginepushBRL = 0;
                            }*/
                        }
                        //the dot product of the last frame was higher than the current frame
                        //the totalDotGoal is positive so it means the ship is currently to the bottom/south  of the waypoint in a 2d view and
                        //the ship is getting away from the waypoint from that position

                        /*var tempclampedspeed = sc_maths.Clamp(speedcurrentframe, 0, 1);

                        if (speedcurrentframe <= speedlastframe) //getting closer to waypoint but speed is increasing. wrong.
                        {
                            //stop accelerating if it was accelerating.
                            //stop engines push in that direction
                            //invert the engines to the other side instead to push the opposite direction.
                            //totalDotgoal0 = -0.99f to 0.1f
                        }
                        else// if (speedcurrentframe > speedlastframe) //getting closer to waypoint but speed is decreasing. right.
                        {
                            //stop accelerating if it was accelerating.
                            //stop engines push in that direction
                            //invert the engines to the other side instead to push the opposite direction.
                            //totalDotgoal0 = -0.99f to 0.1f
                        }*/
                    }
                }
                else if (totalDotgoal > -0.01f)
                {
                    speedcurrentframe = dronerigid.velocity.magnitude;

                    if (totalDotgoallastframe <= totalDotgoal) // 
                    {
                        //the dot product of the last frame was smaller than the current frame
                        //the totalDotGoal is negative so it means the ship is currently to the top/north of the waypoint in a 2d view and
                        //the ship is getting closer to the waypoint from that position
                        //if the ship is getting closer to the waypoint

                        //var tempclampedspeed = sc_maths.Clamp(speedcurrentframe, 0, 1);
                        var scaledspeed = (speedcurrentframe - 0) / (1 - 0);
                        //Debug.Log(scaledspeed); // goes up when getting closer to the waypoint from the top

                        if (speedcurrentframe <= speedlastframe) //getting closer to the waypoint but the speed is increasing. wrong.
                        {
                            //stop accelerating if it was accelerating.
                            //stop engines push in that direction
                            //invert the engines to the other side instead to push the opposite direction.
                            //totalDotgoal0 = -0.99f to 0.1f
                            Debug.Log("000");
                            //Debug.Log("0: " + (counterenginepushBRL) + " 1: " + scaledspeed);
                            if (counterenginepushBRL > (scaledspeed)) // scaledspeed currently between 0++ to 6++ getting higher when nearer waypoint
                            {
                                var rigiddrone = drone.gameObject.GetComponent<Rigidbody>();
                                rigiddrone.AddForce(rigiddrone.transform.up * force * totalDotgoal, ForceMode.Impulse);
                                counterenginepushBRL = 0;
                            }
                        }
                        else// if (speedcurrentframe > speedlastframe) //getting closer to the waypoint from the north but the speed is decreasing. right.
                        {
                            Debug.Log("111");
                            //Debug.Log("0: " + (counterenginepushBRL) + " 1: " + scaledspeed);
                            if (counterenginepushBRL > (scaledspeed)) // scaledspeed currently between 0++ to 6++ getting higher when nearer waypoint
                            {
                                //totalDotgoal getting smaller when near to waypoint. 0.99f--

                                var rigiddrone = drone.gameObject.GetComponent<Rigidbody>();
                                rigiddrone.AddForce(rigiddrone.transform.up * force * totalDotgoal, ForceMode.Impulse);
                                counterenginepushBRL = 0;
                            }

                            //var rigiddrone = drone.gameObject.GetComponent<Rigidbody>();
                            //rigiddrone.AddForce(rigiddrone.transform.up * force * totalDotgoal * scaledspeed, ForceMode.Impulse);


                            //stop accelerating if it was accelerating.
                            //stop engines push in that direction
                            //invert the engines to the other side instead to push the opposite direction.
                            //totalDotgoal0 = -0.99f to 0.1f

                            /*if (maxenginepushBRL - counterenginepushBRL < (scaledspeed))// scaledspeed currently between 0++ to 6++ getting higher when nearer waypoint
                            {
                                var rigiddrone = drone.gameObject.GetComponent<Rigidbody>();
                                rigiddrone.AddForce(-rigiddrone.transform.up * force * totalDotgoal, ForceMode.Impulse);
                                counterenginepushBRL = 0;
                            }*/
                        }
                    }
                    else// if (totalDotgoallastframe > totalDotgoal)
                    {
                        var scaledspeed = (speedcurrentframe - 0) / (1 - 0);
                        //Debug.Log(scaledspeed); // goes up when getting closer to the waypoint from the top

                        if (speedcurrentframe <= speedlastframe) //getting closer to the waypoint but the speed is increasing. wrong.
                        {
                            //stop accelerating if it was accelerating.
                            //stop engines push in that direction
                            //invert the engines to the other side instead to push the opposite direction.
                            //totalDotgoal0 = -0.99f to 0.1f
                            Debug.Log("0000");
                            //Debug.Log("0: " + (counterenginepushBRL) + " 1: " + scaledspeed);
                            if (counterenginepushBRL > (scaledspeed)) // scaledspeed currently between 0++ to 6++ getting higher when nearer waypoint
                            {
                                var rigiddrone = drone.gameObject.GetComponent<Rigidbody>();
                                rigiddrone.AddForce(rigiddrone.transform.up * force * totalDotgoal, ForceMode.Impulse);
                                counterenginepushBRL = 0;
                            }
                        }
                        else// if (speedcurrentframe > speedlastframe) //getting closer to the waypoint from the north but the speed is decreasing. right.
                        {
                            if (counterenginepushBRL > (scaledspeed)) // scaledspeed currently between 0++ to 6++ getting higher when nearer waypoint
                            {
                                var rigiddrone = drone.gameObject.GetComponent<Rigidbody>();
                                rigiddrone.AddForce(rigiddrone.transform.up * force * totalDotgoal, ForceMode.Impulse);
                                counterenginepushBRL = 0;
                            }
                            Debug.Log("1111");
                            //Debug.Log("0: " + (counterenginepushBRL) + " 1: " + scaledspeed);
                            /*if (counterenginepushBRL > (scaledspeed)) // scaledspeed currently between 0++ to 6++ getting higher when nearer waypoint
                            {
                                //totalDotgoal getting smaller when near to waypoint. 0.99f--

                                var rigiddrone = drone.gameObject.GetComponent<Rigidbody>();
                                rigiddrone.AddForce(-rigiddrone.transform.up * force * totalDotgoal, ForceMode.Impulse);
                                counterenginepushBRL = 0;
                            }*/

                            //var rigiddrone = drone.gameObject.GetComponent<Rigidbody>();
                            //rigiddrone.AddForce(rigiddrone.transform.up * force * totalDotgoal * scaledspeed, ForceMode.Impulse);


                            //stop accelerating if it was accelerating.
                            //stop engines push in that direction
                            //invert the engines to the other side instead to push the opposite direction.
                            //totalDotgoal0 = -0.99f to 0.1f

                            /*if (maxenginepushBRL - counterenginepushBRL < (scaledspeed))// scaledspeed currently between 0++ to 6++ getting higher when nearer waypoint
                            {
                                var rigiddrone = drone.gameObject.GetComponent<Rigidbody>();
                                rigiddrone.AddForce(-rigiddrone.transform.up * force * totalDotgoal, ForceMode.Impulse);
                                counterenginepushBRL = 0;
                            }*/
                        }
                        //the dot product of the last frame was higher than the current frame
                        //the totalDotGoal is positive so it means the ship is currently to the bottom/south  of the waypoint in a 2d view and
                        //the ship is getting away from the waypoint from that position

                        /*var tempclampedspeed = sc_maths.Clamp(speedcurrentframe, 0, 1);

                        if (speedcurrentframe <= speedlastframe) //getting closer to waypoint but speed is increasing. wrong.
                        {
                            //stop accelerating if it was accelerating.
                            //stop engines push in that direction
                            //invert the engines to the other side instead to push the opposite direction.
                            //totalDotgoal0 = -0.99f to 0.1f
                        }
                        else// if (speedcurrentframe > speedlastframe) //getting closer to waypoint but speed is decreasing. right.
                        {
                            //stop accelerating if it was accelerating.
                            //stop engines push in that direction
                            //invert the engines to the other side instead to push the opposite direction.
                            //totalDotgoal0 = -0.99f to 0.1f
                        }*/
                    }
                } 
            }
            else
            {
                //in range of waypoint
            }

















           
            /*if (totalDotgoal >= 0.1f || totalDotgoal <= -0.1f)
            {
                if (totalRight > totalLeft)
                {
                    var rigiddrone = drone.gameObject.GetComponent<Rigidbody>();
                    rigiddrone.AddForce(rigiddrone.transform.up * force * totalDotgoal, ForceMode.Impulse);
                    //Debug.Log("player is RIGHt 0-0");
                    //drone.transform.Rotate(new Vector3(0, 0, -10 * Mathf.Abs(totalDotgoal)), Space.World);
                }
                else if (totalRight < totalLeft)
                {
                    var rigiddrone = drone.gameObject.GetComponent<Rigidbody>();
                    rigiddrone.AddForce(-rigiddrone.transform.up * force * -totalDotgoal, ForceMode.Impulse);
                    //Debug.Log("player is LEFT 0-0");
                    //drone.transform.Rotate(new Vector3(0, 0, 10 * Mathf.Abs(totalDotgoal)), Space.World);
                }
                else
                {
                    if (frame4chooseRorL == 0)
                    {
                        var rigiddrone = drone.gameObject.GetComponent<Rigidbody>();
                        rigiddrone.AddForce(rigiddrone.transform.up * force * totalDotgoal, ForceMode.Impulse);
                        //drone.transform.Rotate(new Vector3(0, 0, (-10 * Mathf.Abs(totalDotgoal))), Space.World);
                    }
                    else
                    {
                        var rigiddrone = drone.gameObject.GetComponent<Rigidbody>();
                        rigiddrone.AddForce(-rigiddrone.transform.up * force * -totalDotgoal, ForceMode.Impulse);
                        //drone.transform.Rotate(new Vector3(0, 0, (10 * Mathf.Abs(totalDotgoal))), Space.World);
                        frame4chooseRorL = 0;
                    }
                }
            }
            else
            {

            }*/

            speedlastframe = speedcurrentframe;
            totalDotgoallastframe = totalDotgoal;
            counterenginepushBRL++;
        }

        float SC_Angle_Divider = 1.0f;
        public float Clamp0360(float eulerAngles)
        {
            float result = eulerAngles - Mathf.CeilToInt(eulerAngles / (360f * SC_Angle_Divider)) * (360f * SC_Angle_Divider);
            if (result < 0)
            {
                result += (360f * SC_Angle_Divider);
            }
            return result;
        }

        Vector2 getFormationWaypoint(float offsetX, float offsetY)
        {
            var pointFrontX = (1 * Mathf.Cos(Clamp0360(player.eulerAngles.z) * Mathf.PI / 180)) + coordsPlayer.x;
            var pointFrontY = (1 * Mathf.Sin(Clamp0360(player.eulerAngles.z) * Mathf.PI / 180)) + coordsPlayer.y;

            var playerDirX = pointFrontX - coordsPlayer.x;
            var playerDirY = pointFrontY - coordsPlayer.y;

            var pointRightX = coordsPlayer.x + ((playerDirX * offsetX));
            var pointRightY = coordsPlayer.y + ((playerDirY * offsetY));

            return new Vector2(pointRightX, pointRightY);
        }
    }
}











//Vector2 dronePos = new Vector2(drone.transform.position.x, drone.transform.position.y);
//var waypointPos = getFormationWaypoint(5, 0);
//formationwaypoint.position = waypointPos;

//var pointRightX = coordsPlayer.x + ((playerDirX * offsetX));
//var pointRightY = coordsPlayer.y + ((playerDirY * offsetY));