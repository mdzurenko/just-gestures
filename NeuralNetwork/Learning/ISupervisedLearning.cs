using System;
using System.Collections.Generic;
using System.Text;

namespace NeuralNetwork.Learning
{
    interface ISupervisedLearning
    {
        double LearningRate { get; }
        double Momentum { get; }
        double Goal { get; }

        /// <summary>
        /// Trains neural network
        /// </summary>
        /// <param name="input">input</param>
        /// <param name="output">demanded output</param>
        /// <param name="epochs">Maximum epochs to run (returns ran epochs)</param>
        /// <returns></returns>
        double Train(double[][] input, double[][] output, ref int epochs);
    }
}
