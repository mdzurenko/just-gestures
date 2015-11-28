using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using System.Threading;
using NeuralNetwork;
using NeuralNetwork.ActivationFunctions;
using NeuralNetwork.Learning;
using JustGestures.Properties;
using JustGestures.GestureParts;

namespace JustGestures
{
    public class MyNeuralNetwork
    {
        /// <summary>
        /// Is required if the user updates program and new algorithm of neural network is implemented, the gestures retraining process will start
        /// </summary>
        const string NEURAL_NETWORK_VERSION = "1.1";

        public const int TRAINING_INTERRUPTED = -1;

        const double MIN_ACCURACY = 0.72; //0.75
        const double MIN_TOTAL_ACCURACY = 0.7;//0.65;
        const double MAX_DIVERGENCE = 3.6;//2.5;

        public delegate void DlgNetworkLearnt(double error, int epochs);
        public DlgNetworkLearnt NetworkLearnt;

        private void OnNetworkLearnt(double error, int epochs)
        {
            m_isTraining = false;
            if (NetworkLearnt != null)
                NetworkLearnt(error, epochs);
        }

        public delegate void DlgEmpty();
        public DlgEmpty NetworkStartTraining;

        private void OnNetworkStartTraining()
        {
            m_isTraining = true;
            if (NetworkStartTraining != null)
                NetworkStartTraining();
        }

        Thread threadTrainNetwork;
        Network m_network;
        string m_version = Config.User.NnVersion;
        int m_inputSize = Config.User.NnInputSize;
        int m_hiddenLayerSize = Config.User.NnHidenLayerSize;
        int m_outputSize = Config.User.NnOutputSize;
        int m_setSize = Config.User.NnTrainingSetSize;
        double m_momentum = Config.User.NnMomentum;
        double m_learningRate = Config.User.NnLearningRate;
        double m_goal = Config.User.NnGoal;

        Dictionary<string, MyCurve> m_curveSets;
        Dictionary<string, ClassicCurve> m_curves;
        BackPropagationLearning m_BPLearning;
        bool m_saveIsRequired = false;
        bool m_isTraining = false;

        public Dictionary<string, ClassicCurve> Curves { get { return m_curves; } set { m_curves = value; } }
        public bool IsTraining { get { return m_isTraining; } }

        /// <summary>
        /// Load neural network from file
        /// (Recreates and retrain if settings are different. Releases training set.)
        /// </summary>
        public MyNeuralNetwork()
        {
            StopLearning();
            m_curves = new Dictionary<string, ClassicCurve>();
            m_network = FileOptions.LoadNeuralNetwork();
            //m_curves = FileOptions.LoadTrainingSet(true);
        }

        public void CheckParams()
        {
            if (m_network == null || m_network.InputsCount != m_inputSize ||
                m_network[0].NeuronsCount != m_hiddenLayerSize ||
                m_network[1].NeuronsCount != m_outputSize || m_version != NEURAL_NETWORK_VERSION)
            {
                Config.User.NnVersion = NEURAL_NETWORK_VERSION;
                Config.User.Save();
                LoadCurves();
                InitializeNetwork();
                OnNetworkStartTraining();
                threadTrainNetwork = new Thread(new ThreadStart(TrainNeuralNetwork));
                threadTrainNetwork.Start();
            }
        }

        /// <summary>
        /// Load curves from file
        /// </summary>
        public void LoadCurves()
        {
            m_curveSets = FileOptions.LoadTrainingSet();
            // checks whether the curves set correspond to curves
            if (m_curveSets.Count != m_curves.Count)
                CreateTrainingSet();
        }

        private void CreateTrainingSet()
        {
            m_curveSets = new Dictionary<string, MyCurve>();
            foreach (ClassicCurve curve in m_curves.Values)// ClassicCurve curve in m_curves)
            {
                MyCurve c = new MyCurve(curve.ID, curve.Points);
                c.CreateTrainingSet();
                m_curveSets.Add(curve.ID, c);
            }
        }

