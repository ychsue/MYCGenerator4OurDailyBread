# 10th Submission
## 27th Commit
### [2019-10-11 21:17] One important thing about "DataContext"
Because the *flyout* is inside a *AppBarButton*, it cannot get data from root, so I need to set *DataContext="{Binding ElementName=page}"* in *AppBarButton*. Then I can bind its data with the *flyout*.
## 26th Commit
### [2019-10-11 15:48] Now, even Spanish part use the SPA used for English one as its web page.
By the way, https://www.bible.com has also changed its logic.

### [2019-10-04 11:52] Let the textbox inside a button's flyout to be inputable
From [this StackOverflow's Q&A](https://stackoverflow.com/questions/39096758/cant-enter-enter-text-in-textbox-control-inside-flyout), you can see that you need to set the property **AllowFocusOnInteraction** of the button.

## 24th Commit
### [2019-04-12 15:48] Stock at building with **Native tool chain** 
1. After many hours compilation, I got an error :
Severity	Code	Description	Project	File	Line	Suppression State
Error		ILT0005: 'C:\Users\*****\.nuget\packages\microsoft.net.native.compiler\1.7.6\tools\x64\ilc\Tools\nutc_driver.exe @"C:\Users\*****\source\repos\MYCGenerator4OurDailyBread\MYCGenerator4OurDailyBread\obj\x64\Release\ilc\intermediate\MDIL\MYCGenerator4OurDailyBread.rsp"' returned exit code 1	MYCGenerator4OurDailyBread	C:\Users\*****\.nuget\packages\microsoft.net.native.compiler\1.7.6\tools\Microsoft.NetNative.targets	697	
2. Then I decided to get the original one from repositry.
After turning off **System.Reactive** and didn't update all packages, it works on VS2019 preview.
3. I got an error when I was running the certification kit. Once I uninstall this App and then delete some folders could fix this problem. The folders are described in [this StackOverflow's Q&A](https://stackoverflow.com/questions/37748697/work-around-unspecified-error-in-windows-app-certification-kit).

### [2019-04-11 16:38] Using querySelector in Javascript can find the elements which are related to your query string
As described in [this StackOverflow's Q&A](https://stackoverflow.com/questions/12166753/how-to-get-child-element-by-class-name), you can use **querySelector** to get an element based on your query.
The syntax of the query is the same as the one used in CSS file.

### [2019-04-11 16:25] Finally, I use WebView to execute javascript
1. It can work; however, it takes a lot of memory.
2. The project **ForSeleniumWebDrive** I do not use it at this moment since I do not use **Selenium** at this moment.

### [2019-02-04 21:07] EdgeDriver, ChromeDriver and FirefoxDriver do not work
1. For Edge, someone ask the same question in [this forum](https://github.com/SeleniumHQ/selenium/issues/6598); however, no use for me....
2. After testing, once I launch **MicrosoftWebDriver.exe* before I launch this APP and provide its port to this APP, it works. Hm...

### [2019-02-04 13:12] Since https://odb.org has become an App like website, I had to find a new way to get its web pages
1. Just like Angular, this website is generated dynamically so that the innerHTML of **web.Load** is useless since I need its javascript code is run.
2. **web.LoadFromBrowser** just work for **WinForm** as described in [this StackOverflow's Q&A](https://stackoverflow.com/questions/46981666/htmlagilitypack-loadfrombrowser-method-not-found) and it introduced me to use [Selenium.WebDriver](https://www.seleniumhq.org/projects/webdriver/).
3. So I follow [this instruction](https://www.automatetheplanet.com/webdriver-dotnetcore2/) and [making this project is available for netstandard2.0](https://blogs.msdn.microsoft.com/dotnet/2017/10/10/announcing-uwp-support-for-net-standard-2-0/). **This Step Take Long Time**
4. If the step 3 can work, I can get the innerHTML from **selenium.WebDriver** and then feed it to **htmlAgilityPack** as described in [this web page](https://stackoverflow.com/questions/52440914/how-to-get-htmlagilitypack-htmldocument-from-selenium-driver-pagesource).## 23th Commit

## 23th Commit
### [2017-10-06 14:20]
Hm.. Today's title includes '. . .' at its end and a folder name seems cannot have '.' at its end. Well, I tried to fix it by adding an extra symbol at the end of the folder name if its title's end is '.'.
# Version 2017.1001.3.0
## 22th Commit
### [2017-10-01 20:13]
It has been submitted to Windows Store. But I found that some more characters need to be taken care. They are shown in [this article for SharePoint](https://support.microsoft.com/en-us/help/905231/information-about-the-characters-that-you-cannot-use-in-site-names--fo). So I add them into the code.  
Hm.. I am trying to add a new functionality into this code which I have never done before. Take time. ^_^
## 21th Commit
### [2017-09-30 16:47]
Fix a problem coming from the reserved characters for a folder name; besides, I also substitute the chars '&' & '#' since these two chars will confuse the url.  
You can get the rule for a suitable file and folder name from [this web site](https://msdn.microsoft.com/zh-tw/library/windows/desktop/aa365247(v=vs.85).aspx).
## 20th Commit
### [2017-09-07 01:58]
Upload a new YouTube video and update README.md.
# Version 2017.906.3.0
## 19th Commit
### [2017-09-06 16:41]
The problems are fixed.
### [2017-09-06 15:13] Fixed
Found two problems.  
1. The ':' in the search for bible verses in German one will be input as ',' so that its result will be wrong.
2. The textBox's binding will not update automatically when I click the buttons to insert or remove items. 
# Version 2017.904.3.0
### [2017-09-03 17:05] Sumbitted & 18th Commit
## 17th Commit
### [2017-09-03 19:34] Solved.
Found a problem. If the user input too large FontSize, it will freeze the Windows 10.  
Therefore, I added a testing before it notified the changing.  
However, the text in the UIControl did not change when I changed it to a reasonable value.
#### [2017-09-03 22:08]
I force it to show a *MessageDialog* when the user's input is invalid and then reset the value when the user click its button. So, the value is changed by that MessageDialog not by the property itself.
## 16th Commit
### [2017-09-02]
[This article](https://stackoverflow.com/questions/20080902/dependency-property-assigned-with-value-binding-does-not-work) teachs me how to run a command when a DependencyProperty is changed. However, that command should be a static one so that I cannot use something like **SomeUIControl.Property** to get or set their values. 
### [2017-09-02 19:23]
The Click event for Default values of view's settings has been provided. Testing.
### [2017-09-02 19:17]
The view's settings worked fine. Keep testing.
### [2017-09-01 17:15]
Well, LocalSettings just accept the generic type of data. Therefore, I cannot store an object with type of Thickness.
### [2017-08-31 20:57]
Let me add settings for 'View'
### [2017-08-31 20:55]
OurDailyBreadPage: Fix bugs for Delete a pair.
## 15th Commit
### [2017-08-30 15:57] Done 1.
Need more test.
### [2017-08-28 16:36]
Now, it can show my user control UCAddRemoveItems whose 6 click events are declared in its OurDailyBreadPage.xaml and its NumPlusItems and NumMinusItems are setting as Dependency Properties so that I can bind them in two ways.
### [2017-08-28 15:27] 
Although new uwp should have support svg file as the source of an image control, it seems not in my case. So, I export them to be png files.
### [2017-08-25 16:22] Inkscape
Try and error, now the buttons' icons have been created.
### [2017-08-25 11:15] Done 3.
### [2017-08-25 10:43] Done 2.
Yes, the listview will not be renewed once I change the texts inside a textbox. I can handle its collectionChanged event by hand now.
### [2017-08-25 10:00] Try
1. Try to let user can add or delete an item from one of the list.  Need to use Inkscape.
2. Besides, let me try to avoid handling redundant collectionChanged event.  
3. Fix the possible error gotten from empty content when she tries to generate a container for MemorizeYC.
# Version 2017.817.2.0
## 14th Commit
### [2017-08-17 21:01] 
Submission 2 to Windows Store.
## 13th Commit
### [2017-08-17 16:52]
Added two new query parameters in its URI: bsid & bookshelf for MemorizeYC.  
Besides, I also show its version at the top-right corner of setting page.
## 12th Commit
### [2017-08-17 16:04]
Add in a parameter named IsIgnoreDueDate to tell MemorizeYC to ignore this Container if that App want to count the due date.
At this moment, MemorizeYC is still out of this functionality.
## 11th Commit
### [2017-08-17 14:29]
Using *binding.UpdateSource()* to update the binding of a TextBox because a TextBox will not lose its Focus when the user click on a Button.  
By the way, I also set all the TextBoxes' *AcceptsReturn* Property to be true so that the user can input multiple lines now.
### [2017-08-17 14:14]
Add a newline for a bibleContent when it encountered <br/>, \<p\> by checking *inode.Name* or \</p\> by checking *inode.PreviousSibling.Name*.
# Version: 2017.808.1.0
## 9th & 10th Commit
### [2017-08-10 15:21]
Add in the Privacy Policy.
## Before 7th Commit
### [2017-08-08 18:00]
It has been submitted into Windows Store. Now, I want to submit it into GitHub.
### [2017-08-08 10:04]
I have used Inkscape to create one svg file and use the extension [UWP Visual Assets Generator](https://visualstudiogallery.msdn.microsoft.com/b3c94468-96bc-4860-8860-4458ab3bc467/view/) to generate all icons for this APP.  
It's time to try to submit this APP. ^_^_
### [2017-08-05 12:35]
Now I can launch MemorizeYC from this App. Remember that, you need to provide its `TargetApplicationPackageFamilyName` which can be found as described in [this StackOverflow's Q&A](https://stackoverflow.com/questions/40456200/universal-app-folderpicker-system-runtime-interopservices-comexception).
### [2017-08-05 11:45]
I have let it can support MRU, get folder from *FolderPicker* (No matter how you need to set its **FileTypeFilter** even you don't need to use it.) create folders for MYContainer.
## Before 5th commit
### [2017-08-02 16:40]
Add an AppButtonBar with *Settings*, *Create a category for MYC* appBarButton.

### [2017-08-02 16:37] 
Now I can play the audio; however, I still do not have a clear view about how to use *Position* and *NaturalDuration* of a MediaElement to control the audio.

### [2017-08-02 14:33] 
Now, I can store the languages into *ApplicationData.Current.LocalSettings* every time they change the language, so that they can get their desired languages when they launch this App.

### [2017-08-02 11:00] 
set the default languages for content and answer based on the user's Language.

### [2017-08-02 10:59]
 Try to write a journal in this Journal.
## Start