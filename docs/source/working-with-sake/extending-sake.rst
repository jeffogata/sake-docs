Extending Sake
--------------

``.shade`` files containing functions, classes, or custom element tags can be imported into the primary build file.  These files can be placed in a directory, which is then provided to Sake using the ``I`` argument.  For example, the following command would run Sake with import files in a directory named ``imports``::

	Sake.exe -I imports -f makefile.shade %*

Custom Functions
^^^^^^^^^^^^^^^^

Import files can contain C# functions and classes in a functions code block:

.. literalinclude:: ../../samples/working-with-sake/extending-sake/imports/CustomFunctions.shade
		:language: c#

These functions can be included in another ``.shade`` file using the ``import`` element:

.. literalinclude:: ../../samples/working-with-sake/extending-sake/makefile.shade
		:language: c#
		:lines: 1-9

Running this produces the following output::

	>build.cmd
	foo
	bar
	baz

.. _custom-element-tags:

Custom Element Tags
^^^^^^^^^^^^^^^^^^^

Import files can also be used to create custom element tags.  To create a custom element, name the file with a leading underscore;  the remainder of the file name will then be the element name.  Within the file, default values for attributes can be specified, and any attribute values not provided with default values must be provided when the element is used.

The following simple example defines a default value of ``"Hello"`` for the ``greeting`` attribute.  A value will be required for the ``name`` attribute when the element is used.

.. literalinclude:: ../../samples/working-with-sake/extending-sake/imports/_echo.shade
		:language: c#

If the sample above is saved as ``_echo.shade``, it can be used in a target like so:

.. literalinclude:: ../../samples/working-with-sake/extending-sake/makefile.shade
		:language: c#
		:lines: 11-12

Running the ``echotag`` target produces the following output::

	>build.cmd echotag
	info: Hello Bob

To use a custom element in C# code, you can define a ``macro``:

.. literalinclude:: ../../samples/working-with-sake/extending-sake/makefile.shade
		:language: c#
		:lines: 19-20

The macro can then be called as you would a C# function:

.. literalinclude:: ../../samples/working-with-sake/extending-sake/makefile.shade
		:language: c#
		:lines: 14-17

Examples
^^^^^^^^

The following files include the code samples in this page.  The ``build.cmd`` file calls Sake specifying an import folder:

.. literalinclude:: ../../samples/working-with-sake/extending-sake/build.cmd
		:language: bat
		:emphasize-lines: 24

Save the ``makefile.shade`` file in the same folder as the ``build.cmd`` file:

.. literalinclude:: ../../samples/working-with-sake/extending-sake/makefile.shade
		:language: c#

Create an ``imports`` folder within the folder containing the ``build.cmd`` file and create the following files in it.

``CustomFunctions.shade``:

.. literalinclude:: ../../samples/working-with-sake/extending-sake/imports/CustomFunctions.shade
		:language: c#

``_echo.shade``:

.. literalinclude:: ../../samples/working-with-sake/extending-sake/imports/_echo.shade
		:language: c#
