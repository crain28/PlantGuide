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
        // interfaces are public by default so any class that implements the interface has to have the same methods as public.
        IEnumerable<PlantDetail> ReadAll();

        void WriteAll(IEnumerable<PlantDetail> plants);
    }
}
