using SharpViewCore;
using SharpViewCore.Network;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SharpViewService
{
    class Program
    {
        private static Random random = new Random ();
        public static string RandomString( int length )
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string (Enumerable.Repeat (chars , length)
              .Select (s => s[ random.Next (s.Length) ]).ToArray ());
        }
        [STAThread]
        static void Main( string[] args )
        {
            Console.Title="SharpViewService - Remote Console";
            RemoteServer server1 = null;

            if ( args.Length!=0 )
                if ( args[ 0 ]=="-window" )
                {
                    Console.WriteLine ("SharpViewService Window [0.1;2016]");
                    Console.WriteLine ("Copyright SharpTech© 2016 ");
                    Console.WriteLine ("");
                    Console.WriteLine ("Type -help for all the available commands");
                    Console.WriteLine ("");

                    for ( ; true; )
                    {
                        Console.WriteLine ("");
                        Console.Write ("SharpService: ");
                        var entry = Console.ReadLine ();
                        Console.Title="SharpViewService - Remote Console"+entry;
                        if ( entry=="-remoteSocket" )
                        {
                            Console.WriteLine ("Setting up server socket, for all expected IPAddress range.. 255.255.255.255");
                            var addresses = NetworkLookup.GetAddresses ("");
                            Console.WriteLine ("Creating NetworkState found in (NetworkState;SharpViewCore.dll)\n");
                            Console.WriteLine ("");
                            var networkState = NetworkState.Get ();
                            Console.WriteLine ("Checking internet connection...");
                            if ( networkState.IsConnected )
                                Console.WriteLine ("Internet connect active!\n\n");
                            else
                            {
                                Console.BackgroundColor=ConsoleColor.Red;
                                Console.ForegroundColor=ConsoleColor.Black;
                                Console.WriteLine ("Internet connection is not available");
                                Console.BackgroundColor=ConsoleColor.Black;
                                Console.ForegroundColor=ConsoleColor.Gray;
                                for ( int i = 0; i<30; i++ )
                                {
                                    var networkState1 = NetworkState.Get ();
                                    if ( networkState.IsConnected==false )
                                    {
                                        Console.WriteLine ("Retrying... "+i);
                                    }
                                    else
                                    {
                                        Console.WriteLine ("Internet connection is not available");
                                        break;
                                    }
                                }
                                if ( !networkState.IsConnected )
                                {
                                    Console.WriteLine ("To create a socket based server, you will need a active internet connection...");
                                }
                                Console.ReadLine ();
                            }
                            Console.WriteLine ("Testing all active addresses found\n");
                            if ( networkState.IsConnected )
                            {
                                for ( int i = 0; i<addresses.Length; i++ )
                                {
                                    NetworkAddress address = addresses[ i ];
                                    if ( address.Address.Contains (".") )
                                    {
                                        try
                                        {
                                            System.Threading.Thread.Sleep (5);
                                            RemoteServerSocket socket = new RemoteServerSocket ();
                                            socket.Bind (address.Address , 8080);
                                            socket.Connect (address.Address , 8080);
                                            string str = "Testing Connection: "+address.Address;
                                            NetworkState.SendPingRequest (address.Address , 3000);
                                            Console.BackgroundColor=ConsoleColor.Green;
                                            Console.ForegroundColor=ConsoleColor.White;
                                            if ( address.Address.Length!=15 )
                                                str+="  - Active";
                                            else
                                                str+=" - Active.";
                                            Console.BackgroundColor=ConsoleColor.Black;
                                            Console.ForegroundColor=ConsoleColor.Gray;
                                            Console.WriteLine (str);
                                        }
                                        catch ( Exception ex )
                                        {
                                            Console.BackgroundColor=ConsoleColor.Red;
                                            Console.ForegroundColor=ConsoleColor.Black;
                                            Console.WriteLine (ex.Message);
                                            Console.BackgroundColor=ConsoleColor.Black;
                                            Console.ForegroundColor=ConsoleColor.White;
                                        }
                                    }
                                }
                                Console.WriteLine ("\n\nRemote Socket created");
                                Console.WriteLine ("");
                            }
                        }
                        else if ( entry=="-remoteDesktop" )
                        {
                            try
                            {
                                Console.WriteLine ("");
                                Console.WriteLine ("Setting up remote desktop server...");
                                Console.WriteLine ("");
                                server1=new RemoteServer ();
                                var pass = RandomString (16);
                                server1.Create (pass);
                                Console.WriteLine ("Connection ID: "+server1.DesktopInvitation.ConnectionString);
                                Console.WriteLine ("Password: "+server1.DesktopInvitation.Password);
                            }
                            catch ( Exception ex )
                            {
                                Console.WriteLine ("Remote desktop server could not be established\n\nReason: A remote server is already active.\nMessage: "+ex.Message);
                            }
                        }
                        else if ( entry=="-exit" )
                        {
                            return;
                        }
                        else if ( entry=="-clear" )
                        {
                            Console.Clear ();
                            Main (args);
                            return;
                        }
                        else if ( entry=="-isAvailable" )
                        {
                            Console.WriteLine (NetworkState.Get ().IsConnected==true ? "There is a internet connection\n" : "There isn't a active internet connection\n");
                        }
                        else if ( entry=="-ping" )
                        {
                            Console.WriteLine ("Enter a hostname /or domain to send a package to.\n");
                            Console.Write ("HostnameOrDomain: ");
                            var entry1 = Console.ReadLine ();

                            if ( entry1!=null&&entry1.Contains (".") )
                            {
                                if ( NetworkState.SendPingRequest (entry1 , 300)==NetworkPingResult.Success )
                                    Console.WriteLine ("Package Send; Reply returned success\n");
                                else
                                    Console.WriteLine ("Package could not be sent to the following:\n"+entry1+"\n");
                            }
                            else
                            {
                                Console.WriteLine ("AddressOrHostName does not apear to fall in the expecting format (example: 255.255.255.255 Or exampledomain.com)");
                            }
                        }
                        else if ( entry=="-addresses" )
                        {
                            foreach ( NetworkAddress address in NetworkLookup.GetAddresses ("") )
                            {
                                Console.WriteLine (address.Address);
                            }
                            Console.WriteLine ("\n");
                        }
                        else if ( entry=="-family" )
                        {
                            Console.WriteLine (NetworkState.Get ().NetworkAddressFamily.ToString ()+"\n");
                        }
                        else if ( entry=="-help" )
                        {
                            Console.WriteLine ("\nServer Commands:\n\n-remoteSocket  = A remote server thats binded on a ipaddress.\n-remoteDesktop = A remote server that is binded on a custom ID, and password\n\nNetwork Commands:\n\n-isAvailable = See if there is any network/internet connection available\n-ping        = Ping a domain name /or host\n-addresses   = Get all active IPAddress connected to this session,\n-family      = Get the address family of my public IPAddress\n-devices     = Get all the available network devices.\n-ip          = Returns your ipaddress.\n\nStandard Commands:\n\n-exit       = Close this window.\n-clear      = Clears the console window text.\n\nSharpView Commands:\n\n-tools = Show the administator tools.");
                            Console.WriteLine ("");
                        }
                        else if ( entry=="-tools" )
                        {
                            Process.Start ("SharpView.exe" , "-t");
                        }
                        else if ( entry=="-devices" )
                        {
                            var networkDevices = (INetworkDevice[])NetworkState.Get ().Devices;
                            foreach ( INetworkDevice device in networkDevices )
                            {
                                Console.WriteLine ("");
                                Console.WriteLine ("-----------------------------------------------");
                                Console.WriteLine ("Name:    "+device.Name + "");
                                if(device.IsConnected)
                                    Console.WriteLine ("Enabled: Yes");
                                else
                                    Console.WriteLine ("Enabled: No");
                                Console.WriteLine ("IP:    "+device.IPAddresss);
                                Console.WriteLine ("-----------------------------------------------");
                            }
                        }
                        else if ( entry=="-ip" )
                        {
                            Console.WriteLine (NetworkState.Get ().NetworkAddress+"\n");
                        }
                        else if ( string.IsNullOrWhiteSpace (entry) ) { }
                        else
                        {
                            Console.WriteLine ("\nUnknown Command");
                        }
                        Console.Title="SharpViewService - Remote Console";
                    }
                }
                else if ( args[ 0 ]=="-remote" )
                {
                    Console.Title="SharpViewService - Remote Server using (IRemoteServer4, IRemoteServerSocket, NetworkState, NetworkAddress); SharpViewCore.dll";
                    if ( args[ 1 ]=="-rsk" )
                    {
                        Console.BackgroundColor=ConsoleColor.White;
                        Console.ForegroundColor=ConsoleColor.Black;
                        Console.WriteLine ("Creating a new server using RemoteServer (IRemoteServer4, IRemoteServerSocket) Method; SharpViewCore.dll");
                        Console.BackgroundColor=ConsoleColor.Black;
                        Console.ForegroundColor=ConsoleColor.White;
                        Console.WriteLine ("");
                        Console.BackgroundColor=ConsoleColor.White;
                        Console.ForegroundColor=ConsoleColor.Black;
                        Console.WriteLine ("Setting up server socket, for all expected IPAddress range.. 255.255.255.255");
                        var addresses = NetworkLookup.GetAddresses ("");
                        Console.BackgroundColor=ConsoleColor.Black;
                        Console.ForegroundColor=ConsoleColor.White;
                        Console.WriteLine ("Creating NetworkState found in (NetworkState;SharpViewCore.dll)\n");
                        Console.WriteLine ("");
                        var networkState = NetworkState.Get ();
                        Console.WriteLine ("Checking internet connection...");
                        if ( networkState.IsConnected )
                            Console.WriteLine ("Internet connect active!\n\n");
                        else
                        {
                            Console.BackgroundColor=ConsoleColor.Red;
                            Console.ForegroundColor=ConsoleColor.Black;
                            Console.WriteLine ("Internet connection is not available");
                            Console.BackgroundColor=ConsoleColor.Black;
                            Console.ForegroundColor=ConsoleColor.Gray;
                            for ( int i = 0; i<30; i++ )
                            {
                                var networkState1 = NetworkState.Get ();
                                if ( networkState.IsConnected==false )
                                {
                                    Console.WriteLine ("Retrying... "+i);
                                }
                                else
                                {
                                    Console.WriteLine ("Internet connection is not available");
                                    break;
                                }
                            }
                            if ( !networkState.IsConnected )
                            {
                                Console.WriteLine ("To create a socket based server, you will need a active internet connection...");
                            }
                            Console.WriteLine ("To create a socket based server, you will need a active internet connection...");
                            Console.ReadLine ();
                        }
                        Console.WriteLine ("Testing all active addresses found\n");
                        if ( networkState.IsConnected )
                        {
                            for ( int i = 0; i<addresses.Length; i++ )
                            {
                                NetworkAddress address = addresses[ i ];
                                if ( address.Address.Contains (".") )
                                {
                                    try
                                    {
                                        System.Threading.Thread.Sleep (5);
                                        Console.WriteLine ("Testing Connection: "+address.Address);
                                        RemoteServerSocket socket = new RemoteServerSocket ();
                                        Console.BackgroundColor=ConsoleColor.Black;
                                        Console.ForegroundColor=ConsoleColor.White;
                                        socket.Bind (address.Address , 8080);
                                        socket.Connect (address.Address , 8080);
                                    }
                                    catch ( Exception ex )
                                    {
                                        Console.BackgroundColor=ConsoleColor.Red;
                                        Console.ForegroundColor=ConsoleColor.Black;
                                        Console.WriteLine (ex.Message);
                                        Console.BackgroundColor=ConsoleColor.Black;
                                        Console.ForegroundColor=ConsoleColor.White;
                                    }
                                }
                            }
                            try
                            {
                                Console.WriteLine ("");
                                Console.BackgroundColor=ConsoleColor.White;
                                Console.ForegroundColor=ConsoleColor.Black;
                                Console.WriteLine ("Setting up remote desktop server...");
                                Console.BackgroundColor=ConsoleColor.Black;
                                Console.ForegroundColor=ConsoleColor.White;
                                Console.WriteLine ("");
                                RemoteServer server = new RemoteServer ();
                                server.Create (args[ 2 ]);
                                Console.WriteLine ("Connection ID: "+server.DesktopInvitation.ConnectionString);
                                Console.WriteLine ("Password: "+server.DesktopInvitation.Password);
                                Console.WriteLine ("Group:"+server.DesktopInvitation.GroupName);
                                Console.ReadLine ();
                            }
                            catch
                            {
                                Console.BackgroundColor=ConsoleColor.Black;
                                Console.ForegroundColor=ConsoleColor.White;
                                Console.WriteLine ("Remote desktop server could not be established\n\nReason: A remote server is already active.");
                                Console.ReadLine ();
                            }
                        }
                    }
                    else
                    {
                        Console.Title="SharpViewService - Remote Server using (IRemoteServer4); SharpViewCore.dll";
                        try
                        {
                            Console.BackgroundColor=ConsoleColor.White;
                            Console.ForegroundColor=ConsoleColor.Black;
                            Console.WriteLine ("Creating a new server using RemoteServer (IRemoteServer4) Method; SharpViewCore.dll");
                            Console.BackgroundColor=ConsoleColor.Black;
                            Console.ForegroundColor=ConsoleColor.White;
                            Console.WriteLine ("");
                            Console.BackgroundColor=ConsoleColor.White;
                            Console.ForegroundColor=ConsoleColor.Black;
                            Console.WriteLine ("Setting up remote desktop server...");
                            Console.BackgroundColor=ConsoleColor.Black;
                            Console.ForegroundColor=ConsoleColor.White;
                            Console.WriteLine ("");
                            RemoteServer server = new RemoteServer ();
                            server.Create (args[ 0 ]);
                            Console.WriteLine ("Connection ID: "+server.DesktopInvitation.ConnectionString);
                            Console.WriteLine ("Password: "+server.DesktopInvitation.Password);
                            Console.WriteLine ("Group:"+server.DesktopInvitation.GroupName);
                            Console.ReadLine ();
                        }
                        catch
                        {
                            Console.BackgroundColor=ConsoleColor.Black;
                            Console.ForegroundColor=ConsoleColor.White;
                            Console.WriteLine ("Remote desktop server could not be established\n\nReason: A remote server is already active.");
                            Console.ReadLine ();
                        }
                    }
                }
                else { return; }
            else
            {
                MessageBox.Show ("Invalid arguments\n\n-window - Open a clear service window\n-remote - Open the remote portion of this service.\n-remote -rsk - To Open the remote socket portion of the console.");
                return;
            }
        }
    }
}
