# Version: 0
## Start
### [2017-08-02 10:59]
 Try to write a journal in this Journal.

### [2017-08-02 11:00] 
set the default languages for content and answer based on the user's Language.

### [2017-08-02 14:33] 
Now, I can store the languages into *ApplicationData.Current.LocalSettings* every time they change the language, so that they can get their desired languages when they launch this App.

### [2017-08-02 16:37] 
Now I can play the audio; however, I still do not have a clear view about how to use *Position* and *NaturalDuration* of a MediaElement to control the audio.

### [2017-08-02 16:40]
Add an AppButtonBar with *Settings*, *Create a category for MYC* appBarButton.

## After 5th commit
## [2017-08-05 11:45]
I have let it can support MRU, get folder from *FolderPicker* (No matter how you need to set its **FileTypeFilter** even you don't need to use it.) create folders for MYContainer.
## [2017-08-05 12:35]
Now I can launch MemorizeYC from this App. Remember that, you need to provide its `TargetApplicationPackageFamilyName` which can be found as described in [this StackOverflow's Q&A](https://stackoverflow.com/questions/40456200/universal-app-folderpicker-system-runtime-interopservices-comexception).
