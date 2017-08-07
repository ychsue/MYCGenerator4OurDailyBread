using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace MYCGenerator4OurDailyBread.Helpers
{
    public class MRUHelper
    {
        /// <summary>
        /// It returns folder's token
        /// </summary>
        /// <param name="folder"></param>
        /// <returns>folder's token</returns>
        public static string AddAFolderIntoMRU(StorageFolder folder)
        {
            if (folder == null)
                return "";
            var mru = Windows.Storage.AccessCache.StorageApplicationPermissions.MostRecentlyUsedList;
            return mru.Add(folder);
        }

        public static void RemoveAFolderFromMRU(string token)
        {
            var mru = Windows.Storage.AccessCache.StorageApplicationPermissions.MostRecentlyUsedList;
            mru.Remove(token);
        }

        internal static async Task<StorageFolder> GetAFolderBackAsync(string v)
        {
            if (v != null && v != "")
                try
                {
                    return await Windows.Storage.AccessCache.StorageApplicationPermissions.MostRecentlyUsedList.GetFolderAsync(v);
                }
                catch (Exception)
                {
                    ErrorHelper.ShowErrorMsg(ErrorHelper.ErrorCode.CannotGetTheFolder, "From MRUHelper.GetAFolderBackAsync:: ");
                    return null;
                }
            else
                return null;
        }
    }
}
