Getting Started
===============

To get started with Sake, create a directory and create the following two files:

``build.cmd``

.. literalinclude:: ../../samples/getting-started/build.cmd
        :language: bat

``makefile.shade``

.. literalinclude:: ../../samples/getting-started/makefile.shade
        :language: c#

.. note:: Andrew Stanton-Nurse has a Sublime 3 package that adds colorization for .shade files:  `Sublime-Sake`_

.. note:: The Spark view engine supports template files using off-side rule formatting where indentation denotes structure, as in Python, Jade, and Haml.  These files have a ``.shade`` file extension to differentate them from ``.spark`` template files, which use opening and closing tags for structure.

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