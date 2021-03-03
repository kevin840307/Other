#include "stdafx.h"
#include "GSocket.h"

GSocket::GSocket(const std::string& k_sIp, const int& k_iMMSTime)
{
	fnCheckIp(k_sIp);
	fnInitTCPIP(k_iMMSTime);
	fnInit();
}

GSocket::~GSocket()
{
	fnClose();
	WSACleanup();
	closesocket(g_sockSocket);
}

void GSocket::fnInit()
{
	fnInitDLL();
}

void GSocket::fnInitDLL()
{
	WSAData wsaData;
	WORD wrDllVer;
	wrDllVer = MAKEWORD(2, 2);
	WSAStartup(wrDllVer, &wsaData);
}

void GSocket::fnInitTCPIP(const int& k_iMMSTime)
{
	g_sockaddAddr.sin_addr.s_addr	= inet_addr(g_sIp.c_str());
	g_sockaddAddr.sin_family		= AF_INET;
	g_tvlTimeOut.tv_sec				= 0;
	g_tvlTimeOut.tv_usec			= k_iMMSTime;
	g_u_lBlockSwitch				= 1;
	g_sErrorMessage					= "";
	FD_ZERO(&g_fdsSet);
}

void GSocket::fnClose()
{
	closesocket(g_sockSocket);
}

const void GSocket::fnCheckIp(const std::string& k_sIp)
{
	int iPos							= 0;
	StringProcess *spProcess		    = new StringProcess();
	std::vector<std::string> &vecSplit	= spProcess->fnSplit(k_sIp, '.');

	while (iPos < vecSplit.size())
	{
		if (vecSplit[iPos].length() > IP_LEN || vecSplit[iPos].length() == 3 && 
			((vecSplit[iPos] > IP_MAX || vecSplit[iPos] < IP_MIN)))
		{
			fnGSException(IP_VALUE_ERROR, k_sIp.c_str(), vecSplit[iPos].c_str());
		}
		iPos++;
	}
	delete spProcess;
	if (vecSplit.size() != IP_SIZE)
	{
		fnGSException(IP_RANGE_ERROR, k_sIp.c_str());
	}
	g_sIp = k_sIp;
}

std::vector<int> GSocket::fnScanner(const int& k_iStartPort, const int& k_iEndPort)
{
	std::vector<int> vecOpenPort;
	if (k_iStartPort < PORT_MIN || k_iEndPort > PORT_MAX || k_iStartPort > k_iEndPort)
	{
		fnGSException(PORT_VALUE_ERROR, std::to_string(k_iStartPort).c_str(), std::to_string(k_iEndPort).c_str());
	}

	for (int iStartPort = k_iStartPort; iStartPort <= k_iEndPort; iStartPort++)
	{
		if (fnCheckPort(iStartPort))
		{
			vecOpenPort.push_back(iStartPort);
		}
		closesocket(g_sockSocket);
	}
	return vecOpenPort;
}

const bool GSocket::fnCheckPort(const int& k_iPort)
{
	g_sockaddAddr.sin_port	= htons(k_iPort);
	g_sockSocket			= socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);

	/* 設置SOCKET加入集合FD */
	FD_SET(g_sockSocket, &g_fdsSet);

	/* 設定阻塞 1:非阻塞 0:阻塞 */
	ioctlsocket(g_sockSocket, FIONBIO, &g_u_lBlockSwitch);
	connect(g_sockSocket, (SOCKADDR*)(&g_sockaddAddr), sizeof(g_sockaddAddr));

	/* 設置監聽讀寫 */
	bool bResult = select(g_sockSocket, &g_fdsSet, &g_fdsSet, &g_fdsSet, &g_tvlTimeOut);
	return bResult;
}

void GSocket::fnGSException(const char& k_cKey, const char* kp_cData, ...)
{
	DateTime* dttNowDate = new DateTime();
	va_list valList;
	va_start(valList, kp_cData);
	switch (k_cKey)
	{
	case IP_VALUE_ERROR:
		g_sErrorMessage = dttNowDate->GetNowDate() + "IP Value Error： ip: '" + kp_cData + "'\n";
		g_sErrorMessage = g_sErrorMessage + "IP Value Error： error value: '" + va_arg(valList, char*) + "'\n\n";
		break;
	case PORT_VALUE_ERROR:
		g_sErrorMessage = dttNowDate->GetNowDate() + "Port Value Error： k_iStartPort: '" + kp_cData + "'\n";
		g_sErrorMessage = g_sErrorMessage + "Port Value Error： k_iEndPort: '" + va_arg(valList, char*) + "'\n\n";
		break;
	case IP_RANGE_ERROR:
		g_sErrorMessage = dttNowDate->GetNowDate() + "IP Value Range Error： ip: '" + kp_cData + "'\n\n";
		break;
	default:
		break;
	}
	delete dttNowDate;
	va_end(valList);
	throw SocketException(g_sErrorMessage);
}




