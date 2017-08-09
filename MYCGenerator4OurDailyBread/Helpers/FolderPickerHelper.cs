using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;

namespace MYCGenerator4OurDailyBread.Helpers
{
    public class FolderPickerHelper
    {
        public static async Task<StorageFolder> GetAFolderAsync(string commitTxt="")
        {
            StorageFolder folder=null;
            var picker = new FolderPicker();
            if (commitTxt != "")
                picker.CommitButtonText = commitTxt;
            picker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            picker.FileTypeFilter.Add(".json");
            folder = await picker.PickSingleFolderAsync();

            return folder;
        }
    }
}
