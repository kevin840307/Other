#ifndef GSocket_H
#define GSocket_H 
#define PORT_MIN			1
#define PORT_MAX			65535
#define IP_LEN				3
#define IP_SIZE				4
#define IP_MIN				"0"
#define IP_MAX				"255"
#define IP_VALUE_ERROR		'1'
#define PORT_VALUE_ERROR	'2'
#define IP_RANGE_ERROR		'3'

#pragma comment(lib, "Ws2_32.lib")
#include <WinSock2.h>
#include "DateTime.h"
#include "StringProcess.h"
#include "SocketException.h"

class GSocket
{
public:
	GSocket();
	~GSocket();
	GSocket(const std::string& k_sIp, const int& k_iMMSTime = 70000);

	std::vector<int>	fnScanner(const int& k_iStartPort, const int& k_iEndPort);
	const bool			fnCheckPort(const int& k_iPort);
	const void			fnCheckIp(const std::string& k_sIp);
private:
	void		fnInit();
	void		fnInitDLL();
	void		fnInitTCPIP(const int& k_iMMSTime);
	void		fnClose();
	void		fnGSException(const char& k_cKey, const char* kp_cData, ...);

	std::string		g_sErrorMessage;
	std::string		g_sIp;
	SOCKET			g_sockSocket;
	SOCKADDR_IN		g_sockaddAddr;
	u_long			g_u_lBlockSwitch;
	timeval			g_tvlTimeOut;
	fd_set			g_fdsSet;
};

#endif 