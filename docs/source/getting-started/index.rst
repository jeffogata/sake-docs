Getting Started
===============

To get started with Sake, create a directory and create the following 2 files:

``build.cmd``

.. code-block:: bat

    @echo off
    cd %~dp0
    
    SETLOCAL
    SET NUGET_VERSION=latest
    SET CACHED_NUGET=%LocalAppData%\NuGet\nuget.%NUGET_VERSION%.exe
    
    IF EXIST %CACHED_NUGET% goto copynuget
    echo Downloading latest version of NuGet.exe...

    IF NOT EXIST %LocalAppData%\NuGet md %LocalAppData%\NuGet
    @powershell -NoProfile -ExecutionPolicy unrestricted -Command "$ProgressPreference = 'SilentlyContinue'; Invoke-WebRequest 'https://dist nuget.org/win-x86-commandline/%NUGET_VERSION%/nuget.exe' -OutFile '%CACHED_NUGET%'"

    :copynuget
    IF EXIST .nuget\nuget.exe goto restore
    md .nuget
    copy %CACHED_NUGET% .nuget\nuget.exe > nul
    
    :restore
    IF EXIST packages\Sake goto run
    .nuget\NuGet.exe install Sake -ExcludeVersion -Source https://www.nuget.org/api/v2/ -Out packages
    
    :run
    packages\Sake\tools\Sake.exe -f makefile.shade

``makefile.shade``

.. code-block:: c#

    #default
       @{
   	      Log.Info("Hello world!");
       }

.. note:: Andrew Stanton-Nurse has a Sublime 3 package that adds colorization for .shade files:  `Sublime-Sake`_

.. _Sublime-Sake: https://github.com/anurse/Sublime-Sake

Run the build:
::
    >build.cmd
    Attempting to gather dependencies information for package 'Sake.0.2.2' with respect to project 'packages', targeting 'Any,Version=v0.0'
    Attempting to resolve dependencies for package 'Sake.0.2.2' with DependencyBehavior 'Lowest'
    Resolving actions to install package 'Sake.0.2.2'
    Resolved actions to install package 'Sake.0.2.2'
    Adding package 'Sake.0.2.2' to folder 'packages'
    Added package 'Sake.0.2.2' to folder 'packages'
    Successfully installed 'Sake 0.2.2' to packages
    info: Hello world!

The build file will restore the Sake nuget package and write out the log message.

Congratulations!  You've created your first Sake build.