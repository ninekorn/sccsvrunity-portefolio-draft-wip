using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SCCoreSystems
{
    public class sccsgearMLBP : MonoBehaviour
    {
        public int turretaxis = 0;
        int hardpointplacement = 0;
        //0 == axis x
        //1 == axis y

        //public Transform hardpointXaxis;
        //public Transform hardpointYaxis;


        public int swtchforresetwaitforseconds = 0;
        public float waitforseconds = 0;
        //0 == bottom
        //1 == top

        const int maxPerceptronInstances = 1;
        const int maxPerceptronInstancesneuronsithink = 3;

        SC_AI[] SC_AI4LR = new SC_AI[maxPerceptronInstances];
        //SC_AI[] SC_AI4TB = new SC_AI[maxPerceptronInstances]; //negativetopwheninunity2dviewwithyaxisasforward
        //SC_AI[] SC_AI4FB = new SC_AI[maxPerceptronInstances];

        int frame4chooseRorL = 0;
        int frame4chooseTorB = 0;
        int frame4chooseForB = 0;

        public Transform player;
        public Transform drone;


        public Transform hardpoint;
        public Transform target;

        public float turret_rotation_speed = 0.0001f;
        public float totalDotgoalLR = 0;
        public float totalDotgoalTB = 0;
        public float totalDotgoalFB = 0;


        int rlprepped = 0;
        int tbprepped = 0;
        int fbprepped = 0;

        int totalRight = 0;
        int totalLeft = 0;

        int totalTop = 0;
        int totalDown = 0;

        int totalForward = 0;
        int totalBack = 0;


        void Start()
        {
            if (hardpointplacement == 0)
            {
                for (int i = 0; i < SC_AI4LR.Length; i++)
                {
                    //SC_AI4LR[i] = new SC_AI(hardpoint, target, maxPerceptronInstancesneuronsithink);
                    SC_AI4LR[i].swtchwaypointtype = 0;
                }

                /*for (int i = 0; i < SC_AI4TB.Length; i++)
                {
                    SC_AI4TB[i] = new SC_AI(hardpoint, target, maxPerceptronInstancesneuronsithink);
                    SC_AI4TB[i].swtchwaypointtype = 1;
                }*/
            }
            else if (hardpointplacement == 1)
            {
                for (int i = 0; i < SC_AI4LR.Length; i++)
                {
                    //SC_AI4LR[i] = new SC_AI(hardpoint, target, maxPerceptronInstancesneuronsithink);
                    SC_AI4LR[i].swtchwaypointtype = 0;
                }

                /*for (int i = 0; i < SC_AI4TB.Length; i++)
                {
                    SC_AI4TB[i] = new SC_AI(hardpoint, target, maxPerceptronInstancesneuronsithink);
                    SC_AI4TB[i].swtchwaypointtype = 1;
                }*/
            }

            /*for (int i = 0; i < SC_AI4FB.Length; i++) // using 10 instances of SC_AI
            {
                SC_AI4FB[i] = new SC_AI(hardpoint, target, maxPerceptronInstancesneuronsithink);
                SC_AI4FB[i].swtchwaypointtype = 2;
            }*/
        }



        void Update()
        {
            rlprepped = 0;
            tbprepped = 0;

            /*Vector3 playerlocation = player.position;

            //friendly fire avoidance/helper
            Vector3 directionright = sc_maths._getDirection(Vector3.right, hardpoint.rotation);

            Vector3 dirturrettoplayer = player.position - hardpoint.position;
            dirturrettoplayer.Normalize();

            Vector3 dirturrettoenemy = target.position - hardpoint.position;
            dirturrettoenemy.Normalize();

            var dottogoal = sc_maths.Dot(dirturrettoplayer.x, dirturrettoplayer.y, dirturrettoenemy.x, dirturrettoenemy.y);
            //Debug.Log(dottogoal);*/


            //MACHINE LEARNING LEFT OR RIGHT FOR DOT PRODUCT AXIS 0 // Y
            totalRight = 0;
            totalLeft = 0;
            totalDotgoalLR = 0;
            for (int i = 0; i < SC_AI4LR.Length; i++)
            {
                SC_AI4LR[i].UpdatePerceptron(swtchforresetwaitforseconds, waitforseconds);
                totalRight += SC_AI4LR[i]._guessedCorrectRight;
                totalLeft += SC_AI4LR[i]._guessedCorrectLeft;
                totalDotgoalLR += SC_AI4LR[i]._dotGoal;
            }
            totalDotgoalLR /= SC_AI4LR.Length;

            //MACHINE LEARNING UP OR DOWN FOR DOT PRODUCT AXIS 1 // 
            /*totalDown = 0;
            totalTop = 0;
            totalDotgoalTB = 0;
            for (int i = 0; i < SC_AI4TB.Length; i++)
            {
                SC_AI4TB[i].UpdatePerceptron();
                totalTop += SC_AI4TB[i]._guessedCorrectRight;
                totalDown += SC_AI4TB[i]._guessedCorrectLeft;
                totalDotgoalTB += SC_AI4TB[i]._dotGoal;
            }
            totalDotgoalTB /= SC_AI4TB.Length;*/

            /*//MACHINE LEARNING FORWARD OR BACK FOR DOT PRODUCT AXIS 2 //
            totalForward = 0;
            totalBack = 0;
            totalDotgoalFB = 0;
            for (int i = 0; i < SC_AI4FB.Length; i++)
            {
                SC_AI4FB[i].UpdatePerceptron();
                totalForward += SC_AI4FB[i]._guessedCorrectRight;
                totalBack += SC_AI4FB[i]._guessedCorrectLeft;
                totalDotgoalFB += SC_AI4FB[i]._dotGoal;
            }
            totalDotgoalFB /= SC_AI4FB.Length;*/
            //get the height. thats it maybe.


            //Debug.Log(totalDotgoalLR);
            //Debug.Log(totalDotgoalTB);


            int switchRL = 0;
            int switchTB = 0;

            //hardpointXaxis.transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
            /*
            if (turretaxis == 0)
            {

                if (totalRight > totalLeft)
                {
                    //Debug.Log("player is RIGHt 0-0");
                    this.transform.Rotate(new Vector3(0, 0, -turret_rotation_speed), Space.World);
                    //transform.GetComponent<Rigidbody>().add
                    switchRL = 1;
                }
                else if (totalRight < totalLeft)
                {
                    //Debug.Log("player is LEFT 0-0");
                    this.transform.Rotate(new Vector3(0, 0, turret_rotation_speed), Space.World);
                    switchRL = 0;
                }
                else
                {
                    if (frame4chooseRorL == 0)
                    {
                        switchRL = 1;
                        this.transform.Rotate(new Vector3(0, 0, -turret_rotation_speed), Space.World);
                    }
                    else
                    {
                        switchRL = 0;
                        this.transform.Rotate(new Vector3(0, 0, turret_rotation_speed), Space.World);
                        frame4chooseRorL = 0;
                    }
                }
                frame4chooseRorL++;

            }
            else
            {

                if (totalTop > totalDown)
                {
                    switchTB = 1;
                    //Debug.Log("player is RIGHt 0-0");
                    this.transform.Rotate(new Vector3(turret_rotation_speed, 0, 0), Space.World);
                }
                else if (totalTop < totalDown)
                {
                    switchTB = 0;
                    //Debug.Log("player is LEFT 0-0");
                    this.transform.Rotate(new Vector3(-turret_rotation_speed, 0, 0), Space.World);
                }
                else
                {
                    if (frame4chooseTorB == 0)
                    {
                        switchTB = 1;
                        this.transform.Rotate(new Vector3(turret_rotation_speed, 0, 0), Space.World);
                    }
                    else
                    {
                        switchTB = 0;
                        frame4chooseTorB = 0;
                        this.transform.Rotate(new Vector3(-turret_rotation_speed, 0, 0), Space.World);
                    }
                }
                frame4chooseTorB++;
            }*/


            //* Mathf.Abs(totalDotgoalTB)


            /*if (switchRL == 0 && switchTB == 0)
            {
                hardpoint.transform.Rotate(new Vector3(-turret_rotation_speed * Mathf.Abs(totalDotgoalTB), 0, turret_rotation_speed * Mathf.Abs(totalDotgoalLR)), Space.Self);
            }
            else if (switchRL == 0 && switchTB == 1)
            {
                hardpoint.transform.Rotate(new Vector3(turret_rotation_speed * Mathf.Abs(totalDotgoalTB), 0, turret_rotation_speed * Mathf.Abs(totalDotgoalLR)), Space.Self);
            }
            else if (switchRL == 1 && switchTB == 1)
            {
                hardpoint.transform.Rotate(new Vector3(turret_rotation_speed * Mathf.Abs(totalDotgoalTB), 0, -turret_rotation_speed * Mathf.Abs(totalDotgoalLR)), Space.Self);
            }
            else if (switchRL == 1 && switchTB == 0)
            {
                hardpoint.transform.Rotate(new Vector3(-turret_rotation_speed * Mathf.Abs(totalDotgoalTB), 0, -turret_rotation_speed * Mathf.Abs(totalDotgoalLR)), Space.Self);
            }*/




         
            





















            if (turretaxis == 0)
            {
                //Debug.Log(totalDotgoal);

                if (totalRight > totalLeft)
                {
                    //Debug.Log("player is RIGHt 0-0");
                    hardpoint.transform.Rotate(new Vector3(0, 0, -turret_rotation_speed * Mathf.Abs(totalDotgoalLR)), Space.World);
                }
                else if (totalRight < totalLeft)
                {
                    //Debug.Log("player is LEFT 0-0");
                    hardpoint.transform.Rotate(new Vector3(0, 0, turret_rotation_speed * Mathf.Abs(totalDotgoalLR)), Space.World);
                }
                else
                {
                    if (frame4chooseRorL == 0)
                    {
                        hardpoint.transform.Rotate(new Vector3(0, 0, (-turret_rotation_speed * Mathf.Abs(totalDotgoalLR))), Space.World);
                    }
                    else
                    {
                        hardpoint.transform.Rotate(new Vector3(0, 0, (turret_rotation_speed * Mathf.Abs(totalDotgoalLR))), Space.World);
                    }
                }

                /*if (drone.tag == "drone")
                {
                    if (dottogoal > 0.75f) //do not shoot. player is in the way
                    {
                        Debug.Log("warning. " + player.name + " is in the way.");
                    }
                    else
                    {
                        //Debug.Log("safe to shoot");

                        if (totalDotgoalLR > -0.023454321 && totalDotgoalLR < 0 || totalDotgoalLR < 0.023454321 && totalDotgoalLR >= 0) //0.01f
                        {
                            rlprepped = 1;
                        }
                    }
                }
                else
                {
                    if (dottogoal > 0.75f) //do not shoot. player is in the way
                    {
                        //Debug.Log("warning. " + player.name + " is in the way.");
                    }
                    else
                    {
                        //Debug.Log("safe to shoot");
                    }

                    if (totalDotgoalLR > -0.023454321 && totalDotgoalLR < 0 || totalDotgoalLR < 0.023454321 && totalDotgoalLR >= 0) //0.01f
                    {
                        rlprepped = 1;
                    }
                }*/
                frame4chooseRorL++;
            }
            ////////////////////
            ////////TODO////////
            ////////////////////
            //if click to rotate turret towards bullseye, then rotate turret. => not implemented yet.
            ////////////////////
            ////////TODO////////
            ////////////////////
        }
    }
}







