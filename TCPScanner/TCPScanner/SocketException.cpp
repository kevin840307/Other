#include "stdafx.h"
#include "SocketException.h"

SocketException::SocketException(const std::string& sMessage)
{
	g_sMessage = sMessage;
}

SocketException::~SocketException()
{
}

const std::string SocketException::fnGetMessages()
{
	return g_sMessage;
}