        /// <summary>
        /// Creates copy of neural network and curves in new instance
        /// </summary>
        /// <param name="network"></param>
        public MyNeuralNetwork(MyNeuralNetwork network)
        {            
            m_network = new Network(network.m_network);
            m_curves = new Dictionary<string, ClassicCurve>();
            
            //necessary to do proper copy, otherwise problems with NnIndex (stayed modyfied when Form_modify canceled)
            foreach (ClassicCurve curve in network.Curves.Values)
                m_curves.Add(curve.ID, new ClassicCurve(curve));

            if (network.m_curveSets != null)
            {
                m_curveSets = new Dictionary<string, MyCurve>();
                foreach (MyCurve curve in network.m_curveSets.Values)
                    m_curveSets.Add(curve.ID, new MyCurve(curve));
                

                //m_curveSets = new List<MyCurve>(network.m_curveSets);
            }
        }

        /// <summary>
        /// Creates default neural network accroding to settings
        /// </summary>
        private void InitializeNetwork()
        {
            double[][] thresholds = new double[][] { new double[m_hiddenLayerSize], new double[m_outputSize] };
            
            for (int i = 0; i < thresholds.Length; i++)
                for (int j = 0; j < thresholds[i].Length; j++)
                    thresholds[i][j] = StaticRandom.RandomDouble() * (-1);

            m_network = new Network(m_inputSize, new int[] { m_hiddenLayerSize, m_outputSize }, thresholds,
                new IActivationFunction[] { new HyperbolicTangentFunction(), new SigmoidFunction() });
        }

        /// <summary>
        /// Save training set and neural network in to file and load reduced curves
        /// (if network is during training it will be saved when the training is finished)
        /// </summary>
        public void SaveNeuralNetwork()
        {
            //if (m_curves.Count == Config.User.nnOutputSize)
            //{
            //    Config.User.nnOutputSize *= 2;
            //    Config.User.Save();
            //}
            
            if (m_isTraining)
            {
                FileOptions.SaveNeuralNetwork(null);
                FileOptions.SaveTrainingSet(null);
                m_saveIsRequired = true;
            }
            else
            {
                if (m_curveSets != null)
                    FileOptions.SaveTrainingSet(m_curveSets);
                m_curveSets = null;
                FileOptions.SaveNeuralNetwork(m_network);
            }
        }

        private void TrainNeuralNetwork()
        {
            Debug.WriteLine("MyNN: Learning Started");
            m_BPLearning = new BackPropagationLearning(m_network, m_learningRate, m_momentum, m_goal);
            double[][] input;
            double[][] output;
            int maxEpochs = 500;
            CreateInputOutput(out input, out output);
            if (input == null && output == null)
                return; //network is too small (needs to be recreated)
            double error = m_BPLearning.Train(input, output, ref maxEpochs);
            Debug.WriteLine(string.Format("MyNN trained: {0} epochs, {1} error", maxEpochs, error));
            if (m_saveIsRequired)
            {                
                m_saveIsRequired = false;
                FileOptions.SaveTrainingSet(m_curveSets);
                m_curveSets = null;
                FileOptions.SaveNeuralNetwork(m_network);
            }

            OnNetworkLearnt(error, maxEpochs);
        }

       
        /// <summary>
        /// Network learns new curve 
        /// (adds curve to collection and runs training )
        /// </summary>
        /// <param name="curve">Curve</param>
        public void LearnNewCurve(ClassicCurve curve)
        {
            int position = FindFreePosition();
            if (position == -1)
            {
                // maximum amount of gestures that can be learnt is reached
                return;
            }
            
            StopLearning(); //just for sure...
            //if (threadTrainNetwork != null && threadTrainNetwork.IsAlive)
            //    threadTrainNetwork.Abort();
            //if (m_newCurveAdded) m_curves.RemoveAt(m_curves.Count - 1);
            
            MyCurve curveToTrain = new MyCurve(curve.ID, curve.Points);            
            curve.NnIndex = position;           
            curveToTrain.CreateTrainingSet();

            // Might happen that curve sets is not compatible with curves 
            // therefore it is required to reinicialize it
            if (m_curveSets.ContainsKey(curve.ID))
                CreateTrainingSet();
            
            m_curveSets.Add(curve.ID, curveToTrain);
            m_curves.Add(curve.ID, curve);

            InitializeNetwork();
            OnNetworkStartTraining();
            threadTrainNetwork = new Thread(new ThreadStart(TrainNeuralNetwork));
            threadTrainNetwork.Start();
        }

