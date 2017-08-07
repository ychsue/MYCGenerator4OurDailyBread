using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MYCGenerator.ViewModels;
using Windows.Storage;
using System.Net;
using Windows.Data.Json;
using MemorizeYC.ViewModels.DescriptFiles;
using MemorizeYC.ViewModels;
using Windows.UI.Popups;
using Windows.System;

namespace MYCGenerator4OurDailyBread.Helpers
{
    public class MYContainerHelper
    {
        internal static async Task CreateAContainer(StorageFolder folder, VM1OurDailyBread ourDailyBread)
        {
            if(ourDailyBread.title[0].Content=="" || ourDailyBread.title[0].Answer == "")
            {
                ErrorHelper.ShowErrorMsg(ErrorHelper.ErrorCode.MainPageLinkFail, "MUContainerHelper:CreateAContainer:: ");
                return;
            }

            var theDate = ourDailyBread.theDate;
            string stMYCont = theDate.Year.ToString("0000") +
                theDate.Month.ToString("00") + " " +
                "Our Daily Bread"; //TODO
            stMYCont = WebUtility.HtmlEncode(stMYCont);
            string stMYCat = theDate.Year.ToString("0000") +
                theDate.Month.ToString("00") +
                theDate.Day.ToString("00") + " " +
                ourDailyBread.title[0].Content + " "+
                ourDailyBread.title[0].Answer;
            stMYCat = WebUtility.HtmlEncode(stMYCat);

            //* [2017-08-04 17:21] Get or create the folder for MYContainer
            StorageFolder MYContainer = await folder?.MyCreateFolderAsync(stMYCont,CreationCollisionOption.OpenIfExists);
            if (MYContainer == null)
            {
                ErrorHelper.ShowErrorMsg(ErrorHelper.ErrorCode.CannotCreateTheFolder,"MYContainerHelper:"+stMYCont+":: ");
                return;
            }

            //* [2017-08-04 17:21] Check whether the MYCategory does exist
            StorageFolder MYCategory = await MYContainer.TryGetItemAsync(stMYCat) as StorageFolder;
            if(MYCategory != null)
            {
                ErrorHelper.ShowErrorMsg(ErrorHelper.ErrorCode.TheFolderDoesExist, "MYContainerHelper:" + stMYCat + ":: ");
                return;
            }
            else
            {
                MYCategory = await MYContainer.MyCreateFolderAsync(stMYCat, CreationCollisionOption.OpenIfExists);
            }
            if (MYContainer == null)
            {
                ErrorHelper.ShowErrorMsg(ErrorHelper.ErrorCode.CannotCreateTheFolder, "MYContainerHelper:" + stMYCat + ":: ");
                return;
            }

            //* [2017-08-04 17:41] Generate MYCategory.json data and create files.
            var JsonMYCategory = new JsonObject();
            //** [2017-08-05 08:52] Set the default ones
            ForPlayPageSettings mySettings = new ForPlayPageSettings() {
                Link = ourDailyBread.pageURL[0].Answer,
                numWCardShown = 100,
                IsDictateTextContentInHint = true,
                IsShownAsList = true,
                SynLang = ourDailyBread.Language[0].Answer,
                ContentSynLang = ourDailyBread.Language[0].Content
            };
            MYCategoryJson.UpdateMYCategoryJsonByMySettings(JsonMYCategory, mySettings, mySettings.SynLang);
            //** [2017-08-05 09:13] Generate each file and JCard
            JsonArray jCards = new JsonArray();
            int ith = 0;
            string stContent = "";
            string stAns = "";
            string fileName = "";
            string audioPath = "";

            //*** [2017-08-05 09:44] Create a box file for title
            fileName = "S" + ith.ToString("00") + ". " + ourDailyBread.title[0].Content+".box";
            stContent = ourDailyBread.imgURL[0].Content + "*****" + ourDailyBread.pageURL[0].Content;
            audioPath = (ourDailyBread.selAnswerMP3?.Answer != null) ? ourDailyBread.selAnswerMP3.Answer : "";
            stAns = ourDailyBread.title[0].Answer;
            await DealWithACard(jCards, MYCategory, fileName, stContent, stAns, audioPath);
            //*** [2017-08-05 09:54] For thought
            foreach (var item in ourDailyBread.thought)
            {
                stAns = item.Answer;
                stContent = item.Content;
                if (stAns == "" && stContent == "")
                    continue;
                ith++;
                fileName = "S" + ith.ToString("00") + ". " + "thought.txt";
                await DealWithACard(jCards, MYCategory, fileName, stContent, stAns, "");
            }
            //*** [2017-08-05 09:54] For Content
            foreach (var item in ourDailyBread.Content)
            {
                stAns = item.Answer;
                stContent = item.Content;
                if (stAns == "" && stContent == "")
                    continue;
                ith++;
                fileName = "S" + ith.ToString("00") + ". " + "content.txt";
                await DealWithACard(jCards, MYCategory, fileName, stContent, stAns, "");
            }
            //*** [2017-08-05 10:05] For Bible verses
            foreach (var item in ourDailyBread.BibleContent)
            {
                stAns = item.Answer;
                stContent = item.Content;
                if (stAns == "" && stContent == "")
                    continue;
                ith++;
                fileName = "S" + ith.ToString("00") + ". " + "Bible verses.txt";
                await DealWithACard(jCards, MYCategory, fileName, stContent, stAns, "");
            }
            //*** [2017-08-05 09:54] For poem
            foreach (var item in ourDailyBread.poem)
            {
                stAns = item.Answer;
                stContent = item.Content;
                if (stAns == "" && stContent == "")
                    continue;
                ith++;
                fileName = "S" + ith.ToString("00") + ". " + "poem.txt";
                await DealWithACard(jCards, MYCategory, fileName, stContent, stAns, "");
            }

            //* [2017-08-05 10:17] Output it into MYCategory.json file
            JsonMYCategory.Add("Cards", jCards);
            await StorageItemHelper.WriteATextFileAsync(MYCategory, "MYCategory.json", JsonMYCategory.Stringify());


            //* [2017-08-05 11:05] Show a Dialog to open MemorizeYC
            MessageDialog dialog = new MessageDialog("Do you want to learn it from MemorizeYC now?", "Open MemorizeYC");
            dialog.Commands.Add(
                new UICommand("OK",
                async (x) =>
                {
                    var options = new LauncherOptions();
                    options.TreatAsUntrusted = false;
                    options.TargetApplicationPackageFamilyName = "48028Young-ChungHsue.MemorizeYC_p5nsf55p85esy";
                    Uri uri = new Uri("memorizeyc://EditPage/?myCont=" +
                        stMYCont + "&myCat=" + stMYCat);
                    await Launcher.LaunchUriAsync(uri, options);
                }
                ));
            dialog.Commands.Add(new UICommand("No, thanks."));
            dialog.DefaultCommandIndex = 0;
            dialog.CancelCommandIndex = 1;
            await dialog.ShowAsync();

        }

        private static async Task DealWithACard(JsonArray jCards, StorageFolder folder, string fileName, string stContent, string stAns, string audioPath)
        {
            if (await StorageItemHelper.WriteATextFileAsync(folder, fileName, stContent))
            {
                jCards.Add(MYCategoryJson.CreateANewJCard(fileName, audioPath, stAns));
            }
        }
    }
}
