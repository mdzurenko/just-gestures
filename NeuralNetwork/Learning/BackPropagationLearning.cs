using System;
using System.Collections.Generic;
using System.Text;

namespace NeuralNetwork.Learning
{
    public class BackPropagationLearning : ISupervisedLearning
    {
        private Network m_network;
        private double m_learningRate = 1;
        private double m_momentum = 0.0;
        private double m_goal = 0.0001;
        private double[][] m_neuronsErrors;
        private double[][][] m_weightsUpdates;

        public double LearningRate { get { return m_learningRate; } }
        public double Momentum { get { return m_momentum; } }
        public double Goal { get { return m_goal; } }

        public BackPropagationLearning(Network network)
        {
            m_network = network;
            InitializeNetwork();
        }

        public BackPropagationLearning(Network network, double learningRate, double momentum, double goal)
        {
            m_network = network;
            m_learningRate = learningRate;
            m_momentum = momentum;
            m_goal = goal;
            InitializeNetwork();
        }

        private void InitializeNetwork()
        {
            m_neuronsErrors = new double[m_network.LayersCount][];
            m_weightsUpdates = new double[m_network.LayersCount][][];
            
            for (int i = 0; i < m_network.LayersCount; i++)
            {
                Layer layer = m_network[i];
                m_neuronsErrors[i] = new double[layer.NeuronsCount];
                m_weightsUpdates[i] = new double[layer.NeuronsCount][];

                for (int j = 0; j < layer.NeuronsCount; j++)
                    m_weightsUpdates[i][j] = new double[layer.InputsCount + 1];
            }            
        }

        public double Train(double[][] input, double[][] output, ref int epochs)
        {            
            int i = 0;
            double error = 1;
            while (i < epochs && error > m_goal)
            {
                //ShuffleSet(ref input, ref output);
                i++;
                error = 0;
                for (int j = 0; j < input.Length; j++)
                {
                    //int r = StaticRandom.RandomInteger(0, input.Length);
                    //error += RunEpoch(input[r], output[r]);
                    error += RunEpoch(input[j], output[j]);
                }
            
                error /= input.Length;
            }
            epochs = i;
            return error;
        }

        static void ShuffleSet(ref double[][] input, ref double[][] output)
        {
            List<double[]> tempInput = new List<double[]>(input);
            List<double[]> tempOutput = new List<double[]>(output);
            List<double[]> randInput = new List<double[]>();
            List<double[]> randOutput = new List<double[]>();
            while (tempInput.Count > 0)
            {
                int i = StaticRandom.RandomInteger(0, tempInput.Count);
                randInput.Add(tempInput[i]);
                randOutput.Add(tempOutput[i]);
                tempInput.RemoveAt(i);
                tempOutput.RemoveAt(i);
            }
            input = randInput.ToArray();
            output = randOutput.ToArray();
        }

        private double RunEpoch(double[] input, double[] output)
        {
            m_network.Compute(input);
            double error = CalculateError(output);
            CalculateUpdates(input);
            UpdateNetwork();
            return error;
        }

        private double CalculateError(double[] demandedOutput)
        {            
            Layer layerCurrent;
            Layer layerPrevious;
            int last_layer = m_network.LayersCount - 1;
            int current_layer;
            int current_neuron;
            int previous_neuron;
            double derivation;
            double delta;
            double error = 0;
            double[] layerErrorsCurrent;
            double[] layerErrorsPrevious;

            for (current_layer = last_layer; current_layer >= 0; current_layer--)
            {
                layerCurrent = m_network[current_layer];
                for (current_neuron = 0; current_neuron < layerCurrent.NeuronsCount; current_neuron++)
                {
                    layerErrorsCurrent = m_neuronsErrors[current_layer];
                    if (current_layer == last_layer)
                    {
                        delta = demandedOutput[current_neuron] - layerCurrent[current_neuron].Output;
                        error += delta * delta;
                    }
                    else
                    {
                        layerPrevious = m_network[current_layer + 1];
                        layerErrorsPrevious = m_neuronsErrors[current_layer + 1];
                        delta = 0;
                        for (previous_neuron = 0; previous_neuron < layerPrevious.NeuronsCount; previous_neuron++)
                            delta += layerErrorsPrevious[previous_neuron] * layerPrevious[previous_neuron][current_neuron];

                    }
                    derivation = layerCurrent[current_neuron].ActivationFunction.Derivative(layerCurrent[current_neuron].Output);
                    layerErrorsCurrent[current_neuron] = delta * derivation;
                }
            }
            return error / m_network[last_layer].NeuronsCount;
        }

        private void CalculateUpdates(double[] input)
        {
            Layer layerCurrent;
            int current_layer;
            int current_neuron;
            int current_weight;
            int threshold_index;
            double delta_x_output;
            double[] outputPrevious = input;
            double[] neuronWeights;

            for (current_layer = 0; current_layer < m_network.LayersCount; current_layer++)
            {
                layerCurrent = m_network[current_layer];
                for (current_neuron = 0; current_neuron < layerCurrent.NeuronsCount; current_neuron++)
                {
                    threshold_index = layerCurrent[current_neuron].InputCount;
                    for (current_weight = 0; current_weight < layerCurrent[current_neuron].InputCount + 1; current_weight++)
                    {
                        if (current_weight == threshold_index)
                            delta_x_output = m_neuronsErrors[current_layer][current_neuron] * layerCurrent[current_neuron].Threshold;
                        else
                            delta_x_output = m_neuronsErrors[current_layer][current_neuron] * outputPrevious[current_weight];
                        
                        neuronWeights = m_weightsUpdates[current_layer][current_neuron];
                        neuronWeights[current_weight] = m_learningRate * delta_x_output + m_momentum * neuronWeights[current_weight];
                    }

                }
                outputPrevious = m_network[current_layer].Output;
            }

        }

        private void UpdateNetwork()
        {
            Layer layerCurrent;
            int current_layer;
            int current_neuron;
            int current_weight;
            for (current_layer = 0; current_layer < m_network.LayersCount; current_layer++)
            {
                layerCurrent = m_network[current_layer];
                for (current_neuron = 0; current_neuron < layerCurrent.NeuronsCount; current_neuron++)
                    for (current_weight = 0; current_weight < layerCurrent[current_neuron].InputCount + 1; current_weight++)
                        layerCurrent[current_neuron][current_weight] += m_weightsUpdates[current_layer][current_neuron][current_weight];
                
            }
        }



    }
}
