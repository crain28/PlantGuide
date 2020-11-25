using PlantGuide.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantGuide.DataLayer
{
    public class DataConfig
    {
        // set the type of persistence        
        public static DataType dataType = DataType.JSON;

        public static string DataPathJson => @"DataLayer\DataJson\PlantsInformation.json";
        public static string ImagePath => @"\DataALayer\Images\";

    }
}
