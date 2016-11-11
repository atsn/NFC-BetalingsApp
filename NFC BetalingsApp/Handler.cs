using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using System.Collections.ObjectModel;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace NFC_BetalingsApp
{
    class Handler
    {

        public static async Task<string> Pay(string Chipid, int amount)
        {
            KøbsHistorik købs = new KøbsHistorik();

            købs.Fk_Chipid = Chipid;
            købs.Amount = amount;
            
            IEnumerable<NFC_Chip> Chips = null;
            try
            {
                Chips = await Facade.GetListAsync(new NFC_Chip());
            }
            catch (Exception e)
            {

                var messageBox = new MessageDialog("Penge ikke Trukket " + e.Message);
                await messageBox.ShowAsync();
                return null;
            }


            foreach (var chip in Chips)
            {
                if (String.Equals(chip.Chipid, Chipid, StringComparison.Ordinal))
                {
                    if (chip.Konto >= amount && amount>=0)
                    {
                        chip.Konto = chip.Konto - amount;
                        try
                        {
                            await Facade.PutAsync(chip, chip.Chipid);
                            await Facade.PostAsync(købs);
                        }
                        catch (Exception e)
                        {
                            var messageBox = new MessageDialog("Penge Trukket lokalt men ikke i databaseb " + e.Message);
                            await messageBox.ShowAsync();
                            return null;

                        }

                        ////Ikke Virkende Music
                        //MediaElement PlayMusic = new MediaElement();
                        //PlayMusic.AudioCategory = Windows.UI.Xaml.Media.AudioCategory.Media;

                        //StorageFolder Folder = Windows.ApplicationModel.Package.Current.InstalledLocation;
                        //Folder = await Folder.GetFolderAsync(@"C:\Users\Ander\Documents\MEGA\Visual Studio Apps\Små Projector\NFC BetalingsApp\NFC BetalingsApp\Assets");
                        //StorageFile sf = await Folder.GetFileAsync("Cha - Ching.mp3");
                        //PlayMusic.SetSource(await sf.OpenAsync(FileAccessMode.Read), sf.ContentType);
                        //PlayMusic.Play();

                        return "Betalt: " + amount + "Konto: " + chip.Konto;

                    }

                    else
                    {
                        return "Ikke nok penge penge på chippen: " + chip.Konto;
                    }
                    

                }
               
            }
            return "Chippen findes ikke";
        }

        public static async Task<string> ADDMoney(string Chipid, int amount)
        {
            IEnumerable<NFC_Chip> Chips = null;
            try
            {
                Chips = await Facade.GetListAsync(new NFC_Chip());
            }
            catch (Exception e)
            {

                var messageBox = new MessageDialog("Kan ikke forbinde til Databasen" + e.Message);
                await messageBox.ShowAsync();
                return null;
            }


            foreach (var chip in Chips)
            {
                if (chip.Chipid == Chipid)
                {
                    chip.Konto = chip.Konto + amount;
                    try
                    {
                        await Facade.PutAsync(chip, chip.Chipid);
                    }
                    catch (Exception e)
                    {
                        var messageBox = new MessageDialog("Penge tilføjet lokalt men ikke i databaseb" + e.Message);
                        await messageBox.ShowAsync();
                        return null;

                    }
                    return "Penge tilføjet: " + amount + " total: " + chip.Konto;
                }
            }
            return "Chippen findes ikke";
        }

        public static async Task<string> AddChip(NFC_Chip chip)
        {
            IEnumerable<NFC_Chip> Chips = null;

            try
            {
                Chips = await Facade.GetListAsync(new NFC_Chip());
            }
            catch (Exception e)
            {

                var messageBox = new MessageDialog("Kan ikke forbinde til Databasen" + e.Message);
                await messageBox.ShowAsync();
                return null;
            }

            foreach (var nfcChip in Chips)
            {
                if (nfcChip.Chipid == chip.Chipid) return "Chippen Findes Allerede";
            }
            try
            {
                await Facade.PostAsync(chip);
            }
            catch (Exception e)
            {

                var messageBox = new MessageDialog("Chippen kan ikke gemmes i Databasen" + e.Message);
                await messageBox.ShowAsync();
                return null;
            }
            return "Chippen gemt i databasen";

        }

        public static async Task<ObservableCollection<NFC_Chip>> GetAllChips()
        {
            try
            {
                var Chips = await Facade.GetListAsync(new NFC_Chip());

                ObservableCollection<NFC_Chip> returnchips = new ObservableCollection<NFC_Chip>();
                foreach (var chip in Chips)
                {
                    chip.Name = chip.Name.Trim();
                    chip.Chipid = chip.Chipid.Trim();
                    returnchips.Add(chip);
                }

                return returnchips;
            }
            catch (Exception e)
            {

                var messageBox = new MessageDialog("Kan ikke forbinde til Databasen" + e.Message);
                await messageBox.ShowAsync();
                return null;
            }
        }

        public static async Task<string> DeleteChip(NFC_Chip chip)
        {
            IEnumerable<NFC_Chip> Chips = null;


            try
            {
                var købshstorik = await Facade.GetListAsync(new KøbsHistorik());
                foreach (var købsHistorik in købshstorik)
                {
                    if (købsHistorik.Fk_Chipid == chip.Chipid) await Facade.DeleteWithIntAsync(købsHistorik, købsHistorik.Id);
                }
                await Facade.DeleteAsync(chip, chip.Chipid);

            }
            catch (Exception e)
            {

                var messageBox = new MessageDialog("Chippen kan ikke slettes i Databasen" + e.Message);
                await messageBox.ShowAsync();
                return null;
            }
            return "Chippen slettet i databasen";

        }

        public static async Task<ObservableCollection<KøbsHistorik>> GetKøbshistorik()
        {
            try
            {
                var Chips = await Facade.GetListAsync(new KøbsHistorik());

                ObservableCollection<KøbsHistorik> returnchips = new ObservableCollection<KøbsHistorik>();
                foreach (var chip in Chips)
                {
                    chip.Fk_Chipid = chip.Fk_Chipid.Trim();
                    returnchips.Add(chip);
                }

                return returnchips;
            }
            catch (Exception e)
            {

                var messageBox = new MessageDialog("Kan ikke forbinde til Databasen" + e.Message);
                await messageBox.ShowAsync();
                return null;
            }
        }


    }
}