//Debug.Log(totalDotgoalTB);
/*
if (totalUp > totalDown)
{
    //Debug.Log("player is UP");
    //hardpoint.transform.Rotate(new Vector3(-turret_rotation_speed * Mathf.Abs(totalDotgoalTB), 0, 0), Space.World);
}
else if (totalUp < totalDown)
{
    //Debug.Log("player is DOWN");
    //hardpoint.transform.Rotate(new Vector3(turret_rotation_speed * Mathf.Abs(totalDotgoalTB), 0, 0), Space.World);
}
else
{
    //Debug.Log("up down same result");
}*/

//Debug.Log(totalDotgoalTB);
/*
if (turretaxis == 1)
{
    if (totalTop > totalDown)
    {
        //Debug.Log("player is RIGHt 0-0");
        hardpoint.transform.Rotate(new Vector3(turret_rotation_speed * Mathf.Abs(totalDotgoalTB), 0, 0), Space.World);
    }
    else if (totalTop < totalDown)
    {
        //Debug.Log("player is LEFT 0-0");
        hardpoint.transform.Rotate(new Vector3(-turret_rotation_speed * Mathf.Abs(totalDotgoalTB), 0, 0), Space.World);
    }
    else
    {
        if (frame4chooseTorB == 0)
        {
            hardpoint.transform.Rotate(new Vector3(turret_rotation_speed * Mathf.Abs(totalDotgoalTB), 0, 0), Space.World);
        }
        else
        {
            frame4chooseTorB = 0;
            hardpoint.transform.Rotate(new Vector3(-turret_rotation_speed * Mathf.Abs(totalDotgoalTB), 0, 0), Space.World);
        }
    }

    if (drone.tag == "drone")
    {
        if (dottogoal > 0.75f) //do not shoot. player is in the way
        {
            Debug.Log("warning. " + player.name + " is in the way.");
        }
        else
        {
            //Debug.Log("safe to shoot");

            if (totalDotgoalTB > -0.023454321 && totalDotgoalTB < 0 || totalDotgoalTB < 0.023454321 && totalDotgoalTB >= 0) //0.01f
            {
                tbprepped = 1;

            }
        }
    }
    else
    {
        if (dottogoal > 0.75f) //do not shoot. player is in the way
        {
            //Debug.Log("warning. " + player.name + " is in the way.");
        }
        else
        {
            //Debug.Log("safe to shoot");
        }

        if (totalDotgoalTB > -0.023454321 && totalDotgoalTB < 0 || totalDotgoalTB < 0.023454321 && totalDotgoalTB >= 0) //0.01f
        {
            tbprepped = 1;
        }
    }
    frame4chooseTorB++;
}
////////////////////
////////TODO////////
////////////////////
//if click to rotate turret towards bullseye, then rotate turret. => not implemented yet.
////////////////////
////////TODO////////
////////////////////
*/






