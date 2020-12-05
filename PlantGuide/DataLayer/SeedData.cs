using PlantGuide.Models;
using System.Collections.Generic;

namespace PlantGuide.DataLayer
{
    public static class SeedData
    {
        public static List<PlantDetail> GetPlants()
        {
            return new List<PlantDetail>()
            {
                new PlantDetail()
                {
                    Id = 1,
                    Name = "Mint",
                    ImageFileName = "Mint.jpg",
                    Description = "Is a edible plant that taste like peppermint, mainly found in wet swampy areas."
                },

                new PlantDetail()
                {
                    Id = 2,
                    Name = "Thisle",
                    Uses = "Edible",
                    ImageFileName = "Thistle.jpg",
                    Description = "The Flower of the thiste plant is edible. you can cook it a while to make the thorns brittle so they fall off"
                },
                new PlantDetail()
                {
                    Id = 3,
                    Name = "Cattail",
                    Uses = "Edible",
                    ImageFileName = "Cattail.jpg",
                    Description = "Everything apart of this plant are edible make sure it is washed be for consummig."
                }


            };
        }
    }
}
