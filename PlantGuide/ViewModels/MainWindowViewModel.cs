using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using PlantGuide.BusinessLayer;
using PlantGuide.DataLayer;
using PlantGuide.HelperMethods;
using PlantGuide.Models;

namespace PlantGuide
{
    class MainWindowViewModel
    {
        private enum OperationStatus
        {
            NONE,
            VIEW,
            ADD,
            EDIT,
            DELETE
        }

        #region COMMANDS

        public ICommand SearchPlantsListCommand
        {
            get { return new RelayCommand(OnSearchPlantsList); }
        }

        public ICommand ResetPlantsListCommand
        {
            get { return new RelayCommand(OnResetPlantsList); }
        }

        public ICommand DeletePlantCommand
        {
            get { return new RelayCommand(OnDeletePlant); }
        }

        public ICommand EditPlantCommand
        {
            get { return new RelayCommand(OnEditPlant); }
        }

        public ICommand SavePlantCommand
        {
            get { return new RelayCommand(OnSavePlant); }
        }

        public ICommand AddPlantCommand
        {
            get { return new RelayCommand(OnAddPlant); }
        }

        public ICommand CancelPlantCommand
        {
            get { return new RelayCommand(OnCancelPlant); }
        }

        public ICommand ViewPlantCommand
        {
            get { return new RelayCommand(OnViewPlant); }
        }

        public ICommand QuitApplicationCommand
        {
            get { return new RelayCommand(OnQuitApplication); }
        }

        public ICommand SortPlantsListCommand
        {
            get { return new RelayCommand(new Action<object>(OnSortPlantsList)); }
        }

        public ICommand RegionFilterPlantsListCommand
        {
            get { return new RelayCommand(OnRegionFilterPlantsList); }
        }


        #endregion

        #region ENUMS



        #endregion

        #region FIELDS

        private ObservableCollection<PlantDetail> _plants;
        private PlantDetail _selectedPlant;
        private PlantDetail _detailedViewPlant;
        private PlantBusiness _pBusiness;
        private OperationStatus _operationStatus = OperationStatus.NONE;

        private bool _isEditingAdding = false;
        private bool _showAddButton = true;

        private string _sortType;
        private string _searhText;
        private string _poisonousText;
        private string _regionText;
        private string _edibleText;
        private string minRegion;
        


        #endregion

        #region PROPERTIES

        public ObservableCollection<PlantDetail> Plants
        {
            get { return _plants; }
            set
            {
                _plants = value;
                OnPropertyChanged(nameof(Plants));
            }
        }

        public PlantDetail DetailedViewPlant
        {
            get { return _detailedViewPlant; }
            set
            {
                if (_detailedViewPlant == value)
                {
                    return;
                }
                _detailedViewPlant = value;
                OnPropertyChanged("DetailedViewPlant");
            }
        }

        public PlantDetail SelectedPlant
        {
            get { return _selectedPlant; }
            set
            {
                if (_selectedPlant == value)
                {
                    return;
                }
                _selectedPlant = value;
                OnPropertyChanged("SelectedPlant");
            }
        }

        public bool IsEditingAdding
        {
            get { return _isEditingAdding; }
            set
            {
                _isEditingAdding = value;
                OnPropertyChanged(nameof(IsEditingAdding));
            }
        }

        public bool ShowAddButton
        {
            get { return _showAddButton; }
            set
            {
                _showAddButton = value;
                OnPropertyChanged(nameof(ShowAddButton));
            }
        }

        public string SortType
        {
            get { return _sortType; }
            set { _sortType = value; }
        }

        public string SearchText
        {
            get { return _searhText; }
            set
            {
                _searhText = value;
                OnPropertyChanged(nameof(SearchText));
            }
        }

        public string PoisionousText
        {
            get { return _poisonousText; }
            set
            {
                _poisonousText = value;
                OnPropertyChanged(nameof(PoisionousText));
            }
        }

        public string EdibleText
        {
            get { return _edibleText; }
            set
            {
                _edibleText = value;
                OnPropertyChanged(nameof(EdibleText));
            }
        }

        public string RegionText
        {
            get { return _regionText; }
            set
            {
                _regionText = value;
                OnPropertyChanged(nameof(RegionText));
            }
        }

        private void OnPropertyChanged(string v)
        {

        }

        #endregion

        #region CONSTRUCTORS

        public MainWindowViewModel()
        {
            // make path for the code below


            //_pBusiness = new PlantBusiness();
            //Plants = new ObservableCollection<PlantDetail>(_pBusiness.AllPlants());

            Plants = new ObservableCollection<PlantDetail>(SeedData.GetPlants());

            UpdateImagePath();
        }

        #endregion

        #region METHODS

        private void UpdateImagePath()
        {
            foreach (var plant in _plants)
            {
                plant.ImageFilePath = DataConfig.ImagePath + plant.ImageFileName;
            }
        }

