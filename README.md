# What is it?

This project implements a custom driver for [LINQPad](www.linqpad.net) in order to enable users to perform queries on .NET metadata (for instance, to list all methods with a specific pattern in the name) in an easy and flexible way.

You can read more about it in [this blog post](http://programing-fun.blogspot.com/2016/06/to-infinity-and-beyond-more-powerful.html)

# How to install ?

Installing it is as simple as:

1. Download the [custom driver](https://github.com/adrianoc/cecil-linqpad-driver/blob/master/Pre-Compiled/Cecil.LINQPad.Driver.lpx6)
2. In LINQPad, click in the **Add connection** -> **View more drivers** -> **Browse** and finally select the custom driver file you just downloaded
3. At this point you' ll be prompted to select a folder with .NET assemblies to be used in your queries.

Alternatively you can also build it yourself (instead of downloading):

1. Clone the repository ([or download the sources](https://github.com/adrianoc/cecil-linqpad-driver))
2. Make sure you have .NET 6.0 installed
3. Make sure you have 7-Zip in your path
4. run Build\package.zip
5. The custom driver will be stored in Pre-Compiled/Cecil.LINQPad.Driver.lpx6

# Lincense

The whole project is released under the Apache 2.0 license.

# How to contribute ?

The most effective way to contribute to the project is by using it and reporting any issues you find and/or adding possible feature requests. You can also contribute code to the project as long as your code abides by the lincense used in the rest of the code.
