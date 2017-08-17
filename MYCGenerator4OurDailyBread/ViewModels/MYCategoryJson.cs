using MemorizeYC.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Windows.Data.Json;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace MemorizeYC.ViewModels.DescriptFiles
{
    public class MYCategoryJson
    {
        #region     Enums
        public enum MYCategoryEnum
        {
            Cards,      //CardsFieldNamesEnum
            Background, //BackgroundFieldNamesEnum
            numWCardShown,
            isPickWCardsRandomly,
            isBGAlsoChange,
            isAudioInterruptable,
            Link,
            SynLang,
            IsShownAsList,              //* [2016-10-27 10:53] For all text content case
            IsDictateTextContentInHint, //* [2016-10-27 10:53] For all text content case
            IsDictateAnsInHint,
            ContentSynLang, //* [2017-06-20 13:56] For ContentSynLang
            isAnsFirst,      //* [2017-06-20 13:56] For ContentSynLang
            IsIgnoreDueDate //* [2017-08-17 15:20] 
        }
        public enum CardEnum
        {
            FileName,
            Dictate,
            Ans_KeyIn, //An array of NotableString
            Ans_Recog, //An array of NotableString
            Description,
            Link,
            Position, // double[2]
            Size, //double[2]
            IsSizeFixed,
            IsXPosFixed,
            IsYPosFixed,
            IsHideShadow,
            //IsUsingAudioFileForDictating,  //* We don't need this one since it depends on AudioFilePathOrUri
            AudioFilePathOrUri //String
        }
        public enum BackgroundEnum
        {
            ImgStyle, //Use an Enum
            AudioProperties //Object
        }
        public enum ImgStyleEnum
        {
            width,
            height,
            opacity,
            background,
            background_image,
            background_size,
            background_repeat
        }
        #endregion  Enums

        #region Save mySettings' infomation into MYCategoryJson
        public static void UpdateMYCategoryJsonByMySettings(JsonObject mYCategoryJson,ForPlayPageSettings mySettings, string synLang="en-US")
        {
            //* [2017-08-17 15:30] Added for Dictating all TextContent and Shown as a list
            string iKey = Enum.GetName(typeof(MYCategoryEnum), MYCategoryEnum.IsIgnoreDueDate);
            if (mySettings.IsIgnoreDueDate == true)
                mYCategoryJson.SetNamedValue(iKey, JsonValue.CreateBooleanValue(mySettings.IsIgnoreDueDate));
            else
                mYCategoryJson.Remove(iKey);
            //if (mySettings.BackgroundPath?.Length > 0)
            //{
            //    JsonObject jBG = MYCategoryJson.GetBackgroundJsonObj(mYCategoryJson);
            //    string BGImgKey = Enum.GetName(typeof(BackgroundEnum), BackgroundEnum.ImgStyle);
            //    jBG.SetNamedValue(BGImgKey, CSSHelper.JsonForBackgroundImg(mySettings));
            //}
            if (mySettings.Link?.Length > 0)
            {
                string LinkKey = Enum.GetName(typeof(MYCategoryEnum), MYCategoryEnum.Link);
                mYCategoryJson.SetNamedValue(LinkKey, JsonValue.CreateStringValue(mySettings.Link));
            }
            if (mySettings.numWCardShown > 0 && mySettings.numWCardShown != 8)
            {
                string numWCardShownKey = Enum.GetName(typeof(MYCategoryEnum), MYCategoryEnum.numWCardShown);
                mYCategoryJson.SetNamedValue(numWCardShownKey,JsonValue.CreateNumberValue(mySettings.numWCardShown));
            }
            //* [2016-10-27 13:52] Added for Dictating all TextContent and Shown as a list
            iKey = Enum.GetName(typeof(MYCategoryEnum), MYCategoryEnum.IsDictateTextContentInHint);
            if (mySettings.IsDictateTextContentInHint)
                mYCategoryJson.SetNamedValue(iKey, JsonValue.CreateBooleanValue(mySettings.IsDictateTextContentInHint));
            else
                mYCategoryJson.Remove(iKey);

            //* [2016-10-27 13:52] Added for Dictating all AnsDictate and Shown as a list
            iKey = Enum.GetName(typeof(MYCategoryEnum), MYCategoryEnum.IsDictateAnsInHint);
            if (mySettings.IsDictateAnsInHint == false) //Because the default is true.
                mYCategoryJson.SetNamedValue(iKey, JsonValue.CreateBooleanValue(mySettings.IsDictateAnsInHint));
            else
                mYCategoryJson.Remove(iKey);

            iKey = Enum.GetName(typeof(MYCategoryEnum), MYCategoryEnum.IsShownAsList);
            if (mySettings.IsShownAsList)
                mYCategoryJson.SetNamedValue(iKey, JsonValue.CreateBooleanValue(mySettings.IsShownAsList));
            else
                mYCategoryJson.Remove(iKey);

            //* [2016-10-22 10:42] Always Set SynLang
            string SynLangKey = Enum.GetName(typeof(MYCategoryEnum), MYCategoryEnum.SynLang);
            if (!(synLang?.Length > 0))
                synLang = "en-US";
            mYCategoryJson.SetNamedValue(SynLangKey, JsonValue.CreateStringValue(synLang));

            //* [2017-06-20 14:09] Set ContentSynLang
            iKey = Enum.GetName(typeof(MYCategoryEnum), MYCategoryEnum.isAnsFirst);
            if (mySettings.isAnsFirst)
                mYCategoryJson.SetNamedValue(iKey, JsonValue.CreateBooleanValue(mySettings.isAnsFirst));
            else
                mYCategoryJson.Remove(iKey);
            string ContentSynLangKey = Enum.GetName(typeof(MYCategoryEnum), MYCategoryEnum.ContentSynLang);
            if (mySettings.ContentSynLang != "" && synLang != mySettings.ContentSynLang)
                mYCategoryJson.SetNamedValue(ContentSynLangKey, JsonValue.CreateStringValue(mySettings.ContentSynLang));
            else
                mYCategoryJson.Remove(ContentSynLangKey);
        }
        #endregion
        public static JsonObject CreateANewJCard(string FileName, string AudioFilePathOrUri = "", string Dictate = "")
        {
            JsonObject jCard = new JsonObject();
            //* [2016-10-21 11:01] Store its fileName
            string FileNameKey = Enum.GetName(typeof(CardEnum), CardEnum.FileName);
            jCard.Add(FileNameKey, JsonValue.CreateStringValue(FileName));
            //* [2016-10-21 11:02] If it has audio, store it, too.
            if (AudioFilePathOrUri!="")
            {
                string CardAudioKey = Enum.GetName(typeof(CardEnum), CardEnum.AudioFilePathOrUri);
                jCard.Add(CardAudioKey, JsonValue.CreateStringValue(AudioFilePathOrUri));
            }

            //* [2017-08-05 08:57] If it Dictate does exist, use it
            if (Dictate != "")
            {
                string DictateKey = Enum.GetName(typeof(CardEnum), CardEnum.Dictate);
                jCard.Add(DictateKey, JsonValue.CreateStringValue(Dictate));
            }

            return jCard;
        }
    }
}