/*if (rlprepped == 1 && tbprepped == 1) //rlprepped == 1 && 
{
    //var childobjecthardpointpivot = hardpoint.Find("turretcannonpivot");
    GetComponent<sc_prep_projectile>().prepareshot();
    GetComponent<sc_prep_projectile>().target = target;
    GetComponent<sc_prep_projectile>().gunEnd = hardpoint.Find("turretbarrel");
    GetComponent<sc_prep_projectile>().bullettypeselected = 0;

    //childL = hardpoint.FindChild("BarrelL");
    //
    var bullseye = hardpoint.Find("bullseye");
    bullseye.position = target.position;
}*/









/*if (totalDotgoalTB <= 0.1f && totalDotgoalTB >= 0 || totalDotgoalTB >= -0.1f && totalDotgoalTB < 0)
{

}
else
{

}*/





/*var childL = hardpoint.Find("turretcannonLpivot");
childL.GetComponent<sc_prep_projectile>().prepareshot();
childL.GetComponent<sc_prep_projectile>().target = target;
childL.GetComponent<sc_prep_projectile>().gunEnd = childL;
//childL = hardpoint.FindChild("BarrelL");
//childL.GetComponent<sc_prep_projectile>().gunEnd = player;
childL = childL.Find("bullseye");
childL.position = target.position;

var childR = hardpoint.Find("turretcannonRpivot");
childR.GetComponent<sc_prep_projectile>().prepareshot();
childR.GetComponent<sc_prep_projectile>().target = target;
childR.GetComponent<sc_prep_projectile>().gunEnd = childR;
childR = childR.Find("bullseye");
childR.position = target.position;

//childR = hardpoint.FindChild("BarrelL");
//childR.GetComponent<sc_prep_projectile>().gunEnd = player;

//drone.GetComponent<sc_jamesleenz_projectile>().prepareshot();*/

