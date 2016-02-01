#include "pch.h"
#include "RTCIceTransportController.h"
#include "helpers.h"

using namespace ortc_winrt_api;

using Platform::Collections::Vector;

extern Windows::UI::Core::CoreDispatcher^ g_windowDispatcher;

RTCIceTransportController::RTCIceTransportController()
{
  mNativePointer = IICETransportController::create();
}

IVector<RTCIceTransport^>^ RTCIceTransportController::GetTransports()
{
  auto ret = ref new Vector<RTCIceTransport^>();
  if (mNativePointer)
  {
    auto candidates = mNativePointer->getTransports();
    for (IICETransportControllerTypes::ICETransportList::iterator it = candidates.begin(); it != candidates.end(); ++it) {
      RTCIceTransport^ transport = CreateEmptyCxObject::IceTransport();
      ret->Append(ConvertObjectToCx::ToIceTransport(*it));
    }
  }
  return ret;
}

void RTCIceTransportController::AddTransport(RTCIceTransport^ transport, size_t index)
{
  if (mNativePointer)
  {
    mNativePointer->addTransport(FetchNativePointer::FromIceTransport(transport), index);
  }
}