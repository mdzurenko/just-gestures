using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace NeuralNetwork.ActivationFunctions
{
    [Serializable]
    public class SigmoidFunction : IActivationFunction, ISerializable
    {
        private double m_alpha = 1;

        public double Alpha
        {
            get { return m_alpha; }
            set { m_alpha = value; }
        }

        public SigmoidFunction() { }

        public SigmoidFunction(double alpha)
        {
            m_alpha = alpha;
        }

        public double Compute(double x)
        {
            return (1 / (1 + Math.Exp(-m_alpha * x)));
        }
      
        public double Derivative(double x)
        {
            double y = Compute(x);
            return (m_alpha * y * (1 - y));
        }

        public SigmoidFunction(SerializationInfo info, StreamingContext context)
        {
            try { m_alpha = info.GetDouble("Alpha"); }
            catch { m_alpha = 1; }            
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Alpha", m_alpha);
        }
    }
}