        /// <summary>
        /// Stop running training
        /// </summary>
        public void StopLearning()
        {
            if (threadTrainNetwork != null && threadTrainNetwork.IsAlive)
            {
                Debug.WriteLine("MyNN: Learning Stoped");
                threadTrainNetwork.Abort();
                OnNetworkLearnt(TRAINING_INTERRUPTED, TRAINING_INTERRUPTED);
            }
        }

        /// <summary>
        /// Network unlearns curve
        /// </summary>
        /// <remarks>
        /// Removes from curve collection and reinicialize the network and start learning process
        /// </remarks>
        /// <param name="curveIDs">Curve ID</param>
        /// <param name="temp">If True only delete (not starts training)</param>
        public void UnlearnCurves(string[] curveIDs, bool temp)
        {
            if (curveIDs.Length == 0) return;
            foreach (string id in curveIDs)
            {
                if (m_curves.ContainsKey(id))
                    m_curves.Remove(id);
                if (m_curveSets.ContainsKey(id))
                    m_curveSets.Remove(id);
            }
            if (!temp)
            {
                StopLearning();
                InitializeNetwork();
                if (m_curves.Count > 0)
                {
                    OnNetworkStartTraining();
                    threadTrainNetwork = new Thread(new ThreadStart(TrainNeuralNetwork));
                    threadTrainNetwork.Start();
                }
            }
        }


        /// <summary>
        /// Returns the name of the recognized curve
        /// </summary>
        /// <param name="angles">Scaled curve to angles</param>
        /// <param name="divergence">Divergence to original curve</param>
        /// <returns></returns>
        public string RecognizeCurve(double[] angles, out double divergence)
        {
            divergence = -1;
            if (threadTrainNetwork != null && threadTrainNetwork.IsAlive)
            {
                Debug.WriteLine("MyNN cannot recognize in learning phase");
                return string.Empty; //cannot recognize during learning phase
            }

            double[] output = m_network.Compute(angles);         
            //learnt error is smaller than 0.00001;
            int pos = -2;
            double total_error = 0;
            Debug.Write("MyNN Output: ");
            for (int i = 0; i < output.Length; i++)
            {
                Debug.Write(string.Format("{0} ", Math.Floor(output[i] * 1000) / 1000.0));
                if (output[i] > MIN_ACCURACY)
                {
                    if (pos == -2) pos = i;
                    else return string.Empty;
                }
                else
                    total_error += output[i];
                //if (output[i] <= MIN_ACCURACY && output[i] >= MAX_ACCURACY)
                //    return string.Empty;                
            }            
            Debug.WriteLine(" ");
            if (pos == -2) return string.Empty;
            double result = output[pos] - total_error;
            Debug.WriteLine(string.Format("Total Accuracy is: {0}", Math.Floor(result * 1000) / 1000.0));
            
            if (result < MIN_TOTAL_ACCURACY) return string.Empty;
            
            MyCurve recognizedCurve = null;
            foreach (ClassicCurve curve in m_curves.Values)
                if (curve.NnIndex == pos)
                {
                    recognizedCurve = new MyCurve(curve.ID, curve.Points);
                    break;
                }
            if (recognizedCurve != null)
            {
                //During recognition only reduced curve is loaded
                divergence = ComputeDivergence(angles, new double[][] { recognizedCurve.GetScaledInput() });
                if (divergence < MAX_DIVERGENCE)
                    return recognizedCurve.ID;
                else
                {
                    divergence = -1;
                    return string.Empty;
                }
            }
            else
                return string.Empty;
        }

