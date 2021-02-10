using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using MVVMDemo.Models;
using MVVMDemo.Commands;
using System.Collections.ObjectModel;

namespace MVVMDemo.ViewModels
{
    public class RobotViewModel: INotifyPropertyChanged
    {
        #region INotifyPropertyChanged_Implementation
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        RobotService ObjRobotService;
        public RobotViewModel()
        {
            ObjRobotService = new RobotService();
            LoadData();
            CurrentRobot = new Robot();
            saveCommand = new RelayCommand(Save);
            searchCommand = new RelayCommand(Search);
            updateCommand = new RelayCommand(Update);
            deleteCommand = new RelayCommand(Delete);
        }

        #region DisplayOperation
        private ObservableCollection<Robot> robotsList;

            public ObservableCollection<Robot> RobotsList
        {
            get { return robotsList; }
            set { robotsList = value; OnPropertyChanged("RobotsList"); }
        }

       /* private ObservableCollection<Robot> robotsList;
        public ObservableCollection<Robot> RobotsList
        {
            get { return robotsList; }
            set { robotsList = value; OnPropertyChanged("RobotsList"); }

        }*/
        private void LoadData()
        {
            RobotsList =new ObservableCollection<Robot>(ObjRobotService.GetAll());
        }
        #endregion

        private Robot currentRobot;

        public Robot CurrentRobot
        {
            get { return currentRobot; }
            set { currentRobot = value; OnPropertyChanged("CurrentRobot"); }
        }

        private string message;

        public string Message
        {
            get { return message; }
            set { message = value;OnPropertyChanged("Message"); }
        }

        #region SaveOperation
        private RelayCommand saveCommand;

        public RelayCommand SaveCommand
        {
            get { return saveCommand; }
            
        }

        public void Save()
        {
            try
            {
                var IsSaved = ObjRobotService.Add(CurrentRobot);
                LoadData();
                if (IsSaved)
                {
                    Message = "Robot Saved";
                }
                else
                {
                    Message = "Save operation failed";
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
              
            }
        }
        #endregion

        #region SearchOperation

        private RelayCommand searchCommand;

        public RelayCommand SearchCommand
        {
            get { return searchCommand; }
           
        }
        public void Search()
        {
            try
            {
                var ObjRobot = ObjRobotService.Search(CurrentRobot.SerialNo);
                if (ObjRobot != null)
                {
                    CurrentRobot.Parameter1 = ObjRobot.Parameter1;
                    CurrentRobot.Parameter2 = ObjRobot.Parameter2;
                    CurrentRobot.Parameter3 = ObjRobot.Parameter3;
                    CurrentRobot.Parameter4 = ObjRobot.Parameter4;
                    CurrentRobot.Parameter5 = ObjRobot.Parameter5;
                }
                else
                {
                    Message = "Robot Not Found";
                }
            }
            catch (Exception ex)
            {

                Message = ex.Message;
            }
        }
        #endregion

        #region UpdateOperation

        private RelayCommand updateCommand;

        public RelayCommand UpdateCommand
        {
            get { return updateCommand; }
            
        }
        public void Update()
        {
            try
            {
                var IsUpdated = ObjRobotService.Update(CurrentRobot);
                if (IsUpdated)
                {
                    Message = "Robot Updated";
                    LoadData();

                }
                else
                {
                    Message = "Update Operation Failed";
                }
            }
            catch (Exception ex)
            {

                Message = ex.Message;
            }
        }
        #endregion

        private RelayCommand deleteCommand;

        public RelayCommand DeleteCommand
        {
            get { return deleteCommand; }
           
        }
        public void Delete()
        {
            try
            {
                var IsDeleted = ObjRobotService.Delete(CurrentRobot.SerialNo);
                if ((bool)IsDeleted)
                {
                    Message = "Robot Is Deleted";
                    LoadData();

                }
                else
                {
                    Message = "Delete Operation Failed";
                }

            }
            catch (Exception ex)
            {

                Message = ex.Message;
            }
        }


    }
}
