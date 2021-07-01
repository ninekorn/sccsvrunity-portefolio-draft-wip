//by steve chassé aka ninekorn

using UnityEngine;
using System;
using Perceptron;

namespace SCCoreSystems
{
    public class sccsaiguess : MonoBehaviour
    {
        public int inputsNumber = 20; // 3 minimum i think
        public int SC_Angle_Divider = 4;
        public int SC_anglesNumber = 360;
        public int errormargin = 5;
        public int weightsNumber = 10;

        public int SC_anglesQuarterNumber = 0; //SC_Angle_Divider * SC_anglesNumber

        public float[][] saveCurrentWeightForTheCurrentAngleInsideOfUnitsOf360;// = new float[SC_anglesQuarterNumber][];

        public int swtchwaypointtype = 0;
        Transform northpoletransform;
        Transform compasspivot;
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

        int guessedCorrect = 0;
        int guessedWrong = 0;

        int turnRight = 0;
        int turnLeft = 0;




        public sccsaiguess(Transform compass, Transform northpole, int maxPerceptronInstancesneurons, float perceptronLearningRate)
        {
            training = new Trainer[inputsNumber];
            this.northpoletransform = northpole;
            this.compasspivot = compass;
            Starter(maxPerceptronInstancesneurons, perceptronLearningRate);
        }

        void Starter(int maxPerceptronInstancesneurons,float perceptronLearningRate)
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
        float angle = 0.0f;
        float angleRounded = 0.0f;
        float currentDiff = 0.0f;
        float currentQuarterRoundedAngle = 0.0f;

