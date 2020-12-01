using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlantGuide.Models;

namespace PlantGuide.BusinessLayer
{
    public interface IPlantRepository
    {
        // interfaces are public by default so any class that implements the interface has to have the same methods as public.
        IEnumerable<PlantDetail> GetAll();
        
        PlantDetail GetById(int id);

        void Add(PlantDetail plant);

        void Update(PlantDetail plant);

        void Delete(int id);
    }
}

