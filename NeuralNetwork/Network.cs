using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Diagnostics;
using NeuralNetwork.ActivationFunctions;

namespace NeuralNetwork
{
    [Serializable]
    public class Network : ISerializable
    {
        private Layer[] m_layers;
        public Layer this[int index] { get { return m_layers[index]; } }         
        public int InputsCount { get { return m_layers[0].InputsCount; } }
        public int LayersCount { get { return m_layers.Length; } }
        public double[] Output { get { return m_layers[m_layers.Length - 1].Output; } }

        //public Network(int[] layersTopology, IActivationFunction[] functions)
        //{
        //    m_layers = new Layer[layersTopology.Length];
        //    int layer_input = layersTopology[0];
        //    for (int i = 0; i < layersTopology.Length; i++)
        //    {
        //        m_layers[i] = new Layer(layer_input, layersTopology[i], functions[i]);
        //        layer_input = layersTopology[i];
        //    }
        //}

        public Network(int inputsCount, int[] layersTopology, double[][] thresholds, IActivationFunction[] functions)
        {
            m_layers = new Layer[layersTopology.Length];
            int layer_input = inputsCount;
            for (int i = 0; i < layersTopology.Length; i++)
            {
                m_layers[i] = new Layer(layer_input, layersTopology[i], thresholds[i], functions[i]);
                layer_input = layersTopology[i];
            }
        }

        public Network(Network network)
        {
            m_layers = new Layer[network.m_layers.Length];
            for (int i = 0; i < m_layers.Length; i++)
                m_layers[i] = new Layer(network[i]);            
        }

        public double[] Compute(double[] input)
        {            
            double[] output = input;
            for (int i = 0; i < m_layers.Length; i++)
                output = m_layers[i].Compute(output);
            return output;
        }

        public Network(SerializationInfo info, StreamingContext context)
        {
            try { m_layers = (Layer[])info.GetValue("Layers", typeof(Layer[])); }
            catch { m_layers = null; }
         }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Layers", m_layers, typeof(Layer[]));
        }
    }
}
