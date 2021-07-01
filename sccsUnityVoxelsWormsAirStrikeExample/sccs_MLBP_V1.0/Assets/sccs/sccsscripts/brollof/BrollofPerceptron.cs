using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SCCoreSystems;

namespace Perceptron
{
    public class BrollofPerceptron
    {
        float[] weights;
        public float learningRate;
        static public Random r = new Random();

        public BrollofPerceptron(int n, float rate)
        {
            this.learningRate = rate;

            this.weights = new float[n];
            // Start with random weights
            for (int i = 0; i < n; i++)
            {
                //0.9999876543210123456789f
                //0.0000000000000000000009f
                this.weights[i] = (float)sc_maths.getSomeRandNumThousandDecimal(0.00123f, 0.99876f, 1); // i would try in double or long later (spike kinda float) 0.9876543210123456789f//)r.NextDouble() * 4 - 1; // range <-1:1> // 1-3 // bad as Sebastian Lague says best results are -3 to 3 i think so we can test all of this shit as there seems to be different opinions maybe on the matter.
            }
        }

        public void _SC_Perceptron_SetRotWeights(float[] w)
        {
            weights = w;
        }

        public void Train(float[] inputs, int desired)
        {

            //the weights are now trained regardless as the compass can turn left or right and both choices are good but one is better than the other.
            //but the weigths are also per angle and so if you use quarter angles, if the compass makes a mistake at 0.25, it will have other weights and a full
            //training for the next angle.

            int guess = this.Guess(inputs);
            float error = desired - guess;

            for (int i = 0; i < weights.Length; i++)
            {
                weights[i] += this.learningRate * error * inputs[i];
            }

            //I have decided to always train the perceptron whatever the result is, because it needs to choose Left or Right based on the best guess. but my guess are random. so let's upgrade that to non-random in the script SC_AI.cs for 2 points, 1 left and 1 right
            /*// if the result does not match expected
            if (result != expectedResult)
            {
                // calculate error (need to convert boolean to a number
                double error = (expectedResult ? 1 : 0) - (result ? 1 : 0);
                for (int i = 0; i < Weights.Length; i++)
                {
                    // adjust the weights
                    Weights[i] += LearningRate * error * inputs[i];
                }
            }*/
        }

        public int Guess(float[] inputs)
        {
            float sum = 0;
            for (int i = 0; i < weights.Length; i++)
            {
                sum += inputs[i] * weights[i];
            }
            return this.Activate(sum);
        }

        private int Activate(float sum)
        {
            if (sum > 0)
                return 1;
            else
                return -1;
        }


        // i think i took that from the Sebastian Lague tutorial but i am also thinking it might come from another dude on github or stackexchange or youtube.        
        /*private int Activate(float x)
        {
            double act = (1 / (1 + Math.Exp((float)-x)));
            if (act > 0)
                return 1;
            else
                return -1;
        }*/

        public float[] GetWeights()
        {
            return this.weights;
        }
    }
}




//ninekorn notes: 
//List of notable tutorials i learned from in order to the best of my memory but there was a lot of back and forth also involved:
//1. https://www.youtube.com/watch?v=Yq0SfuiOVYE #after a dual jagged array, i am completely lost. i tried to learn from this one but i failed. it's difficult to visualize a 3d cubic Matrix array of some sorts.
//                                               #i would maybe prefer to start working on my "memory palace" in 2D :) https://www.youtube.com/watch?v=Ou9h-lCUxRU as even up until now 2020-05-20, i am having 
//                                               #difficulties visualizing 3d objects when i "think" of them. My brain is tortured by this mess of the C...s and whatnot so it's difficult to stay concentrated
//                                               #on one thing only, which is the Bitcoin Mining program i would love to build some day. it might takes years though.

//2. https://www.youtube.com/watch?v=bVQUSndDllU #this one i followed and i think i was close to translating from Python to C# but after finding the Brollof Perceptron, i have decided to immediately go there and learn from that simple example. Then
//                                               #i also wonder the speed difference or performance difference in the linear option and the non-linear option and their different usage in every circle of programming and equations and whatnot.
//                                               #but i quit school a long time ago and those f(x) curves, whatever, i've always been bad at school with them. but that was a long time ago. 15++ years back.
//        

// i got annoyed in the upper vidéo 1 of the variable names like "neurons and perceptrons and weigths" and then he plugs in 3d jagged arrays lol and it completely lost me. i thought, well those are arrays so
// why are you guys all naming them by the name of what they represent in a "humain brain"... it's annoying in terms of someone who, even like me after 2-3 years of programming and knowing how to index a 3d
// chunk terrain inside of a 3d array with the use of a flat array... it annoyed me that the names were not "fitting" to a "simple" tutorial. i believe his tutorial is 1 notch above tutorial 2 in the sense that
// it litterally explains also how you need to manipulate the variables in order to send the stuff inside of the array. with python, you don't even have to breath and it does it for you. hence why BAM i went
// straight to the Sebastian Lague tutorial and that's where i believe i started mixing both of tutorial 1 and 2 in order to learn but it didn't compute perfectly. It still doesn't in my brains for how the 
// learning of the perceptron works. it's just that part that irritates me although based on guesses, i need much more knowledge on the weights. something is missing in my learning process there. Also, i watched
// a very nice Microsoft presentation for neural networks but that one is above in terms of theories and it lost me. i couldn't for fuck sake code anything after watching the video but will never remove his
// example from my "brains database". 
//https://www.youtube.com/watch?v=-zT1Zi_ukSk
//James McCaffrey, Ph.D.
//Software Research Engineer
//Microsoft Research - Deep Learning / AI School
//Also there is that other really cool presenter coding Train and although the following seems like a very good playlist, i only watched parts of 1 maximum 2 episodes and i am unsure if it is the following
//series https://www.youtube.com/watch?v=XJ7HLz9VYz0&list=PLRqwX-V7Uu6aCibgK1PTWWu9by6XFdCfh