/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SCCoreSystems
{

    public class SC_AI_Start : MonoBehaviour
    {
        public Transform pivotpointL;
        public Transform bullseyeL;

        public Transform pivotpointR;
        public Transform bullseyeR;

        public GameObject weapon_prefab;
        public GameObject[] barrel_hardpoints;
        public float turret_rotation_speed = 3f;
        public float shot_speed;
        int barrel_index = 0;

        SC_AI[] SC_AIL = new SC_AI[10];
        SC_AI[] SC_AIR = new SC_AI[10];


        public float rotspeed = 0.0001f;
        public float totalDotgoal = 0;






        void Start()
        {
            for (int i = 0; i < SC_AIL.Length; i++) // using 10 instances of SC_AI
            {
                SC_AIL[i] = new SC_AI(pivotpointL, bullseyeL);
                SC_AIL[i].swtchwaypointtype = 2;

                SC_AIR[i] = new SC_AI(pivotpointL, bullseyeR);
                SC_AIR[i].swtchwaypointtype = 2;
            }
        }

        int totalGuessturretLeftBraintookdecisionRight = 0;
        int totalGuessturretLeftBraintookdecisionLeft = 0;
        int frame4turretLeftbrainchooseRorL = 0;



        int totalGuessturretRightBraintookdecisionRight = 0;
        int totalGuessturretRightBraintookdecisionLeft = 0;
        int frame4turretRightbrainchooseRorL = 0;





        void Update()
        {



            //machine learning left barrel.
            totalGuessturretLeftBraintookdecisionRight = 0;
            totalGuessturretLeftBraintookdecisionLeft = 0;
            totalDotgoal = 0;

            for (int i = 0; i < SC_AIL.Length; i++)
            {
                SC_AIL[i].UpdatePerceptron();
                totalGuessturretLeftBraintookdecisionRight += SC_AIL[i]._guessedCorrectRight;
                totalGuessturretLeftBraintookdecisionLeft += SC_AIL[i]._guessedCorrectLeft;
                totalDotgoal += SC_AIL[i]._dotGoal;
            }
            totalDotgoal /= SC_AIL.Length;
            //rotspeed = totalDotgoal;

            //Debug.Log(totalDotgoal);
             
            if (totalGuessturretLeftBraintookdecisionRight > totalGuessturretLeftBraintookdecisionLeft)
            {
                //Debug.Log("player is RIGHt 0-0");
                pivotpointL.transform.Rotate(new Vector3(0, 0, -10 * Mathf.Abs(totalDotgoal)), Space.World);
            }
            else if (totalGuessturretLeftBraintookdecisionRight < totalGuessturretLeftBraintookdecisionLeft)
            {
                //Debug.Log("player is LEFT 0-0");
                pivotpointL.transform.Rotate(new Vector3(0, 0, 10 * Mathf.Abs(totalDotgoal)), Space.World);
            }
            else
            {
                if (frame4turretLeftbrainchooseRorL == 0)
                {
                    pivotpointL.transform.Rotate(new Vector3(0, 0, (-10 * Mathf.Abs(totalDotgoal))), Space.World);
                }
                else
                {
                    pivotpointL.transform.Rotate(new Vector3(0, 0, (10 * Mathf.Abs(totalDotgoal))), Space.World);
                }
            }

            if (totalDotgoal > -0.023454321 && totalDotgoal < 0 || totalDotgoal < 0.023454321 && totalDotgoal >=0) //0.01f
            {
                this.GetComponent<sc_prep_projectile>().prepareshot();
                this.GetComponent<sc_prep_projectile>().target = bullseyeL;
                this.GetComponent<sc_prep_projectile>().rigidbody2Dtarget = bullseyeL.GetComponent<Rigidbody2D>();
                //pivotpointL.GetComponent<sc_jamesleenz_projectile>().prepareshot();
                //pivotpointL.GetComponent<sc_prep_projectile>().target = bullseye;
            }



















            //machine learning right barrel
            totalGuessturretRightBraintookdecisionRight = 0;
            totalGuessturretRightBraintookdecisionLeft = 0;
            totalDotgoal = 0;

            for (int i = 0; i < SC_AIR.Length; i++)
            {
                SC_AIR[i].UpdatePerceptron();
                totalGuessturretRightBraintookdecisionRight += SC_AIR[i]._guessedCorrectRight;
                totalGuessturretRightBraintookdecisionLeft += SC_AIR[i]._guessedCorrectLeft;
                totalDotgoal += SC_AIR[i]._dotGoal;
            }
            totalDotgoal /= SC_AIR.Length;
            //rotspeed = totalDotgoal;

            //Debug.Log(totalDotgoal);

            if (totalGuessturretRightBraintookdecisionRight > totalGuessturretRightBraintookdecisionLeft)
            {
                //Debug.Log("player is RIGHt 0-0");
                pivotpointR.transform.Rotate(new Vector3(0, 0, -10 * Mathf.Abs(totalDotgoal)), Space.World);
            }
            else if (totalGuessturretRightBraintookdecisionRight < totalGuessturretRightBraintookdecisionLeft)
            {
                //Debug.Log("player is LEFT 0-0");
                pivotpointR.transform.Rotate(new Vector3(0, 0, 10 * Mathf.Abs(totalDotgoal)), Space.World);
            }
            else
            {
                if (frame4turretRightbrainchooseRorL == 0)
                {
                    pivotpointR.transform.Rotate(new Vector3(0, 0, (-10 * Mathf.Abs(totalDotgoal))), Space.World);
                }
                else
                {
                    pivotpointR.transform.Rotate(new Vector3(0, 0, (10 * Mathf.Abs(totalDotgoal))), Space.World);
                }
            }

            if (totalDotgoal > -0.023454321 && totalDotgoal < 0 || totalDotgoal < 0.023454321 && totalDotgoal >= 0) //0.01f
            {
                this.GetComponent<sc_prep_projectile>().prepareshot();
                this.GetComponent<sc_prep_projectile>().target = bullseyeR;
                this.GetComponent<sc_prep_projectile>().rigidbody2Dtarget = bullseyeR.GetComponent<Rigidbody2D>();
                //pivotpointR.GetComponent<sc_jamesleenz_projectile>().prepareshot();
                //pivotpointR.GetComponent<sc_prep_projectile>().target = bullseye;
            }



















            /*if (totalDotgoal <= 0.1f && totalDotgoal >= 0 || totalDotgoal >= -0.1f && totalDotgoal < 0)
            {

            }
            else
            {

            }
frame4turretLeftbrainchooseRorL++;
            frame4turretRightbrainchooseRorL++;

        }
    }
}
*/
