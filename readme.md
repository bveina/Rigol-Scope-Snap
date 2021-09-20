# Rigol Scope Snap
A lightweight application that uses the NI-runtime/ SCPI-interface to take
screen shots of the MSO5000 series oscilloscope.

![Interactive or Advanced Mode](/images/InteractiveMode.PNG)

Additionally, basic control of the scope is accomplished through dedicated
buttons and the touchscreen interface.

## Table of Contents
- [How To Install](#installing)
- [Connecting to your Scope](#connecting-to-your-scope--shortcut-ctrln)
- [Saving to a File ](#saving-images-shortcut-ctrls)
- [Copying to Clipboard](#copying-images-shortcut-ctrlc)
- [Using the SCPI interface](#scpi-interface)
- [Extending to new Devices](#extending-to-new-devices)
- [Building from Source](#building-from-source)

# Installing
This app relies on the NI runtime available: https://www.ni.com/en-us/support/downloads/drivers/download.ni-visa.html

Other than that requirement, the app is standalone.


# Usage
![Startup Screen](/images/OnStartup.PNG)
## Connecting to your scope ( Shortcut: Ctrl+N)
When starting Scope Snap, the application will automatically attempt to connect to a scope if no errors occurred on the last run of the program. If the application does not auto search you must choose a device to connect to. Clicking search will attempt to find all USB devices connected to your machine. If only one device is found Scope Snap will attempt to auto connect to that device and grab the first image to display on screen.

If multiple devices are found, select from the available list items then press connect.

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

## Options menu
The Options Menu can change how the UI appears and how the Image Appears.
* Invert - Uses the Scopes Invert Function to provide a white background. This is
the default, as it looks better in reports.
* Grayscale - makes the image only shades of black and white...thats it.
* Live - Enables or Disables Wide mode.
* Show Buttons - adds/removes a toolbox of buttons on the left side.
* Show Right Panel - adds/removes the connection panel on the right side of the
screen.
* Advanced Panel - Manually Communicate via the SCPI interface (ascii only, no TMC or raw packets.)


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

## Live Mode
Live mode is on by default unless the program was unable to connect to a scope on the last run.
Live mode will refresh the screen image roughly once a second. It can be toggled by checking either:
* The Live Checkbox in the lower Right.
* The _Options->Live_ menu item.

As of Version 1.5 there is now an option to adjust the refresh rate. This will be a matter of personal preference to most users. the software defaults to 2000mS at startup.

- For a computer-only interface 1000mS is nice, but can caused miss button presses.
- If using the scope touchscreen while in live mode, using 2000 mS or greater.

## SCPI interface
![SCPI Command Box](/images/SCPICommands.PNG)
Advanced Users may wish to send raw SCPI commands or queries to the scope. users may issue multiple commands at once by placing sequential commands on multiple lines then selecting "Write". to get the banner image (AM modulation with color-grading) try pasting the code below into the SCPI command box.

```
*RST
SAVE:IMAGE:INVERT ON
SOURCE1:FREQ 1000000
SOURCE1:OUTPUT ON
SOURCE1:MOD:TYPE AM
SOURCE1:MOD:AM:INTernal:FREQuency 1000
TIMEBASE:SCALE 500uS
TRIGger:EDGE:LEVel 0.200
DISPlay:COLor ON
```



Multiple Queries can also be accomplished in the same way.

## Extending to new devices
As of Version 1.6 you can augment an `InstrumentList.xml` file to add support for new devices.
The Schema is included in the repo but it is also possible to just copy an existing similar instrument and then tweak it for your purposes. this file is loaded Dynamically at runtime to try to identify what type of device is being connected to.

Every instrument should have:
- A RegularExpressionString to identify it uniquely. (eg `MSO5[0-9][0-9][0-9]`)
- The SCPI command used to query the image from the device (eg `DISP:DATA?`)
- An enumeration for processing the raw bytestream that is returned for the SCPI query.
 - these are limited to GetImage, GetJFIF, GetRaw8bpp, GetRaw1bpp
  - GetImage assumes a TMC packet with a full bmp file being transmitted.
  - GetJFIF assumes a full jpg file from the instrument.
  - GetRaw8bpp assumes a data-stream of pixel data with no header in RR GGG BBB format.
  - GetRaw1bpp assumes a data-stream of pixel data with no header with 1 bit per pixel
- A list of button that should be shown in the side panel. each button should have two attributes.
 - Display: the user friendly name of the button.
 - Command: the SCPI command that will be sent when you press the virtual button.
- other items are listed in the schema `SCPI-instrument-definition.xsd`

## Building from source

I'd like to thing that if you have Visual Studio installed on your machine, it should be as easy as hitting Build.
but the most likely stumbling block will be generating ```SCPI-instrument-definition.cs``` this is done with the XSD tool that comes with Visual Studio.
there is a prebuild step to auto generate the file before build time but the path to XSD.exe is hardcoded ``` C:\Program Files (x86)\Microsoft SDKs\Windows\v10.0A\bin\NETFX 4.6.1 Tools\x64\xsd.ex``` should be changed to point to your installed netfx tools location.
