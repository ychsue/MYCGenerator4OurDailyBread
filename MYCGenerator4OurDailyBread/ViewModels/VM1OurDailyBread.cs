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

namespace MYCGenerator.ViewModels
{
    public class VM1OurDailyBread : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName]string propName="")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        //private VMCollection4Comparison _paragraph = new VMCollection4Comparison();
        //public VMCollection4Comparison paragraph
        //{
        //    get { return _paragraph; }
        //    set { _paragraph = value; NotifyPropertyChanged(); }
        //}

        private VMCollection4Comparison _mp3URLs = new VMCollection4Comparison();
        public VMCollection4Comparison mp3URLs
        {
            get { return _mp3URLs; }
            set { _mp3URLs = value; NotifyPropertyChanged(); }
        }

        private VMCollection4Comparison _pageURL = new VMCollection4Comparison();
        public VMCollection4Comparison pageURL
        {
            get { return _pageURL; }
            set { _pageURL = value; NotifyPropertyChanged(); }
        }

        private VMCollection4Comparison _imgURL =new VMCollection4Comparison();
        public VMCollection4Comparison imgURL
        {
            get { return _imgURL; }
            set { _imgURL = value; NotifyPropertyChanged(); }
        }

        private VMCollection4Comparison _title =new VMCollection4Comparison();
        public VMCollection4Comparison title
        {
            get { return _title; }
            set { _title = value; NotifyPropertyChanged(); }
        }

        private VMCollection4Comparison _contentMp3 = new VMCollection4Comparison();//Content: its #text, Answer: its Address
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


        private VMCollection4Comparison _answerMP3 = new VMCollection4Comparison();
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




        private VMCollection4Comparison _bibleURL = new VMCollection4Comparison();
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

        internal async void initialize(string stURL, PropertyInfo OneOfPropInfo, Func<object> p = null,string stLangCode ="", object date = null)
        {
            //* [2017-07-31 12:21] initialize strings
            IniStrings(OneOfPropInfo);
            //* [2017-06-21 14:23] Follow the instruction of http://html-agility-pack.net/
            var web = new HtmlWeb();
            HtmlDocument doc;
            try
            {
                doc = await web.LoadFromWebAsync(stURL);
            }
            catch (Exception)
            {
                ErrorHelper.ShowErrorMsg(ErrorHelper.ErrorCode.MainPageLinkFail, stURL);
                p?.Invoke();
                return;
            }
            doc.OptionOutputOriginalCase = true;
            var docNode = doc.DocumentNode;
            HtmlNode ndBuf;

            //* [2017-07-14 15:55] Get MainURL
            if (pageURL.Count == 0)
                pageURL.Add(new VMContentAnswerPair());
            string pageUri = stURL;
            if (date == null)
            {
                ndBuf = docNode.Descendants("div").Where(x => x.Attributes["id"]?.Value.Contains("page-body") == true).FirstOrDefault();
                ndBuf = ndBuf?.Descendants("div").Where(x => x.Attributes["class"]?.Value.Contains("section") == true).FirstOrDefault();
                ndBuf = ndBuf?.Descendants("a").FirstOrDefault();
                pageUri = ndBuf?.Attributes["href"]?.Value;
                if (pageUri== null)
                {
                    ErrorHelper.ShowErrorMsg(ErrorHelper.ErrorCode.CannotGetPageLink);
                    pageUri = stURL;
                }
            }
            //else //TODO ******************

            OneOfPropInfo.SetValue(pageURL[0], pageUri);
            //* [2017-07-15 22:30] Get the main Part
            //** [2017-06-21 16:15] Get its WebSite
            if (pageUri != stURL)
            {
                try
                {
                    doc = await web.LoadFromWebAsync((string)OneOfPropInfo.GetValue(pageURL[0]));
                    doc.OptionOutputOriginalCase = true;
                    //* [2017-06-21 16:15] Get its title
                    docNode = doc.DocumentNode;
                }
                catch (Exception)
                {
                    ErrorHelper.ShowErrorMsg(ErrorHelper.ErrorCode.MainPageLinkFail, stURL);
                    p?.Invoke();
                    return;
                }
            }

            string title = docNode.Descendants("h2")?.Where(x => x.Attributes["class"]?.Value.Contains("entry-title")==true).FirstOrDefault()?.InnerText.Trim();
            if (title == null)
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
            ndBuf = docNode.Descendants("figure").Where(x => x.Attributes["class"]?.Value.Contains("entry-thumbnail")==true).FirstOrDefault();
            if (this.imgURL.Count == 0)
                this.imgURL.Add(new VMContentAnswerPair());
            string imgURL = ndBuf?.Descendants("img").FirstOrDefault()?.Attributes["src"]?.Value;
            if (imgURL == null || imgURL =="")
            {
                ErrorHelper.ShowErrorMsg(ErrorHelper.ErrorCode.NoImageUri, pageUri);
            }
            else
            {
                if (imgURL.IndexOf("http") != 0)
                    imgURL = "https:" + imgURL;
                OneOfPropInfo.SetValue(this.imgURL[0], imgURL);
            }
            //* [2017-07-19 12:48] Get its bible's URL
            ndBuf = docNode.Descendants("div").Where(x => x.Attributes["class"]?.Value.Contains("passage-box") == true).FirstOrDefault();
            string bibleURL = ndBuf?.Descendants("a").FirstOrDefault()?.Attributes["href"]?.Value;
            if (bibleURL == null)
            {
                ErrorHelper.ShowErrorMsg(ErrorHelper.ErrorCode.NoBibleUri, pageUri);
            }
            else
            {
                if (bibleURL != "" && bibleURL.IndexOf("http") != 0)
                    bibleURL = "http:" + bibleURL;
                if (this.bibleURL.Count == 0)
                    this.bibleURL.Add(new VMContentAnswerPair());
                OneOfPropInfo.SetValue(this.bibleURL[0], bibleURL);

                //* [2017-07-19 12:55] Get its bible's content
                ObservableCollection<string> BibleContent = new ObservableCollection<string>();
                await GetBibleContent(bibleURL,stLangCode, BibleContent);
                for (int i0 = 0; i0 < BibleContent.Count; i0++)
                {
                    while (i0 >= this.BibleContent.Count)
                        this.BibleContent.Add(new VMContentAnswerPair());
                    OneOfPropInfo.SetValue(this.BibleContent[i0], BibleContent[i0]);
                }
            }
            //* [2017-07-06 17:28] Get its mp3's URL
            //var nodes = docNode.Descendants("div").Where(x => x.Attributes["class"]?.Value.Contains("download-mp3")==true);      *****************

            //ndBuf = docNode.Descendants("div").Where(x => x.Attributes["class"]?.Value == "download-mp3").FirstOrDefault();
            int iMp3 = 0;
            bool isAnswer = OneOfPropInfo.Name.ToLower() == "answer";
            var mp3Link = (isAnswer) ? this.answerMP3 : this.contentMp3;
            mp3Link.Clear(); //Initialize it.
            //var nodes = ndBuf?.Descendants("a");
            var nodes = docNode.Descendants("audio");
            foreach (var node in nodes)
            {
                //string mp3URL = node.Descendants("a").FirstOrDefault()?.Attributes["href"]?.Value;
                string mp3URL = node.Attributes["src"]?.Value;
                if(mp3URL != null)
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
            var ndContent = docNode.Descendants("div").Where(x => x.Attributes["class"]?.Value.Contains("entry-content")==true).FirstOrDefault();
            if (ndContent == null)
            {
                ErrorHelper.ShowErrorMsg(ErrorHelper.ErrorCode.NoContent,pageUri);
            }
            else
            {
                ndBuf = ndContent.Descendants("div").Where(x => x.Attributes["class"]?.Value.Contains("post-content")==true).FirstOrDefault();
                if (ndBuf == null)
                {
                    ErrorHelper.ShowErrorMsg(ErrorHelper.ErrorCode.NoPostContent, pageUri);
                }
                else
                {
                    ObservableCollection<string> Content = new ObservableCollection<string>();
                    GetContent(ndBuf, Content);
                    for (int i0 = 0; i0 < Content.Count; i0++)
                    {
                        while (i0 >= this.Content.Count)
                            this.Content.Add(new VMContentAnswerPair());
                        OneOfPropInfo.SetValue(this.Content[i0], Content[i0]);
                    }
                }
                //** [2017-07-27 14:23] Get poem
                ndBuf = ndContent.Descendants("div").Where(x => x.Attributes["class"]?.Value.Contains("poem-box")==true).FirstOrDefault();
                string poem = ndBuf?.InnerText;
                if (poem == null)
                {
                    ErrorHelper.ShowErrorMsg(ErrorHelper.ErrorCode.NoPoem, pageUri);
                }
                else
                {
                    if (this.poem.Count == 0)
                        this.poem.Add(new VMContentAnswerPair());
                    OneOfPropInfo.SetValue(this.poem[0], WebUtility.HtmlDecode(poem));
                }
                //** [2017-07-27 14:23] Get thought
                ndBuf = ndContent.Descendants("div").Where(x => x.Attributes["class"]?.Value.Contains("thought-box")==true).FirstOrDefault();
                string thought = ndBuf?.InnerText;
                if (thought == null)
                {
                    ErrorHelper.ShowErrorMsg(ErrorHelper.ErrorCode.NoThought, pageUri);
                }
                else
                {
                    if (this.thought.Count == 0)
                        this.thought.Add(new VMContentAnswerPair());
                    OneOfPropInfo.SetValue(this.thought[0], WebUtility.HtmlDecode(thought));
                }
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

        private async Task GetBibleContent(string bibleURL,string stLangCode ,ObservableCollection<string> bibleContent)
        {
            var web = new HtmlWeb();
            var doc = await web.LoadFromWebAsync(bibleURL);
            doc.OptionOutputOriginalCase = true;
            IEnumerable<HtmlNode> textNodes = new List<HtmlNode>();
            if (stLangCode == "ms")
            {
                textNodes = doc.DocumentNode.Descendants("div").Where(x => x.Attributes["class"]?.Value.Contains("verse-outter") == true);
            }
            else
            //* [2017-07-29 16:21] For zh-TW, zh-CN & en-US
            {
                textNodes = doc.DocumentNode.Descendants("div").Where(x => x.Attributes["class"]?.Value.Contains("text-html") == true);
                if (textNodes.Count() == 0)
                {
                    var ndsBuf = doc.DocumentNode.Descendants("div").Where(x => x.Attributes["class"]?.Value == "container");
                    if (ndsBuf.Count() > 0)
                    {
                        var ndBuf = ndsBuf?.FirstOrDefault();
                        if (ndsBuf.Count() > 1)
                            ndBuf = ndsBuf.ElementAt(1);
                        textNodes = new List<HtmlNode>();
                        if (ndBuf != null)
                            ((List<HtmlNode>)textNodes).Add(ndBuf);
                    }
                    else
                    {
                        textNodes = doc.DocumentNode.Descendants("div").Where(x => x.Attributes["class"]?.Value == "texts"); //For Bahasa Indonesia
                    }
                }
            }

                foreach (var node in textNodes)
                {
                    UpdateBibleContentByEachNode(node, bibleContent);
                }
        }

        private void UpdateBibleContentByEachNode(HtmlNode node, ObservableCollection<string> bibleContent)
        {
            string stBuf = "";
            //* [2017-07-20 10:00] Remove all "footnote"
            foreach (var inode in node.Descendants("sup").Where(x => x.Attributes["class"]?.Value == "footnote"))
            {
                inode.InnerHtml = "";
            }
            foreach (var inode in node.Descendants("div").Where(x => x.Attributes["class"]?.Value == "font-btn" || x.Attributes["class"]?.Value.Contains("hidden")==true || x.Attributes["id"]?.Value=="ChapterSidebar"))
            {
                inode.InnerHtml = "";
            }

            //* [2017-07-20 10:02] Get each verse ended before a "number"
            bool isNum = false;
            foreach (var inode in node.Descendants())
            {
                var value = inode.Attributes["class"]?.Value;
                if (
                    (value?.Contains("num") == true || value?.Contains("verse") == true || value=="vref" || 
                    (inode.Name=="sup" && inode.Attributes["data-reactid"]!=null)
                    ) 
                    && value!="passage-verse-wrap") //"vref" for Bahasa Indonesia
                {
                    isNum = true;
                    if (stBuf.Trim() != "")
                    {
                        bibleContent.Add(WebUtility.HtmlDecode(stBuf.Trim()));
                        stBuf = "";
                    }
                }
                else if (inode.Name == "#text")
                {
                    stBuf += inode.InnerText;
                    if (isNum)
                    {
                        isNum = false;
                        stBuf += "\n";
                    }
                }
                else if (value?.ToLower().Contains("footnotes") == true || value?.ToLower().Contains("footer") == true)
                    break;
            }

            if (stBuf.Trim() != "")
                bibleContent.Add(WebUtility.HtmlDecode(stBuf.Trim()));

        }

        private void GetContent(HtmlNode node, ObservableCollection<string> Contents)
        {
            string stBuf = "";
            //* [2017-08-02 10:12] Clean the tweet part
            foreach (var item in node.Descendants("div").Where(x =>x.Attributes["class"]?.Value.Contains("tweet")==true))
            {
                item.InnerHtml = "";
            }
            foreach (var innerNode in node.Descendants())
            {
                if(innerNode.Name=="p" || innerNode.Name == "br")
                {
                    stBuf = stBuf.Trim();
                    if (stBuf != "")
                        Contents.Add(WebUtility.HtmlDecode(stBuf));
                    stBuf = "";
                }
                else if(innerNode.Name=="#text")
                {
                    stBuf += innerNode.InnerText;
                }
            }

            if (stBuf.Trim() != "")
                Contents.Add(WebUtility.HtmlDecode(stBuf));
            stBuf = "";
        }

    }
}
