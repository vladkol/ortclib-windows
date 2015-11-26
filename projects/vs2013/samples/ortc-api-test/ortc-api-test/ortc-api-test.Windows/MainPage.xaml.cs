﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

using ortc_winrt_api;

namespace ortc_api_test
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            ConfigureOrtcEngine conf = new ConfigureOrtcEngine();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
          
          RTCIceGatherOptions options = new RTCIceGatherOptions();
          RTCIceServer server = new RTCIceServer();

          server.UserName = "Bojan";
          server.Credential = "12345";
          server.URLs = new List<String>();
          server.URLs.Add("stun:stun.vline.com");
          options.IceServers = new List<RTCIceServer>();
          options.IceServers.Add(server);
          
          _iceGatherer = new RTCIceGatherer(options);
          _iceGatherer.OnICEGathererStateChanged += this.RTCIceGatherer_onICEGathererStateChanged;
          _iceGatherer.OnICEGathererLocalCandidate += this.RTCIceGatherer_onICEGathererLocalCandidate;
          _iceGatherer.OnICEGathererCandidateComplete += this.RTCIceGatherer_onICEGathererCandidateComplete;
          _iceGatherer.OnICEGathererLocalCandidateGone += this.RTCIceGatherer_onICEGathererLocalCandidateGone;
          _iceGatherer.OnICEGathererError += this.RTCIceGatherer_onICEGathererError;


          _iceGatherer2 = new RTCIceGatherer(options);
          _iceGatherer2.OnICEGathererStateChanged += this.RTCIceGatherer_onICEGathererStateChanged2;
          _iceGatherer2.OnICEGathererLocalCandidate += this.RTCIceGatherer_onICEGathererLocalCandidate2;
          _iceGatherer2.OnICEGathererCandidateComplete += this.RTCIceGatherer_onICEGathererCandidateComplete2;
          _iceGatherer2.OnICEGathererLocalCandidateGone += this.RTCIceGatherer_onICEGathererLocalCandidateGone2;
          _iceGatherer2.OnICEGathererError += this.RTCIceGatherer_onICEGathererError;

          _iceTransport = new RTCIceTransport(_iceGatherer);
          _iceTransport.OnICETransportStateChanged += RTCIceTransport_onICETransportStateChanged;
          _iceTransport.OnICETransportCandidatePairAvailable += RTCIceTransport_onICETransportCandidatePairAvailable;
          _iceTransport.OnICETransportCandidatePairGone += RTCIceTransport_onICETransportCandidatePairGone;
          _iceTransport.OnICETransportCandidatePairChanged += RTCIceTransport_onICETransportCandidatePairChanged;

          _iceTransport2 = new RTCIceTransport(_iceGatherer);
          _iceTransport2.OnICETransportStateChanged += RTCIceTransport_onICETransportStateChanged2;
          _iceTransport2.OnICETransportCandidatePairAvailable += RTCIceTransport_onICETransportCandidatePairAvailable2;
          _iceTransport2.OnICETransportCandidatePairGone += RTCIceTransport_onICETransportCandidatePairGone2;
          _iceTransport2.OnICETransportCandidatePairChanged += RTCIceTransport_onICETransportCandidatePairChanged2;
          
        }

        //----------------------------------------------------------------------------------
        // ICE GATHERER EVENT HANDLERS
        //----------------------------------------------------------------------------------

        private void RTCIceGatherer_onICEGathererStateChanged(RTCIceGathererStateChangeEvent evt)
        {
          if(evt.State == RTCIceGathererState.State_Complete)
          {
            _iceTransport.start(_iceGatherer, _iceGatherer2.getLocalParameters(), RTCIceRole.Role_Controlled);
          }
        }

        private void RTCIceGatherer_onICEGathererLocalCandidate(RTCIceGathererCandidateEvent evt)
        {
          _iceTransport2.addRemoteCandidate(evt.Candidate);
        }

        private void RTCIceGatherer_onICEGathererCandidateComplete(RTCIceGathererCandidateCompleteEvent evt)
        {
          int i = 0;
          i++;
        }

        private void RTCIceGatherer_onICEGathererLocalCandidateGone(RTCIceGathererCandidateEvent evt)
        {
          int i = 0;
          i++;
        }

        private void RTCIceGatherer_onICEGathererError(RTCIceGathererErrorEvent evt)
        {
          int i = 0;
          i++;
        }

      //***********************************************************************************************

        private void RTCIceGatherer_onICEGathererStateChanged2(RTCIceGathererStateChangeEvent evt)
        {
          if (evt.State == RTCIceGathererState.State_Complete)
          {
            _iceTransport2.start(_iceGatherer2, _iceGatherer.getLocalParameters(), RTCIceRole.Role_Controlling);
          }
        }
        private void RTCIceGatherer_onICEGathererLocalCandidate2(RTCIceGathererCandidateEvent evt)
        {
          _iceTransport.addRemoteCandidate(evt.Candidate);

        }

        private void RTCIceGatherer_onICEGathererCandidateComplete2(RTCIceGathererCandidateCompleteEvent evt)
        {
          int i = 0;
          i++;
        }

        private void RTCIceGatherer_onICEGathererLocalCandidateGone2(RTCIceGathererCandidateEvent evt)
        {
          int i = 0;
          i++;
        }

        private void RTCIceGatherer_onICEGathererError2(RTCIceGathererErrorEvent evt)
        {
          int i = 0;
          i++;
        }

      //----------------------------------------------------------------------------------
      // ICE TRANSPORT EVENT HANDLERS
      //----------------------------------------------------------------------------------

        private void RTCIceTransport_onICETransportStateChanged(RTCIceTransportStateChangeEvent evt)
        {
          int i = 0;
          i++;
        }

        private void RTCIceTransport_onICETransportCandidatePairAvailable(RTCIceTransportCandidatePairEvent evt)
        {
          int i = 0;
          i++;
        }
        private void RTCIceTransport_onICETransportCandidatePairGone(RTCIceTransportCandidatePairEvent evt)
        {
          int i = 0;
          i++;
        }
        private void RTCIceTransport_onICETransportCandidatePairChanged(RTCIceTransportCandidatePairEvent evt)
        {
          int i = 0;
          i++;
        }

      //****************************************************************************************************

        private void RTCIceTransport_onICETransportStateChanged2(RTCIceTransportStateChangeEvent evt)
        {
          int i = 0;
          i++;
        }

        private void RTCIceTransport_onICETransportCandidatePairAvailable2(RTCIceTransportCandidatePairEvent evt)
        {
          int i = 0;
          i++;
        }
        private void RTCIceTransport_onICETransportCandidatePairGone2(RTCIceTransportCandidatePairEvent evt)
        {
          int i = 0;
          i++;
        }
        private void RTCIceTransport_onICETransportCandidatePairChanged2(RTCIceTransportCandidatePairEvent evt)
        {
          int i = 0;
          i++;
        }


      //******************************************************************************************************
        RTCIceGatherer _iceGatherer;
        RTCIceTransport _iceTransport;

        RTCIceGatherer _iceGatherer2;
        RTCIceTransport _iceTransport2;
    }
}