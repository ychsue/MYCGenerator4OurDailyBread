using HtmlAgilityPack;
using MYCGenerator.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using MYCGenerator4OurDailyBread.Helpers;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using System.Threading;
using Windows.Web.Http;

namespace MYCGenerator.ViewModels
{
    public class VM1OurDailyBread : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName]string propName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        private DateTimeOffset _theDate = new DateTimeOffset(DateTime.Now);
        public DateTimeOffset theDate
        {
            get { return _theDate; }
            set { _theDate = value; NotifyPropertyChanged(); }
        }

        private VMCollection4Comparison _Language = new VMCollection4Comparison() { doNotifyCollectionChangedWhenPropChanged = true };
        public VMCollection4Comparison Language
        {
            get { return _Language; }
            set { _Language = value; NotifyPropertyChanged(); }
        }

        private VMCollection4Comparison _mp3URLs = new VMCollection4Comparison() { doNotifyCollectionChangedWhenPropChanged = true };
        public VMCollection4Comparison mp3URLs
        {
            get { return _mp3URLs; }
            set { _mp3URLs = value; NotifyPropertyChanged(); }
        }

        private VMCollection4Comparison _pageURL = new VMCollection4Comparison() { doNotifyCollectionChangedWhenPropChanged = true };
        public VMCollection4Comparison pageURL
        {
            get { return _pageURL; }
            set { _pageURL = value; NotifyPropertyChanged(); }
        }

        private VMCollection4Comparison _imgURL = new VMCollection4Comparison() { doNotifyCollectionChangedWhenPropChanged = true };
        public VMCollection4Comparison imgURL
        {
            get { return _imgURL; }
            set { _imgURL = value; NotifyPropertyChanged(); }
        }

        private VMCollection4Comparison _title = new VMCollection4Comparison() { doNotifyCollectionChangedWhenPropChanged = true };
        public VMCollection4Comparison title
        {
            get { return _title; }
            set { _title = value; NotifyPropertyChanged(); }
        }

        private VMCollection4Comparison _contentMp3 = new VMCollection4Comparison() { doNotifyCollectionChangedWhenPropChanged = true };//Content: its #text, Answer: its Address
        public VMCollection4Comparison contentMp3
        {
            get { return _contentMp3; }
            set { _contentMp3 = value; NotifyPropertyChanged(); }
        }
        private VMContentAnswerPair _selContentMP3;
        public VMContentAnswerPair selContentMP3
        {
            get { return _selContentMP3; }
            set { _selContentMP3 = value; NotifyPropertyChanged(); }
        }


        private VMCollection4Comparison _answerMP3 = new VMCollection4Comparison() { doNotifyCollectionChangedWhenPropChanged = true };
        public VMCollection4Comparison answerMP3
        {
            get { return _answerMP3; }
            set { _answerMP3 = value; NotifyPropertyChanged(); }
        }
        private VMContentAnswerPair _selAnswerMP3;
        public VMContentAnswerPair selAnswerMP3
        {
            get { return _selAnswerMP3; }
            set { _selAnswerMP3 = value; NotifyPropertyChanged(); }
        }




        private VMCollection4Comparison _bibleURL = new VMCollection4Comparison() { doNotifyCollectionChangedWhenPropChanged = true };
        public VMCollection4Comparison bibleURL
        {
            get { return _bibleURL; }
            set { _bibleURL = value; NotifyPropertyChanged(); }
        }

        private VMCollection4Comparison _BibleContent = new VMCollection4Comparison();
        public VMCollection4Comparison BibleContent
        {
            get { return _BibleContent; }
            set { _BibleContent = value; NotifyPropertyChanged(); }
        }

        private VMCollection4Comparison _poem = new VMCollection4Comparison();
        public VMCollection4Comparison poem
        {
            get { return _poem; }
            set { _poem = value; NotifyPropertyChanged(); }
        }

        private VMCollection4Comparison _thought = new VMCollection4Comparison();
        public VMCollection4Comparison thought
        {
            get { return _thought; }
            set { _thought = value; NotifyPropertyChanged(); }
        }

        private VMCollection4Comparison _Content = new VMCollection4Comparison();
        public VMCollection4Comparison Content
        {
            get { return _Content; }
            set { _Content = value; NotifyPropertyChanged(); }
        }

        private async Task<string> LoadPageAsync(WebView wv, Uri uri)
        {
            SemaphoreSlim waitUntil = new SemaphoreSlim(0, 1);
            string result = "";
            // * [2019-08-12 21:05] Initialize events
            Windows.Foundation.TypedEventHandler<WebView, WebViewNavigationCompletedEventArgs> Wv_NavigationCompleted = (WebView sender, WebViewNavigationCompletedEventArgs args) =>
            {
                waitUntil.Release();
            };
            WebViewNavigationFailedEventHandler Wv_NavigationFailed = (object sender, WebViewNavigationFailedEventArgs e) =>
            {
                result = "fail";
                waitUntil.Release();
            };

            wv.NavigationCompleted += Wv_NavigationCompleted;
            wv.NavigationFailed += Wv_NavigationFailed;
            // ** [2020-03-03 09:43] Gotten from https://stackoverflow.com/questions/33391437/universal-windows-10-webview-how-to-clear-disable-cache
            HttpRequestMessage rmsg = new HttpRequestMessage(HttpMethod.Get, uri);
            rmsg.Headers.Add("Cache-Control", "no-cache, no-store, must-revalidate");
            rmsg.Headers.Add("Pragma", "no-cache");
            wv.NavigateWithHttpRequestMessage(rmsg);
//            wv.Navigate(uri);
            await waitUntil.WaitAsync();
            wv.NavigationCompleted -= Wv_NavigationCompleted;
            wv.NavigationFailed -= Wv_NavigationFailed;
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stURL"></param>
        /// <param name="OneOfPropInfo"></param>
        /// <param name="p"></param>
        /// <param name="stLangCode">If it is "mainURL", skip finding mainURL</param>
        /// <param name="date"></param>
        internal async void initialize(string stURL, PropertyInfo OneOfPropInfo, Func<object> p = null, string stLangCode = "")
        {
            bool isAnswer = OneOfPropInfo.Name.ToLower() == "answer";
            bool ismainURL = stLangCode == "mainURL";
            string mainURL = (ismainURL) ? stURL : "";
            //* [2017-07-31 12:21] initialize strings
            IniStrings(OneOfPropInfo);
            //* [2017-08-05 08:46] Add the langCode into this object
            if (this.Language.Count == 0)
                this.Language.Add(new VMContentAnswerPair());
            WebView webView = null;
            if (isAnswer)
            {
                if (ismainURL == false)
                {
                    this.Language[0].Answer = stLangCode;
                } else
                {
                    stLangCode = this.Language[0].Answer;
                }
                webView = OurDailyBreadPage.Current.webviewAnswer;
            }
            else
            {
                if (ismainURL == false)
                {
                    this.Language[0].Content = stLangCode;
                } else
                {
                    stLangCode = this.Language[0].Content;
                }
                webView = OurDailyBreadPage.Current.webviewContent;
            }
            //* [2017-06-21 14:23] Follow the instruction of http://html-agility-pack.net/
            string webScript = "";

            if (mainURL == "")
            {
                try
                {
                    bool isPass = theDate.Date != DateTime.Now.Date || ((stLangCode.ToLower() == "en-us") || (stLangCode.ToLower() == "es") || (stLangCode.ToLower() == "ar"));
                    if (isPass)
                    {
                        stURL += $"/{theDate.Year.ToString()}/{theDate.Month.ToString("D2")}/{theDate.Day.ToString("D2")}/?calendar-redirect=true&post-type=post";
                    }
                    string bufQ = (stURL.Contains("?") == true) ? "&" : "?";
                    string mesg = await LoadPageAsync(webView, new Uri(stURL +bufQ+"t="+DateTime.Now.Millisecond.ToString()));
                    if (mesg != "") ErrorHelper.ShowErrorMsg(ErrorHelper.ErrorCode.MainPageLinkFail, stURL + ": *LoadPageAsync* fail : " + mesg);

                    if (isPass)
                    {
                        mainURL = await webView.InvokeScriptAsync("eval", new string[] { "location.href;" });
                    }
                    else
                    {
                        //if (theDate.Date != DateTime.Now.Date)
                        //{
                        //    webScript = "a=document.querySelector(\"#page-body .panel-body a\");(a==null)?\"\":a.href";
                        //} else
                        //{
                        webScript = ((stLangCode.ToLower() == "en-us") || (stLangCode.ToLower() == "es")) ? "a=document.querySelector('a[class$=\"-today\"]');if(a){a.href;}else{''}" : "a=document.querySelector('#page-body .section a');if(a){a.href;}else{''}";
                        //}
                        //else
                        for (int i0 = 0; i0 < 40; i0++)
                        {
                            mainURL = await webView.InvokeScriptAsync("eval", new string[] { webScript });
                            if (mainURL == "")
                            {
                                ErrorHelper.ShowErrorMsg(ErrorHelper.ErrorCode.MainPageLinkFail, stURL + "::" + "Imcomplete Loading: " + i0);
                                await Task.Delay(300);
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }
                catch (Exception exc)
                {
                    Debug.WriteLine(stURL + ":" + exc.Message);
                    ErrorHelper.ShowErrorMsg(ErrorHelper.ErrorCode.MainPageLinkFail, stURL + ":" + exc.Message + ": Please refresh it.");
                    p?.Invoke();
                    return;
                }
                if (mainURL == "")
                {
                    ErrorHelper.ShowErrorMsg(ErrorHelper.ErrorCode.MainPageLinkFail, stURL + ":" + "載不了");
                    p?.Invoke();
                    return;
                }
            }

            //* [2017-07-14 15:55] Get MainURL
            if (pageURL.Count == 0)
                pageURL.Add(new VMContentAnswerPair());
            string pageUri = mainURL;
                if (pageUri == null)
                {
                    ErrorHelper.ShowErrorMsg(ErrorHelper.ErrorCode.CannotGetPageLink);
                    pageUri = stURL;
                }

                // * [2019-02-04 00:05] Since for english one, it just provides its own related path

            OneOfPropInfo.SetValue(pageURL[0], pageUri);
            //* [2017-07-15 22:30] Get the main Part
            //** [2017-06-21 16:15] Get its WebSite
            if ((pageUri != stURL) || ismainURL)
            {
                try
                {
                    string bufQ = (pageUri.Contains("?") == true) ? "&" : "?";
                    for (int i0 = 0; i0 < 10; i0++)
                    {
                        string mesg = await LoadPageAsync(webView, new Uri(pageUri+bufQ+"t="+DateTime.Now.Millisecond.ToString()));
                        if (mesg != "")
                        {
                            ErrorHelper.ShowErrorMsg(ErrorHelper.ErrorCode.MainPageLinkFail, pageUri + ": *LoadPageAsync* fail :" + mesg);
                            await Task.Delay(300);
                        }
                        else break;
                    }
                }
                catch (Exception)
                {
                    ErrorHelper.ShowErrorMsg(ErrorHelper.ErrorCode.MainPageLinkFail, stURL + "->" + pageUri);
                    p?.Invoke();
                    return;
                }
            }

            string title="";
            try
            {
                for (int i0 = 0; i0 < 40; i0++)
                {
                    title = await webView.InvokeScriptAsync("eval", new string[] { "a=document.querySelectorAll('h2[class$=\"-title\"], h1[class$=\"-title\"]');if(a.length>0){a[0].innerText}" });
                    if (title == "")
                    {
                        ErrorHelper.ShowErrorMsg(ErrorHelper.ErrorCode.MainPageLinkFail, pageUri + ":" + ": Loading imcomplete : " + i0);
                        await Task.Delay(300);
                    }
                    else
                    {
                        break;
                    }
                }
            }
            catch (Exception exc)
            {
                title = exc.Message;
            }

            if (title == "")
            {
                ErrorHelper.ShowErrorMsg(ErrorHelper.ErrorCode.NoTitle, pageUri);
            }
            else
            {
                if (this.title.Count == 0)
                    this.title.Add(new VMContentAnswerPair());
                OneOfPropInfo.SetValue(this.title[0], WebUtility.HtmlDecode(title));
            }
            //* [2017-07-06 17:11] Get its image
            //ndBuf = docNode.Descendants("figure").Where(x => x.Attributes["class"]?.Value.Contains("entry-thumbnail")==true).FirstOrDefault();
            if (this.imgURL.Count == 0)
                this.imgURL.Add(new VMContentAnswerPair());
            //string imgURL = ndBuf?.Descendants("img").FirstOrDefault()?.Attributes["src"]?.Value;
            string imgURL;
            try
            {
                imgURL = await webView.InvokeScriptAsync("eval", new string[] { "a=document.querySelector('img.today-img, img.post-thumbnail');a.src" });
            }
            catch (Exception)
            {
                imgURL = "";
            }
            if (imgURL == null || imgURL == "")
            {
                ErrorHelper.ShowErrorMsg(ErrorHelper.ErrorCode.NoImageUri, pageUri);
            }
            else
            {
                if (imgURL.IndexOf("http") != 0)
                    imgURL = "https:" + imgURL;
                OneOfPropInfo.SetValue(this.imgURL[0], imgURL);
            }
            //* [2017-07-06 17:28] Get its mp3's URL
            int iMp3 = 0;
            var mp3Link = (isAnswer) ? this.answerMP3 : this.contentMp3;
            mp3Link.Clear(); //Initialize it.
            string stAudios = "";
            webScript = ((stLangCode.ToLower() == "en-us")|| (stLangCode.ToLower() == "es")) ? "a=document.querySelectorAll('.mobile-audio-links a');a[1].href" : "a=document.querySelectorAll('audio');c=[];a.forEach(function(b){c.push(b.src)});c.toString()";
            try
            {
                stAudios = await webView.InvokeScriptAsync("eval", new string[] { webScript });
            }
            catch (Exception)
            {
            }

            foreach (var node in stAudios.Split(','))
            {
                //string mp3URL = node.Descendants("a").FirstOrDefault()?.Attributes["href"]?.Value;
                if (node.IndexOf("http") != 0) continue;
                //string mp3URL = node.Attributes["src"]?.Value;
                string mp3URL = node;
                if (mp3URL != null)
                {
                    mp3Link.Add(new VMContentAnswerPair());
                    mp3Link.Last().Content = "Audio " + (1 + iMp3).ToString();
                    mp3Link.Last().Answer = mp3URL;
                    if (mp3Link.Count == 1)
                    {
                        if (isAnswer)
                            this.selAnswerMP3 = mp3Link[0];
                        else
                            this.selContentMP3 = mp3Link[0];
                    }
                    iMp3++;
                }
            }

            //* [2017-07-06 22:38] Get values for MYC cards
            // * [2019-04-10 13:38] Get the container of content
            webScript = ((stLangCode.ToLower() == "en-us") || (stLangCode.ToLower() == "es")) ? "a=document.querySelectorAll('.container .content div');" : "a=document.querySelectorAll('.entry-content .post-content');";
            bool hasContent = false;
            try
            {
                hasContent = await webView.InvokeScriptAsync("eval", new string[] { webScript + "a.length.toString()" }) != "0";
            }
            catch (Exception)
            {
                hasContent = false;
            }
            if (!hasContent)
            {
                ErrorHelper.ShowErrorMsg(ErrorHelper.ErrorCode.NoPostContent, pageUri);
            }
            else
            {
                ObservableCollection<string> Content = new ObservableCollection<string>();
                try
                {
                    await GetContent(webView, webScript, Content);
                }
                catch (Exception)
                {
                }
                for (int i0 = 0; i0 < Content.Count; i0++)
                {
                    while (i0 >= this.Content.Count)
                        this.Content.Add(new VMContentAnswerPair());
                    OneOfPropInfo.SetValue(this.Content[i0], Content[i0]);
                }
                //* [2017-08-25 10:33] Notify that the content is changed
                this.Content.NoticeCollectionChanged(this.Content, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }
            //** [2017-07-27 14:23] Get question
            //ndBuf = ndContent.Descendants("div").Where(x => x.Attributes["class"]?.Value.Contains("poem-box")==true).FirstOrDefault();
            //string poem = ndBuf?.InnerText;
            webScript = ((stLangCode.ToLower() == "en-us") || (stLangCode.ToLower() == "es")) ? "a=document.querySelectorAll('.container .devo-reflection.devo-question');" : "a=document.querySelectorAll('.entry-content .thought-box');";
            webScript = webScript + "if(a.length>0){a[0].innerText} else {''}";
            string poem = "";
            try
            {
                poem = await webView.InvokeScriptAsync("eval", new string[] { webScript });

            }
            catch (Exception)
            {
            }
            if (poem == "")
            {
                ErrorHelper.ShowErrorMsg(ErrorHelper.ErrorCode.NoPoem, pageUri);
            }
            else
            {
                if (this.poem.Count == 0)
                    this.poem.Add(new VMContentAnswerPair());
                OneOfPropInfo.SetValue(this.poem[0], WebUtility.HtmlDecode(poem));
                //* [2017-08-25 10:33] Notify that the poem is changed
                this.poem.NoticeCollectionChanged(this.poem, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }
            //** [2017-07-27 14:23] Get answer
            //ndBuf = ndContent.Descendants("div").Where(x => x.Attributes["class"]?.Value.Contains("thought-box")==true).FirstOrDefault();
            //string thought = ndBuf?.InnerText;
            webScript = ((stLangCode.ToLower() == "en-us") || (stLangCode.ToLower() == "es")) ? "a=document.querySelectorAll('.container .devo-reflection.devo-prayer');" : "a=document.querySelectorAll('.entry-content .poem-box');";
            webScript = webScript + "if(a.length>0){a[0].innerText} else {''}";
            string thought = null;
            try
            {
                thought = await webView.InvokeScriptAsync("eval", new string[] { webScript });
            }
            catch (Exception)
            {
            }

            if (thought == null)
            {
                ErrorHelper.ShowErrorMsg(ErrorHelper.ErrorCode.NoThought, pageUri);
            }
            else
            {
                if (this.thought.Count == 0)
                    this.thought.Add(new VMContentAnswerPair());
                OneOfPropInfo.SetValue(this.thought[0], WebUtility.HtmlDecode(thought));
                //* [2017-08-25 10:33] Notify that the thought is changed
                this.thought.NoticeCollectionChanged(this.thought, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }
            //}
            //* [2017-07-19 12:48] Get its bible's URL
            string bibleURL = ""; // Gotten from its link
            string biblegetwayURL = ""; // For some cases
            try
            {
                bibleURL = await webView.InvokeScriptAsync("eval", new string[] { "a=document.querySelector('div[class^=\"passage-box\"] a');a.href" });
            }
            catch (Exception)
            {
                bibleURL = "";
            }

            if (bibleURL == "" || (stLangCode.ToLower() == "en-us") || (stLangCode == "ta") || (stLangCode == "hi") || (stLangCode == "it") || (stLangCode == "nl") || (stLangCode == "pt")
                || (stLangCode == "es") || (stLangCode == "de"))
            {
                string search = "";
                string version = "";
                string script = ((stLangCode.ToLower() == "en-us") || (stLangCode.ToLower() == "es")) ? "encodeURI(document.querySelector('.devo-scriptureinsight a, .devo-scriptureinsight button').innerText)" : "a=document.querySelector('div.passage-box span');if(!!a){encodeURI(a.innerText)}";
                try
                {
                    search = await webView.InvokeScriptAsync("eval", new string[] { script });
                }
                catch (Exception)
                {
                    search = "null";
                }

                switch (stLangCode.ToLower())
                {
                    case "en-us":
                        version = "";
                        break;
                    case "es":
                        version = "RVR1960";
                        break;
                    case "de":
                        version = "HOF";
                        break;
                    case "ru":
                        version = "NRT";
                        break;
                    case "ta":
                        version = "ERV-TA";
                        break;
                    case "hi":
                        version = "ERV-HI";
                        break;
                    case "it":
                        version = "NR2006";
                        break;
                    case "nl":
                        version = "BB";
                        break;
                    case "pt":
                        version = "ARC";
                        break;
                    default:
                        break;
                }
                if (search != "null")
                {
                    if (stLangCode == "de")
                    {
                        search = search.Replace(",", "%3A").Replace(".",",").Replace("%2C", "%3A");
                    }
                    biblegetwayURL = "https://www.biblegateway.com/passage/?search=" + search +
                        ((version == "") ? "" : ("&version=" + version));
                }
            }

            if ((bibleURL == "") && (biblegetwayURL == ""))
            {
                ErrorHelper.ShowErrorMsg(ErrorHelper.ErrorCode.NoBibleUri, pageUri);
            }
            else
            {
                ////* [2017-09-06 15:42] Correction for German's bible

                if (bibleURL != "" && bibleURL.IndexOf("http") != 0)
                    bibleURL = "http:" + bibleURL;
                if (this.bibleURL.Count == 0)
                    this.bibleURL.Add(new VMContentAnswerPair());
                string stBurlForBtn = (bibleURL != "") ? bibleURL : biblegetwayURL;
                OneOfPropInfo.SetValue(this.bibleURL[0], stBurlForBtn);

                //* [2017-07-19 12:55] Get its bible's content
                ObservableCollection<string> BibleContent = new ObservableCollection<string>();
                try
                {
                    await GetBibleContent((biblegetwayURL != "") ? biblegetwayURL : bibleURL, stLangCode, BibleContent, webView);
                }
                catch (Exception exc)
                {
                    ErrorHelper.ShowErrorMsg(ErrorHelper.ErrorCode.NoBibleUri, exc.Message);
                }
                for (int i0 = 0; i0 < BibleContent.Count; i0++)
                {
                    while (i0 >= this.BibleContent.Count)
                        this.BibleContent.Add(new VMContentAnswerPair());
                    OneOfPropInfo.SetValue(this.BibleContent[i0], BibleContent[i0]);
                }
                //* [2017-08-25 10:33] Notify that the bible content is changed
                this.BibleContent.NoticeCollectionChanged(this.BibleContent, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }

            p?.Invoke();
            return;
        }

        private void IniStrings(PropertyInfo oneOfPropInfo)
        {
            foreach (var item in this.BibleContent)
            {
                oneOfPropInfo.SetValue(item, "");
            }
            foreach (var item in this.bibleURL)
            {
                oneOfPropInfo.SetValue(item, "");
            }
            foreach (var item in this.Content)
            {
                oneOfPropInfo.SetValue(item, "");
            }
            foreach (var item in this.imgURL)
            {
                oneOfPropInfo.SetValue(item, "/Assets/StoreLogo.png");  // Give it a default image
            }
            foreach (var item in this.pageURL)
            {
                oneOfPropInfo.SetValue(item, "");
            }
            foreach (var item in this.poem)
            {
                oneOfPropInfo.SetValue(item, "");
            }
            foreach (var item in this.thought)
            {
                oneOfPropInfo.SetValue(item, "");
            }
            foreach (var item in this.title)
            {
                oneOfPropInfo.SetValue(item, "");
            }
        }

        private async Task GetBibleContent(string bibleURL, string stLangCode, ObservableCollection<string> bibleContent, WebView wv = null)
        {
            //* [2020-03-04 11:20] Try to use webview instead of HtmlWeb;
            string webScript = "";
            if (bibleURL.Contains("bibleonline.ru"))
            {
                webScript = @"function getMsg (x) {
                                        let nName = x.nodeName;
                                        if(nName =='H3') {Msg+=x.innerText+'\n';}
                                        else if(nName == 'SPAN') {
                                            Msg+='BBBBB';
                                            Msg+=x.getAttribute('vers');
                                            Msg+='\n';
                                            Msg+=x.innerText;
                                        }
                                        else {
                                            let len = x.children.length;
                                            if(x.className=='panel panel-default panel-bible'){Msg+='BBBBB';}
                                            for(let i0=0;i0<x.children.length;i0++){
                                                getMsg(x.children[i0]);
                                            }
                                        }
                                        return;
                                    };
                                    a=document.querySelectorAll('.tab-content [tabindex=""0""]');
                                    Msg="""";a.forEach(getMsg);Msg;
                                        ";
            } else if (bibleURL.Contains("www.biblegateway.com"))
            {
                webScript = @"function getMsg (x) {
                                        let nName = x.nodeName;
                                        if(nName =='H1' || nName == 'H3') {Msg+='\n'+x.innerText+'\n';}
                                        else if(nName == 'SPAN') {
                                            if(x.querySelectorAll('.versenum, .chapternum').length>0){Msg+='BBBBB';}
                                            Msg+='\n';
                                            Msg+=x.innerText;
                                        }
                                        else {
                                            let len = x.children.length;
                                            if(x.className=='panel panel-default panel-bible'){Msg+='BBBBB';}
                                            for(let i0=0;i0<x.children.length;i0++){
                                                getMsg(x.children[i0]);
                                            }
                                        }
                                        return;
                                    };
                                    document.querySelectorAll('.footnote, .footnotes, .info').forEach(function(x){x.remove();});
                                    a=document.querySelectorAll('.passage-text');
                                    Msg='';a.forEach(function(x){getMsg(x);Msg+='BBBBB';});Msg;
                                    ";
            }
            else if (bibleURL.Contains("odb-ministries.org"))
            {
                webScript = @"function getMsg (x) {
                                        let nName = x.nodeName;
                                        if(nName=='BR' && nBR <4) {
                                            nBR++;
                                            if(nBR==1) Msg+='\n';
                                            else if(nBR==2) Msg+='BBBBB';
                                            else if(nBR==3) Msg+='Next Section';
                                        } else nBR=0;
//                                        Msg+='*'+nName+'*';
                                        if(nName =='H1' || nName =='H2' || nName == 'H3') {Msg+='\n'+x.innerText+'\n';}
                                        else if(nName == 'SUP') {
                                            Msg+='BBBBB';
                                            Msg+=x.innerText+'\n';
                                        } else if(nName == '#text'){
                                            Msg+=x.textContent.trim();
                                        }
                                        else {
                                            let len = x.childNodes.length;
                                            if(x.className=='panel panel-default panel-bible'){Msg+='BBBBB';}
                                            for(let i0=0;i0<len;i0++){
                                                getMsg(x.childNodes[i0]);
                                            }
                                        }
                                        return;
                                    };
                                    document.querySelectorAll('footer, script, .cd-top, .jumbotron').forEach(function(x){x.remove();});
                                    a=document.querySelectorAll('.container');
                                    Msg='';nBR=0;a.forEach(function(x){Msg+='BBBBB';getMsg(x);});
                                    (Msg.lastIndexOf('BBBBBNext Section')==(Msg.length-17))?Msg.substring(0,Msg.length-17):Msg;
                                    ";
            }
            else if (bibleURL.Contains("alkitab.sabda.org"))
            {
                webScript = @"function getMsg (x) {
                                        let nName = x.nodeName;
//                                        Msg+='*'+nName+'*';
                                        if(nName =='H1' || nName =='H2' || nName == 'H3') {Msg+='\n'+x.innerText+'\n';}
                                        else if(nName == 'SPAN') {
                                            if(x.className.indexOf('vref')>=0) Msg+='BBBBB';
                                            Msg+=x.innerText+'\n';
                                        } else if(nName == '#text'){
                                            Msg+=x.textContent.trim();
                                        }
                                        else {
                                            let len = x.childNodes.length;
                                            if(x.className=='panel panel-default panel-bible'){Msg+='BBBBB';}
                                            for(let i0=0;i0<len;i0++){
                                                getMsg(x.childNodes[i0]);
                                            }
                                        }
                                        return;
                                    };
                                    document.querySelectorAll('center').forEach(function(x){x.remove();});
                                    a=document.querySelectorAll('.texts');
                                    Msg='';a.forEach(function(x){getMsg(x);Msg+='BBBBB';});Msg;
                                    ";
            }
            else if (bibleURL.Contains("www.bible.com"))
            {
                webScript = @"function getMsg (x) {
                                        if (isBreak) return;
                                        let nName = x.nodeName;
//                                        Msg+='*'+nName+'*';
                                        if(nName =='A') {
                                            Msg+='BBBBB'+x.innerText;
                                        } else if(nName=='AMP-IMG'){
                                            isBreak =true;
                                        }
                                        else {
                                            let len = x.childNodes.length;
                                            if(x.className=='panel panel-default panel-bible'){Msg+='BBBBB';}
                                            for(let i0=0;i0<len;i0++){
                                                getMsg(x.childNodes[i0]);
                                            }
                                        }
                                        return;
                                    };
                                    document.querySelectorAll('.measure-wide-m.center div div div').forEach(x=>x.remove());
                                    a = document.querySelectorAll('.measure-wide-m.center');
                                    isBreak = false; Msg = ''; a.forEach(function(x){ getMsg(x); Msg += 'BBBBB'; }); Msg;
                ";
            } else
            {
                return;
            }

            await this.LoadPageAsync(wv, new Uri(bibleURL));
            string data = await wv.InvokeScriptAsync("eval", new string[] { webScript });
            data.Split(new string[] { "BBBBB" }, StringSplitOptions.RemoveEmptyEntries).All(
                x => {
                    string y = x.Substring((x.IndexOf('\n') == 0) ? 1 : 0);
                    if (y.LastIndexOf('\n') == (y.Length - 1)) y = y.Substring(0, y.Length - 1);
                    bibleContent.Add(y);
                    return true;
                });
            return;

        }


        private async Task GetContent(WebView webView, string webScript, ObservableCollection<string> Contents)
        {
            //string stBuf = "";
            //* [2017-08-02 10:12] Clean the tweet part
            //* [2019-04-10 14:42] This script will crash the code
            await webView.InvokeScriptAsync("eval", new string[] { webScript + "b=a[0].querySelectorAll('[class*=\"tweet\"]');" + "b.forEach(function(c){c.innerHtml=\"\"});" });
            //foreach (var item in node.Descendants("div").Where(x =>x.Attributes["class"]?.Value.Contains("tweet")==true))
            //{
            //    item.InnerHtml = "";
            //}
            string scriptP = webScript + "b=a[0].querySelectorAll('p');";
            string stN = await webView.InvokeScriptAsync("eval", new string[] { scriptP + "b.length.toString()" });
            int n = Convert.ToInt32(stN);
            for (int i0 = 0; i0 < n; i0++)
            {
                string para = await webView.InvokeScriptAsync("eval", new string[] { scriptP + "b[" + i0.ToString() + "].innerText" });
                if (para.Trim() == "") continue;
                Contents.Add(para);
            }
            //foreach (var innerNode in node.Descendants())
            //{
            //    if (innerNode.Name=="p" || innerNode.Name == "br")
            //    {
            //        stBuf = stBuf.Trim();
            //        if (stBuf != "")
            //            Contents.Add(WebUtility.HtmlDecode(stBuf));
            //        stBuf = "";
            //    }
            //    else if(innerNode.Name=="#text")
            //    {
            //        stBuf += innerNode.InnerText;
            //    }
            //}

            //if (stBuf.Trim() != "")
            //    Contents.Add(WebUtility.HtmlDecode(stBuf));
            //stBuf = "";
        }

    }
}
