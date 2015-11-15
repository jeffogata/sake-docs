Extending Sake
--------------

``.shade`` files containing functions, classes, or custom element tags can be imported into the primary build file.  These files can be placed in a directlry, which is then provided to Sake using the ``I`` argument.  For example, the following command would run Sake with import files in a directory named ``imports``::

	Sake.exe -I imports -f makefile.shade %*

Custom Functions
^^^^^^^^^^^^^^^^

Import files can contain C# functions and classes in a functions code block:

.. code-block:: aspx-cs

	use namespace="System"
	use namespace="System.Collections.Generic"

	functions
		@{
			private List<CustomItem> _items = new List<CustomItem>();

			public void AddCustomItem(string name)
			{
				_items.Add(new CustomItem { Name = name });
			}

			public void PrintCustomItems()
			{
				foreach(var item in _items)
				{
					Console.WriteLine(item.Name);
				}
			}

			public class CustomItem
			{
				public string Name { get; set; }
			}
		}

These functions can be included in another ``.shade`` file using the ``import`` element:

.. code-block:: aspx-cs

	use import="CustomFunctions"

	#default
		@{
			AddCustomItem('foo');
			AddCustomItem('bar');
			AddCustomItem('baz');
			PrintCustomItems();
		}

Running this produces the following output::

	>build.cmd
	foo
	bar
	baz

Custom Element Tags
^^^^^^^^^^^^^^^^^^^

Import files can also be used to create custom element tags.  To create a custom element, name the file with a leading underscore;  the remainder of the file name will then be the element name.  Within the file, default values for attributes can be specified, and any attribute values not provided with default values must be provided when the element is used.

The following simple example defines a default value of ``"Hello"`` for the ``greeting`` attribute.  A value will be required for the ``name`` attribute when the element is used.

.. code-block:: aspx-cs

	default greeting='Hello'

	@{
		Log.Info(greeting + " " + name);
	}

If the sample above is saved as ``_echo.shade``, it can be used in a target like so:

.. code-block:: aspx-cs

	#echotag
		echo name="Bob"

::

	>build.cmd echotag
	info: Hello Bob

To use a custom element in C# code, you can define a ``macro``:

.. code-block:: aspx-cs

	macro name='Echo' name='string' greeting='string'
   		echo	 

The macro can then be called as you would a C# function:

.. code-block:: aspx-cs

	#echomacro
		@{
			Echo("Jack", "Good morning");
		}

Examples
^^^^^^^^

The following files include the code samples in this page.  The ``build.cmd`` file calls Sake specifying an import folder:

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
	packages\Sake\tools\Sake.exe -I imports -f makefile.shade %*

Save the ``makefile.shade`` file in the same folder as the ``build.cmd`` file:

.. code-block:: aspx-cs

	use import="CustomFunctions"

	#default
		@{
			AddCustomItem('foo');
			AddCustomItem('bar');
			AddCustomItem('baz');
			PrintCustomItems();
		}

	#echotag
		echo name="Bob"

	#echomacro
		@{
			Echo("Jack", "Good morning");
		}

	macro name='Echo' name='string' greeting='string'
	   echo	 

Create an ``imports`` folder within the folder containing the ``build.cmd`` file and create the following files in it.

``CustomFunctions.shade``:

.. code-block:: aspx-cs

	use namespace="System"
	use namespace="System.Collections.Generic"

	functions
		@{
			private List<CustomItem> _items = new List<CustomItem>();

			public void AddCustomItem(string name)
			{
				_items.Add(new CustomItem { Name = name });
			}

			public void PrintCustomItems()
			{
				foreach(var item in _items)
				{
					Console.WriteLine(item.Name);
				}
			}

			public class CustomItem
			{
				public string Name { get; set; }
			}
		}

``_echo.shade``:

.. code-block:: aspx-cs

	default greeting='Hello'

	@{
		Log.Info(greeting + " " + name);
	}