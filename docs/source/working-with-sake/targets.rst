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

.. code-block:: c#

   #target-b .target-a

For example:

.. code-block:: c#

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

.. code-block:: c#

   #target-1 target="target-2"
      log info='target 1'

   #target-2 target="target-3"
      log info='target 2'

   #target-3
      log info='target 3'

Running ``target-3`` executes ``target-1`` and ``target-2`` as expected:

.. code-block:: c#

	>build.cmd target-3
	info: target 1
	info: target 2
	info: target 3

Example
-------

``build.cmd``

.. literalinclude:: ../../samples/working-with-sake/targets/build.cmd
		:language: bat
		:emphasize-lines: 24

``makefile.shade``

.. literalinclude:: ../../samples/working-with-sake/targets/makefile.shade
		:language: c#
