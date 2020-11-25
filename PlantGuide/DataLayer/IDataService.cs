using PlantGuide.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantGuide.DataLayer
{
    public interface IDataService
    {
        IEnumerable<PlantDetail> ReadAll();
        void WriteAll(IEnumerable<PlantDetail> plants);
    }
}
