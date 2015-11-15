Targets
=======

Sake build steps are organized into targets.  Targets are defined in a ``.shade`` file as an element starting with a ``#`` and can be set up to be dependent on each other.

An example ``.shade`` file illustrating the topics presented in this page appears at the end of the page.  Be sure to review the ``build.cmd`` file as it changes slightly to pass a target to Sake.

Default Target
--------------

The first target in a ``.shade`` file is the default target.  If Sake is executed without specifying a target, the default target is executed.

Dependencies
------------

To indicate that a target ``target-b`` depends on ``target-a`` to run before it, add ``.target-a`` to the declaration of ``target-b``:

.. code-block:: aspx-cs

	#target-b .target-a

For example:

.. code-block:: aspx-cs

	#target-a
		log info='target a'

	#target-b .target-a
		log info='target b'

	#target-c .target-b
		log info='target c'

When run specifying ``target-c``, the following output is produced::

	>build.cmd target-c
	info: target a
	info: target b
	info: target c

Dependencies can also be specified from the predecessor by using the ``target`` attribute:

.. code-block:: aspx-cs

	#target-1 target="target-2"
		log info='target 1'

	#target-2 target="target-3"
		log info='target 2'

	#target-3
		log info='target 3'

Running ``target-3`` executes ``target-1`` and ``target-2`` as expected:

.. code-block:: aspx-cs

	>build.cmd target-3
	info: target 1
	info: target 2
	info: target 3

Example
-------

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
	packages\Sake\tools\Sake.exe -f makefile.shade %*

``makefile.shade``

.. code-block:: aspx-cs

	use namespace="System"

	#default
		log info='default'

	#target-c .target-b
		log info='target c'

	#target-a
		log info='target a'

	#target-b .target-a
		log info='target b'

	#target-3
		log info='target 3'

	#target-1 target="target-2"
		log info='target 1'

	#target-2 target="target-3"
		log info='target 2'