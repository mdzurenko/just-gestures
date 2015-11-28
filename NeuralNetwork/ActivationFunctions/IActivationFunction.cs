using System;
using System.Collections.Generic;
using System.Text;

namespace NeuralNetwork.ActivationFunctions
{
    public interface IActivationFunction
    {
        double Alpha { get; set; }
        double Compute(double x);
        double Derivative(double x);     
    }
}
