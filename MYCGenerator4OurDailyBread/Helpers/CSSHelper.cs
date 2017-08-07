using System;
using System.Collections.Generic;
using System.Text;
using MemorizeYC.ViewModels;
using Windows.Data.Json;
using Windows.UI.Xaml;

namespace MemorizeYC.Helpers
{
    public class CSSHelper
    {
        public static double GetDoubleFromCSStyle(string stDouble,string forWhat="width")
        {
            string toDouble = stDouble;
            double result = double.NaN;
            try
            {
                if (toDouble.LastIndexOf('%') > 0)
                {
                    result = Convert.ToDouble(toDouble.Substring(0, toDouble.LastIndexOf('%')));
                    if (forWhat.ToLower() == "width")
                        result = result / 100 * Window.Current.Bounds.Width;
                    else if (forWhat.ToLower() == "height")
                        result = result / 100 * Window.Current.Bounds.Height;
                }
                else if (toDouble.LastIndexOf("px") > 0)
                    result = Convert.ToDouble(toDouble.Substring(0, toDouble.LastIndexOf("px")));
                else if (toDouble.LastIndexOf("vw") > 0)
                {
                    result = Convert.ToDouble(toDouble.Substring(0, toDouble.LastIndexOf("vw")));
                    result = result / 100 * Window.Current.Bounds.Width;
                }
                else if (toDouble.LastIndexOf("vh") > 0)
                {
                    result = Convert.ToDouble(toDouble.Substring(0, toDouble.LastIndexOf("vh")));
                    result = result / 100 * Window.Current.Bounds.Height;
                }
                else if (toDouble.LastIndexOf("vmin") > 0)
                {
                    result = Convert.ToDouble(toDouble.Substring(0, toDouble.LastIndexOf("vmin")));
                    result = result / 100 * Math.Min(Window.Current.Bounds.Width, Window.Current.Bounds.Height);
                }
                else if (toDouble.LastIndexOf("vmax") > 0)
                {
                    result = Convert.ToDouble(toDouble.Substring(0, toDouble.LastIndexOf("vmax")));
                    result = result / 100 * Math.Max(Window.Current.Bounds.Width, Window.Current.Bounds.Height);
                }
                else
                    result = Convert.ToDouble(toDouble);
            }
            catch (Exception exc)
            {
                //: TODO:
            }

            return result;
        }

        internal static JsonObject JsonForBackgroundImg(ForPlayPageSettings mySettings)
        {
            //** [2016-10-21 11:16] Generate a CSS3 style Background
            var jBackgroundImg = new JsonObject();
            jBackgroundImg.Add("opacity", JsonValue.CreateStringValue("0.3"));
            jBackgroundImg.Add("background-size", JsonValue.CreateStringValue("cover"));
            jBackgroundImg.Add("background-repeat", JsonValue.CreateStringValue("no-repeat"));
            jBackgroundImg.Add("background-image", JsonValue.CreateStringValue(mySettings.BackgroundPath));
            if(mySettings.ImgBackground?.ActualHeight >0 && mySettings.ImgBackground?.ActualWidth > 0)
            {
                jBackgroundImg.Add("width", JsonValue.CreateStringValue(Math.Round(mySettings.ImgBackground.ActualWidth).ToString() + "px"));
                jBackgroundImg.Add("height", JsonValue.CreateStringValue(Math.Round(mySettings.ImgBackground.ActualHeight).ToString() + "px"));
            }
            return jBackgroundImg;
        }
    }
}
