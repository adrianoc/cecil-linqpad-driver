# What is it?

This project implements a custom driver for [LINQPad](www.linqpad.net) in order to enable users to perform queries on .NET metadata (for instance, to list all methods with a specific pattern in the name) in an easy and flexible way.

You can read more about it in [this blog post](http://programing-fun.blogspot.com/2016/06/to-infinity-and-beyond-more-powerful.html)

# How to install ?

Installing it is as simple as:

1. Download the [custom driver](https://github.com/adrianoc/cecil-linqpad-driver/Pre-Compiled/Cecil.LINQPad.Driver.lpx)
2. In LINQPad, click in the **Add connection** -> **View more drivers** -> **Browse** and finally select the custom driver file you just downloaded

Alternatively you can also build it yourself:

1. Clone the repository ([or download the sources](https://github.com/adrianoc/cecil-linqpad-driver))
2. Build the project (VS 2015) (you'll need to fix the reference to LINQPad executable)
3. ZIP the contents of the build folder (bin/debug) (excluding LINQPad executable) and change the extension of the resulting file to .lpx

# Lincense

The whole project is released under the MIT license.


# How to contribute ?

The most effective way to contribute to the project is by using it and reporting any issues you find and/or adding possible feature requests. You can also contribute code to the project as long as your code abides by the lincense used in the rest of the code.