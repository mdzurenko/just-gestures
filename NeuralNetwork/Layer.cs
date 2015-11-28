using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using NeuralNetwork.ActivationFunctions;


namespace NeuralNetwork
{
    [Serializable]
    public class Layer : ISerializable
    {
        private int m_inputsCount;
        private int m_neuronsCount;
        private double[] m_output;
        private Neuron[] m_neurons;
        private IActivationFunction m_function;

        public int InputsCount { get { return m_inputsCount; } }
        public int NeuronsCount { get { return m_neuronsCount; } }
        public double[] Output { get { return m_output; } }
        public Neuron this[int index] { get { return m_neurons[index]; } }
        public IActivationFunction ActivationFunction { get { return m_function; } }

        public Layer(int inputsCount, int neuronsCount, IActivationFunction function)
        {
            m_inputsCount = inputsCount;
            m_neuronsCount = neuronsCount;
            m_function = function;
            m_neurons = new Neuron[neuronsCount];

            for (int i = 0; i < neuronsCount; i++)
                m_neurons[i] = new Neuron(m_inputsCount, m_function);

            m_output = new double[neuronsCount];
        }

        public Layer(int inputsCount, int neuronsCount, double[] thresholds, IActivationFunction function)
        {
            m_inputsCount = inputsCount;
            m_neuronsCount = neuronsCount;
            m_function = function;
            m_neurons = new Neuron[neuronsCount];

            for (int i = 0; i < neuronsCount; i++)
                m_neurons[i] = new Neuron(m_inputsCount, m_function, thresholds[i]);

            m_output = new double[neuronsCount];
        }

        public Layer(Layer layer)
        {
            m_inputsCount = layer.m_inputsCount;
            m_neuronsCount = layer.m_neuronsCount;
            m_function = layer.m_function;
            m_neurons = new Neuron[m_neuronsCount];
            for (int i = 0; i < m_neuronsCount; i++)
                m_neurons[i] = new Neuron(layer[i]);
            m_output = (double[])layer.m_output.Clone();
        }

        public double[] Compute(double[] input)
        {
            for (int i = 0; i < m_neurons.Length; i++)
                m_output[i] = m_neurons[i].Compute(input);
            return m_output;
        }

        public Layer(SerializationInfo info, StreamingContext context)
        {
            try { m_inputsCount = info.GetInt32("InputsCount"); }
            catch { m_inputsCount = 0; }
            try { m_neuronsCount = info.GetInt32("NeuronsCount"); }
            catch { m_neuronsCount = 0; }
            try { m_output = (double[])info.GetValue("Outputs", typeof(double[])); }
            catch { m_output = null; }
            try { m_neurons = (Neuron[])info.GetValue("Neurons", typeof(Neuron[])); }
            catch { m_neurons = null; }
            try { m_function = (IActivationFunction)info.GetValue("Function", typeof(IActivationFunction)); }
            catch { m_function = null; }
         }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("InputsCount", m_inputsCount);
            info.AddValue("NeuronsCount", m_neuronsCount);
            info.AddValue("Outputs", m_output, typeof(double[]));
            info.AddValue("Neurons", m_neurons, typeof(Neuron[]));
            info.AddValue("Function", m_function, typeof(IActivationFunction));
        }
    }
}
