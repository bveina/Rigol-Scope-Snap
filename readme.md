# Rigol Scope Snap
A lightweight application that uses the NI-runtime/ SCPI-interface to take
screen shots of the MSO5000 series oscilloscope.

![Interactive or Advanced Mode](/images/InteractiveMode.PNG)

Additionally, basic control of the scope is accomplished through dedicated
buttons and the touchscreen interface.

# Installing
This app relies on the NI runtime available: https://www.ni.com/en-us/support/downloads/drivers/download.ni-visa.html

Other than that requirement, the app is standalone.


# Usage
![Startup Screen](/images/OnStartup.PNG)
## Connecting to your scope ( Shortcut: Ctrl+N)
When starting Scope Snap, you must choose a device to connect to. clicking
search will attempt to find all USB devices connected to your machine. If only
device is found Scope Snap will attempt to auto connect to that device. And grab
the first image to display on screen.

If multiple devices are found, select from the available list items then press
connect.

**Warning: There is currently no checking if your device is a MSO5xxx. This
could result in unintended side effects.**

### Connecting over the network
If your scope is on a network you can still connect to the scope.
first find your scopes VISA Address in _Utility->IO->LAN_ it should be in the
form ```TCPIP::192.168.0.100::INSTR``` where the ip in the middle will be the IP
address of the scope.

Then double click on the grayed out text box to enable editing and paste the
VISA connection string into the textbox.

finally click connect, now you can control your scope over the network too!

![Successfully Connected](/images/OnConnect.PNG)

## Saving Images (Shortcut: Ctrl+S)
Saving the currently shown image can be accomplished in several ways:
* The _Save_ button on the bottom right.
* the _File -> save_ menu option
* the _Ctrl + S_ shortcut key.

Regardless of how a save is initiated, a file dialog will take you through a
normal "save file" workflow. at this time, only BMP format is supported. this is
the native format that the MSO5xxx outputs.

## Copying Images (Shortcut: Ctrl+C)
If you need to use an image in another program you can use the clipboard to
transfer you file using "Edit->Copy" or "Ctrl+C". You can then paste into your
preferred editing application with your preferred paste method.


## Interacting
The mouse can be used to interact with the Scope in several ways.
1. Left Clicking on the image will simulate a touch screen press at that location.
2. Right Clicking on the image will cause a new image to be retrieved from the Scope.
3. Users can use the Soft Keys described in the options menu section.

## Live Mode (unstable)
Live mode will refresh the screen image roughly once a second. It can be
activated by checking either:
* The Live Checkbox in the lower Right.
* The _Options->Live_ menu item.

This isn't implemented in a thread-safe way, mouse clicks can be lost as a result.
Please be patient and submit bug reports as needed. Alternatively only update
the screen when you need to using the _Mouse Right Click_.

## Options menu
The Options Menu can change how the UI appears and how the Image Appears.
* Invert - Uses the Scopes Invert Function to provide a white background. This is
the default, as it looks better in reports.
* Grayscale - makes the image only shades of black and white...thats it.
* Live - Enables or Disables Wide mode.
* Show Buttons - adds/removes a toolbox of buttons on the left side.
* Show Right Panel - adds/removes the connection panel on the right side of the
screen.