        private void OnDeletePlant()
        {
            if (_selectedPlant != null)
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show($"Are you sure you want to delete {_selectedPlant.Name}?", "Delete Plant", System.Windows.MessageBoxButton.OKCancel);

                if (messageBoxResult == MessageBoxResult.OK)
                {
                    // delete plant from persistence             
                    _pBusiness.DeletePlant(SelectedPlant.Id);

                    // remove plant from list - update view
                    _plants.Remove(_selectedPlant);
                }
            }
        }

        private void OnEditPlant()
        {
            if (_selectedPlant != null)
            {
                _operationStatus = OperationStatus.EDIT;
                IsEditingAdding = true;
                ShowAddButton = false;
                UpdateDetailedViewPlantToSelected();
            }
        }

        private void OnViewPlant()
        {
            if (_selectedPlant != null)
            {
                _operationStatus = OperationStatus.VIEW;
                UpdateDetailedViewPlantToSelected();
            }
        }

        private void OnAddPlant()
        {
            ResetDetailedViewPlant();
            IsEditingAdding = true;
            ShowAddButton = false;
            _operationStatus = OperationStatus.ADD;
        }

        private void OnSavePlant()
        {
            switch (_operationStatus)
            {
                case OperationStatus.ADD:
                    if (_detailedViewPlant != null)
                    {
                        // add plant to persistence
                        _pBusiness.AddPlant(_detailedViewPlant);
                        
                        // add plant to list - update view
                        _plants.Add(DetailedViewPlant);
                    }
                    break;
                case OperationStatus.EDIT:
                    PlantDetail plantToUpdate = _plants.FirstOrDefault(c => c.Id == SelectedPlant.Id);

                    if (plantToUpdate != null)
                    {
                        // update plant in persistence
                        _pBusiness.UpdatePlant(DetailedViewPlant);

                        // update plant in list - update view
                        _plants.Remove(plantToUpdate);
                        _plants.Add(DetailedViewPlant);
                    }
                    break;
                default:
                    break;
            }

            ResetDetailedViewPlant();
            IsEditingAdding = false;
            ShowAddButton = true;
            _operationStatus = OperationStatus.NONE;
        }

        private void OnCancelPlant()
        {
            ResetDetailedViewPlant();
            _operationStatus = OperationStatus.NONE;
            IsEditingAdding = false;
            ShowAddButton = true;
        }

        private void UpdateDetailedViewPlantToSelected()
        {
            _detailedViewPlant = new PlantDetail();
            _detailedViewPlant.Id = _selectedPlant.Id;
            _detailedViewPlant.Name = _selectedPlant.Name;
            _detailedViewPlant.Description = _selectedPlant.Description;
            _detailedViewPlant.ImageFileName = _selectedPlant.ImageFileName;
            _detailedViewPlant.ImageFilePath = _selectedPlant.ImageFilePath;
            OnPropertyChanged("DetailedViewPlant");
        }

        private void ResetDetailedViewPlant()
        {
            _detailedViewPlant = new PlantDetail();
            _detailedViewPlant.Name = "";
            _detailedViewPlant.Description = "";
            _detailedViewPlant.ImageFileName = "";
            _detailedViewPlant.ImageFilePath = "";
            OnPropertyChanged("DetailedViewPlant");
        }

        private void OnQuitApplication()
        {
            // call a house keeping method in the business class
            System.Environment.Exit(1);
        }

        private void OnSortListByPoisionous()
        {
            Plants = new ObservableCollection<PlantDetail>(_plants.OrderBy(c => c.Poisionous));
        }

        private void OnSortPlantsList(object obj)
        {
            string sortType = obj.ToString();
            switch (sortType)
            {
                case "Poisionous":
                    Plants = new ObservableCollection<PlantDetail>(Plants.OrderBy(c => c.Poisionous));
                    break;

                default:
                    break;
            }
        }

        private void OnSearchPlantsList()
        {
            // reset poisionous filters
            PoisionousText = "";
            EdibleText = "";

            // reset to full list before search
            _plants = new ObservableCollection<PlantDetail>(_pBusiness.AllPlants());
            UpdateImagePath();

            Plants = new ObservableCollection<PlantDetail>(_plants.Where(c => c.Name.ToLower().Contains(_searhText)));
        }

        // Region need to make changes to have region N, S, W, E as values

        private void OnRegionFilterPlantsList()
        {
            // reset search text box
            SearchText = "";

            if (int.TryParse(RegionText, out int minRegion) && int.TryParse(RegionText, out int maxRegion))
            {
                // reset to full list before search
                _plants = new ObservableCollection<PlantDetail>(_pBusiness.AllPlants());
                UpdateImagePath();

                Plants = new ObservableCollection<PlantDetail>(_plants.Where(c => c.Region >= minRegion && c.Region <= maxRegion));
            }
        }

        private void OnResetPlantsList()
        {
            // reset search and filter text boxes
            SearchText = "";
            PoisionousText = "";
            EdibleText = "";

            // reset to full list 
            _plants = new ObservableCollection<PlantDetail>(_pBusiness.AllPlants());
            UpdateImagePath();

            Plants = _plants;
        }

        #endregion

        #region EVENTS


        #endregion


    }
}
