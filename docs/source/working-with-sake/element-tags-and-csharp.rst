Element Tags and C#
===================

``.shade`` files use an offside-rule format, like Python, Jade, or Haml, which means that indentation determines structure.  ``.shade`` templates can contain a mix of element tags and C# code.  The concept of element tags in a ``.shade`` file makes sense when you consider that ``.shade`` files are templates processed by the Spark view engine into a dynamically generated class.  Originally, these classes would have been used to generate a response in a web site (think Razor and ``.cshtml`` files); Sake repurposes Spark to process ``.shade`` template files into classes that run a build.

Working with Element Tags
-------------------------

Sake comes with a `standard set`_ of element tags, like ``exec`` and ``log``, and you can also create your own :ref:`custom-element-tags`.  A ``.shade`` file will also use element tags from Spark, like ``use`` and ``macro``.  See the `Spark elements reference`_ for more information.

.. note::  I have not tried to use all of the Spark elements in a Sake build file, and some may not make sense to use in a build file, or may not work as described in the Spark documentation.

The following ``.shade`` file illustrates the basics of working with element tags and can be run using the ``build.cmd`` file from :doc:`/getting-started/index`.  Sake requires at least one target, so we define one here named ``#default``.  :ref:`targets` are explained in detail later in the documentation.

Interesting things to note:

* Strings are delimited with single or double quotes.
* Element tags that are not indented run before targets.

.. literalinclude:: ../../samples/working-with-sake/element-tags-and-csharp/working-with-element-tags/makefile.shade
		:language: c#

Running the file above produces the following output::

	warn: This executes first.
	warn: This also executes before the target.
	info: Hello world

.. _`standard set`: https://github.com/sakeproject/sake/tree/master/src/Sake.Library/Shared
.. _`Spark elements reference`: https://github.com/SparkViewEngine/spark/wiki/Elements-Reference

Working with C#
---------------

C# code can be used as a code block, delimited with ``@{`` and ``}``:

.. code-block:: c#

   @{
      var message = "Hello world!";
      Log.Info(message);
   }

C# can also appear in element tags, delimited with ``${`` and ``}``:

.. code-block:: c#

   log info="The current date and time is ${DateTime.Now.ToString()}."

.. note:: Version 0.2.2 of Sake targets .NET 4.0, which corresponds to C# 4.

String Delimiters
^^^^^^^^^^^^^^^^^

As with element tags, strings in C# code can be delimited using either single or double quotes.  This raises the interesting problem of working with char variables in C#.  For example, the following code will generate an exception in a ``.shade`` file:

.. code-block:: c#

   var tokens = "a,b,c".Split(',');

The ``','`` argument is treated as a string, and an exception will be thrown because ``Split`` expects a char.  To work around this, cast to char:

.. code-block:: c#

   var tokens = "a,b,c".Split((char)',');

Namespaces
^^^^^^^^^^

The ``use`` element is analogous to the ``using`` directive in C#.  In the example below, ``Console`` and ``Directory`` do not need to be fully qualified because the ``System`` and ``System.IO`` namespaces are specified by the ``use`` elements:

.. code-block:: c#

   use namespace="System"
   use namespace="System.IO"

   #default
      @{
         Console.WriteLine(Directory.GetCurrentDirectory());
      }

The following ``.shade`` file shows the basics of working with C# in Sake, and also how you can work with both C# and tags in the same build file.

.. literalinclude:: ../../samples/working-with-sake/element-tags-and-csharp/working-with-csharp/makefile.shade
		:language: c#

This produces the following output::

	>build.cmd
	Hello world using C#!
	info: Hello world using tags!  It is 11/14/2015 12:24:29 PM
