using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiPlatform.Domain.Models.Ui.Map
{
    public enum PushPinType { A, B, C, D }
    public class PushpinModel
    {
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public PushPinType Type { get; set; }
    }
}
