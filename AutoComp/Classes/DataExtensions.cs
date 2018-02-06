using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Positivo.AutoComp
{
    public static class DataExtensions
    {
        public static double Mean(this List<double> values)
        {
            return values.Count == 0 ? 0 : values.Mean(0, values.Count);
        }

        public static double Mean(this List<double> values, int start, int end)
        {
            double s = 0;

            for (int i = start; i < end; i++)
            {
                s += values[i];
            }

            return s / (end - start);
        }

        public static double Variance(this List<double> values)
        {
            return values.Variance(values.Mean(), 0, values.Count);
        }

        public static double Variance(this List<double> values, double mean)
        {
            return values.Variance(mean, 0, values.Count);
        }

        public static double Variance(this List<double> values, double mean, int start, int end)
        {
            double variance = 0;

            for (int i = start; i < end; i++)
            {
                variance += Math.Pow((values[i] - mean), 2);
            }

            int n = end - start;
            if (start > 0) n -= 1;

            return variance / (n - 1);
        }

        public static double StandardDeviation(this List<double> values)
        {
            return values.Count == 0 ? 0 : values.StandardDeviation(0, values.Count);
        }

        public static double StandardDeviation(this List<double> values, int start, int end)
        {
            double mean = values.Mean(start, end);
            double variance = values.Variance(mean, start, end);

            return Math.Sqrt(variance);
        }

        public static double Cp(this List<double> values, double HighLimit, double LowLimit)
        {
            double sd = values.Count == 0 ? 0 : values.StandardDeviation(0, values.Count);
            
            return ((HighLimit-LowLimit)/(6*sd));
        }

        private static double _correction;

        public static double Cpk(this List<double> values, double HighLimit, double LowLimit)
        {
            double _target = (HighLimit + LowLimit)/2;
            
            double sd = values.Count == 0 ? 0 : values.StandardDeviation(0, values.Count);
            double mean = values.Count == 0 ? 0 : values.Mean(0, values.Count);
            double cp = values.Cp(HighLimit, LowLimit);

            double cpk_high = (HighLimit - mean) / (3 * sd);
            double cpk_low = (mean - LowLimit) / (3 * sd);

            _correction = cpk_high > cpk_low ? (_target - mean) : (mean -_target);

            _correction = cp > 1 ? _correction : 0;

            return Math.Min(cpk_low, cpk_high);
        }

        public static double CompensationCorrection(this List<double> values, double HighLimit, double LowLimit)
        {
            double cpk = values.Cpk(HighLimit, LowLimit);

            return _correction;
        }

    }
    public static class DataColumnCollectionExtensions
    {
        public static IEnumerable<DataColumn> AsEnumerable(this DataColumnCollection source)
        {
            return source.Cast<DataColumn>();
        }
    }
}
