using PlantGuide.HelperMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantGuide.Models
{
    public class PlantDetail : ObservableObject
    {
        #region ENUMS



        #endregion

        #region FIELDS

        private int _id;
        private string _name;
        private string _imageFileName;
        private string _imageFilePath;
        private string _description;
        private string _uses;
        private string _region;
        private string _poisonous;
        private string _edible;


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

        public string Uses 
        {
            get { return _uses; }
            set { _uses = value;
                OnPropertyChanged(nameof(Uses));
            } 
        }

        public string Region
        {
            get { return _region; }
            set
            {
                _region = value;
                OnPropertyChanged(nameof(Region));
            }
        }

        public string Poisonous
        {
            get { return _poisonous; }
            set
            {
                _poisonous = value;
                OnPropertyChanged(nameof(Poisonous));
            }
        }

        public string Edible
        {
            get { return _edible; }
            set
            {
                _edible = value;
                OnPropertyChanged(nameof(Edible));
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
