#include "pch.h"

#include <org/ortc/Ortc.h>
#include <org/ortc/helpers.h>

#include <ortc/IORTC.h>
#include <zsLib/SafeInt.h>

#include "webrtc/common_video/video_common_winrt.h"

using namespace ortc;

namespace Org
{
  namespace Ortc
  {
    CoreDispatcher ^OrtcWithDispatcher::_dispatcher = nullptr;

    int64 Ortc::NtpServerTime::get()
    {
      return SafeInt<int64>(IORTC::ntpServerTime().count());
    }

    void Ortc::NtpServerTime::set(int64 value)
    {
      IORTC::ntpServerTime(Milliseconds(SafeInt<Milliseconds::rep>(value)));
    }

    void Ortc::Setup()
    {
      IORTC::setup(IMessageQueuePtr());
    }

    void Ortc::StartMediaTracing()
    {
      IORTC::startMediaTracing();
    }

    void Ortc::StopMediaTracing()
    {
      IORTC::stopMediaTracing();
    }

    bool Ortc::IsMediaTracing()
    {
      return IORTC::isMediaTracing();
    }

    bool Ortc::SaveMediaTrace(Platform::String ^filename)
    {
      std::string filenameValue = Internal::Helper::FromCx(filename);
      return IORTC::saveMediaTrace(filenameValue);
    }

    bool Ortc::SaveMediaTrace(Platform::String ^host, int port)
    {
      std::string hostValue = Internal::Helper::FromCx(host);
      return IORTC::saveMediaTrace(hostValue, port);
    }

    void OrtcWithDispatcher::Setup(Windows::UI::Core::CoreDispatcher ^dispatcher)
    {
#define TODO_THIS_FEELS_WRONG_USING_GLOBAL 1
#define TODO_THIS_FEELS_WRONG_USING_GLOBAL 2
      //g_windowDispatcher = _dispatcher = dispatcher;
	  _dispatcher = dispatcher;
	  webrtc::VideoCommonWinRT::SetCoreDispatcher(dispatcher);
      zsLib::setup(dispatcher);
      IORTC::setup(IMessageQueuePtr());
    }

  } // namespace ortc
} // namespace org
