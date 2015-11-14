Element Tags and C#
===================

This page shows how to work with element tags and C# in a Sake ``.shade`` file.  

Working with Element Tags
-------------------------

Sake uses a custom version of the Spark view engine to process ``.shade`` files, which allows for the use of custom element tags.  In the example below, ``log`` is an element tag with an attribute named ``info``::

    log info='testing'

``log`` is part of a library of custom elements that is defined in the Sake project.  You can also create your own elements, which is documented in another page.

The following ``.makefile`` illustrates the basics of working with element tags and can be run using the ``build.cmd`` file from :doc:`/getting-started/index`.  Sake requires at least one target, so we define one here named ``#default``.  Targets are explained in detail later in the documentation.

Interesting things to note:

* Strings are delimited with single or double quotes.
* Element tags that are not indented run before targets.
::

	-// example of a single-line comment

	-/* 
		example of a 
		multi-line comment
	*/

	log warn="This executes first."

	#default
		log info='Hello world'

	log warn="This also executes before the target."

Running the file above produces the following output::

	warn: This executes first.
	warn: This also executes before the target.
	info: Hello world

Working with C#
---------------

C# code can be used as a code block, delimited with ``@{`` and ``}``:

.. code-block:: aspx-cs

    @{
    	var message = "Hello world!";
    	Log.Info(message);
    }

C# can also appear in element tags, delimited with ``${`` and ``}``:

.. code-block:: aspx-cs

	log info="The current date and time is ${DateTime.Now.ToString()}."

.. note:: Version 0.2.2 of Sake targets .NET 4.0, which corresponds to C# 4.

String Delimiters
^^^^^^^^^^^^^^^^^

As with element tags, strings in C# code can be delimited using either single or double quotes.  This raises the interesting problem of working with char variables in C#.  For example, the following code will generate an exception in a ``.shade`` file:

.. code-block:: aspx-cs

   var tokens = "a,b,c".Split(',');

The ``','`` argument is treated as a string, and an exception will be thrown because ``Split`` expects a char.  To work around this, cast to char:

.. code-block:: aspx-cs

   var tokens = "a,b,c".Split((char)',');

Namespaces
^^^^^^^^^^

Sake provides a ``use`` element that is analogous to the ``using`` directive in C#.  In the example below, ``Console`` and ``Path`` do not need to be fully qualified because the ``System`` and ``System.IO`` namespaces are specified by the ``use`` elements:

.. code-block:: aspx-cs

	use namespace="System"
	use namespace="System.IO"

	#default
		@{
			Console.WriteLine(Directory.GetCurrentDirectory());
		}

Examples
--------

The following example .makefile illustrates the basics of working with C# and element tags in Sake.  
