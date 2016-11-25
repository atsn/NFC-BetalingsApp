using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using StatueApp.Common;
using System.Threading;
using Windows.Media.Streaming.Adaptive;
using Windows.UI.Composition;
using Windows.UI.Xaml.Controls;

namespace NFC_BetalingsApp
{
    public class StarpageWiewmodel : INotifyPropertyChanged
    {
        private string _response;
        private int _amount;
        private string _chipid;
        private string _name;
        private bool _isRunning;

        public int Total
        {
            get { return _total; }
            set { _total = value; OnPropertyChanged(); }
        }

        public string username
        {
            get { return _username; }
            set { _username = value; OnPropertyChanged(); }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged(); }
        }

        public MediaElement Chaching = new MediaElement();

        public ObservableCollection<KøbshistoriksVisninsApp> KøbsVis
        {
            get { return _købsVis; }
            set { _købsVis = value; OnPropertyChanged(); }
        }

        public ObservableCollection<KøbsHistorik> KøbsHistoriks

        {
            get { return _købsHistoriks; }
            set
            { _købsHistoriks = value; OnPropertyChanged(); }
        }


        public bool sycseeded
        {
            get { return _sycseeded; }
            set { _sycseeded = value; OnPropertyChanged(); }
        }

        public ObservableCollection<NFC_Chip> Chips
        {
            get { return _chips; }
            set { _chips = value; OnPropertyChanged(); }
        }

        public NFC_Chip selectetChip
        {
            get { return _selectetChip; }
            set { _selectetChip = value; OnPropertyChanged(); }
        }


        private NFC_Chip _selectetChip;
        private ObservableCollection<NFC_Chip> _chips;
        private bool _sycseeded;
        private ObservableCollection<KøbsHistorik> _købsHistoriks;
        private ObservableCollection<KøbshistoriksVisninsApp> _købsVis;
        private string _username;
        private string _password;
        private int _total;


        public RelayCommand addcommand { get; }
        public RelayCommand paycommand { get; }
        public RelayCommand AddchipCommand { get; }
        public RelayCommand GetAllChipsCommand { get; }
        public RelayCommand DeleteChipCommand { get; }
        public RelayCommand Getkøbshistorik { get; }


        public StarpageWiewmodel()
        {
            Chips = new ObservableCollection<NFC_Chip>();
            KøbsHistoriks = new ObservableCollection<KøbsHistorik>();
            addcommand = new RelayCommand(callADD);
            paycommand = new RelayCommand(callpay);
            AddchipCommand = new RelayCommand(calladdchip);
            GetAllChipsCommand = new RelayCommand(callGetAllChips);
            DeleteChipCommand = new RelayCommand(callDeleteChip);
            KøbsVis = new ObservableCollection<KøbshistoriksVisninsApp>();
            Getkøbshistorik = new RelayCommand(callGetKøbhistorik);
        }



        public string response
        {
            get { return _response; }
            set { _response = value; OnPropertyChanged(); }
        }

        public int amount
        {
            get { return _amount; }
            set { _amount = value; OnPropertyChanged(); }
        }

        public string chipid
        {
            get { return _chipid; }
            set { _chipid = value; OnPropertyChanged(); }
        }

        public string name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(); }
        }
        public bool IsRunning
        {
            get { return _isRunning; }
            set { _isRunning = value; OnPropertyChanged(); }
        }

        public async void callpay()
        {

            IsRunning = true;

            if (chipid != null)
            {
                response = await Handler.Pay(chipid, amount);
            }



            else
            {
                response = "intast venligtst chipid";
            }

            if (response.ToLower().Contains("betalt"))
            {
                sycseeded = true;
            }

            IsRunning = false;
        }

        public async void callADD()
        {
            IsRunning = true;
            if (chipid != null)
            {
                response = await Handler.ADDMoney(chipid, amount);

            }

            else response = "intast venligtst chipid";
            IsRunning = false;
        }

        public async void calladdchip()
        {
            IsRunning = true;
            if (chipid != null)
            {
                response = await Handler.AddChip(new NFC_Chip(chipid, name));
            }

            else response = "intast venligtst chipid";
            IsRunning = false;
        }

        public async void callGetAllChips()
        {
            IsRunning = true;
            Chips = await Handler.GetAllChips();
            IsRunning = false;
        }
        public async void callDeleteChip()
        {
            IsRunning = true;

            if (selectetChip != null)
            {
                response = await Handler.DeleteChip(selectetChip);
                callGetAllChips();
            }
            else
            {
                response = "Intet er valgt";
            }


            IsRunning = false;


        }

        public async void callGetKøbhistorik()
        {
            IsRunning = true;
            Chips = await Handler.GetAllChips();
            KøbsHistoriks = await Handler.GetKøbshistorik();


            var Liste = from h in KøbsHistoriks join C in Chips on h.Fk_Chipid equals C.Chipid select new { h, C.Name };
            Total = 0;

            foreach (var VARIABLE in Liste)
            {
                KøbsVis.Add(new KøbshistoriksVisninsApp(VARIABLE.h.Id, VARIABLE.h.Amount, VARIABLE.h.Fk_Chipid,
                    VARIABLE.Name));
                if (VARIABLE.Name != "Underviser")
                {
                    Total = Total + VARIABLE.h.Amount;
                }
            }


            IsRunning = false;
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
