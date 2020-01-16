<a href="https://skyhop.org"><img src="https://app.skyhop.org/assets/images/skyhop.svg" width=200 alt="skyhop logo" /></a>

----

# AprsClient

The Aprs project contains everything to get started to parse APRS messages. This project is focused on working with APRS messages from Open Glider Net (known as OGN flavoured APRS).

This project has been kickstarted by the code found on [aprs.codeplex.com](aprs.codeplex.com). The following edits have been made:

- The TCP library has been replaced to facilitate high throughput with low CPU usage. (The TCP library can be found at [https://github.com/Boerman/Boerman.TcpLib](https://github.com/Boerman/Boerman.TcpLib))
- A object oriented wrapper has been crafted for this library to be a bit more modular in use
- Event handlers have been added to be able to easily plug in on incomming messages
- Support for [OGN flavoured APRS](http://wiki.glidernet.org/wiki:ogn-flavoured-aprs) has been added


## Installing the library (NuGet)

The library is available for download on [NuGet](https://www.nuget.org/packages/Skyhop.Aprs.Client):

    Install-Package Skyhop.Aprs.Client


## Configuring the library

Configuration of this library happens through a config file. Please create a file named `appconfig.json` in the root of your project and set it to copy to your build output. Configuration in the JSON file goes as follows:

    {
      "aprsClient": {
        "uri": "aprs.glidernet.org",
        "port": "14580",
        "callsign": "0",
        "password": "-1",
        "filter": "t/poimqstunw",
        "useOgnAdditives": "true"
      }
    }

For more information about the possible configuration check out [AprsConfig.cs](Skyhop.Aprs.Client/AprsConfig.cs).


## Using the library

***Please be aware this library expects to work with OGN flavoured APRS by default! Use the `App.Config` file to turn this off.***

To start listening for incoming APRS messages you can kickstart the library:

    using Skyhop.Aprs.Client;
    ...
    new Listener().PacketReceived += (sender, eventArgs) =>
    {
        // Do some work...
    };

To only receive the raw messages without any parsing you can use the `DataReceived` event. 

    new Listener().DataReceived += async (sender, eventArgs) =>
    {
        // Do some work...
    };

*A command line tool which shows how to use this library is available at [https://github.com/CorstianBoerman/AprsClient-Example](https://github.com/CorstianBoerman/AprsClient-Example)*
