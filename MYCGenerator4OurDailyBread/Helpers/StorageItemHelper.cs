using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace MYCGenerator4OurDailyBread.Helpers
{
    public static class StorageItemHelper
    {
        /// <summary>
        /// It is based on StorageFolder.CreateFolderAsync; however, I force it to return null if some exceptions happened.
        /// </summary>
        /// <param name="sItem"></param>
        /// <param name="stMYCont"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public static async Task<StorageFolder> MyCreateFolderAsync(this StorageFolder sItem,string stMYCont,CreationCollisionOption option = CreationCollisionOption.OpenIfExists)
        {
            StorageFolder folder = null;
            try
            {
                folder = await sItem?.CreateFolderAsync(stMYCont, option);
            }
            catch (Exception)
            {
                return null;
            }

            return folder;
        }

        public static async Task<StorageFile> MyCreateFileAsync(this StorageFolder sItem, string fileName, CreationCollisionOption option = CreationCollisionOption.OpenIfExists)
        {
            StorageFile file = null;
            try
            {
                file = await sItem?.CreateFileAsync(fileName, option);
            }
            catch (Exception)
            {
                return null;
            }

            return file;
        }

        internal static async Task<bool> WriteATextFileAsync(StorageFolder folder,string fileName, string stContent)
        {
            var file = await MyCreateFileAsync(folder, fileName);
            if (file == null)
                return false;
            try
            {
                await FileIO.WriteTextAsync(file, stContent);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
