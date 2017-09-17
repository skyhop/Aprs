# Boerman.Aeronautics.AprsClient

The AprsClient project contains everything to get started with APRS messages. This project is focused on working with APRS messages from Open Glider Net (known as OGN flavoured APRS).

This project has been kickstarted by the code found on [aprs.codeplex.com](aprs.codeplex.com). The following edits have been made:

- The TCP library has been replaced to facilitate high throughput with low CPU usage. (The TCP library can be found at [https://github.com/Boerman/Boerman.TcpLib](https://github.com/Boerman/Boerman.TcpLib))
- A object oriented wrapper has been crafted for this library to be a bit more modular in use
- Event handlers have been added to be able to easily plug in on incomming messages
- Support for [OGN flavoured APRS](http://wiki.glidernet.org/wiki:ogn-flavoured-aprs) has been added


## Installing the library (NuGet)

The library is available for download on [NuGet](https://www.nuget.org/packages/Boerman.Aeronautics.AprsClient):

    Install-Package Boerman.Aeronautics.AprsClient


## Configuring the library

Before starting your application please be sure you added the required configuration in the `App.Config` file:

    <configSections>
        <section name="AprsConfig" type="Boerman.Aeronautics.AprsClient.Config.AprsConfig,Boerman.Aeronautics.AprsClient" />
    </configSections>
    
    <AprsConfig uri="aprs.glidernet.org" port="14580" password="-1" filter="t/poimqstunw"></AprsConfig>

For more information about the possible configuration check out [Config/AprsConfigSection.cs](Config/AprsConfigSection.cs).


## Using the library

***Please be aware this library expects to work with OGN flavoured APRS by default! Use the `App.Config` file to turn this off.***

To start listening for incoming APRS messages you can kickstart the library:

    using Boerman.Aeronautics.AprsClient;
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
