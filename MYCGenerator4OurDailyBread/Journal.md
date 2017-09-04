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