        public void UpdatePerceptron()
        {
            _guessedCorrectRight = 0;
            _guessedCorrectLeft = 0;

            compassPos = new Vector2(compasspivot.transform.position.x, compasspivot.transform.position.y);

            if (swtchwaypointtype == 0)
            {   
                /*var right = compasspivot.transform.right;
                 right.y = 0;
                 right *= Mathf.Sign(compasspivot.transform.up.y);
                 var fwd = Vector3.Cross(right, Vector3.up).normalized;
                 float pitch = Vector3.Angle(fwd, compasspivot.transform.forward) * Mathf.Sign(compasspivot.transform.forward.y);
                 */

                angle = sc_maths.ClampValue(compasspivot.transform.eulerAngles.z, 0, SC_anglesQuarterNumber);
            }
            else if (swtchwaypointtype == 1)
            {
                angle = sc_maths.ClampValue(compasspivot.transform.eulerAngles.y, 0, SC_anglesQuarterNumber);
            }
            else if (swtchwaypointtype == 2)
            {
                angle = sc_maths.ClampValue(compasspivot.transform.eulerAngles.x, 0, SC_anglesQuarterNumber);
            }



            angleRounded = Mathf.Round(angle);
            currentDiff = (angle - angleRounded);
            currentQuarterRoundedAngle = Mathf.Round(currentDiff * SC_Angle_Divider) / SC_Angle_Divider;
            currentQuarterRoundedAngle *= 100;
            currentQuarterRoundedAngle = (angle * SC_Angle_Divider);
            currentQuarterRoundedAngle = sc_maths.ClampValue(currentQuarterRoundedAngle, 0, SC_anglesQuarterNumber);

            weights = perc.GetWeights();

            if ((int)currentQuarterRoundedAngle < saveCurrentWeightForTheCurrentAngleInsideOfUnitsOf360.Length)
            {
                perc._SC_Perceptron_SetRotWeights(saveCurrentWeightForTheCurrentAngleInsideOfUnitsOf360[(int)currentQuarterRoundedAngle]);

                if (swtchwaypointtype == 0)
                {
                    Vector2 dirbulletprimerright = new Vector2(compasspivot.transform.right.x, compasspivot.transform.right.y);
                    dirbulletprimerright.Normalize();

                    Vector2 dirprimertonorthpoletransform = new Vector2(northpoletransform.position.x, northpoletransform.position.y) - new Vector2(compasspivot.position.x, compasspivot.position.y);
                    dirprimertonorthpoletransform.Normalize();

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
                else if (swtchwaypointtype == 1)
                {
                    Vector2 dirbulletprimerright = new Vector2(-compasspivot.transform.right.z,compasspivot.transform.right.x);
                    dirbulletprimerright.Normalize();

                    Vector2 dirprimertonorthpoletransform = new Vector2(northpoletransform.position.x, northpoletransform.position.z) - new Vector2(compasspivot.position.x, compasspivot.position.z);
                    dirprimertonorthpoletransform.Normalize();

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
                else if (swtchwaypointtype == 2)
                {
                    Vector2 dirbulletprimerright = new Vector2(compasspivot.transform.forward.z, compasspivot.transform.forward.y);
                    dirbulletprimerright.Normalize();

                    Vector2 dirprimertonorthpoletransform =new Vector2(compasspivot.position.z, compasspivot.position.y) - new Vector2(northpoletransform.position.z, northpoletransform.position.y);
                    dirprimertonorthpoletransform.Normalize();

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









                if (swtchwaypointtype ==0)
                {
                    compassPos = new Vector2(compasspivot.transform.position.x, compasspivot.transform.position.y);

                    for (int i = 0; i < training.Length; i++)
                    {
                        double angleInRadians = random.Next(360) / (2 * Math.PI);

                        // randomly getting a point at the location of the compass
                        float x = (float)(0.001f * Math.Cos(angleInRadians) + compassPos.x);
                        float y = (float)(0.001f * Math.Sin(angleInRadians) + compassPos.y);

                        training[i] = new Trainer(weightsNumber, x, y, answer);
                        perc.Train(training[i].inputs, training[i].answer);
                    }
                }
                else if (swtchwaypointtype == 1)
                {
                    compassPos = new Vector2(-compasspivot.transform.position.z, compasspivot.transform.position.x);

                    for (int i = 0; i < training.Length; i++)
                    {
                        double angleInRadians = random.Next(360) / (2 * Math.PI);

                        // randomly getting a point at the location of the compass
                        float x = (float)(0.001f * Math.Cos(angleInRadians) + compassPos.x);
                        float y = (float)(0.001f * Math.Sin(angleInRadians) + compassPos.y);

                        training[i] = new Trainer(weightsNumber, x, y, answer);
                        perc.Train(training[i].inputs, training[i].answer);
                    }
                }
                else if (swtchwaypointtype == 2)
                {
                    compassPos = new Vector2(compasspivot.transform.position.z, compasspivot.transform.position.y);

                    for (int i = 0; i < training.Length; i++)
                    {
                        double angleInRadians = random.Next(360) / (2 * Math.PI);

                        // randomly getting a point at the location of the compass
                        float x = (float)(0.001f * Math.Cos(angleInRadians) + compassPos.x);
                        float y = (float)(0.001f * Math.Sin(angleInRadians) + compassPos.y);

                        training[i] = new Trainer(weightsNumber, x, y, answer);
                        perc.Train(training[i].inputs, training[i].answer);
                    }
                }








                guessedCorrect = 0;
                guessedWrong = 0;

                turnRight = 0;
                turnLeft = 0;

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

                if (guessedCorrect >= (training.Length * 0.5f) - errormargin|| // if the guess is higher than half of training.length
                   guessedWrong >= (training.Length * 0.5f) - errormargin ||
                   guessedCorrect <= (training.Length * 0.5f) + errormargin ||
                   guessedWrong <= (training.Length * 0.5f) + errormargin)
                {
                    if (turnRight >= (training.Length * 0.5f) - errormargin||
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
                    }
                }
                else
                {
                    randguess = (int)(Math.Floor(sc_maths.getSomeRandNumThousandDecimal(0, 2,0))); // random value between 0 and 1

                    if (randguess == 0)
                    {
                        _guessedCorrectRight++;
                    }
                    else
                    {
                        _guessedCorrectLeft++;
                    }
                    //Debug.Log("Data is too similar 0");
                }
                saveCurrentWeightForTheCurrentAngleInsideOfUnitsOf360[(int)currentQuarterRoundedAngle] = perc.GetWeights();
            }
            else
            {
                Debug.Log("out of range: " + currentQuarterRoundedAngle);
            }
        }
    }
}
