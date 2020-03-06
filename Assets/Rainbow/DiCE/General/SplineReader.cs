using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using UnityEngine;

namespace Rainbow.SplineHandling {
    class SplineDescription {
        private List<double> m_Input;
        private List<double> m_LeftOut;
        private List<double> m_RightOut;

        private List<float> m_LeftSpline;
        private List<float> m_RightSpline;

        private float m_LogLuminanceSpan;
        private float m_LogLuminanceFloor;
        private float m_LogLuminanceCeiling;

       public List<double> Input { get { return m_Input; } }
       public float[] LeftEyeLUT { get { return m_LeftSpline.ToArray(); } }
       public float[] RightEyeLUT { get { return m_RightSpline.ToArray(); } }

       public float LogLuminanceSpan { get { return m_LogLuminanceSpan; } }
       public float LogLuminanceFloor { get { return m_LogLuminanceFloor; } }
       public float LogLuminanceCeiling { get { return m_LogLuminanceCeiling; } }

        public SplineDescription(List<double> input, List<double> leftOut, List<double> rightOut)
        {
            m_Input = input;
            m_LeftOut = leftOut;
            m_RightOut = rightOut;
            m_LogLuminanceSpan = (float)Math.Abs(m_Input[0] - m_Input[m_Input.Count - 1]);
            m_LogLuminanceFloor = (float)m_Input[0];
            m_LogLuminanceCeiling = (float)m_Input[m_Input.Count - 1];
        }
        //Creates LUT for left and right eye
        //To get index based on input log luminance in shader:
        // INDEX = (LOG_LUMINANCE + LUMINANCE_SPAN) * (LUT_TABLE_SIZE / LUMINANCE_SPAN)
        public void ComputeLUT()
        {
            m_LeftSpline = new List<float>();
            m_RightSpline = new List<float>();

            for (int i = 1; i <m_Input.Count; ++i)
            {
                int a = (int)(Math.Round(m_Input[i - 1],3) * 1000);
                int b = (int)(Math.Round(m_Input[i], 3) * 1000);
                int steps = Math.Abs(a - b);
                double stepSize = 1.0 / (double)steps;
                
                for(int j = 0; j < steps-1;++j)
                {
                    double l = Lerp(m_LeftOut[i - 1], m_LeftOut[i], stepSize * j);
                    m_LeftSpline.Add((float)l);

                    double r = Lerp(m_RightOut[i - 1], m_RightOut[i], stepSize * j);
                    m_RightSpline.Add((float)r);  
                }

                if(i == m_Input.Count - 1)
                {
                    m_LeftSpline.Add((float)Lerp(m_LeftOut[i - 1], m_LeftOut[i], stepSize * steps));
                    m_RightSpline.Add((float)Lerp(m_RightOut[i - 1], m_RightOut[i], stepSize * steps));          
                } 
            }  
 
        }


        private double Lerp(double a, double b, double t)
        {
            return (1.0 - t) * a + t * b;
        }
        public void PrintInputs()
        {
            Debug.Log(String.Format("{0,-20}{1,-20}{2,-20}", "Input","LeftOut","RightOut" ));
            for(int i = 0; i < m_Input.Count;++i)
            {
                Debug.Log(String.Format("{0,-20}{1,-20}{2,-20}", m_Input[i].ToString(), m_LeftOut[i].ToString(), m_RightOut[i].ToString()));
            }
        }

        public void PrintOutputs()
        {
            //Console.WriteLine("{0,-20}{1,-20}{2,-20}", "Index", "LeftSpline", "RightSpline");
            for (int k = 0; k < m_LeftSpline.Count; ++k)
            {
				//Console.WriteLine("{0,-20}{1,-20}{2,-20}", k.ToString(), m_LeftSpline[k].ToString(), m_RightSpline[k].ToString()); 
				Debug.Log( k.ToString() + ",   " +  m_LeftSpline[k].ToString() + ",     " + m_RightSpline[k].ToString());

            }
        }
    }

    class SplineReader
    {
        static public SplineDescription GenSplineDescription(string csvFile)
        {
            List<double> input = new List<double>();
            List<double> left = new List<double>();
            List<double> right = new List<double>();
            //Debug.Log(csvFile);
            TextAsset ta = (TextAsset)Resources.Load(csvFile, typeof(TextAsset));
            //StreamReader reader = new StreamReader(csvFile);
            string[] lines = ta.text.Split('\n');
            foreach(string s in lines) {
                string [] ar  = s.Split(',');
                if (ar.Length < 3)
                    continue;
                input.Add(Convert.ToDouble(ar[0]));
                left.Add(Convert.ToDouble(ar[1]));
                right.Add(Convert.ToDouble(ar[2]));
            }
            var instance = new SplineDescription(input, left, right);
            instance.ComputeLUT();
            //instance.PrintOutputs();
            return instance;
        }

    }
}
