using PlantGuide.DataLayer;
using PlantGuide.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantGuide.BusinessLayer
{    
    /// <summary>
    /// Repository for CRUD
    /// Note: the _dataService object is instantiated with either the XML or Json class based on the value set in the DataConfig class
    /// </summary>
   class PlantRepository : IPlantRepository, IDisposable
    {
        private IDataService _dataService;
        List<PlantDetail> _plants;

        /// <summary>
        /// set the correct data service Json data service and read in a list of all plants
        /// </summary>
        public PlantRepository()
        {
            _dataService = SetDataService();

            try
            {
                _plants = _dataService.ReadAll() as List<PlantDetail>;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// instantiate and return the correct data service Json
        /// </summary>
        /// <returns>data service object</returns>
        private IDataService SetDataService()
        {
            switch (DataConfig.dataType)
            {              
                case DataType.JSON:
                    return new DataServiceJson();

                default:
                    throw new Exception();
            }
        }

        /// <summary>
        /// retrieve all plants
        /// </summary>
        /// <returns>all plants</returns>
        public IEnumerable<PlantDetail> GetAll()
        {
            return _plants;
        }

        /// <summary>
        /// retrieve a plant by the id
        /// </summary>
        /// <param name="id">plant id</param>
        /// <returns></returns>
        public PlantDetail GetById(int id)
        {
            return _plants.FirstOrDefault(c => c.Id == id);
        }

        /// <summary>
        /// add a new plant
        /// </summary>
        /// <param name="plant">plant</param>
        public void Add(PlantDetail plants)
        {
            try
            {
                plants.Id = NextId();
                _plants.Add(plants);
                _dataService.WriteAll(_plants);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// delete a plant
        /// </summary>
        /// <param name="id">plant id</param>
        public void Delete(int id)
        {
            try
            {
                _plants.Remove(_plants.FirstOrDefault(c => c.Id == id));

                _dataService.WriteAll(_plants);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// update a plant
        /// </summary>
        /// <param name="plant">plant</param>
        public void Update(PlantDetail plants)
        {
            try
            {
                _plants.Remove(_plants.FirstOrDefault(c => c.Id == plants.Id));
                _plants.Add(plants);

                _dataService.WriteAll(_plants);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// get the next id value
        /// </summary>
        /// <returns>next id value</returns>
        private int NextId()
        {
            // get maximum id number and return incremented value
            return ++_plants.OrderByDescending(c => c.Id).First().Id;
        }

        /// <summary>
        /// required if class will be use in a 'using" block
        /// </summary>
        public void Dispose()
        {
            _dataService = null;
            _plants = null;
        }
    }
}