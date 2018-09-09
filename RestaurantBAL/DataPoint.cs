using System;
using System.Runtime.Serialization;

namespace RestaurantBAL
{
    [DataContract]
    public class DataPoint
    {
        public DataPoint(double y, string label)
        {
            try { 
            this.Y = y;
            this.Label = label;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public DataPoint(double x, double y)
        {
            try { 
            this.X = x;
            this.Y = y;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public DataPoint(double x, double y, string label)
        {
            try { 
            this.X = x;
            this.Y = y;
            this.Label = label;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public DataPoint(double x, double y, double z)
        {
            try { 
            this.X = x;
            this.Y = y;
            this.Z = z;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public DataPoint(double x, double y, double z, string label)
        {
            try { 
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.Label = label;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        //Explicitly setting the name to be used while serializing to JSON. 
        [DataMember(Name = "label")]
        public string Label = null;

        //Explicitly setting the name to be used while serializing to JSON.
        [DataMember(Name = "y")]
        public Nullable<double> Y = null;

        //Explicitly setting the name to be used while serializing to JSON.
        [DataMember(Name = "x")]
        public Nullable<double> X = null;

        //Explicitly setting the name to be used while serializing to JSON.
        [DataMember(Name = "z")]
        public Nullable<double> Z = null;
    }
}