Summary
-------
The solution considers of 2 Visual Studio solutions. 
1. Web Application (MarketingWebApp)
2. Monitoring Application (MarketingMonitor)

References
----------
I googled (mainly stack exchange).

Web Application (MarketingWebApp)
---------------------------------
1. The web application allows users to upload files and to view reports from the database (persons and their orders).
2. No styling (no CSS, no Javascript).
3. A simple navigation bar allows users to switch between the upload and the report.
4. Users can upload multiple files.
5. Uploaded files are validated before being saved to the XML folder.
6. The report shows all people and a HTML link goes to the orders for that person.
7. Unity is used to inject dependencies into various classes.
8. After uploading a list of the files is displayed with the status of each upload.

Monitoring Application (MarketingMonitor)
-----------------------------------------
1. The Marketing Monitor is a C# library that starts a .NET FileWatcher. The FileWatcher monitors the XML folder. The monitor has 2 threads which write to the database and to the report file. When a new file arrives the monitor passes the file name to the database writer and the report writer.
2. No check is done for the uniqueness of a person. Duplicates are possible if the same file is uploaded again.
3. Reports are writen to a text file.
4. The database writer and the report writer run concurrently.
5. The monitor does NOT detect files that are already in the XML folder. Only files that arrive after the application starts are detected.

Console Application MarkertingServiceCA
---------------------------------------
To test the MarketingMonitor a console application MarkertingServiceCA starts the monitor and runs until the user presses enter. This simulates e.g. a Windows Service.

Settings
--------
Settings in the Web.Config of MarketingWebApp
1. XMLFolder (location where to store XML files)
2. XSDPathAndName (location and name of the XSD file for validating XML files)
3. ConnectionString (database connection string)

Settings for the Monitor
The monitor requires the following settings. They are passed via the command line to the console application which passes the settings to the monitor. The command line order is:
1. XMLFolder (location where to store XML files)
3. ConnectionString (database connection string)
3. ReportPathAndName (location and name of the report file)

Logging
-------
Log4Net is used.

Configuration
-------------
log4net.config 

Database
--------
1. Code first was used to create the database.
2. The monitor creates the database if one does not exist.
3. Web site will should also create the database if none exists.

Building
--------
1. First build the MarketingMonitor solution. Because the web app depends on it.
2. Then build MarketingWebApp.

Running
-------
1. Start the monitor console application (remember to set as startup project).
2. Start the web site. (Upload files which will trigger the monitor which creates the database.)

XML
---
xsd.exe was used to generate C# classes from the xsd.

Dependencies
------------
(All dependencies managed via nuget.)
1. log4net 2 (the projects use log4net.config)
2. EntityFramework 6
3. Unity 4 (for dependency injection)

Issues and Todos
----------------
-Investigate replacement FileWatcher.
-Implement robust file-is-locked detection.
-Code migrations not used for Entity Framework.
-Error handling is minimal.
-Files that are processed need to be flagged (e.g. rename extension)
-All person records returned from database and displayed on website (need to implement paging).
-ViewModels in the web app need to be refactored. They include models from the database.
-Duplicate data can be upload.
-No unit tests. But I have used dependency injection and interfaces to decouple the classes which will allow for unit testing.
-The web app has dependencies on the monitoring application.
-In the web app, the process classes are tightly coupled to Asp .NET MVC classes e.g. HttpPostedFileBase.
-Investigate concurrency in the Multitasker.
-Console app slow to close. Investigate if Multitasker stops immediately.
-The monitor does NOT detect files that are already in the XML folder. Only files that arrive after the application starts are detected.
