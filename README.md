HE DDNS Updater Service
=======================

About
-----
This is a simple Windows service that will perform an update to [Hurricane Electric's free Dynamic DNS (DDNS) service](https://dns.he.net/). This service simply performs a HTTP GET request to HE's update service with the credentials you configured on their website. Since it runs as a Windows service, there's no need to use scheduled tasks or other flaky forms of automation to ensure your domain name points to the correct IP address.

System Requirements
-------------------
*  Microsoft Windows running .NET Framework 2.0 or later (this should be Windows XP SP2 and above)

Installation and Configuration
------------------------------
Installation is a little complicated since installing a service isn't as easy as installing a program. I *could* have rolled this into an installer program or a batch script, but I just didn't feel like it.

1. Copy the HeDdnsUpdaterService.exe and HeDdnsUpdaterService.exe.config files into a permanent location (something like C:\Program Files\HE Updater\)
2. Modify HeDdnsUpdaterService.exe.config to fit your needs
	1. Change line 27 to contain the amount of time you the service to wait between updates (in hr:min:sec format) `<value>01:00:00</value>`
	2. Change line 30 to contain the hostname you wish to update with the machine's current IP address (no HTTP or slashes) `<value>hostname.example.com</value>`
	3. Change line 33 to contain the authorization key you obtained from HE's DDNS web app (see their website for more information) `<value>lskdjfoiernvjsdkakleru</value>`
3. Install the service
	1. Open an administrator command prompt
	2. Navigate to the path you copied the binaries to in step 1
	2. Execute: `%SystemRoot%\Microsoft.NET\Framework\v2.0.50727\installutil.exe .\HeDdnsUpdaterService.exe`
	3. You should see a successful output message.
4. Start the service by executing `net start HeDdnsUpdater` or using the Services admin tool snap-in.
5. (optionally) Confirm that the service was successful by looking in the event viewer for events from HeDdnsUpdater. If you were successful, you should see an information event like: `Successful update: good 129.21.49.1` If you were not successful, the event log will help you determine the issue. A error event with `badauth` tells you that the hostname/key combination you entered in step 2 is not valid. An information event with `nochg` means your authentication information was correct, but the IP provided is the same as the IP the DDNS servers have on record for your hostname.

License
-------
This project is licensed under the MIT license. See COPYRIGHT for more information. 
