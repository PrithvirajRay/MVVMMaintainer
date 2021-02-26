using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace MVVMDemo.Models
{   

    public class Robot : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private int serialNo;
        public int SerialNo
        {
            get { return serialNo; }
            set { serialNo = value; OnPropertyChanged("SerialNo"); }
        }

        private double parameter1;

        public double Parameter1
        {
            get { return parameter1; }
            set { parameter1 = value; OnPropertyChanged("Parameter1"); }
        }

        private double parameter2;

        public double Parameter2
        {
            get { return parameter2; }
            set { parameter2 = value; OnPropertyChanged("Parameter2"); }
        }

        private double parameter3;

        public double Parameter3
        {
            get { return parameter3; }
            set { parameter3 = value; OnPropertyChanged("Parameter3"); }
        }

        private double parameter4;

        public double Parameter4
        {
            get { return parameter4; }
            set { parameter4 = value; OnPropertyChanged("Parameter4"); }
        }

        private double parameter5;

        public double Parameter5
        {
            get { return parameter5; }
            set { parameter5 = value; OnPropertyChanged("Parameter5"); }
        }



    }
}
