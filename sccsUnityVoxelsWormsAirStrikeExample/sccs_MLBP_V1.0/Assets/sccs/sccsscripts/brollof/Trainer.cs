

namespace Perceptron
{
    public class Trainer
    {
        public float[] inputs;
        public int answer;

        public Trainer(int neurons, float x, float y, int a)
        {
            inputs = new float[neurons];
            inputs[0] = x;
            inputs[1] = y;
            inputs[2] = 1;
            answer = a;
        }
    }
}
