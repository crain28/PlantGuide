using PlantGuide.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantGuide.BusinessLayer
{
    class PlantBusiness
    {
        public FileIoMessage FileIoStatus { get; set; }

        public PlantBusiness()
        {


        }

        /// <summary>
        /// retrieve a plant using the repository
        /// </summary>
        /// <returns>plant</returns>
        private PlantDetail GetPlant(int id)
        {
            PlantDetail plant = null;
            FileIoStatus = FileIoMessage.None;

            try
            {
                using (PlantRepository pRepository = new PlantRepository())
                {
                    plant = pRepository.GetById(id);
                };

                if (plant != null)
                {
                    FileIoStatus = FileIoMessage.Complete;
                }
                else
                {
                    FileIoStatus = FileIoMessage.RecordNotFound;
                }
            }
            catch (Exception)
            {
                FileIoStatus = FileIoMessage.FileAccessError;
            }

            return plant;
        }

        /// <summary>
        /// retrieve a list of all plants using the repository
        /// </summary>
        /// <returns>all plants</returns>
        private List<PlantDetail> GetAllPlants()
        {
            List<PlantDetail> plants = null;
            FileIoStatus = FileIoMessage.None;

            try
            {
                using (PlantRepository pRepository = new PlantRepository())
                {
                    plants = pRepository.GetAll() as List<PlantDetail>;
                };

                if (plants != null)
                {
                    FileIoStatus = FileIoMessage.Complete;
                }
                else
                {
                    FileIoStatus = FileIoMessage.NoRecordsFound;
                }
            }
            catch (Exception)
            {
                FileIoStatus = FileIoMessage.FileAccessError;
            }

            return plants;
        }

        /// <summary>
        /// provide a list of all plants
        /// </summary>
        /// <returns>list of all plants</returns>
        public List<PlantDetail> AllPlants()
        {
            return GetAllPlants() as List<PlantDetail>;
        }

        /// <summary>
        /// retrieve a plant by id 
        /// </summary>
        /// <param name="id">plant id</param>
        /// <returns>plant</returns>
        public PlantDetail PlantsById(int id)
        {
            return GetPlant(id);
        }

        /// <summary>
        /// add a new plant
        /// </summary>
        /// <param name="plant">plant to add</param>
        public void AddPlant(PlantDetail plant)
        {
            try
            {
                if (plant != null)
                {
                    using (PlantRepository pRepository = new PlantRepository())
                    {
                        pRepository.Add(plant);
                    };

                    FileIoStatus = FileIoMessage.Complete;
                }
            }
            catch (Exception)
            {
                FileIoStatus = FileIoMessage.FileAccessError;
            }
        }

        /// <summary>
        /// retrieve a plant by id 
        /// </summary>
        /// <param name="id">plant id</param>
        public void DeletePlant(int id)
        {
            try
            {
                if (GetPlant(id) != null)
                {
                    using (PlantRepository repo = new PlantRepository())
                    {
                        repo.Delete(id);
                    }

                    FileIoStatus = FileIoMessage.Complete;
                }
                else
                {
                    FileIoStatus = FileIoMessage.RecordNotFound;
                }
            }
            catch (Exception)
            {
                FileIoStatus = FileIoMessage.FileAccessError;
            }
        }

        public void UpdatePlant(PlantDetail updatedPlant)
        {
            try
            {
                if (GetPlant(updatedPlant.Id) != null)
                {
                    using (PlantRepository repo = new PlantRepository())
                    {
                        repo.Update(updatedPlant);
                    }

                    FileIoStatus = FileIoMessage.Complete;
                }
                else
                {
                    FileIoStatus = FileIoMessage.RecordNotFound;
                }
            }
            catch (Exception)
            {
                FileIoStatus = FileIoMessage.FileAccessError;
            }
        }

    }
}
