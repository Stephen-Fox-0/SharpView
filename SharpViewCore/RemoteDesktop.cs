using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using RDPCOMAPILib;
using AxRDPCOMAPILib;
using System.ComponentModel;
using MSTSCLib;
using SharpViewCore.EventArgs;
using SharpViewCore.Service;
using SharpViewCore.Network;
using AxMSTSCLib;

namespace SharpViewCore
{
    public class RemoteDesktop: UserControl, IComponent, IDisposable, IRemoteDesktop
    {
        /// <summary>
        /// Occurs when connected.
        /// </summary>
        public event AxEventHandler<RemoteConnectedEventArgs> Connected;

        /// <summary>
        /// Occurs when disconnected.
        /// </summary>
        public event AxEventHandler<RemoteDisconnectedEventArgs> Disconnected;

        /// <summary>
        /// Occurs when disconnect text changed.
        /// </summary>
        public event AxEventHandler<RemoteDisconnectedTextChangedEventArgs> DisconnectTextChanged;

        public event AxEventHandler<RemoteValueChanged<bool>> SmartSizingChanged;
        /// <summary>
        /// Occurs when connecting failed.
        /// </summary>
        public event AxEventHandler<RemoteValueChanged<object>> ConnectingFailed;

        /// <summary>
        /// Occurs when fatel error.
        /// </summary>
        public event AxEventHandler<RemoteValueChanged<object>> FatelError;

        /// <summary>
        /// The viewer.
        /// </summary>
        public event AxEventHandler<RemoteValueChanged<int>> PortChanged;
        /// <summary>
        /// Occurs when port changed.
        /// </summary>
        public event AxEventHandler<RemoteValueChanged<string>> ConnectedTextChanged;
        /// <summary>
        /// Occurs when reconnecting.
        /// </summary>
        public event AxEventHandler<RemoteAutoReconnectContinueState , RemoteReconnecting2EventArgs> Reconnecting;
        /// <summary>
        /// Occurs when reconnecting2.
        /// </summary>
        public event AxEventHandler<RemoteReconnectEventArgs> Reconnecting2;
        /// <summary>
        /// Occurs when login failed.
        /// </summary>
        public event AxEventHandler<RemoteEventArgs> LoginFailed;
        /// <summary>
        /// Occurs when login complete.
        /// </summary>
        public event AxEventHandler<RemoteLoginCompleteEventArgs> LoginComplete;
        /// <summary>
        /// Occurs when warning.
        /// </summary>
        public event AxEventHandler<RemoteWarningEventArgs> Warning;
    
        /// <summary>
        /// Occurs when authentication warning dismissed.
        /// </summary>
        public event AxEventHandler<RemoteEventArgs> AuthenticationWarningDismissed;
        /// <summary>
        /// Occurs when authentication warning displayed.
        /// </summary>
        public event AxEventHandler<RemoteEventArgs> AuthenticationWarningDisplayed;
        /// <summary>
        /// Occurs when confirm close.
        /// </summary>
        public event AxEventHandler<bool, RemoteEventArgs> ConfirmClose;
        /// <summary>
        /// Occurs when channel received data.
        /// </summary>
        public event AxEventHandler<object , RemoteChannelRecievedDataEventArgs> ChannelReceivedData;
        /// <summary>
        /// Occurs when connection bar pull down.
        /// </summary>
        public event AxEventHandler<RemoteEventArgs> ConnectionBarPullDown;
        /// <summary>
        /// Occurs when devices button pressed.
        /// </summary>
        public event AxEventHandler<RemoteEventArgs> DevicesButtonPressed;
        /// <summary>
        /// Occurs when enter full screen mode.
        /// </summary>
        public event AxEventHandler<RemoteEventArgs> EnterFullScreenMode;
        /// <summary>
        /// Occurs when focus released.
        /// </summary>
        public event AxEventHandler<RemoteFocusReleasedEventArgs> FocusReleased;

        AxRDPViewer viewer = null;
        AxMsRdpClient9 viewer2;

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:SharpViewCore.RemoteDesktop"/> has service console.
        /// </summary>
        /// <value><c>true</c> if has service console; otherwise, <c>false</c>.</value>
        public bool HasServiceConsole
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the service console.
        /// </summary>
        /// <value>The service console.</value>
        public ServiceConsoleManager ServiceConsole
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the attende manager.
        /// </summary>
        /// <value>The attende manager.</value>
        public IRDPSRAPIAttendeeManager AttendeManager
        {
            get
            {
                return viewer.Attendees;
            }
        }
        /// <summary>
        /// Gets the advanced settings.
        /// </summary>
        /// <value>The advanced settings.</value>
        public IMsTscAdvancedSettings AdvancedSettings
        {
            get { return viewer2.AdvancedSettings; }
        }
        /// <summary>
        /// Gets the advanced settings2.
        /// </summary>
        /// <value>The advanced settings2.</value>
        public IMsRdpClientAdvancedSettings AdvancedSettings2
        {
            get { return viewer2.AdvancedSettings2; }
        }
        /// <summary>
        /// Gets the advanced settings3.
        /// </summary>
        /// <value>The advanced settings3.</value>
        public IMsRdpClientAdvancedSettings2 AdvancedSettings3
        {
            get { return viewer2.AdvancedSettings3; }
        }
        /// <summary>
        /// Gets the advanced settings4.
        /// </summary>
        /// <value>The advanced settings4.</value>
        public IMsRdpClientAdvancedSettings3 AdvancedSettings4
        {
            get { return viewer2.AdvancedSettings4; }
        }
        /// <summary>
        /// Gets the advanced settings5.
        /// </summary>
        /// <value>The advanced settings5.</value>
        public IMsRdpClientAdvancedSettings4 AdvancedSettings5
        {
            get { return viewer2.AdvancedSettings5; }
        }
        /// <summary>
        /// Gets the advanced settings6.
        /// </summary>
        /// <value>The advanced settings6.</value>
        public IMsRdpClientAdvancedSettings5 AdvancedSettings6
        {
            get { return viewer2.AdvancedSettings6; }
        }
        /// <summary>
        /// Gets the advanced settings7.
        /// </summary>
        /// <value>The advanced settings7.</value>
        public IMsRdpClientAdvancedSettings6 AdvancedSettings7
        {
            get { return viewer2.AdvancedSettings7; }
        }
        /// <summary>
        /// Gets the advanced settings8.
        /// </summary>
        /// <value>The advanced settings8.</value>
        public IMsRdpClientAdvancedSettings7 AdvancedSettings8
        {
            get { return viewer2.AdvancedSettings8; }
        }
        /// <summary>
        /// Gets the advanced settings9.
        /// </summary>
        /// <value>The advanced settings9.</value>
        public IMsRdpClientAdvancedSettings8 AdvancedSettings9
        {
            get { return viewer2.AdvancedSettings9; }
        }
        /// <summary>
        /// Gets the transport settings.
        /// </summary>
        /// <value>The transport settings.</value>
        public IMsRdpClientTransportSettings TransportSettings
        {
            get { return viewer2.TransportSettings; }
        }
        /// <summary>
        /// Gets the transport settings2.
        /// </summary>
        /// <value>The transport settings2.</value>
        public IMsRdpClientTransportSettings2 TransportSettings2
        {
            get { return viewer2.TransportSettings2; }
        }
        /// <summary>
        /// Gets the transport settings3.
        /// </summary>
        /// <value>The transport settings3.</value>
        public IMsRdpClientTransportSettings3 TransportSettings3
        {
            get { return viewer2.TransportSettings3; }
        }
        /// <summary>
        /// Gets the transport settings4.
        /// </summary>
        /// <value>The transport settings4.</value>
        public IMsRdpClientTransportSettings4 TransportSettings4
        {
            get { return viewer2.TransportSettings4; }
        }

