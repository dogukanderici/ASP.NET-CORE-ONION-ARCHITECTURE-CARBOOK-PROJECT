using CarBook.Dto.CarDtos;
using CarBook.Dto.FeatureDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Dto.CarFeatureDtos
{
    public class ResultCarFeatureDto
    {
        public int CarFeatureID { get; set; }
        public int CarID { get; set; }
        public ResultCarDto Car { get; set; }
        public int FeatureID { get; set; }
        public ResultFeatureDto Feature { get; set; }
        public bool Available { get; set; }
    }
}
