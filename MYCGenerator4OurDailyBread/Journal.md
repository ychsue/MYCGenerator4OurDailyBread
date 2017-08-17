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
# Version: 2017.808.1.0