        /// <summary>
        /// Gets or sets the port.
        /// </summary>
        /// <value>The port.</value>
        public int Port
        {
            get { return this.AdvancedSettings2.RDPPort; }
            set
            {
                this.AdvancedSettings2.RDPPort=value;
                AxEventHandler<RemoteValueChanged<int>> handler = this.PortChanged;
                if ( handler!=null )
                    handler (new RemoteValueChanged<int> (value));
            }
        }
        /// <summary>
        /// Gets a value indicating whether this <see cref="T:SharpViewCore.RemoteDesktopControl"/> is connected.
        /// </summary>
        /// <value><c>true</c> if is connected; otherwise, <c>false</c>.</value>
        public bool IsConnected
        {
            get { return viewer2.Connected==1 ? true : false; }
        }
        /// <summary>
        /// Gets the invitiations.
        /// </summary>
        /// <value>The invitiations.</value>
        public IRDPSRAPIInvitationManager Invitiations
        {
            get
            {
                return viewer.Invitations;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:SharpViewCore.RemoteDesktopControl"/> smart sizing.
        /// </summary>
        /// <value><c>true</c> if smart sizing; otherwise, <c>false</c>.</value>
        public bool SmartSizing
        {
            get
            {
                return viewer.SmartSizing;
            }

            set
            {
                AxEventHandler<RemoteValueChanged<bool>> handler = this.SmartSizingChanged;
                if ( handler!=null )
                    handler (new RemoteValueChanged<bool> (value));

            }
        }
        /// <summary>
        /// Gets the secure settings.
        /// </summary>
        /// <value>The secure settings.</value>
        public IMsTscSecuredSettings SecureSettings
        {
            get { return viewer2.SecuredSettings; }
        }
        /// <summary>
        /// Gets the secure settings2.
        /// </summary>
        /// <value>The secure settings2.</value>
        public IMsRdpClientSecuredSettings SecureSettings2
        {
            get { return viewer2.SecuredSettings2; }
        }
        /// <summary>
        /// Gets the secure settings3.
        /// </summary>
        /// <value>The secure settings3.</value>
        public IMsRdpClientSecuredSettings2 SecureSettings3
        {
            get { return viewer2.SecuredSettings3; }
        }
        /// <summary>
        /// Gets a value indicating whether this <see cref="T:SharpViewCore.RemoteDesktopControl"/> secure setting enabled.
        /// </summary>
        /// <value><c>true</c> if secure setting enabled; otherwise, <c>false</c>.</value>
        public bool SecureSettingEnabled
        {
            get { return viewer2.SecuredSettingsEnabled==1 ? true : false; }
        }
        /// <summary>
        /// Connect the specified invitation, userName and password.
        /// </summary>
        /// <param name="invitation">Invitation.</param>
        /// <param name="userName">User name.</param>
        /// <param name="password">Password.</param>
        /// <exception cref="RemoteException"></exception>
        public void Connect( string invitation , string userName , string password )
        {
            try
            {
                viewer.Connect (invitation , userName , password);

                AxEventHandler<RemoteConnectedEventArgs> handler = this.Connected;
                if ( handler!=null )
                    handler (new RemoteConnectedEventArgs (new RemoteDesktopInfo ()
                    {
                        Invitation=invitation ,
                        UserName=userName ,
                        Password=password ,
                    }));
            }
            catch(System.Exception ex)
            {
                throw new RemoteException (ex.Message);
            }
        }

        /// <summary>
        /// Connect the specified server, userName and password.
        /// </summary>
        /// <param name="server">Server.</param>
        /// <param name="userName">User name.</param>
        /// <param name="password">Password.</param>
        /// <exception cref="RemoteException"></exception>
        public void Connect( RemoteServer server , string userName , string password )
        {
            if ( server==null )
                return;

            this.Connect (server.ConnectionId , userName , password);
        }

        /// <summary>
        /// Connect the specified info.
        /// </summary>
        /// <param name="info">Info.</param>
        /// <exception cref="RemoteException"></exception>
        public void Connect( RemoteDesktopInfo info )
        {
            this.Connect (info.Invitation , info.UserName , info.Password);
        }

        /// <summary>
        /// Creates the virtual channel.
        /// </summary>
        /// <param name="channelName">Channel name.</param>
        /// <param name="priority">Priority.</param>
        /// <param name="flags">Flags.</param>
        /// <exception cref="RemoteException"></exception>
        public RemoteVirtualChannel CreateVirtualChannel( string channelName , RemoteChannelPriority priority , uint flags )
        {
            try
            {
                if ( priority==RemoteChannelPriority.High )
                    viewer.VirtualChannelManager.CreateVirtualChannel (channelName , CHANNEL_PRIORITY.CHANNEL_PRIORITY_HI , flags);
                if ( priority==RemoteChannelPriority.Medium )
                    viewer.VirtualChannelManager.CreateVirtualChannel (channelName , CHANNEL_PRIORITY.CHANNEL_PRIORITY_MED , flags);
                if ( priority==RemoteChannelPriority.Low )
                    viewer.VirtualChannelManager.CreateVirtualChannel (channelName , CHANNEL_PRIORITY.CHANNEL_PRIORITY_LO , flags);

                return new SharpViewCore.RemoteVirtualChannel (channelName , priority , flags == 1? RemoteVirtualChannelFlags.DefaultChannel : RemoteVirtualChannelFlags.RemoteChannel);
            }
            catch
            {
                throw new RemoteException ();
            }
        }

        /// <summary>
        /// Disconnect this instance.
        /// </summary>
        /// <exception cref="RemoteException"></exception>
        public void Disconnect()
        {
            try
            {
                viewer.Disconnect ();

                AxEventHandler<RemoteDisconnectedEventArgs> handler = this.Disconnected;
                if ( handler!=null )
                    handler (new RemoteDisconnectedEventArgs (this));
            }
            catch
            {
                throw new RemoteException ();
            }
        }

        /// <summary>
        /// Gets the ocx.
        /// </summary>
        /// <returns>The ocx.</returns>
        public object GetOcx()
        {
            return viewer2.GetOcx ();
        }

        /// <summary>
        /// Requests the color depth change.
        /// </summary>
        /// <param name="dpp">Dpp.</param>
        public void RequestColorDepthChange( uint dpp )
        {
            viewer.RequestColorDepthChange ((int)dpp);
        }
        /// <summary>
        /// Requests the control.
        /// </summary>
        /// <param name="controlLevel">Control level.</param>
        public void RequestControl( RemoteControlLevel controlLevel )
        {
            if ( controlLevel==RemoteControlLevel.Interative )
                viewer.RequestControl (CTRL_LEVEL.CTRL_LEVEL_INTERACTIVE);
            if ( controlLevel==RemoteControlLevel.Invaild )
                viewer.RequestControl (CTRL_LEVEL.CTRL_LEVEL_INVALID);
            if ( controlLevel==RemoteControlLevel.Max )
                viewer.RequestControl (CTRL_LEVEL.CTRL_LEVEL_MAX);
            if ( controlLevel==RemoteControlLevel.Min )
                viewer.RequestControl (CTRL_LEVEL.CTRL_LEVEL_MIN);
            if ( controlLevel==RemoteControlLevel.None )
                viewer.RequestControl (CTRL_LEVEL.CTRL_LEVEL_NONE);
            if ( controlLevel==RemoteControlLevel.View )
                viewer.RequestControl (CTRL_LEVEL.CTRL_LEVEL_VIEW);
        }
        /// <summary>
        /// Shows the property pages.
        /// </summary>
        public void ShowPropertyPages()
        {
            viewer.ShowPropertyPages ();
        }
        /// <summary>
        /// Shows the property pages.
        /// </summary>
        /// <param name="control">Control.</param>
        public void ShowPropertyPages( Control control )
        {
            viewer.ShowPropertyPages (control);
        }

        /// <summary>
        /// Starts the reverse connect listener.
        /// </summary>
        /// <param name="invitation">Invitation.</param>
        /// <param name="userName">User name.</param>
        /// <param name="password">Password.</param>
        /// <exception cref="RemoteException"></exception>
        public void StartReverseConnectListener( string invitation , string userName , string password )
        {
            try
            {
                viewer.StartReverseConnectListener (invitation , userName , password);
            }
            catch
            {
                throw new RemoteException ();
            }
        }

        /// <summary>
        /// Gets the state of the net.
        /// </summary>
        /// <returns>The net state.</returns>
        public NetworkState GetNetState()
        {
            return NetworkState.Get ();
        }

        /// <summary>
        /// Gets the network is available.
        /// </summary>
        /// <returns>The network is available.</returns>
        public bool GetNetworkIsAvailable()
        {
            return GetNetState ().IsConnected;
        }

        /// <summary>
        /// Creates the server.
        /// </summary>
        /// <returns>The server.</returns>
        /// <param name="password">Password.</param>
        public RemoteServer CreateServer( string password )
        {
            RemoteServer server = new RemoteServer ();
            server.Create (password);
            return server;
        }

        /// <summary>
        /// Creates the server.
        /// </summary>
        /// <returns>The server.</returns>
        /// <param name="info">Info.</param>
        public RemoteServer CreateServer( RemoteServerInfo info )
        {
            return this.CreateServer (info.Password);
        }

        /// <summary>
        /// Sets the desktop info.
        /// </summary>
        /// <param name="info">Info.</param>
        public void SetDesktopInfo( RemoteDesktopInfo2 info )
        {
            viewer2.DesktopHeight=info.Height;
            viewer2.DesktopWidth=info.Width;
            viewer2.ColorDepth=info.ColorDepth;
            viewer2.FullScreen=info.Fullscreen;
            viewer2.FullScreenTitle=info.FullscreenText;
        }

        /// <summary>
        /// Gets the session info.
        /// </summary>
        /// <returns>The session info.</returns>
        public RemoteSessionInfo GetSessionInfo()
        {
            var client = this.viewer2;
            RemoteSessionInfo info = new RemoteSessionInfo ();
            info.Server=client.Server;
            info.Domain=client.Domain;
            info.UserName=client.UserName;
            info.Port=client.AdvancedSettings2.RDPPort;
            return info;
        }

        /// <summary>
        /// Gets the info2.
        /// </summary>
        /// <returns>The info2.</returns>
        public RemoteDesktopInfo2 GetInfo2()
        {
            return new SharpViewCore.RemoteDesktopInfo2 ()
            {
                Width=viewer2.DesktopWidth ,
                Height=viewer2.DesktopHeight ,
                Fullscreen=viewer2.FullScreen ,
                FullscreenText="" ,
                ColorDepth=viewer2.ColorDepth ,
            };
        }

        /// <summary>
        /// Gets the info3.
        /// </summary>
        /// <returns>The info3.</returns>
        public RemoteDesktopInfo3 GetInfo3()
        {
            return new RemoteDesktopInfo3 ()
            {
                Domain=viewer2.Domain ,
                UserName=viewer2.UserName ,
                Server=viewer2.Server ,
            };
        }

        /// <summary>
        /// Sends the on virtual channel.
        /// </summary>
        /// <param name="channelName">Channel name.</param>
        /// <param name="data">Data.</param>
        public void SendOnVirtualChannel( string channelName , string data )
        {
            viewer2.SendOnVirtualChannel (channelName , data);
        }

        /// <summary>
        /// Updates the session display settings.
        /// </summary>
        /// <param name="ulDesktopWidth">Ul desktop width.</param>
        /// <param name="ulDesktopHeight">Ul desktop height.</param>
        /// <param name="ulPhysicalWidth">Ul physical width.</param>
        /// <param name="ulPhysicalHeight">Ul physical height.</param>
        /// <param name="ulOrientation">Ul orientation.</param>
        /// <param name="ulDesktopScaleFactor">Ul desktop scale factor.</param>
        /// <param name="ulDeviceScaleFactor">Ul device scale factor.</param>
        public virtual void UpdateSessionDisplaySettings( uint ulDesktopWidth , uint ulDesktopHeight , uint ulPhysicalWidth , uint ulPhysicalHeight , uint ulOrientation , uint ulDesktopScaleFactor , uint ulDeviceScaleFactor )
        {
            viewer2.UpdateSessionDisplaySettings (ulDesktopWidth , ulDesktopHeight , ulPhysicalWidth , ulPhysicalHeight , ulOrientation , ulDesktopScaleFactor , ulDeviceScaleFactor);
        }

        /// <summary>
        /// Sets the virtual channel options.
        /// </summary>
        /// <param name="channelName">Channel name.</param>
        /// <param name="options">Options.</param>
        public void SetVirtualChannelOptions( string channelName , int options )
        {
            viewer2.SetVirtualChannelOptions (channelName , options);
        }
        /// <summary>
        /// Gets the application.
        /// </summary>
        /// <returns>The application.</returns>
        /// <param name="index">Index.</param>
        public SharpViewCore.RemoteApplication GetApplication( int index )
        {
            var app = viewer.ApplicationFilter.Applications[ index ];
            return new SharpViewCore.RemoteApplication (app.Name , app.Id , app.Flags) { Shared=app.Shared };
        }

        /// <summary>
        /// Syncs the session display settings.
        /// </summary>
        public void SyncSessionDisplaySettings()
        {
            this.viewer2.SyncSessionDisplaySettings ();
        }

        /// <summary>
        /// Sends the remote action.
        /// </summary>
        /// <param name="actionType">Action type.</param>
        public virtual void SendRemoteAction( RemoteSessionActionType actionType )
        {
            MSTSCLib.RemoteSessionActionType action = MSTSCLib.RemoteSessionActionType.RemoteSessionActionStartScreen;
            if ( actionType==RemoteSessionActionType.Appbar )
                action=MSTSCLib.RemoteSessionActionType.RemoteSessionActionAppbar;
            if ( actionType==RemoteSessionActionType.AppSwitch )
                action=MSTSCLib.RemoteSessionActionType.RemoteSessionActionAppSwitch;
            if ( actionType==RemoteSessionActionType.Charms )
                action=MSTSCLib.RemoteSessionActionType.RemoteSessionActionCharms;
            if ( actionType==RemoteSessionActionType.Snap )
                action=MSTSCLib.RemoteSessionActionType.RemoteSessionActionSnap;
            if ( actionType==RemoteSessionActionType.StartScreen )
                action=MSTSCLib.RemoteSessionActionType.RemoteSessionActionStartScreen;
            viewer2.SendRemoteAction (action);
        }

        /// <summary>
        /// Gets the status text.
        /// </summary>
        /// <returns>The status text.</returns>
        /// <param name="statusCode">Status code.</param>
        public string GetStatusText( uint statusCode )
        {
            return viewer2.GetStatusText (statusCode);
        }

        /// <summary>
        /// Gets the error description.
        /// </summary>
        /// <returns>The error description.</returns>
        /// <param name="disconnectReason">Disconnect reason.</param>
        /// <param name="extendedDisconnectReason">Extended disconnect reason.</param>
        public string GetErrorDescription( uint disconnectReason , uint extendedDisconnectReason )
        {
            return viewer2.GetErrorDescription (disconnectReason , extendedDisconnectReason);
        }

        /// <summary>
        /// Attachs the event.
        /// </summary>
        /// <param name="eventName">Event name.</param>
        /// <param name="callback">Callback.</param>
        public void AttachEvent( string eventName , object callback )
        {
            this.viewer2.attachEvent (eventName , callback);
        }

        /// <summary>
        /// Reconnect the specified ulWidth and ulHeight.
        /// </summary>
        /// <param name="ulWidth">Ul width.</param>
        /// <param name="ulHeight">Ul height.</param>
        public RemoteReconnectState Reconnect( uint ulWidth , uint ulHeight )
        {
            var t = viewer2.Reconnect (ulWidth , ulHeight);
            return t==ControlReconnectStatus.controlReconnectBlocked ? RemoteReconnectState.Blocked : RemoteReconnectState.Started;
        }

        /// <summary>
        /// Detachs the event.
        /// </summary>
        /// <param name="eventName">Event name.</param>
        /// <param name="callback">Callback.</param>
        public void DetachEvent( string eventName , object callback )
        {
            this.viewer2.detachEvent (eventName , callback);
        }

        /// <summary>
        /// Gets all connected addresses.
        /// </summary>
        /// <returns>The all connected addresses.</returns>
        public NetworkAddress[] GetAllConnectedAddresses()
        {
            return LookupAddress (GetNetState ().NetworkAddress);
        }

        /// <summary>
        /// Lookups the address.
        /// </summary>
        /// <returns>The address.</returns>
        /// <param name="hostnameOrIpAddress">Hostname or ip address.</param>
        public NetworkAddress[] LookupAddress( string hostnameOrIpAddress )
        {
            return NetworkLookup.GetAddresses (GetNetState ().NetworkAddress);
        }
        /// <summary>
        /// Sets the disconnected text.
        /// </summary>
        /// <param name="disconnectedText">Disconnected text.</param>
        public void SetDisconnectedText( string disconnectedText )
        {
            this.viewer2.DisconnectedText=disconnectedText;
            this.viewer.DisconnectedText=disconnectedText;

            AxEventHandler<RemoteDisconnectedTextChangedEventArgs> handler = this.DisconnectTextChanged;
            if ( handler!=null )
                handler (new RemoteDisconnectedTextChangedEventArgs (disconnectedText));
        }

        /// <summary>
        /// Sets the connected text.
        /// </summary>
        /// <param name="connectedText">Connected text.</param>
        public void SetConnectedText( string connectedText )
        {
            this.viewer2.ConnectingText=connectedText;

            AxEventHandler<RemoteValueChanged<string>> handler = this.ConnectedTextChanged;
            if ( handler!=null )
                handler (new RemoteValueChanged<string> (connectedText));
        }

        IRemoteDesktop instance;

        /// <summary>
        /// Get an instance of <see cref="SharpViewCore.RemoteDesktop"/>
        /// </summary>
        /// <returns>A <see cref="IRemoteDesktop"/> value.</returns>
        public IRemoteDesktop Instance()
        {
            if ( instance==null )
                instance=new RemoteDesktop ();

            return instance;
        }

        /// <summary>
        /// Sets the desktop info2.
        /// </summary>
        /// <param name="info4">Info4.</param>
        public void SetDesktopInfo2( RemoteDesktopInfo4 info4 )
        {
            this.viewer2.AdvancedSettings9.EnableMouse=info4.MouseEnabled == true? 1: 0;
            this.viewer2.AdvancedSettings9.EnableWindowsKey=info4.WindowsKeyEnabled==true ? 1: 0;
            this.viewer2.AdvancedSettings9.Compress=info4.Compress;
            this.viewer2.AdvancedSettings9.EnableSuperPan=info4.SuperPanEnabled;
            this.viewer2.AdvancedSettings9.EncryptionEnabled=info4.EncryptionEnabled==true ? 1 : 0;
            this.viewer2.AdvancedSettings9.EnableAutoReconnect=info4.AutoReconnectEnabled;
            this.viewer2.AdvancedSettings9.DisplayConnectionBar=info4.ConnectionBarEnabled;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SharpViewCore.RemoteDesktopControl"/> class.
        /// </summary>
        public RemoteDesktop()
        {
            this.ServiceConsole=new Service.ServiceConsoleManager ();
            this.HasServiceConsole=false;

            this.viewer2=new AxMSTSCLib.AxMsRdpClient9 ();
            this.viewer2.Dock=DockStyle.Fill;
            this.viewer=new AxRDPViewer ();
            this.viewer.Dock=DockStyle.Fill;
            this.viewer2.OnAutoReconnecting2+=( sender , e ) =>
            {
                AxEventHandler<RemoteAutoReconnectContinueState , RemoteReconnecting2EventArgs> handler = this.Reconnecting;
                if ( handler!=null )
                    handler (new RemoteReconnecting2EventArgs (e.disconnectReason , e.attemptCount , e.networkAvailable==true ? 1 : 0 , e.maxAttemptCount));
            };
            this.viewer2.OnAutoReconnecting+=Viewer2_OnAutoReconnecting;
            this.viewer2.OnLoginComplete+=( sender , e ) =>
            {
                AxEventHandler<RemoteLoginCompleteEventArgs> handler = this.LoginComplete;
                if ( handler!=null ) handler (new RemoteLoginCompleteEventArgs (this.GetInfo3 ()));
            };
            this.viewer2.OnLogonError+=( sender , e ) =>
            {
                AxEventHandler<RemoteEventArgs> handler = this.LoginFailed;
                if ( handler!=null ) handler (new RemoteEventArgs ());
            };
            this.viewer2.OnConnectionBarPullDown += (sender, e)=>
            {
                AxEventHandler<RemoteEventArgs> handler = this.ConnectionBarPullDown;
                if ( handler!=null )
                    handler (new RemoteEventArgs ());
            };
            this.viewer2.OnDevicesButtonPressed += (sender, e)=>
            {
                AxEventHandler<RemoteEventArgs> handler = this.DevicesButtonPressed;
                if ( handler!=null ) handler (new RemoteEventArgs ());
            };
            this.viewer2.OnEnterFullScreenMode += (sender, e)=>
            {
                AxEventHandler<RemoteEventArgs> handler = this.EnterFullScreenMode;
                if ( handler!=null )
                    handler (new RemoteEventArgs ());
            };
            this.viewer2.OnFocusReleased += (sender, e)=>
            {
                AxEventHandler<RemoteFocusReleasedEventArgs> handler = this.FocusReleased;
                if ( handler!=null )
                    handler (new EventArgs.RemoteFocusReleasedEventArgs (e.iDirection));
            };
            this.viewer2.OnChannelReceivedData += (sender, e)=>
            {
                AxEventHandler<object , RemoteChannelRecievedDataEventArgs> handler = this.ChannelReceivedData;
                if ( handler!=null )
                    handler (new EventArgs.RemoteChannelRecievedDataEventArgs (e.chanName , e.data));
            };
            this.viewer2.OnAuthenticationWarningDismissed += (sender, e) =>
            {
                AxEventHandler<RemoteEventArgs> handler = this.AuthenticationWarningDismissed;
                if ( handler!=null )
                    handler (new RemoteEventArgs ());
            };
            this.viewer2.OnAuthenticationWarningDisplayed += (sender, e)=>
            {
                AxEventHandler<RemoteEventArgs> handler = this.AuthenticationWarningDisplayed;
                if ( handler!=null ) handler (new EventArgs.RemoteEventArgs ());
            };
            this.viewer2.OnConfirmClose += (sender, e) =>
            {
                AxEventHandler<bool , RemoteEventArgs> handler = this.ConfirmClose;
                if ( handler!=null )
                    handler (new RemoteEventArgs ());
                return true;
            };
            this.viewer2.OnWarning+=( sender , e ) =>
            {
                AxEventHandler<RemoteWarningEventArgs> handler = this.Warning;
                if ( handler!=null ) handler (new RemoteWarningEventArgs (e.warningCode));
            };
            this.viewer.OnConnectionFailed+=( s , e ) =>
            {
                AxEventHandler<RemoteValueChanged<object>> handler = this.ConnectingFailed;
                if ( handler!=null )
                    handler (new RemoteValueChanged<object> (s));
            };
            this.viewer.OnError+=( s , e ) =>
            {
                AxEventHandler<RemoteValueChanged<object>> handler = this.FatelError;

                if ( handler!=null )
                    handler (new RemoteValueChanged<object> (e.errorInfo));
            };
            this.Controls.Add (this.viewer);
            this.Controls.Add (this.viewer2);
        }


        private AutoReconnectContinueState Viewer2_OnAutoReconnecting( object sender , AxMSTSCLib.IMsTscAxEvents_OnAutoReconnectingEvent e )
        {
            AxEventHandler<RemoteReconnectEventArgs> handler = this.Reconnecting2;
            if ( handler!=null )
                handler (new RemoteReconnectEventArgs (e.disconnectReason , e.attemptCount));
            return AutoReconnectContinueState.autoReconnectContinueAutomatic;
        }
    }

    #region otherClasses

    public class RemoteDesktopInfo
    {
        /// <summary>
        /// Gets or sets the invitation.
        /// </summary>
        /// <value>The invitation.</value>
        public string Invitation { get; set; }
        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>The name of the user.</value>
        public string UserName { get; set; }
        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>The password.</value>
        public string Password { get; set; }
    }
    public class RemoteServerInfo
    {
        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>The password.</value>
        public string Password { get; set; }
    }
    public class RemoteDesktopInfo2
    {
        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        /// <value>The width.</value>
        public int Width { get; set; }
        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        /// <value>The height.</value>
        public int Height { get; set; }
        /// <summary>
        /// Gets or sets the color depth.
        /// </summary>
        /// <value>The color depth.</value>
        public int ColorDepth { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:SharpViewCore.RemoteDesktopOCXInfo"/> is fullscreen.
        /// </summary>
        /// <value><c>true</c> if fullscreen; otherwise, <c>false</c>.</value>
        public bool Fullscreen { get; set; }
        /// <summary>
        /// Gets or sets the fullscreen text.
        /// </summary>
        /// <value>The fullscreen text.</value>
        public string FullscreenText { get; set; }
    }
    public class RemoteSessionInfo
    {
        /// <summary>
        /// Gets or sets the server.
        /// </summary>
        /// <value>The server.</value>
        public string Server { get; set; }
        /// <summary>
        /// Gets or sets the domain.
        /// </summary>
        /// <value>The domain.</value>
        public string Domain { get; set; }
        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>The name of the user.</value>
        public string UserName { get; set; }
        /// <summary>
        /// Gets or sets the port.
        /// <value>The port address.</value>
        /// </summary>
        public int Port { get; set; }
    }
    public class RemoteDesktopInfo3
    {
        /// <summary>
        /// Gets or sets the server.
        /// </summary>
        /// <value>The server.</value>
        public string Server { get; set; }
        /// <summary>
        /// Gets or sets the domain.
        /// </summary>
        /// <value>The domain.</value>
        public string Domain { get; set; }
        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>The name of the user.</value>
        public string UserName { get; set; }
    }
    public enum RemoteSessionActionType
    {
        /// <summary>
        /// appbar.
        /// </summary>
        Appbar,
        /// <summary>
        /// app switch.
        /// </summary>
        AppSwitch,
        /// <summary>
        /// charms.
        /// </summary>
        Charms,
        /// <summary>
        /// snap.
        /// </summary>
        Snap,
        /// <summary>
        /// start screen.
        /// </summary>
        StartScreen
    }
    public enum RemoteReconnectState
    {
        /// <summary>
        ///  blocked.
        /// </summary>
        Blocked,
        /// <summary>
        ///  started.
        /// </summary>
        Started
    }
    public enum RemoteAutoReconnectContinueState
    {
        /// <summary>
        /// Remote auto reconnect continue state.
        /// </summary>
        Automatic,
        /// <summary>
        /// Remote auto reconnect continue state.
        /// </summary>
        Manual,
        /// <summary>
        /// Remote auto reconnect continue state.
        /// </summary>
        Stop
    }
    public class RemoteConnectionInfo
    {
        string localIP, peerIP;
        int localPort, peerPort, protocal;

        /// <summary>
        /// Gets the local ip.
        /// </summary>
        /// <value>The local ip.</value>
        public string LocalIP
        {
            get
            {
                return localIP;
            }
        }

        /// <summary>
        /// Gets the local port.
        /// </summary>
        /// <value>The local port.</value>
        public int LocalPort
        {
            get
            {
                return localPort;
            }
        }

        /// <summary>
        /// Gets the peer ip.
        /// </summary>
        /// <value>The peer ip.</value>
        public string PeerIP
        {
            get
            {
                return peerIP;
            }
        }

        /// <summary>
        /// Gets the peer port.
        /// </summary>
        /// <value>The peer port.</value>
        public int PeerPort
        {
            get
            {
                return peerPort;
            }
        }

        /// <summary>
        /// Gets the protocol.
        /// </summary>
        /// <value>The protocol.</value>
        public int Protocol
        {
            get
            {
                return protocal;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SharpViewCore.RemoteConnectionInfo"/> class.
        /// </summary>
        /// <param name="localIp">Local ip.</param>
        /// <param name="peerIP">Peer ip.</param>
        /// <param name="localPort">Local port.</param>
        /// <param name="peerPort">Peer port.</param>
        /// <param name="protocal">Protocal.</param>
        public RemoteConnectionInfo( string localIp , string peerIP , int localPort , int peerPort , int protocal )
        {
            this.localIP=localIp;
            this.peerIP=peerIP;
            this.peerPort=peerPort;
            this.localPort=localPort;
            this.protocal=protocal;
        }
    }
    public class RemoteDesktopInfo4
    {
        /// <summary>
        /// Gets a value indicating whether this <see cref="T:SharpViewCore.RemoteDesktopInfo4"/> mouse enabled.
        /// </summary>
        /// <value><c>true</c> if mouse enabled; otherwise, <c>false</c>.</value>
        public bool MouseEnabled { get; private set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:SharpViewCore.RemoteDesktopInfo4"/> windows key enabled.
        /// </summary>
        /// <value><c>true</c> if windows key enabled; otherwise, <c>false</c>.</value>
        public bool WindowsKeyEnabled { get; set; }
        /// <summary>
        /// Gets the compress.
        /// </summary>
        /// <value>The compress.</value>
        public int Compress { get; private set; }
        /// <summary>
        /// Gets a value indicating whether this <see cref="T:SharpViewCore.RemoteDesktopInfo4"/> super pan enabled.
        /// </summary>
        /// <value><c>true</c> if super pan enabled; otherwise, <c>false</c>.</value>
        public bool SuperPanEnabled { get; private set; }
        /// <summary>
        /// Gets a value indicating whether this <see cref="T:SharpViewCore.RemoteDesktopInfo4"/> encryption enabled.
        /// </summary>
        /// <value><c>true</c> if encryption enabled; otherwise, <c>false</c>.</value>
        public bool EncryptionEnabled { get; private set; }
        /// <summary>
        /// Gets a value indicating whether this <see cref="T:SharpViewCore.RemoteDesktopInfo4"/> auto reconnect enabled.
        /// </summary>
        /// <value><c>true</c> if auto reconnect enabled; otherwise, <c>false</c>.</value>
        public bool AutoReconnectEnabled { get; private set; }
        /// <summary>
        /// Gets a value indicating whether this <see cref="T:SharpViewCore.RemoteDesktopInfo4"/> connection bar enabled.
        /// </summary>
        /// <value><c>true</c> if connection bar enabled; otherwise, <c>false</c>.</value>
        public bool ConnectionBarEnabled { get; private set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="T:SharpViewCore.RemoteDesktopInfo4"/> class.
        /// </summary>
        /// <param name="mouseEnabled">If set to <c>true</c> mouse enabled.</param>
        /// <param name="windowsKeyEnabled">If set to <c>true</c> windows key enabled.</param>
        /// <param name="compress">Compress.</param>
        /// <param name="superPanEnabled">If set to <c>true</c> super pan enabled.</param>
        /// <param name="encyptionEnabled">If set to <c>true</c> encyption enabled.</param>
        /// <param name="autoReconnectEnabled">If set to <c>true</c> auto reconnect enabled.</param>
        /// <param name="connectionBarEnabled">If set to <c>true</c> connection bar enabled.</param>
        public RemoteDesktopInfo4(bool mouseEnabled, bool windowsKeyEnabled, int compress, bool superPanEnabled, bool encyptionEnabled, bool autoReconnectEnabled, bool connectionBarEnabled)
        {
            this.MouseEnabled=mouseEnabled;
            this.WindowsKeyEnabled=windowsKeyEnabled;
            this.Compress=compress;
            this.SuperPanEnabled=superPanEnabled;
            this.EncryptionEnabled=encyptionEnabled;
            this.AutoReconnectEnabled=autoReconnectEnabled;
            this.ConnectionBarEnabled=connectionBarEnabled;
        }
    }
    #endregion

    #region Events
    namespace EventArgs
    {
        public class RemoteConnectedEventArgs
        {
            /// <summary>
            /// Gets the remote desktop info.
            /// </summary>
            /// <value>The remote desktop info.</value>
            public RemoteDesktopInfo RemoteDesktopInfo
            {
                get;
                private set;
            }
            /// <summary>
            /// Initializes a new instance of the <see cref="T:SharpViewCore.EventArgs.RemoteConnectedEventArgs"/> class.
            /// </summary>
            /// <param name="info">Info.</param>
            public RemoteConnectedEventArgs( RemoteDesktopInfo info )
            {
                this.RemoteDesktopInfo=info;
            }
        }
        public class RemoteDisconnectedEventArgs
        {
            /// <summary>
            /// Gets the dekstop.
            /// </summary>
            /// <value>The dekstop.</value>
            public IRemoteDesktop Dekstop
            {
                get;
                private set;
            }
            /// <summary>
            /// Initializes a new instance of the <see cref="T:SharpViewCore.EventArgs.RemoteDisconnectedEventArgs"/> class.
            /// </summary>
            /// <param name="remoteDekstop">Remote dekstop.</param>
            public RemoteDisconnectedEventArgs( IRemoteDesktop remoteDekstop )
            {
                this.Dekstop=remoteDekstop;
            }
        }
        public class RemoteDisconnectedTextChangedEventArgs
        {
            /// <summary>
            /// Gets the text.
            /// </summary>
            /// <value>The text.</value>
            public string Text { get; private set; }

            /// <summary>
            /// Initializes a new instance of the
            /// <see cref="T:SharpViewCore.EventArgs.RemoteDisconnectedTextChangedEventArgs"/> class.
            /// </summary>
            /// <param name="text">Text.</param>
            public RemoteDisconnectedTextChangedEventArgs( string text )
            {
                this.Text=text;
            }
        }
        public class RemoteValueChanged<TValue>
        {
            /// <summary>
            /// Gets the value.
            /// </summary>
            /// <value>The value.</value>
            public TValue Value
            {
                get;
                private set;
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="T:SharpViewCore.EventArgs.RemoteValueChanged`1"/> class.
            /// </summary>
            /// <param name="value">Value.</param>
            public RemoteValueChanged( TValue value )
            {
                this.Value=value;
            }
        }
        public class RemoteValueChanged<TValue, TValue2>
        {
            /// <summary>
            /// Gets the value.
            /// </summary>
            /// <value>The value.</value>
            public TValue Value
            {
                get; private set;
            }
            /// <summary>
            /// Gets the value2.
            /// </summary>
            /// <value>The value2.</value>
            public TValue2 Value2
            {
                get; private set;
            }
            /// <summary>
            /// Initializes a new instance of the <see cref="T:SharpViewCore.EventArgs.RemoteValueChanged`2"/> class.
            /// </summary>
            /// <param name="value">Value.</param>
            /// <param name="value2">Value2.</param>
            public RemoteValueChanged( TValue value , TValue2 value2 )
            {
                this.Value=value;
                this.Value2=value2;
            }
        }
        public class RemoteReconnectEventArgs
        {
            /// <summary>
            /// Gets the reason for disconnecting.
            /// </summary>
            public int DisconnectReason { get; private set; }

            /// <summary>
            /// Gets the amount of times that, <see cref="RemoteDesktop"/> Has tried to reconnect.
            /// </summary>
            public int AttemptCount { get; private set; }

            /// <summary>
            /// Initialize a new instance of <see cref="RemoteReconnectEventArgs"/>
            /// </summary>
            /// <param name="disconnectReason">The disconnectReason</param>
            /// <param name="attempCount">The attemped count.</param>
            public RemoteReconnectEventArgs( int disconnectReason , int attemptCount )
            {
                this.DisconnectReason=disconnectReason;
                this.AttemptCount=attemptCount;
            }
        }
        public class RemoteReconnecting2EventArgs: RemoteReconnectEventArgs
        {
            /// <summary>
            /// Gets a <see cref="int"/> value indercating weather the network is available.
            /// </summary>
            public int NetworkAvailiable { get; private set; }
            /// <summary>
            /// Gets the max attempt Count.
            /// </summary>
            public int MaxAttemptCount { get; private set; }

            /// <summary>
            /// Initialize a new instance of <see cref="RemoteReconnecting2EventArgs"/> 
            /// </summary>
            /// <param name="disconnectReason">The disconnect reason.</param>
            /// <param name="attemptCount">The attempt count.</param>
            /// <param name="networkAvailable">The network available.</param>
            /// <param name="maxAttempCount">The max attempt count</param>
            public RemoteReconnecting2EventArgs( int disconnectReason , int attemptCount , int networkAvailable , int maxAttempCount )
                : base (disconnectReason , attemptCount)
            {
                this.NetworkAvailiable=networkAvailable;
                this.MaxAttemptCount=maxAttempCount;
            }
        }
        public class RemoteLoginCompleteEventArgs
        {
            /// <summary>
            /// Gets the remote desktop information.
            /// </summary>
            public RemoteDesktopInfo3 Info { get; private set; }

            /// <summary>
            /// Initialize a new instance of <see cref="RemoteLoginCompleteEventArgs"/> Class.
            /// </summary>
            /// <param name="info">The info></param>
            public RemoteLoginCompleteEventArgs( RemoteDesktopInfo3 info )
            {
                this.Info=info;
            }
        }
        public class RemoteEventArgs
        {
            /// <summary>
            /// Initialize a new instance of <see cref="RemoteEventArgs"/>
            /// </summary>
            public RemoteEventArgs()
            {
            }
        }
        public class RemoteWarningEventArgs
        {
            /// <summary>
            /// Gets the <see cref="int"/> value for the warning code.
            /// </summary>
            public int WarningCode { get; private set; }


            /// <summary>
            /// Initialize a new instance of <see cref="RemoteWarningEventArgs"/>
            /// </summary>
            /// <param name="warningCode"></param>
            public RemoteWarningEventArgs( int warningCode )
            {
                this.WarningCode=warningCode;
            }
        }
        public class RemoteChannelRecievedDataEventArgs
        {
            /// <summary>
            /// Gets the name of the channel.
            /// </summary>
            /// <value>The name of the channel.</value>
            public string ChannelName { get; private set; }
            /// <summary>
            /// Gets the data.
            /// </summary>
            /// <value>The data.</value>
            public string Data { get; private set; }
            /// <summary>
            /// Initializes a new instance of the
            /// <see cref="T:SharpViewCore.EventArgs.RemoteChannelRecievedDataEventArgs"/> class.
            /// </summary>
            /// <param name="channelName">Channel name.</param>
            /// <param name="data">Data.</param>
            public RemoteChannelRecievedDataEventArgs(string channelName, string data)
            {
                this.ChannelName=channelName;
                this.Data=data;
            }
        }
        public class RemoteFocusReleasedEventArgs
        {
            /// <summary>
            /// Gets the direction.
            /// </summary>
            /// <value>The direction.</value>
            public int Direction { get; private set; }
            /// <summary>
            /// Initializes a new instance of the <see cref="T:SharpViewCore.EventArgs.RemoteFocusReleasedEventArgs"/> class.
            /// </summary>
            /// <param name="direction">Direction.</param>
            public RemoteFocusReleasedEventArgs(int direction)
            {
                this.Direction=direction;
            }
        }
        /// <summary>
        /// Event Handler
        /// </summary>
        /// <typeparam name="T">The {T} object</typeparam>
        /// <param name="eventArgs">The event args.</param>
        public delegate void AxEventHandler<T>( T eventArgs );
        /// <summary>
        /// Event Handler
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="T1"></typeparam>
        /// <param name="eventArgs"></param>
        /// <returns></returns>
        public delegate T AxEventHandler<T, T1>( T1 eventArgs );
    }
    #endregion

}