        /// <summary>
        /// Computes divergence between actual and original curve
        /// </summary>
        /// <param name="angles">Scaled input to angles of actual curve</param>
        /// <param name="recognizedSets">Scalend input to angles or original curve</param>
        /// <returns></returns>
        private double ComputeDivergence(double[] angles, double[][] recognizedSets)
        {
            double error = 0;
            double value;
            double diff;
            for (int i = 0; i < recognizedSets.Length; i++)
            {
                value = 0;
                for (int j = 0; j < angles.Length; j++)
                {
                    diff = angles[j] - recognizedSets[i][j];                    
                    value += diff * diff;
                }
                error += Math.Sqrt(value);
            }
            error /= (double)recognizedSets.Length;
            error = Math.Floor(error * 1000) / 1000.0;
            Debug.WriteLine("MyNN Divergence : " + error.ToString());
            return error;
        }

        /// <summary>
        /// Creates input and output for nerual network
        /// </summary>
        /// <param name="input"></param>
        /// <param name="output"></param>
        private void CreateInputOutput(out double[][] input, out double[][] output)
        {
            int maxPos = -1;
            string[] positions = new string[m_outputSize];
            List<ClassicCurve> curves = new List<ClassicCurve>(m_curves.Values);
            for (int i = 0; i < positions.Length; i++)
                positions[i] = string.Empty;

            for (int i = 0; i < curves.Count; i++)
            {
                int nnIndex = curves[i].NnIndex;
                positions[nnIndex] = curves[i].ID;
                maxPos = maxPos > nnIndex ? maxPos : nnIndex;
            }
            maxPos++; //we need to increment the position so we can reach it in a cycle (as on this position some gesture actualy is!)
            
            List<double[]> inputList = new List<double[]>();
            List<double[]> outputList = new List<double[]>();         
            for (int i = 0; i < maxPos; i++)
            {
                if (positions[i] != string.Empty)
                    inputList.AddRange(m_curveSets[positions[i]].TrainingSet);                    
                else
                    inputList.AddRange(MyCurve.EmptyTrainingSet());
            }
            //input = inputList.ToArray();

            int k = -1;
            int trainingSize = m_setSize * maxPos;//m_curves.Count;
                        
            //output = new double[trainingSize][];
            for (int i = 0; i < trainingSize; i++)
            {
                if (i % m_setSize == 0)
                    k++;
                double[] oneOutput = new double[m_outputSize];
                //output[i] = new double[m_outputSize];
                if (positions[k] != string.Empty)
                    //output[i][m_curves[positions[k]].NnIndex] = 1;
                    oneOutput[m_curves[positions[k]].NnIndex] = 1;
                
                outputList.Add(oneOutput);
            }

            //add false patterns
            //double[] emptyOutput = new double[m_outputSize];
            //foreach (MyCurve c in m_curveSets.Values)
            //{
            //    double[][] falsePatterns = c.GenerateFalsePatterns();
            //    for (int i = 0; i < falsePatterns.Length; i++)
            //    {
            //        inputList.Add(falsePatterns[i]);
            //        outputList.Add(emptyOutput);
            //    }
            //}

            //shuffle it 
            List<double[]> tempInput = inputList; //new List<double[]>(input);
            List<double[]> tempOutput = outputList; //new List<double[]>(output);
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

       
        /// <summary>
        /// Finds free position in neural network output
        /// </summary>
        /// <returns></returns>
        private int FindFreePosition()
        {
            int pos = -1;
            int[] positions = new int[m_outputSize];
            for (int i = 0; i < positions.Length; i++)
                positions[i] = 0;
            foreach (ClassicCurve curve in m_curves.Values)
                if (curve.NnIndex != -1) positions[curve.NnIndex] = 1;
            for (int i = 0; i < m_outputSize; i++)
                if (positions[i] == 0 && pos == -1)
                    pos = i;
            return pos;
        }

        
    }
}
