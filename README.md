# Keyboard Lock App

English | [日本語](README.ja.md)

# What does this app do?
While running, the app prevents any keyboard input from being accepted.

# What is it used for?
It is useful when you want to clean your keyboard while keeping your PC powered on.  
(This feature was once included in an old freeware called *“Mado no Te”*.)

# How to use
When launched, the app window appears at the bottom-right corner of the main monitor.  
While the app is running, all keyboard input is ignored, so you can safely clean the keyboard.  
After cleaning, close the app by clicking the **×** button.

# Notes
For wireless keyboards, you don’t need this app—just switch the keyboard off.

# Known issues
## Incorrect initial window position
Originally, the window was intended to appear at the bottom-right corner of the main display without any gap.  
However, the window size cannot be retrieved accurately, and a slightly larger value is obtained, leaving a small gap.  
This seems to be a WinForms issue, and it is unlikely to be fixed.
