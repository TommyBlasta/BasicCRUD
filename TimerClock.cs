using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicCRUD
{
    class TimerClock: INotifyPropertyChanged
    {
        private bool IsCounting { get; set; } = false;
        public DateTime StartTime { get; private set; } = DateTime.MinValue;
        public DateTime StopTime { get; private set; } = DateTime.MinValue;
        public string Duration
        {
            get
            {
                if(IsCounting)
                {
                    return (DateTime.Now - StartTime).ToString();
                }
                else
                {
                    return (StopTime - StartTime).ToString();
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName="")
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        public void Start()
        {
            IsCounting = true;
            StartTime = DateTime.Now;
        }
        public void Stop()
        {
            StopTime = DateTime.Now;
            IsCounting = false;
        }
    }
}
