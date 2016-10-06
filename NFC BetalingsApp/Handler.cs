using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using System.Collections.ObjectModel;

namespace NFC_BetalingsApp
{
    class Handler
    {

        public static async Task<string> Pay(int Chipid, int amount)
        {
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
                if (chip.Chipid == Chipid)
                {
                    if (chip.Konto >= amount)
                    {
                        chip.Konto = chip.Konto - amount;
                        try
                        {
                            await Facade.PutAsync(chip, chip.Chipid);
                        }
                        catch (Exception e)
                        {
                            var messageBox = new MessageDialog("Penge Trukket lokalt men ikke i databaseb " + e.Message);
                            await messageBox.ShowAsync();
                            return null;

                        }

                        return "Betalt: " + amount;
                    }

                    return "Ikke nok penge penge på chippen: " + chip.Konto;

                }
            }
            return "Chippen findes ikke";
        }

        public static async Task<string> ADDMoney(int Chipid, int amount)
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
                    return "Penge tilføjet: " + amount +" total: " + chip.Konto;
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
    }
}
