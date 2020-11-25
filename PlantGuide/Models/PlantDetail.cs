using PlantGuide.HelperMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantGuide.Models
{
    class PlantDetail : ObservableObject
    {
        #region ENUMS



        #endregion

        #region FIELDS

        private int _id;
        private string _name;
        private string _imageFileName;
        private string _imageFilePath;
        private string _description;


        #endregion

        #region PROPERTIES

        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
                OnPropertyChanged(nameof(Name));
            }
        }


        public string ImageFileName
        {
            get { return _imageFileName; }
            set
            {
                _imageFileName = value;
                OnPropertyChanged(nameof(ImageFileName));
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        public string ImageFilePath
        {
            get { return _imageFilePath; }
            set
            {
                _imageFilePath = value;
                OnPropertyChanged(nameof(ImageFilePath));
            }
        }

        #endregion

        #region CONSTRUCTORS

         

        #endregion

        #region METHODS


        #endregion

        #region EVENTS



        #endregion

    }
}
