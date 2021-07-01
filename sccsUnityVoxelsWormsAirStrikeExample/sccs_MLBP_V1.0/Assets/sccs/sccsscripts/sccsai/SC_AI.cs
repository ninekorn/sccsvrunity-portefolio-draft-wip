using UnityEngine;
using System;
using Perceptron;
using System.Collections;


namespace SCCoreSystems
{
    public class SC_AI : MonoBehaviour
    {
        public int linearframeguessselection = 0;
    

        public int inputsNumber = 3; // 3 minimum i think
        public int SC_Angle_Divider = 4;
        public int SC_anglesNumber = 360;
        public int errormargin = 1;
        public int weightsNumber = 3;

        public int SC_anglesQuarterNumber = 0; //SC_Angle_Divider * SC_anglesNumber

        public float[][] saveCurrentWeightForTheCurrentAngleInsideOfUnitsOf360;// = new float[SC_anglesQuarterNumber][];

        public int swtchwaypointtype = 0;
        public Vector3 northpoletransform;
        public Transform compasspivot;
        public Vector2 waypointpos;
        Perceptron.BrollofPerceptron perc;
        float[] weights;
        float xmin, xmax, ymin, ymax;
        Trainer[] training;// = new Trainer[inputsNumber];
        public int _guessedCorrectRight = 0;
        public int _guessedCorrectLeft = 0;
        public float _dotGoal;
        int answer;
        System.Random random;
        float lastAngle = 0;
        Vector2 northpolepos;
        Vector2 compassPos;
        float randguess = 0;
        float waitforseconds = 0;

        public SC_AI(Transform compassOriginPos, Vector3 northpoleOrBullseyePos, int maxPerceptronInstancesneurons, float perceptronLearningRate, float waitforsecondsswtch_, float waitforseconds_)
        {
            training = new Trainer[inputsNumber];
            this.northpoletransform = northpoleOrBullseyePos;
            this.compasspivot = compassOriginPos;
            this.waitforseconds = waitforseconds_;
            //initWaitforSeconds(waitforsecondsswtch_);
            waitforsecondsclass = new WaitForSeconds(waitforsecondsswtch_);
            Starter(maxPerceptronInstancesneurons, perceptronLearningRate);
        }

        void Starter(int maxPerceptronInstancesneurons, float perceptronLearningRate)
        {
            SC_anglesQuarterNumber = SC_anglesNumber * SC_Angle_Divider;
            saveCurrentWeightForTheCurrentAngleInsideOfUnitsOf360 = new float[SC_anglesQuarterNumber][];

            random = new System.Random();
            perc = new Perceptron.BrollofPerceptron(maxPerceptronInstancesneurons, perceptronLearningRate);

            //perceptronLearningRate = sc_maths.getSomeRandNumThousandDecimal();

            for (int i = 0; i < saveCurrentWeightForTheCurrentAngleInsideOfUnitsOf360.Length; i++)
            {
                weights = perc.GetWeights();
                saveCurrentWeightForTheCurrentAngleInsideOfUnitsOf360[i] = weights;
            }
        }

        public void initWaitforSeconds(float waitforsecondsswtch_)
        {
            waitforsecondsclass = new WaitForSeconds(waitforsecondsswtch_);
        }

        WaitForSeconds waitforsecondsclass;// = new WaitForSeconds();
        int waitforsecondsswtch = 0;

