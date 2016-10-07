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
        private int _chipid;
        private string _name;
        private bool _isRunning;
        public MediaElement Chaching = new MediaElement();


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


        public RelayCommand addcommand { get; }
        public RelayCommand paycommand { get; }
        public RelayCommand AddchipCommand { get; }
        public RelayCommand GetAllChipsCommand { get; }
        public RelayCommand DeleteChipCommand { get; }

        public StarpageWiewmodel()
        {
            Chips = new ObservableCollection<NFC_Chip>();
            addcommand = new RelayCommand(callADD);
            paycommand = new RelayCommand(callpay);
            AddchipCommand = new RelayCommand(calladdchip);
            GetAllChipsCommand = new RelayCommand(callGetAllChips);
            DeleteChipCommand = new RelayCommand(callDeleteChip);

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

        public int chipid
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
            response = await Handler.Pay(chipid, amount);
            IsRunning = false;

            if (response.ToLower().Contains("betalt"))
            {

                sycseeded = true;
            }
        }

        public async void callADD()
        {
            IsRunning = true;
            response = await Handler.ADDMoney(chipid, amount);
            IsRunning = false;
        }

        public async void calladdchip()
        {
            IsRunning = true;
            response = await Handler.AddChip(new NFC_Chip(chipid, name));
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


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
