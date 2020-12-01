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

        public void Add(PlantDetail plant);

        public void Update(PlantDetail plant);

        public void Delete(int id);
    }
}

