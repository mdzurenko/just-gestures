using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using NeuralNetwork.ActivationFunctions;

namespace NeuralNetwork
{
    [Serializable]
    public class Neuron : ISerializable
    {
        private int m_inputsCount = 0;
        private double[] m_weights = null;
        private IActivationFunction m_function = null;
        private double m_threshold;
        private double m_output = 0;

        public int InputCount { get { return m_inputsCount; } }
        public double this[int index]
        {
            get { return m_weights[index]; }
            set { m_weights[index] = value; }
        }
        public IActivationFunction ActivationFunction { get { return m_function; } }
        public double Threshold { get { return m_threshold; } }
        public double Output { get { return m_output; } }

        public Neuron(int inputCount, IActivationFunction function)
        {
            m_inputsCount = inputCount;
            m_function = function;
            m_weights = new double[inputCount + 1];
            SetRandomWeights();
        }

        public Neuron(int inputCount, IActivationFunction function, double threshold)
        {
            m_inputsCount = inputCount;
            m_function = function;            
            m_weights = new double[inputCount + 1];
            SetRandomWeights();
            m_threshold = threshold; 
        }

        public Neuron(int inputCount, double[] weights, IActivationFunction function, double threshold)
        {
            m_inputsCount = inputCount;
            m_weights = weights;
            m_function = function;
            m_threshold = threshold;
        }

        public Neuron(Neuron neuron)
        {
            m_inputsCount = neuron.m_inputsCount;
            m_weights = (double[])neuron.m_weights.Clone();
            m_function = neuron.m_function;
            m_threshold = neuron.m_threshold;
        }

        public double Compute(double[] input)
        {
            double output = 0;

            for (int i = 0; i < input.Length; i++)
                output += m_weights[i] * input[i];
            output += m_weights[input.Length] * m_threshold; 
            output = m_function.Compute(output);
            m_output = output;
            return output;
        }

        public void SetRandomWeights()
        {
            //Random r = new Random(DateTime.Now.Millisecond);            
            for (int i = 0; i < m_weights.Length; i++)
                m_weights[i] = StaticRandom.RandomDouble(); //r.NextDouble();
        }

        public Neuron(SerializationInfo info, StreamingContext context)
        {
            try { m_inputsCount = info.GetInt32("InputsCount"); }
            catch { m_inputsCount = 0; }
            try { m_weights = (double[])info.GetValue("Weights", typeof(double[])); }
            catch { m_weights = null; }
            try { m_function = (IActivationFunction)info.GetValue("Function", typeof(IActivationFunction)); }
            catch { m_function = null; }
            try { m_threshold = info.GetDouble("Threshold"); }
            catch { m_threshold = 0; }
            try { m_output = info.GetDouble("Output"); }
            catch { m_output = 0; }
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("InputsCount", m_inputsCount);
            info.AddValue("Weights", m_weights, typeof(double[]));
            info.AddValue("Function", m_function, typeof(IActivationFunction));
            info.AddValue("Threshold", m_threshold);
            info.AddValue("Output", m_output);
        }

    }
}