        public IEnumerator UpdatePerceptron(int waitforsecondsswtch_, float waitforseconds_)
        {

            if (waitforsecondsswtch_ == 1)
            {
                waitforsecondsswtch = 1;
            }

            if (waitforsecondsswtch == 1)
            {
                waitforsecondsclass = new WaitForSeconds(waitforsecondsswtch_);
                waitforsecondsswtch = 0;
            }

            _guessedCorrectRight = 0;
            _guessedCorrectLeft = 0;

            if (compasspivot != null && compasspivot.transform != null)
            {

            }
            compassPos = new Vector2(compasspivot.transform.position.x, compasspivot.transform.position.y);


            float currentQuarterRoundedAngle = 0.0f;
            float angle = 0.0f;

            if (swtchwaypointtype == 0)
            {
                angle = sc_maths.ClampValue(compasspivot.transform.eulerAngles.x, 0, SC_anglesQuarterNumber);
                var angleRounded = Mathf.Round(angle);
                var currentDiff = (angle - angleRounded);
                currentQuarterRoundedAngle = Mathf.Round(currentDiff * SC_Angle_Divider) / SC_Angle_Divider;
                currentQuarterRoundedAngle *= 100;
                currentQuarterRoundedAngle = (angle * SC_Angle_Divider);
                currentQuarterRoundedAngle = sc_maths.ClampValue(currentQuarterRoundedAngle, 0, SC_anglesQuarterNumber);
                weights = perc.GetWeights();
            }
            else if (swtchwaypointtype == 1)
            {
                angle = sc_maths.ClampValue(compasspivot.transform.eulerAngles.z, 0, SC_anglesQuarterNumber);
                var angleRounded = Mathf.Round(angle);
                var currentDiff = (angle - angleRounded);
                currentQuarterRoundedAngle = Mathf.Round(currentDiff * SC_Angle_Divider) / SC_Angle_Divider;
                currentQuarterRoundedAngle *= 100;
                currentQuarterRoundedAngle = (angle * SC_Angle_Divider);
                currentQuarterRoundedAngle = sc_maths.ClampValue(currentQuarterRoundedAngle, 0, SC_anglesQuarterNumber);
                weights = perc.GetWeights();
            }





            if ((int)currentQuarterRoundedAngle < saveCurrentWeightForTheCurrentAngleInsideOfUnitsOf360.Length)
            {
                perc._SC_Perceptron_SetRotWeights(saveCurrentWeightForTheCurrentAngleInsideOfUnitsOf360[(int)currentQuarterRoundedAngle]);

                float pointForwardDirNPCX = (float)(1 * Math.Cos(Math.PI * angle / 180.0)) + compassPos.x; // * Math.PI / 180
                float pointForwardDirNPCY = (float)(1 * Math.Sin(Math.PI * angle / 180.0)) + compassPos.y;

                Vector2 dirRightNPC = new Vector2(pointForwardDirNPCY - compassPos.y, -1 * (pointForwardDirNPCX - compassPos.x));
                Vector2 dirNPCToPlayer = new Vector2(northpolepos.x - compassPos.x, northpolepos.y - compassPos.y);

                var someOtherMAG = (float)Math.Sqrt((dirNPCToPlayer.x * dirNPCToPlayer.x) + (dirNPCToPlayer.y * dirNPCToPlayer.y));
                dirNPCToPlayer.x /= someOtherMAG;
                dirNPCToPlayer.y /= someOtherMAG;

                if (swtchwaypointtype == 0)
                {
                    /*Vector2 dirbulletprimerright = new Vector2(compasspivot.transform.right.z, -compasspivot.transform.right.x);
                    dirbulletprimerright.Normalize();

                    //var alignedDirectionWithPlayerDirectionRIGHTDOT = SC_Utilities.Dot(nData.nForward.y, -nData.nForward.x, pData.pForward.x, pData.pForward.y);
                    //Vector2 dirbulletprimerforward = new Vector2(compasspivot.transform.up.x, compasspivot.transform.up.y);
                    //dirbulletprimerforward.Normalize();

                    Vector2 dirprimertonorthpoletransform = new Vector2(compasspivot.position.x, compasspivot.position.y) - new Vector2(northpoletransform.x, northpoletransform.y);
                    dirprimertonorthpoletransform.Normalize();

                    //Vector3 dirprimertonorthpoletransform = new Vector3(northpoletransform.x, northpoletransform.y, northpoletransform.z) - new Vector3(compasspivot.position.x, compasspivot.position.y, compasspivot.position.z);
                    //dirprimertonorthpoletransform.Normalize();

                    _dotGoal = sc_maths.Dot(dirbulletprimerright.x, dirbulletprimerright.y, dirprimertonorthpoletransform.x, dirprimertonorthpoletransform.y);

                    if (_dotGoal >= 0) // 0.001f
                    {
                        answer = 1;
                    }
                    else if (_dotGoal < 0) //-0.001f
                    {
                        answer = -1;
                    }*/

                    /*Vector2 dirbulletprimerright = new Vector2(compasspivot.transform.forward.z, compasspivot.transform.forward.y);
                    dirbulletprimerright.Normalize();

                    //var alignedDirectionWithPlayerDirectionRIGHTDOT = SC_Utilities.Dot(nData.nForward.y, -nData.nForward.x, pData.pForward.x, pData.pForward.y);
                    //Vector2 dirbulletprimerforward = new Vector2(compasspivot.transform.up.x, compasspivot.transform.up.y);
                    //dirbulletprimerforward.Normalize();

                    Vector2 dirprimertonorthpoletransform = new Vector2(compasspivot.position.z, compasspivot.position.y) - new Vector2(northpoletransform.z, northpoletransform.y);
                    dirprimertonorthpoletransform.Normalize();

                    _dotGoal = sc_maths.Dot(dirbulletprimerright.x, dirbulletprimerright.y, dirprimertonorthpoletransform.x, dirprimertonorthpoletransform.y);

                    if (_dotGoal >= 0) // 0.001f
                    {
                        answer = 1;
                    }
                    else if (_dotGoal < 0) // -0.001f
                    {
                        answer = -1;
                    }*/

                    /*Vector3 dirprimertonorthpoletransform = compasspivot.position - northpoletransform;
                    dirprimertonorthpoletransform.Normalize();

                    Vector3 forward = compasspivot.transform.right;
                    forward.y = 0;
                    forward.z = 0;

                    //dirprimertonorthpoletransform.y = 0;
                    dirprimertonorthpoletransform.z = 0;

                    _dotGoal = Vector3.Dot(forward, dirprimertonorthpoletransform);
                    */

                    //Debug.Log(_dotGoal);
                    /*Vector2 dirbulletprimerright = new Vector2(compasspivot.transform.forward.x, compasspivot.transform.forward.z);
                    dirbulletprimerright.Normalize();

                    //var alignedDirectionWithPlayerDirectionRIGHTDOT = SC_Utilities.Dot(nData.nForward.y, -nData.nForward.x, pData.pForward.x, pData.pForward.y);

                    //Vector2 dirbulletprimerforward = new Vector2(compasspivot.transform.up.x, compasspivot.transform.up.y);
                    //dirbulletprimerforward.Normalize();

                    Vector2 dirprimertonorthpoletransform = new Vector2(compasspivot.position.x, compasspivot.position.y) - new Vector2(northpoletransform.x, northpoletransform.y);
                    dirprimertonorthpoletransform.Normalize();

                    //Vector3 dirprimertonorthpoletransform = new Vector3(northpoletransform.x, northpoletransform.y, northpoletransform.z) - new Vector3(compasspivot.position.x, compasspivot.position.y, compasspivot.position.z);
                    //dirprimertonorthpoletransform.Normalize();

                    _dotGoal = sc_maths.Dot(dirbulletprimerright.x, dirbulletprimerright.y, dirprimertonorthpoletransform.x, dirprimertonorthpoletransform.y);

                    if (_dotGoal >= 0) // 0.001f
                    {
                        answer = 1;
                    }
                    else if (_dotGoal < 0) //-0.001f
                    {
                        answer = -1;
                    }*/

                    Vector2 dirbulletprimerright = new Vector2(compasspivot.transform.right.z, compasspivot.transform.right.y);
                    dirbulletprimerright.Normalize();
                    //dirbulletprimerright.y = 0;

                    Vector2 dirprimertonorthpoletransform = new Vector2(northpoletransform.x, northpoletransform.y) - new Vector2(compasspivot.position.x, compasspivot.position.y);
                    dirprimertonorthpoletransform.Normalize();
                    //dirprimertonorthpoletransform.y = 0;

                    _dotGoal = sc_maths.Dot(dirbulletprimerright.x, dirbulletprimerright.y, dirprimertonorthpoletransform.x, dirprimertonorthpoletransform.y);

                    if (_dotGoal >= 0) // 0.001f
                    {
                        answer = 1;
                    }
                    else if (_dotGoal < 0)//-0.001f
                    {
                        answer = -1;
                    }
                }
                else if (swtchwaypointtype == 1)
                {
                    Vector2 dirbulletprimerright = new Vector2(compasspivot.transform.right.x, compasspivot.transform.right.z);
                    dirbulletprimerright.Normalize();

                    //var alignedDirectionWithPlayerDirectionRIGHTDOT = SC_Utilities.Dot(nData.nForward.y, -nData.nForward.x, pData.pForward.x, pData.pForward.y);
                    //Vector2 dirbulletprimerforward = new Vector2(compasspivot.transform.up.x, compasspivot.transform.up.y);
                    //dirbulletprimerforward.Normalize();

                    Vector2 dirprimertonorthpoletransform = new Vector2(compasspivot.position.x, compasspivot.position.z) - new Vector2(northpoletransform.x, northpoletransform.z);
                    dirprimertonorthpoletransform.Normalize();

                    //Vector3 dirprimertonorthpoletransform = new Vector3(northpoletransform.x, northpoletransform.y, northpoletransform.z) - new Vector3(compasspivot.position.x, compasspivot.position.y, compasspivot.position.z);
                    //dirprimertonorthpoletransform.Normalize();

                    _dotGoal = sc_maths.Dot(dirbulletprimerright.x, dirbulletprimerright.y, dirprimertonorthpoletransform.x, dirprimertonorthpoletransform.y);

                    if (_dotGoal >= 0) // 0.001f
                    {
                        answer = 1;
                    }
                    else if (_dotGoal < 0) //-0.001f
                    {
                        answer = -1;
                    }
                }

                //incomplete
                if (swtchwaypointtype == 0)
                {
                    compassPos = new Vector2(compasspivot.transform.position.z, compasspivot.transform.position.y);
                    if (linearframeguessselection == 0)
                    {
                        for (int i = 0; i < training.Length; i++)
                        {
                            if (i == 0) // Left
                            {
                                //sccsgimbal v1.0 and v1.1 were using this random point around the position of the compass itself for a random guess. it's one way to do it but i prefer the instant choice of 2 points only, one left and one right and maybe a third random one.
                                //double angleDeg = random.Next(360) / (2 * Math.PI);

                                // randomly getting a point at the location of the compass
                                float x = (float)(0.001f * Math.Cos(179.123f) + compassPos.x); //179.9999876543210123456789f
                                float y = (float)(0.001f * Math.Sin(179.123f) + compassPos.y); //179.9999876543210123456789f

                                training[i] = new Trainer(weightsNumber, x, y, answer);
                                perc.Train(training[i].inputs, training[i].answer);
                            }
                            else if (i == 1) //right 
                            {
                                //sccsgimbal v1.0 and v1.1 were using this random point around the position of the compass itself for a random guess. it's one way to do it but i prefer the instant choice of 2 points only, one left and one right and maybe a third random one.
                                //double angleDeg = random.Next(360) / (2 * Math.PI);

                                // randomly getting a point at the location of the compass
                                float x = (float)(0.001f * Math.Cos(0.00123f) + compassPos.x);
                                float y = (float)(0.001f * Math.Sin(0.00123f) + compassPos.y);

                                training[i] = new Trainer(weightsNumber, x, y, answer);
                                perc.Train(training[i].inputs, training[i].answer);
                            }
                            else if (i == 2)
                            {
                                //sccsgimbal v1.0 and v1.1 were using this random point around the position of the compass itself for a random guess. it's one way to do it but i prefer the instant choice of 2 points only, one left and one right and maybe a third random one.
                                double angleDeg = random.Next(360) / (2 * Math.PI);

                                // randomly getting a point at the location of the compass
                                float x = (float)(0.001f * Math.Cos(angleDeg) + compassPos.x);
                                float y = (float)(0.001f * Math.Sin(angleDeg) + compassPos.y);

                                training[i] = new Trainer(weightsNumber, x, y, answer);
                                perc.Train(training[i].inputs, training[i].answer);
                            }
                        }
                    }
                    else if (linearframeguessselection == 1)
                    {

                        for (int i = 0; i < training.Length; i++)
                        {
                            if (i == 2) // Left
                            {
                                //sccsgimbal v1.0 and v1.1 were using this random point around the position of the compass itself for a random guess. it's one way to do it but i prefer the instant choice of 2 points only, one left and one right and maybe a third random one.
                                //double angleDeg = random.Next(360) / (2 * Math.PI);

                                // randomly getting a point at the location of the compass
                                float x = (float)(0.001f * Math.Cos(179.123f) + compassPos.x); //179.9999876543210123456789f
                                float y = (float)(0.001f * Math.Sin(179.123f) + compassPos.y); //179.9999876543210123456789f

                                training[i] = new Trainer(weightsNumber, x, y, answer);
                                perc.Train(training[i].inputs, training[i].answer);
                            }
                            else if (i == 0) //right 
                            {
                                //sccsgimbal v1.0 and v1.1 were using this random point around the position of the compass itself for a random guess. it's one way to do it but i prefer the instant choice of 2 points only, one left and one right and maybe a third random one.
                                //double angleDeg = random.Next(360) / (2 * Math.PI);

                                // randomly getting a point at the location of the compass
                                float x = (float)(0.001f * Math.Cos(0.00123f) + compassPos.x);
                                float y = (float)(0.001f * Math.Sin(0.00123f) + compassPos.y);

                                training[i] = new Trainer(weightsNumber, x, y, answer);
                                perc.Train(training[i].inputs, training[i].answer);
                            }
                            else if (i == 1)
                            {
                                //sccsgimbal v1.0 and v1.1 were using this random point around the position of the compass itself for a random guess. it's one way to do it but i prefer the instant choice of 2 points only, one left and one right and maybe a third random one.
                                double angleDeg = random.Next(360) / (2 * Math.PI);

                                // randomly getting a point at the location of the compass
                                float x = (float)(0.001f * Math.Cos(angleDeg) + compassPos.x);
                                float y = (float)(0.001f * Math.Sin(angleDeg) + compassPos.y);

                                training[i] = new Trainer(weightsNumber, x, y, answer);
                                perc.Train(training[i].inputs, training[i].answer);
                            }
                        }
                    }
                    else if (linearframeguessselection == 2)
                    {

                        for (int i = 0; i < training.Length; i++)
                        {
                            if (i == 1) // Left
                            {
                                //sccsgimbal v1.0 and v1.1 were using this random point around the position of the compass itself for a random guess. it's one way to do it but i prefer the instant choice of 2 points only, one left and one right and maybe a third random one.
                                //double angleDeg = random.Next(360) / (2 * Math.PI);

                                // randomly getting a point at the location of the compass
                                float x = (float)(0.001f * Math.Cos(179.123f) + compassPos.x); //179.9999876543210123456789f
                                float y = (float)(0.001f * Math.Sin(179.123f) + compassPos.y); //179.9999876543210123456789f

                                training[i] = new Trainer(weightsNumber, x, y, answer);
                                perc.Train(training[i].inputs, training[i].answer);
                            }
                            else if (i == 2) //right 
                            {
                                //sccsgimbal v1.0 and v1.1 were using this random point around the position of the compass itself for a random guess. it's one way to do it but i prefer the instant choice of 2 points only, one left and one right and maybe a third random one.
                                //double angleDeg = random.Next(360) / (2 * Math.PI);

                                // randomly getting a point at the location of the compass
                                float x = (float)(0.001f * Math.Cos(0.00123f) + compassPos.x);
                                float y = (float)(0.001f * Math.Sin(0.00123f) + compassPos.y);

                                training[i] = new Trainer(weightsNumber, x, y, answer);
                                perc.Train(training[i].inputs, training[i].answer);
                            }
                            else if (i == 0)
                            {
                                //sccsgimbal v1.0 and v1.1 were using this random point around the position of the compass itself for a random guess. it's one way to do it but i prefer the instant choice of 2 points only, one left and one right and maybe a third random one.
                                double angleDeg = random.Next(360) / (2 * Math.PI);

                                // randomly getting a point at the location of the compass
                                float x = (float)(0.001f * Math.Cos(angleDeg) + compassPos.x);
                                float y = (float)(0.001f * Math.Sin(angleDeg) + compassPos.y);

                                training[i] = new Trainer(weightsNumber, x, y, answer);
                                perc.Train(training[i].inputs, training[i].answer);
                            }
                        }
                    }
                    else if (linearframeguessselection == 3)
                    {

                        for (int i = 0; i < training.Length; i++)
                        {
                            if (i == 2) // Left
                            {
                                //sccsgimbal v1.0 and v1.1 were using this random point around the position of the compass itself for a random guess. it's one way to do it but i prefer the instant choice of 2 points only, one left and one right and maybe a third random one.
                                //double angleDeg = random.Next(360) / (2 * Math.PI);

                                // randomly getting a point at the location of the compass
                                float x = (float)(0.001f * Math.Cos(179.123f) + compassPos.x); //179.9999876543210123456789f
                                float y = (float)(0.001f * Math.Sin(179.123f) + compassPos.y); //179.9999876543210123456789f

                                training[i] = new Trainer(weightsNumber, x, y, answer);
                                perc.Train(training[i].inputs, training[i].answer);
                            }
                            else if (i == 1) //right 
                            {
                                //sccsgimbal v1.0 and v1.1 were using this random point around the position of the compass itself for a random guess. it's one way to do it but i prefer the instant choice of 2 points only, one left and one right and maybe a third random one.
                                //double angleDeg = random.Next(360) / (2 * Math.PI);

                                // randomly getting a point at the location of the compass
                                float x = (float)(0.001f * Math.Cos(0.00123f) + compassPos.x);
                                float y = (float)(0.001f * Math.Sin(0.00123f) + compassPos.y);

                                training[i] = new Trainer(weightsNumber, x, y, answer);
                                perc.Train(training[i].inputs, training[i].answer);
                            }
                            else if (i == 0)
                            {
                                //sccsgimbal v1.0 and v1.1 were using this random point around the position of the compass itself for a random guess. it's one way to do it but i prefer the instant choice of 2 points only, one left and one right and maybe a third random one.
                                double angleDeg = random.Next(360) / (2 * Math.PI);

                                // randomly getting a point at the location of the compass
                                float x = (float)(0.001f * Math.Cos(angleDeg) + compassPos.x);
                                float y = (float)(0.001f * Math.Sin(angleDeg) + compassPos.y);

                                training[i] = new Trainer(weightsNumber, x, y, answer);
                                perc.Train(training[i].inputs, training[i].answer);
                            }
                        }
                    }
                    else if (linearframeguessselection == 4)
                    {

                        for (int i = 0; i < training.Length; i++)
                        {
                            if (i == 1) // Left
                            {
                                //sccsgimbal v1.0 and v1.1 were using this random point around the position of the compass itself for a random guess. it's one way to do it but i prefer the instant choice of 2 points only, one left and one right and maybe a third random one.
                                //double angleDeg = random.Next(360) / (2 * Math.PI);

                                // randomly getting a point at the location of the compass
                                float x = (float)(0.001f * Math.Cos(179.123f) + compassPos.x); //179.9999876543210123456789f
                                float y = (float)(0.001f * Math.Sin(179.123f) + compassPos.y); //179.9999876543210123456789f

                                training[i] = new Trainer(weightsNumber, x, y, answer);
                                perc.Train(training[i].inputs, training[i].answer);
                            }
                            else if (i == 0) //right 
                            {
                                //sccsgimbal v1.0 and v1.1 were using this random point around the position of the compass itself for a random guess. it's one way to do it but i prefer the instant choice of 2 points only, one left and one right and maybe a third random one.
                                //double angleDeg = random.Next(360) / (2 * Math.PI);

                                // randomly getting a point at the location of the compass
                                float x = (float)(0.001f * Math.Cos(0.00123f) + compassPos.x);
                                float y = (float)(0.001f * Math.Sin(0.00123f) + compassPos.y);

                                training[i] = new Trainer(weightsNumber, x, y, answer);
                                perc.Train(training[i].inputs, training[i].answer);
                            }
                            else if (i == 2)
                            {
                                //sccsgimbal v1.0 and v1.1 were using this random point around the position of the compass itself for a random guess. it's one way to do it but i prefer the instant choice of 2 points only, one left and one right and maybe a third random one.
                                double angleDeg = random.Next(360) / (2 * Math.PI);

                                // randomly getting a point at the location of the compass
                                float x = (float)(0.001f * Math.Cos(angleDeg) + compassPos.x);
                                float y = (float)(0.001f * Math.Sin(angleDeg) + compassPos.y);

                                training[i] = new Trainer(weightsNumber, x, y, answer);
                                perc.Train(training[i].inputs, training[i].answer);
                            }
                        }
                    }
                    else if (linearframeguessselection == 5)
                    {

                        for (int i = 0; i < training.Length; i++)
                        {
                            if (i == 0) // Left
                            {
                                //sccsgimbal v1.0 and v1.1 were using this random point around the position of the compass itself for a random guess. it's one way to do it but i prefer the instant choice of 2 points only, one left and one right and maybe a third random one.
                                //double angleDeg = random.Next(360) / (2 * Math.PI);

                                // randomly getting a point at the location of the compass
                                float x = (float)(0.001f * Math.Cos(179.123f) + compassPos.x); //179.9999876543210123456789f
                                float y = (float)(0.001f * Math.Sin(179.123f) + compassPos.y); //179.9999876543210123456789f

                                training[i] = new Trainer(weightsNumber, x, y, answer);
                                perc.Train(training[i].inputs, training[i].answer);
                            }
                            else if (i == 2) //right 
                            {
                                //sccsgimbal v1.0 and v1.1 were using this random point around the position of the compass itself for a random guess. it's one way to do it but i prefer the instant choice of 2 points only, one left and one right and maybe a third random one.
                                //double angleDeg = random.Next(360) / (2 * Math.PI);

                                // randomly getting a point at the location of the compass
                                float x = (float)(0.001f * Math.Cos(0.00123f) + compassPos.x);
                                float y = (float)(0.001f * Math.Sin(0.00123f) + compassPos.y);

                                training[i] = new Trainer(weightsNumber, x, y, answer);
                                perc.Train(training[i].inputs, training[i].answer);
                            }
                            else if (i == 1)
                            {
                                //sccsgimbal v1.0 and v1.1 were using this random point around the position of the compass itself for a random guess. it's one way to do it but i prefer the instant choice of 2 points only, one left and one right and maybe a third random one.
                                double angleDeg = random.Next(360) / (2 * Math.PI);

                                // randomly getting a point at the location of the compass
                                float x = (float)(0.001f * Math.Cos(angleDeg) + compassPos.x);
                                float y = (float)(0.001f * Math.Sin(angleDeg) + compassPos.y);

                                training[i] = new Trainer(weightsNumber, x, y, answer);
                                perc.Train(training[i].inputs, training[i].answer);
                            }
                        }
                    }
                    /*compassPos = new Vector2(compasspivot.transform.position.x, compasspivot.transform.position.y);
                    for (int i = 0; i < training.Length; i++)
                    {
                        double angleDeg = random.Next(360) / (2 * Math.PI);

                        // randomly getting a point at the location of the compass
                        float x = (int)(0.001f * Math.Cos(angleDeg) + compassPos.x);
                        float y = (int)(0.001f * Math.Sin(angleDeg) + compassPos.y);

                        training[i] = new Trainer(weightsNumber, x, y, answer);
                        perc.Train(training[i].inputs, training[i].answer);
                    }*/
                }
                else if (swtchwaypointtype == 1)
                {
                    compassPos = new Vector2(compasspivot.transform.position.x, compasspivot.transform.position.z);
                    if (linearframeguessselection == 0)
                    {
                        for (int i = 0; i < training.Length; i++)
                        {
                            if (i == 0) // Left
                            {
                                //sccsgimbal v1.0 and v1.1 were using this random point around the position of the compass itself for a random guess. it's one way to do it but i prefer the instant choice of 2 points only, one left and one right and maybe a third random one.
                                //double angleDeg = random.Next(360) / (2 * Math.PI);

                                // randomly getting a point at the location of the compass
                                float x = (float)(0.001f * Math.Cos(179.123f) + compassPos.x); //179.9999876543210123456789f
                                float y = (float)(0.001f * Math.Sin(179.123f) + compassPos.y); //179.9999876543210123456789f

                                training[i] = new Trainer(weightsNumber, x, y, answer);
                                perc.Train(training[i].inputs, training[i].answer);
                            }
                            else if (i == 1) //right 
                            {
                                //sccsgimbal v1.0 and v1.1 were using this random point around the position of the compass itself for a random guess. it's one way to do it but i prefer the instant choice of 2 points only, one left and one right and maybe a third random one.
                                //double angleDeg = random.Next(360) / (2 * Math.PI);

                                // randomly getting a point at the location of the compass
                                float x = (float)(0.001f * Math.Cos(0.00123f) + compassPos.x);
                                float y = (float)(0.001f * Math.Sin(0.00123f) + compassPos.y);

                                training[i] = new Trainer(weightsNumber, x, y, answer);
                                perc.Train(training[i].inputs, training[i].answer);
                            }
                            else if (i == 2)
                            {
                                //sccsgimbal v1.0 and v1.1 were using this random point around the position of the compass itself for a random guess. it's one way to do it but i prefer the instant choice of 2 points only, one left and one right and maybe a third random one.
                                double angleDeg = random.Next(360) / (2 * Math.PI);

                                // randomly getting a point at the location of the compass
                                float x = (float)(0.001f * Math.Cos(angleDeg) + compassPos.x);
                                float y = (float)(0.001f * Math.Sin(angleDeg) + compassPos.y);

                                training[i] = new Trainer(weightsNumber, x, y, answer);
                                perc.Train(training[i].inputs, training[i].answer);
                            }
                        }
                    }
                    else if (linearframeguessselection == 1)
                    {
                        
                        for (int i = 0; i < training.Length; i++)
                        {
                            if (i == 2) // Left
                            {
                                //sccsgimbal v1.0 and v1.1 were using this random point around the position of the compass itself for a random guess. it's one way to do it but i prefer the instant choice of 2 points only, one left and one right and maybe a third random one.
                                //double angleDeg = random.Next(360) / (2 * Math.PI);

                                // randomly getting a point at the location of the compass
                                float x = (float)(0.001f * Math.Cos(179.123f) + compassPos.x); //179.9999876543210123456789f
                                float y = (float)(0.001f * Math.Sin(179.123f) + compassPos.y); //179.9999876543210123456789f

                                training[i] = new Trainer(weightsNumber, x, y, answer);
                                perc.Train(training[i].inputs, training[i].answer);
                            }
                            else if (i == 0) //right 
                            {
                                //sccsgimbal v1.0 and v1.1 were using this random point around the position of the compass itself for a random guess. it's one way to do it but i prefer the instant choice of 2 points only, one left and one right and maybe a third random one.
                                //double angleDeg = random.Next(360) / (2 * Math.PI);

                                // randomly getting a point at the location of the compass
                                float x = (float)(0.001f * Math.Cos(0.00123f) + compassPos.x);
                                float y = (float)(0.001f * Math.Sin(0.00123f) + compassPos.y);

                                training[i] = new Trainer(weightsNumber, x, y, answer);
                                perc.Train(training[i].inputs, training[i].answer);
                            }
                            else if (i == 1)
                            {
                                //sccsgimbal v1.0 and v1.1 were using this random point around the position of the compass itself for a random guess. it's one way to do it but i prefer the instant choice of 2 points only, one left and one right and maybe a third random one.
                                double angleDeg = random.Next(360) / (2 * Math.PI);

                                // randomly getting a point at the location of the compass
                                float x = (float)(0.001f * Math.Cos(angleDeg) + compassPos.x);
                                float y = (float)(0.001f * Math.Sin(angleDeg) + compassPos.y);

                                training[i] = new Trainer(weightsNumber, x, y, answer);
                                perc.Train(training[i].inputs, training[i].answer);
                            }
                        }
                    }
                    else if (linearframeguessselection == 2)
                    {
                        
                        for (int i = 0; i < training.Length; i++)
                        {
                            if (i == 1) // Left
                            {
                                //sccsgimbal v1.0 and v1.1 were using this random point around the position of the compass itself for a random guess. it's one way to do it but i prefer the instant choice of 2 points only, one left and one right and maybe a third random one.
                                //double angleDeg = random.Next(360) / (2 * Math.PI);

                                // randomly getting a point at the location of the compass
                                float x = (float)(0.001f * Math.Cos(179.123f) + compassPos.x); //179.9999876543210123456789f
                                float y = (float)(0.001f * Math.Sin(179.123f) + compassPos.y); //179.9999876543210123456789f

                                training[i] = new Trainer(weightsNumber, x, y, answer);
                                perc.Train(training[i].inputs, training[i].answer);
                            }
                            else if (i == 2) //right 
                            {
                                //sccsgimbal v1.0 and v1.1 were using this random point around the position of the compass itself for a random guess. it's one way to do it but i prefer the instant choice of 2 points only, one left and one right and maybe a third random one.
                                //double angleDeg = random.Next(360) / (2 * Math.PI);

                                // randomly getting a point at the location of the compass
                                float x = (float)(0.001f * Math.Cos(0.00123f) + compassPos.x);
                                float y = (float)(0.001f * Math.Sin(0.00123f) + compassPos.y);

                                training[i] = new Trainer(weightsNumber, x, y, answer);
                                perc.Train(training[i].inputs, training[i].answer);
                            }
                            else if (i == 0)
                            {
                                //sccsgimbal v1.0 and v1.1 were using this random point around the position of the compass itself for a random guess. it's one way to do it but i prefer the instant choice of 2 points only, one left and one right and maybe a third random one.
                                double angleDeg = random.Next(360) / (2 * Math.PI);

                                // randomly getting a point at the location of the compass
                                float x = (float)(0.001f * Math.Cos(angleDeg) + compassPos.x);
                                float y = (float)(0.001f * Math.Sin(angleDeg) + compassPos.y);

                                training[i] = new Trainer(weightsNumber, x, y, answer);
                                perc.Train(training[i].inputs, training[i].answer);
                            }
                        }
                    }
                    else if (linearframeguessselection == 3)
                    {
                        
                        for (int i = 0; i < training.Length; i++)
                        {
                            if (i == 2) // Left
                            {
                                //sccsgimbal v1.0 and v1.1 were using this random point around the position of the compass itself for a random guess. it's one way to do it but i prefer the instant choice of 2 points only, one left and one right and maybe a third random one.
                                //double angleDeg = random.Next(360) / (2 * Math.PI);

                                // randomly getting a point at the location of the compass
                                float x = (float)(0.001f * Math.Cos(179.123f) + compassPos.x); //179.9999876543210123456789f
                                float y = (float)(0.001f * Math.Sin(179.123f) + compassPos.y); //179.9999876543210123456789f

                                training[i] = new Trainer(weightsNumber, x, y, answer);
                                perc.Train(training[i].inputs, training[i].answer);
                            }
                            else if (i == 1) //right 
                            {
                                //sccsgimbal v1.0 and v1.1 were using this random point around the position of the compass itself for a random guess. it's one way to do it but i prefer the instant choice of 2 points only, one left and one right and maybe a third random one.
                                //double angleDeg = random.Next(360) / (2 * Math.PI);

                                // randomly getting a point at the location of the compass
                                float x = (float)(0.001f * Math.Cos(0.00123f) + compassPos.x);
                                float y = (float)(0.001f * Math.Sin(0.00123f) + compassPos.y);

                                training[i] = new Trainer(weightsNumber, x, y, answer);
                                perc.Train(training[i].inputs, training[i].answer);
                            }
                            else if (i == 0)
                            {
                                //sccsgimbal v1.0 and v1.1 were using this random point around the position of the compass itself for a random guess. it's one way to do it but i prefer the instant choice of 2 points only, one left and one right and maybe a third random one.
                                double angleDeg = random.Next(360) / (2 * Math.PI);

                                // randomly getting a point at the location of the compass
                                float x = (float)(0.001f * Math.Cos(angleDeg) + compassPos.x);
                                float y = (float)(0.001f * Math.Sin(angleDeg) + compassPos.y);

                                training[i] = new Trainer(weightsNumber, x, y, answer);
                                perc.Train(training[i].inputs, training[i].answer);
                            }
                        }
                    }
                    else if (linearframeguessselection == 4)
                    {
                        
                        for (int i = 0; i < training.Length; i++)
                        {
                            if (i == 1) // Left
                            {
                                //sccsgimbal v1.0 and v1.1 were using this random point around the position of the compass itself for a random guess. it's one way to do it but i prefer the instant choice of 2 points only, one left and one right and maybe a third random one.
                                //double angleDeg = random.Next(360) / (2 * Math.PI);

                                // randomly getting a point at the location of the compass
                                float x = (float)(0.001f * Math.Cos(179.123f) + compassPos.x); //179.9999876543210123456789f
                                float y = (float)(0.001f * Math.Sin(179.123f) + compassPos.y); //179.9999876543210123456789f

                                training[i] = new Trainer(weightsNumber, x, y, answer);
                                perc.Train(training[i].inputs, training[i].answer);
                            }
                            else if (i == 0) //right 
                            {
                                //sccsgimbal v1.0 and v1.1 were using this random point around the position of the compass itself for a random guess. it's one way to do it but i prefer the instant choice of 2 points only, one left and one right and maybe a third random one.
                                //double angleDeg = random.Next(360) / (2 * Math.PI);

                                // randomly getting a point at the location of the compass
                                float x = (float)(0.001f * Math.Cos(0.00123f) + compassPos.x);
                                float y = (float)(0.001f * Math.Sin(0.00123f) + compassPos.y);

                                training[i] = new Trainer(weightsNumber, x, y, answer);
                                perc.Train(training[i].inputs, training[i].answer);
                            }
                            else if (i == 2)
                            {
                                //sccsgimbal v1.0 and v1.1 were using this random point around the position of the compass itself for a random guess. it's one way to do it but i prefer the instant choice of 2 points only, one left and one right and maybe a third random one.
                                double angleDeg = random.Next(360) / (2 * Math.PI);

                                // randomly getting a point at the location of the compass
                                float x = (float)(0.001f * Math.Cos(angleDeg) + compassPos.x);
                                float y = (float)(0.001f * Math.Sin(angleDeg) + compassPos.y);

                                training[i] = new Trainer(weightsNumber, x, y, answer);
                                perc.Train(training[i].inputs, training[i].answer);
                            }
                        }
                    }
                    else if (linearframeguessselection == 5)
                    {
                        
                        for (int i = 0; i < training.Length; i++)
                        {
                            if (i == 0) // Left
                            {
                                //sccsgimbal v1.0 and v1.1 were using this random point around the position of the compass itself for a random guess. it's one way to do it but i prefer the instant choice of 2 points only, one left and one right and maybe a third random one.
                                //double angleDeg = random.Next(360) / (2 * Math.PI);

                                // randomly getting a point at the location of the compass
                                float x = (float)(0.001f * Math.Cos(179.123f) + compassPos.x); //179.9999876543210123456789f
                                float y = (float)(0.001f * Math.Sin(179.123f) + compassPos.y); //179.9999876543210123456789f

                                training[i] = new Trainer(weightsNumber, x, y, answer);
                                perc.Train(training[i].inputs, training[i].answer);
                            }
                            else if (i == 2) //right 
                            {
                                //sccsgimbal v1.0 and v1.1 were using this random point around the position of the compass itself for a random guess. it's one way to do it but i prefer the instant choice of 2 points only, one left and one right and maybe a third random one.
                                //double angleDeg = random.Next(360) / (2 * Math.PI);

                                // randomly getting a point at the location of the compass
                                float x = (float)(0.001f * Math.Cos(0.00123f) + compassPos.x);
                                float y = (float)(0.001f * Math.Sin(0.00123f) + compassPos.y);

                                training[i] = new Trainer(weightsNumber, x, y, answer);
                                perc.Train(training[i].inputs, training[i].answer);
                            }
                            else if (i == 1)
                            {
                                //sccsgimbal v1.0 and v1.1 were using this random point around the position of the compass itself for a random guess. it's one way to do it but i prefer the instant choice of 2 points only, one left and one right and maybe a third random one.
                                double angleDeg = random.Next(360) / (2 * Math.PI);

                                // randomly getting a point at the location of the compass
                                float x = (float)(0.001f * Math.Cos(angleDeg) + compassPos.x);
                                float y = (float)(0.001f * Math.Sin(angleDeg) + compassPos.y);

                                training[i] = new Trainer(weightsNumber, x, y, answer);
                                perc.Train(training[i].inputs, training[i].answer);
                            }
                        }
                    }
                }









                int guessedCorrect = 0;
                int guessedWrong = 0;

                int turnRight = 0;
                int turnLeft = 0;

                for (int i = 0; i < training.Length; i++)
                {
                    int guess = perc.Guess(training[i].inputs);
                    //Vector2 neededPos = new Vector2(training[i].inputs[0], training[i].inputs[1]);

                    if (training[i].answer == 1)
                    {
                        turnRight++;
                    }
                    else if (training[i].answer == -1)
                    {
                        turnLeft++;
                    }

                    if (guess >= 0)
                    {

                        guessedCorrect++;
                    }
                    else
                    {
                        guessedWrong++;
                    }
                }

                if (guessedCorrect >= (training.Length * 0.5f) - errormargin || // if the guess is higher than half of training.length
                   guessedWrong >= (training.Length * 0.5f) - errormargin ||
                   guessedCorrect <= (training.Length * 0.5f) + errormargin ||
                   guessedWrong <= (training.Length * 0.5f) + errormargin)
                {
                    if (turnRight >= (training.Length * 0.5f) - errormargin ||
                        turnLeft >= (training.Length * 0.5f) - errormargin ||
                        turnRight <= (training.Length * 0.5f) + errormargin ||
                        turnLeft <= (training.Length * 0.5f) + errormargin)
                    {
                        if (turnRight > turnLeft)
                        {
                            _guessedCorrectRight++;
                        }
                        else if (turnRight < turnLeft)
                        {
                            _guessedCorrectLeft++;
                        }
                        else
                        {
                            randguess = (int)(Math.Floor(sc_maths.getSomeRandNumThousandDecimal(0, 2, 0))); // random value between 0 and 1

                            if (randguess == 0)
                            {
                                _guessedCorrectRight++;
                            }
                            else
                            {
                                _guessedCorrectLeft++;
                            }
                            //Debug.Log("Data is too similar");
                            //Debug.Log(randguess);
                        }
                    }
                    else
                    {
                        randguess = (int)(Math.Floor(sc_maths.getSomeRandNumThousandDecimal(0, 2, 0))); // random value between 0 and 1

                        if (randguess == 0)
                        {
                            _guessedCorrectRight++;
                        }
                        else
                        {
                            _guessedCorrectLeft++;
                        }
                        //Debug.Log("Data is too similar");
                        //Debug.Log(randguess);
                    }
                }
                else
                {
                    randguess = (int)(Math.Floor(sc_maths.getSomeRandNumThousandDecimal(0, 2, 0))); // random value between 0 and 1

                    if (randguess == 0)
                    {
                        _guessedCorrectRight++;
                    }
                    else
                    {
                        _guessedCorrectLeft++;
                    }
                    //Debug.Log("Data is too similar 0");
                    //Debug.Log(randguess);
                }
                saveCurrentWeightForTheCurrentAngleInsideOfUnitsOf360[(int)currentQuarterRoundedAngle] = perc.GetWeights();
            }
            else
            {
                //Debug.Log("out of range: " + currentQuarterRoundedAngle);
            }


            linearframeguessselection++;
            yield return waitforsecondsclass;
        }
    }
}
