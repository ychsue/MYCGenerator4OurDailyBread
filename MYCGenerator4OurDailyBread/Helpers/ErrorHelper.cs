using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace MYCGenerator4OurDailyBread.Helpers
{
    public class ErrorHelper
    {
        public enum ErrorCode
        {
            MainPageLinkFail, PageLinkFail, CannotGetPageLink,
            NoImageUri,
            NoTitle,
            NoBibleUri,
            NoContent,
            NoPoem,
            NoThought,
            NoPostContent,
            CannotGetYourSystemLang
        }
        public static void ShowErrorMsg(ErrorCode errCode,object para = null)
        {
            string content = "";
            string title = "";

            switch (errCode)
            {
                case ErrorCode.MainPageLinkFail:
                    var mainUri = para as string;
                    content = mainUri + " cannot be loaded at this moment. Click the Reflesh button might solve this problem.";
                    title = Enum.GetName(typeof(ErrorCode), ErrorCode.MainPageLinkFail);
                    break;
                case ErrorCode.CannotGetPageLink:
                    content = "Cannot get its page Uri. Click the Reflesh button might solve this problem.";
                    title = Enum.GetName(typeof(ErrorCode), ErrorCode.CannotGetPageLink);
                    break;
                case ErrorCode.PageLinkFail:
                    var pageUri = para as string;
                    content = pageUri + " cannot be loaded at this moment. Click the Reflesh button might solve this problem.";
                    title = Enum.GetName(typeof(ErrorCode), ErrorCode.PageLinkFail);
                    break;
                case ErrorCode.NoTitle:
                    pageUri = para as string;
                    content = pageUri + "\n I cannot get its title";
                    title = Enum.GetName(typeof(ErrorCode), ErrorCode.NoTitle);
                    break;
                case ErrorCode.NoImageUri:
                    pageUri = para as string;
                    content = pageUri + "\n I cannot get its image";
                    title = Enum.GetName(typeof(ErrorCode), ErrorCode.NoImageUri);
                    break;
                case ErrorCode.NoBibleUri:
                    pageUri = para as string;
                    content = pageUri + "\n I cannot get its Bible Uri";
                    title = Enum.GetName(typeof(ErrorCode), ErrorCode.NoBibleUri);
                    break;
                case ErrorCode.NoContent:
                    pageUri = para as string;
                    content = pageUri + "\n I cannot get its Paragraph";
                    title = Enum.GetName(typeof(ErrorCode), ErrorCode.NoContent);
                    break;
                case ErrorCode.NoPoem:
                    pageUri = para as string;
                    content = pageUri + "\n I cannot get its Poem";
                    title = Enum.GetName(typeof(ErrorCode), ErrorCode.NoPoem);
                    break;
                case ErrorCode.NoThought:
                    pageUri = para as string;
                    content = pageUri + "\n I cannot get its thought part";
                    title = Enum.GetName(typeof(ErrorCode), ErrorCode.NoThought);
                    break;
                case ErrorCode.NoPostContent:
                    pageUri = para as string;
                    content = pageUri + "\n I cannot get its post-content part";
                    title = Enum.GetName(typeof(ErrorCode), ErrorCode.NoPostContent);
                    break;
                case ErrorCode.CannotGetYourSystemLang:
                    content = "I cannot get your system's default language. Very strange.";
                    title = Enum.GetName(typeof(ErrorCode), ErrorCode.CannotGetYourSystemLang);
                    break;
                default:
                    break;
            }

            MainPage.Current.stMsg = DateTime.Now.ToString() + " " + title + "\n  " + content
                + "\n"+ MainPage.Current.stMsg;
            MainPage.Current.MsgVisibility = Windows.UI.Xaml.Visibility.Visible;
        }
    }
}
