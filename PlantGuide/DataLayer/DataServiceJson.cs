using Newtonsoft.Json;
using PlantGuide.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantGuide.DataLayer
{
    class DataServiceJson : IDataService
    {
        private string _dataFilePath;

        /// <summary>
        /// read the json file and load a list of plant objects
        /// </summary>
        /// <returns>list of plants</returns>
        public IEnumerable<PlantDetail> ReadAll()
        {
            List<PlantDetail> plants;

            try
            {
                using (StreamReader sr = new StreamReader(_dataFilePath))
                {
                    string jsonString = sr.ReadToEnd();

                    plants = JsonConvert.DeserializeObject<List<PlantDetail>>(jsonString);
                }
            }
            catch (Exception e)
            {
                var errorMessage = e.Message;
                throw;
            }

            return plants;
        }

        /// <summary>
        /// write the current list of plants to the json data file
        /// </summary>
        /// <param name="plants">list of plants</param>
        public void WriteAll(IEnumerable<PlantDetail> plants)
        {
            string jsonString = JsonConvert.SerializeObject(plants, Formatting.Indented);

            try
            {
                StreamWriter writer = new StreamWriter(_dataFilePath);
                using (writer)
                {
                    writer.WriteLine(jsonString);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataServiceJson()
        {
            _dataFilePath = DataConfig.DataPathJson;
        }
    }
}
