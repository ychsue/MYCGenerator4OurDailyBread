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

namespace MYCGenerator.ViewModels
{
    public class VM1OurDailyBread : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName]string propName="")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        private string _mainURL;
        public string mainURL
        {
            get { return _mainURL; }
            set { _mainURL = value; NotifyPropertyChanged(); }
        }

        private string _imgURL;
        public string imgURL
        {
            get { return _imgURL; }
            set { _imgURL = value; NotifyPropertyChanged(); }
        }

        private string _title;
        public string title
        {
            get { return _title; }
            set { _title = value; NotifyPropertyChanged(); }
        }

        private string _mp3URL;
        public string mp3URL
        {
            get { return _mp3URL; }
            set { _mp3URL = value; NotifyPropertyChanged(); }
        }

        private string _bibleURL;
        public string bibleURL
        {
            get { return _bibleURL; }
            set { _bibleURL = value; NotifyPropertyChanged(); }
        }

        private ObservableCollection<string> _BibleContent = new ObservableCollection<string>();
        public ObservableCollection<string> BibleContent
        {
            get { return _BibleContent; }
            set { _BibleContent = value; NotifyPropertyChanged(); }
        }

        private string _poem;
        public string poem
        {
            get { return _poem; }
            set { _poem = value; NotifyPropertyChanged(); }
        }

        private string _thought;
        public string thought
        {
            get { return _thought; }
            set { _thought = value; NotifyPropertyChanged(); }
        }

        private ObservableCollection<string> _Content = new ObservableCollection<string>();
        public ObservableCollection<string> Content
        {
            get { return _Content; }
            set { _Content = value; NotifyPropertyChanged(); }
        }


        public async void initialize(string stURL,bool isTodayURL = false,object callbackObj = null)
        {
            var ODBPage = callbackObj as OurDailyBreadPage;
            //* [2017-06-21 14:23] Follow the instruction of http://html-agility-pack.net/
            var web = new HtmlWeb();
            var doc = await web.LoadFromWebAsync(stURL);
            doc.OptionOutputOriginalCase = true;
            var docNode = doc.DocumentNode;
            HtmlNode ndBuf;

            //* [2017-07-14 15:55] Get MainURL
            if (isTodayURL == false)
            {
                ndBuf = docNode.Descendants("div").Where(x => x.Attributes["id"]?.Value.Contains("page-body") == true).FirstOrDefault();
                ndBuf = ndBuf?.Descendants("div").Where(x => x.Attributes["class"]?.Value.Contains("section") == true).FirstOrDefault();
                ndBuf = ndBuf?.Descendants("a").FirstOrDefault();
                this.mainURL = ndBuf?.Attributes["href"].Value;
            }
            else
                this.mainURL = stURL;
            //* [2017-07-15 22:30] Get the main Part
            //** [2017-06-21 16:15] Get its WebSite
            doc = await web.LoadFromWebAsync(mainURL);
            doc.OptionOutputOriginalCase = true;
            //* [2017-06-21 16:15] Get its title
            docNode = doc.DocumentNode;
            title = docNode.Descendants("h2")?.Where(x => x.Attributes["class"]?.Value == "entry-title").FirstOrDefault()?.InnerText;
            //* [2017-07-06 17:11] Get its image
            ndBuf = docNode.Descendants("figure").Where(x => x.Attributes["class"]?.Value == "entry-thumbnail").FirstOrDefault();
            imgURL = ndBuf?.Descendants("img").FirstOrDefault()?.Attributes["src"]?.Value;
            if (ODBPage != null)
            {
                ODBPage.todayTitle = WebUtility.HtmlDecode(title);
                ODBPage.todayImageURL = imgURL;
            }

            //* [2017-07-19 12:48] Get its bible's URL
            ndBuf = docNode.Descendants("div").Where(x => x.Attributes["class"]?.Value.Contains("passage-box") == true).FirstOrDefault();
            bibleURL = ndBuf?.Descendants("a").FirstOrDefault()?.Attributes["href"].Value;

            //* [2017-07-06 17:28] Get its mp3's URL
            ndBuf = docNode.Descendants("div").Where(x => x.Attributes["class"]?.Value == "download-mp3").FirstOrDefault();
            mp3URL = ndBuf?.Descendants("a").FirstOrDefault()?.Attributes["href"]?.Value;
            //* [2017-07-06 22:38] Get its content
            var ndContent = docNode.Descendants("div").Where(x => x.Attributes["class"]?.Value == "entry-content").FirstOrDefault();
            ndBuf = ndContent.Descendants("div").Where(x => x.Attributes["class"]?.Value == "post-content").FirstOrDefault();
            GetContent(ndBuf, Content);
            ndBuf = ndContent.Descendants("div").Where(x => x.Attributes["class"]?.Value == "poem-box").FirstOrDefault();
            poem = ndBuf?.InnerText;
            ndBuf = ndContent.Descendants("div").Where(x => x.Attributes["class"]?.Value == "thought-box").FirstOrDefault();
            thought = ndBuf?.InnerText;

            //* [2017-07-19 12:55] Get its bible's content
            GetBibleContent(bibleURL, BibleContent);
        }

        private async void GetBibleContent(string bibleURL, ObservableCollection<string> bibleContent)
        {
            var web = new HtmlWeb();
            var doc = await web.LoadFromWebAsync(bibleURL);
            doc.OptionOutputOriginalCase = true;
            var textNodes = doc.DocumentNode.Descendants("div").Where(x => x.Attributes["class"]?.Value.Contains("text-html") == true);
            foreach (var node in textNodes)
            {
                UpdateBibleContentByEachNode(node,bibleContent);
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

            //* [2017-07-20 10:02] Get each verse ended before a "number"
            bool isNum = false;
            foreach (var inode in node.Descendants())
            {
                if (inode.Attributes["class"]?.Value.Contains("num") == true)
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
                else if (inode.Attributes["class"]?.Value.Contains("footnotes") == true)
                    break;
            }

            if (stBuf.Trim() != "")
                bibleContent.Add(WebUtility.HtmlDecode(stBuf.Trim()));

        }

        private void GetContent(HtmlNode ndBuf, ObservableCollection<string> todayContent1)
        {
            //* [2017-07-07 09:32] Ignore #text before the first <p>
            var currentNode = ndBuf.FirstChild;
            while (currentNode?.Name != "p")
            {
                currentNode = currentNode.NextSibling;
            }
            //* [2017-07-07 09:37] Get the content
            string stBuf = "";
            do
            {
                //** [2017-07-07 09:43] Check whether it is a <p> node
                if (currentNode.Name == "p")
                {
                    //*** [2017-07-07 11:15] Dump stBuf
                    if (stBuf != "")
                    {
                        if(stBuf.Trim()!="")
                            todayContent1.Add(stBuf); //Store the accumulated one
                        stBuf = ""; //Initailize this buffer again
                    }
                    //*** [2017-07-07 11:16] Dump <p>'s innerText if it is not ""
                    if (currentNode.InnerText != "")
                        todayContent1.Add(currentNode.InnerText);
                }
                else
                {
                    if (currentNode.Name != "div") //Skip tweetable-content one
                    {
                        stBuf += currentNode.InnerText;
                    }
                }

                currentNode = currentNode.NextSibling;
            } while (currentNode != null);
        }

    }
}
